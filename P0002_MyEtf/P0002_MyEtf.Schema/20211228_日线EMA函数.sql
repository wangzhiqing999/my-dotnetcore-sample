-- 获取 日线的 EMA.
CREATE OR REPLACE FUNCTION my_etf.get_day_ema(
	IN p_etf_code	VARCHAR,
	IN p_ema_num	INT,
	OUT trading_date TIMESTAMP WITHOUT TIME ZONE,
	OUT ema NUMERIC
)
RETURNS SETOF RECORD AS
$$
DECLARE
	v_prev_ema	NUMERIC;
	v_etf_data 	RECORD;
BEGIN

	FOR v_etf_data IN 
		SELECT
		  ROW_NUMBER() OVER(ORDER BY edl.trading_date) AS NO,
		  edl.trading_date,
		  edl.close_price
		FROM
		  my_etf.etf_day_line edl
		WHERE
		  edl.etf_code = p_etf_code
		ORDER BY
		  edl.trading_date
	LOOP

		trading_date := v_etf_data.trading_date;
		
		IF v_etf_data.NO = 1 THEN
			ema := v_etf_data.close_price;
		ELSE
			ema := v_etf_data.close_price * 2 / (p_ema_num + 1); 
			ema := ema + v_prev_ema *  (p_ema_num - 1) / (p_ema_num + 1) ;
		END IF;
		
		v_prev_ema := ema;
		
		RETURN NEXT;

	END LOOP;

END;
$$ LANGUAGE plpgsql;






-- 测试
SELECT * FROM my_etf.get_day_ema('SH513030', 12);
SELECT * FROM my_etf.get_day_ema('SH513030', 26);



-- 关联测试.
SELECT
	ema12.trading_date,
	ema12.ema	AS ema12,
	ema26.ema	AS ema26
FROM
  my_etf.get_day_ema('SH513030', 12) as ema12,
  my_etf.get_day_ema('SH513030', 26) as ema26
WHERE
  ema12.trading_date = ema26.trading_date;



-- 与 C# 计算的数据，进行对比.
select * from my_etf.etf_day_ema ede WHERE  ede.etf_code = 'SH513030' order by 2 







