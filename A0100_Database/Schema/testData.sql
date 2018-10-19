
-- Test 数据库.

CREATE TABLE test_abc (
	id	VARCHAR(32)  PRIMARY KEY,
	a	INT,
	b	INT,
	c	INT
);


INSERT INTO test_abc(
	id, a, b, c
) VALUES (
	'TEST', 1,2,3
);

INSERT INTO test_abc(
	id, a, b, c
) VALUES (
	'TEST2', 4,5,6
);