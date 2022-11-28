

-- 获取指定 ETF 日线的 最新收盘价.
CREATE OR REPLACE FUNCTION my_etf.etf_day_line_get_last_close_price(
	p_etf_code varchar(16)
)
RETURNS numeric AS
$$
DECLARE
	v_close_price numeric;
BEGIN

	SELECT 
		close_price INTO v_close_price
	FROM 
		etf_day_line
	WHERE
		etf_code = p_etf_code
	ORDER BY 
	    trading_date desc
	LIMIT 1;

	RETURN v_close_price;

END;
$$ LANGUAGE plpgsql;




-- 视图： 最新的日线.
CREATE OR REPLACE VIEW v_etf_day_line_last
AS
SELECT
	t.*
FROM
	etf_day_line t
WHERE
	NOT EXISTS (SELECT 1 FROM etf_day_line t2 WHERE t.etf_code = t2.etf_code  AND t.trading_date < t2.trading_date)
;




SELECT 
	t.etf_code,	
	MAX(t.close_price) AS max_close_price,
	v.close_price,
	MIN(t.close_price) AS min_close_price,


	MAX(t.close_price) - v.close_price AS goto_max_close_price,
	100 * (MAX(t.close_price) - v.close_price) / v.close_price AS  goto_max_close_price_per,
	
	v.close_price - MIN(t.close_price) AS goto_min_close_price,
	100 * (v.close_price - MIN(t.close_price)) / v.close_price AS  goto_min_close_price_per,
	
	
	
	
	SUM(
		CASE WHEN t.close_price > v.close_price THEN 1
		ELSE 0
		END
	) AS more_then_last_close_price,
	SUM( 
		CASE WHEN t.close_price < v.close_price THEN 1
		ELSE 0
		END
	) AS less_then_last_close_price
FROM
	etf_day_line t
		JOIN v_etf_day_line_last v ON(t.etf_code = v.etf_code)
GROUP BY
	t.etf_code,
	v.close_price;
