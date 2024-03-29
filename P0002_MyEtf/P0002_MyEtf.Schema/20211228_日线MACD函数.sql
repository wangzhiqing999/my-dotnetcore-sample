-- 获取 日线的 MACD.
CREATE OR REPLACE FUNCTION my_etf.get_day_macd(
	IN p_etf_code	VARCHAR,
	IN p_fast		INT,
	IN p_slow		INT,
	IN p_dif		INT,
	OUT trading_date date,
	OUT diff 		NUMERIC,
	OUT dea			NUMERIC,
	OUT macd		NUMERIC
)
RETURNS SETOF RECORD AS
$$
DECLARE
	v_prev_dea	NUMERIC;
	v_etf_data 	RECORD;
BEGIN

	FOR v_etf_data IN
		SELECT
			ROW_NUMBER() OVER(ORDER BY ema12.trading_date) AS NO,
			ema12.trading_date,
			ema12.ema	AS ema12,
			ema26.ema	AS ema26
		FROM
			my_etf.get_day_ema(p_etf_code, p_fast) as ema12,
			my_etf.get_day_ema(p_etf_code, p_slow) as ema26
		WHERE
			ema12.trading_date = ema26.trading_date
		ORDER BY
			ema12.trading_date
	LOOP

		trading_date := v_etf_data.trading_date;

		-- DIFF = EMA(12) - EMA(26)
		diff := v_etf_data.ema12 - v_etf_data.ema26;

		IF v_etf_data.NO = 1 THEN
			dea := diff;
		ELSE
			-- DEA = 2/(9+1) * 今日DIFF + 8/(9+1) * 昨日DEA
			dea := diff * 2 / (p_dif + 1);
			dea := dea +  v_prev_dea * (p_dif - 1) / (p_dif + 1);
		END IF;

		macd := 2 * (diff - dea);

		v_prev_dea := dea;


		diff := ROUND(diff, 3);
		dea := ROUND(dea, 3);
		macd := ROUND(macd, 3);

		RETURN NEXT;

	END LOOP;


END;
$$ LANGUAGE plpgsql;




-- 获取 日线的 MACD (使用 默认的参数).
CREATE OR REPLACE FUNCTION my_etf.get_day_macd(
	IN p_etf_code	VARCHAR,
	OUT trading_date date,
	OUT diff 		NUMERIC,
	OUT dea			NUMERIC,
	OUT macd		NUMERIC
)
RETURNS SETOF RECORD AS
$$
DECLARE
	v_etf_data 	RECORD;
BEGIN

	FOR v_etf_data IN
		SELECT
			*
		FROM
			my_etf.get_day_macd(p_etf_code, 12, 26, 9)
	LOOP
		trading_date := v_etf_data.trading_date;
		diff := v_etf_data.diff;
		dea := v_etf_data.dea;
		macd := v_etf_data.macd;

		RETURN NEXT;
	END LOOP;

END;
$$ LANGUAGE plpgsql;



-- 测试 - 指定全部参数.
SELECT * FROM my_etf.get_day_macd('SH513030', 12, 26, 9);

-- 测试 - 使用参数的 默认值.
SELECT * FROM my_etf.get_day_macd('SH513030');

-- 与 C# 计算的数据，进行对比.
select * from my_etf.etf_day_macd edm WHERE  edm.etf_code = 'SH513030' order by 2




