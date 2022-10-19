-- 获取 日线的 MA.
CREATE OR REPLACE FUNCTION my_etf.get_day_ma(
	IN p_etf_code	VARCHAR,
	IN p_ma_num		INT,
	OUT trading_date date,
	OUT ma NUMERIC
)
RETURNS SETOF RECORD AS
$$
DECLARE
	v_etf_data 	RECORD;
BEGIN

	FOR v_etf_data IN
		SELECT
		  edl.trading_date,
		  AVG(MAX(edl.close_price)) OVER(ORDER BY edl.trading_date ROWS p_ma_num PRECEDING) AS ma
		FROM
		  my_etf.etf_day_line edl
		WHERE
		  edl.etf_code = p_etf_code
		GROUP BY
		  edl.trading_date
	LOOP

		trading_date := v_etf_data.trading_date;		
		ma := ROUND(v_etf_data.ma, 3);

		RETURN NEXT;

	END LOOP;

END;
$$ LANGUAGE plpgsql;



-- 测试
SELECT * FROM my_etf.get_day_ma('SH516920', 5);
SELECT * FROM my_etf.get_day_ma('SH516920', 10);
SELECT * FROM my_etf.get_day_ma('SH516920', 20);




-- 关联测试.
SELECT
	dma5.trading_date,
	dma5.ma		AS ma5,
	dma10.ma	AS ma10,
	dma20.ma	AS ma20,
	dma30.ma	AS ma30,
	dma60.ma	AS ma60
FROM
  my_etf.get_day_ma('SH516920', 5) as dma5,
  my_etf.get_day_ma('SH516920', 10) as dma10,
  my_etf.get_day_ma('SH516920', 20) as dma20,
  my_etf.get_day_ma('SH516920', 30) as dma30,
  my_etf.get_day_ma('SH516920', 60) as dma60
WHERE
  dma5.trading_date = dma10.trading_date
  AND dma5.trading_date = dma20.trading_date
  AND dma5.trading_date = dma30.trading_date
  AND dma5.trading_date = dma60.trading_date;


执行好了，去 https://xueqiu.com/S/SH516920 核对数据.





-- EF Core 原生 SQL 查询，需要一个 VIEW
-- 参考：https://docs.microsoft.com/zh-cn/ef/core/modeling/keyless-entity-types?tabs=data-annotations
-- 仅仅是需要这样 视图的列名 与 存储过程返回的列名 一致。
CREATE VIEW my_etf.v_ma AS
SELECT
  edl.etf_code,
  edl.trading_date,
  edl.close_price AS ma
FROM
  my_etf.etf_day_line edl
;