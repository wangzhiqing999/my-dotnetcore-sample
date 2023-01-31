
INSERT INTO holding (
	item_code, item_name, source_name, reader_name
) VALUES
	('110017', '易方达增强回报A', 'Z', 'eastmoney'),
	('485011', '工银瑞信双利B', 'Z', 'eastmoney');



INSERT INTO holding (
	item_code, item_name, source_name, reader_name
) VALUES
	('020019', '国泰双利债', 'G', 'eastmoney'),
	('485111', '工银双利债券A', 'G', 'eastmoney');


INSERT INTO holding (
	item_code, item_name, source_name, reader_name
) VALUES
	('110007', '易方达稳健收益债券A', 'Z', 'eastmoney'),
	('320021', '诺安双利债券', 'Z', 'eastmoney'),
	('001256', '泓德优选成长混合', 'Z', 'eastmoney'),
	('040005', '华安宏利混合A', 'Z', 'eastmoney');




-- 没有抓取历史数据，简单插入一行数据.
INSERT INTO holding_log (
	item_code, log_date, quantity, price
)
SELECT
	item_code, '2023-01-01', 100, 0
FROM
	holding
;



