




-- 插入日线数据.
CREATE OR REPLACE FUNCTION my_etf.insert_day_line(
	p_code		varchar(16), 
	p_date		timestamp, 
	p_open		numeric, 
	p_close		numeric, 
	p_highest	numeric, 
	p_lowest	numeric, 
	p_volume	numeric
)
RETURNS json AS
$$
DECLARE
	v_last_trading_date	timestamp;
	v_trading_week		timestamp;
BEGIN

	-- 先查询.
	SELECT
		MAX(trading_date) INTO v_last_trading_date
	FROM
		my_etf.etf_day_line
	WHERE
		etf_code = p_code;
	
	
	IF v_last_trading_date >= p_date THEN
		-- 数据已存在.
		RETURN json_build_object ('result_code', 'DATA_HAD_EXISTS', 'result_message', '数据已存在！');
	END IF;


	-- 插入日线数据.
	INSERT INTO my_etf.etf_day_line (
		etf_code, trading_date, 
		open_price, close_price, 
		highest_price, lowest_price, 
		volume
	) VALUES(
		p_code, p_date, 
		p_open, p_close, 
		p_highest, p_lowest, 
		p_volume
	);


	
	-- 取得交易日所在的周.
	v_trading_week := date_trunc('week', p_date);
	
	
	-- 查询周线数据.
	SELECT
		MAX(trading_date) INTO v_last_trading_date
	FROM
		my_etf.etf_week_line
	WHERE
		etf_code = p_code;
	
	
	IF v_trading_week =  date_trunc('week', v_last_trading_date) THEN
		
		-- 本周的数据已存在.
		-- 更新操作.
		UPDATE 
			my_etf.etf_week_line
		SET 
			-- 周线的交易日 = 今天.
			trading_date = p_date,
		
			-- 周收盘 = 今天收盘.
			close_price = p_close,
			
			-- 最高 = 之前的最高 与 今天的最高中， 更大的那一个数据.
			highest_price = GREATEST(highest_price, p_highest), 
			
			-- 最低 = 之前的最低 与 今天的最低中， 更小的那一个数据.
			lowest_price = LEAST(lowest_price, p_lowest), 
			
			-- 成交额 = 原有的成交额 + 今天的.
			volume = volume + p_volume
		WHERE 
			etf_code = p_code
			AND trading_date = v_last_trading_date;

	ELSE
	
		-- 本周的数据不存在.
		-- 插入的操作.
		INSERT INTO my_etf.etf_week_line (
			etf_code, trading_date, 
			open_price, close_price, 
			highest_price, lowest_price, 
			volume
		) VALUES(
			p_code, p_date, 
			p_open, p_close, 
			p_highest, p_lowest, 
			p_volume
		);
	
	END IF;

	-- 返回“处理成功”
	RETURN json_build_object ('result_code', '0', 'result_message', 'success');


	-- 异常情况下，返回错误代码与错误消息.
	EXCEPTION
	  WHEN OTHERS  THEN
		RETURN json_build_object ('result_code', SQLSTATE, 'result_message', SQLERRM);
		
END;
$$ LANGUAGE plpgsql;




/* 
测试调用

SELECT my_etf.insert_day_line('SH510050','2022-01-24',	3.192,3.195,	3.203,3.165,	22.20);
SELECT my_etf.insert_day_line('SH510050','2022-01-25',	3.179,3.129,	3.181,3.125,	23.45);
SELECT my_etf.insert_day_line('SH510050','2022-01-26',	3.136,3.144,	3.152,3.113,	26.48);
SELECT my_etf.insert_day_line('SH510050','2022-01-27',	3.145,3.107,	3.145,3.098,	30.30);
SELECT my_etf.insert_day_line('SH510050','2022-01-28',	3.117,3.036,	3.125,3.029,	36.88);


*/


/*

测试 postgrest 调用.

NOTIFY pgrst, 'reload schema';


curl -H "Accept: application/json" -H "Content-type: application/json" -X POST -d '{"p_code":"SZ159920", "p_date":"2022-01-24", "p_open":1.269, "p_close":1.271, "p_highest":1.277, "p_lowest":1.263, "p_volume":8.14}'  http://192.168.1.152:3000/rpc/insert_day_line






*/
