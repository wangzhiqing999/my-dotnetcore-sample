

-- 创建一个视图， 日线 中， 加一个 本周第一天 的计算结果.
CREATE VIEW my_etf.v_etf_day_line AS
SELECT 
	etf_code, 
	date_trunc('week', trading_date) AS trading_week, 
	trading_date,
	open_price, 
	close_price, 
	highest_price, 
	lowest_price, 
	volume
FROM 
	my_etf.etf_day_line
;




-- 预期用于周线的查询.
SELECT
	d.etf_code,
	d.trading_week, 
	-- 2022-01-29 追加：周线中的日期，不是本周第一天，而是使用 本周的最后一个交易日.
	MAX(d.trading_date),
    (SELECT o.open_price FROM my_etf.v_etf_day_line o WHERE d.etf_code = o.etf_code AND d.trading_week = o.trading_week ORDER BY o.trading_date LIMIT 1) AS open_price,
	(SELECT c.close_price FROM my_etf.v_etf_day_line c WHERE d.etf_code = c.etf_code AND d.trading_week = c.trading_week ORDER BY c.trading_date DESC LIMIT 1) AS close_price,
	MAX(highest_price) AS highest_price,
	MIN(lowest_price) AS lowest_price,
	SUM(volume) AS volume
FROM
	my_etf.v_etf_day_line d
GROUP BY
    d.etf_code, 
	d.trading_week;




-- 插入数据到 周线的表中.
INSERT INTO my_etf.etf_week_line
(etf_code, trading_date, open_price, close_price, highest_price, lowest_price, volume)
SELECT
	d.etf_code,
	-- d.trading_week, 
	MAX(d.trading_date),
    (SELECT o.open_price FROM my_etf.v_etf_day_line o WHERE d.etf_code = o.etf_code AND d.trading_week = o.trading_week ORDER BY o.trading_date LIMIT 1) AS open_price,
	(SELECT c.close_price FROM my_etf.v_etf_day_line c WHERE d.etf_code = c.etf_code AND d.trading_week = c.trading_week ORDER BY c.trading_date DESC LIMIT 1) AS close_price,
	MAX(highest_price) AS highest_price,
	MIN(lowest_price) AS lowest_price,
	SUM(volume) AS volume
FROM
	my_etf.v_etf_day_line d
GROUP BY
    d.etf_code, 
	d.trading_week;


