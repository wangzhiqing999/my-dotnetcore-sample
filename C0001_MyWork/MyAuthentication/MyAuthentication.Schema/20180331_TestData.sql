
-- 组织机构.
INSERT INTO [my_organization](
	[login_organization_code],[organization_name],
	[create_time],[create_user],[last_update_time],[last_update_user],[status]
) 
SELECT	'TEST_001', '测试组001',	GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'		UNION  ALL
SELECT	'TEST_002', '测试组002',	GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'		UNION  ALL
SELECT	'TEST_003', '测试组003',	GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'		UNION  ALL
SELECT	'TEST_004', '测试组004',	GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'		UNION  ALL
SELECT	'TEST_005', '测试组005',	GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'		UNION  ALL
SELECT	'TEST_006', '测试组006',	GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'		UNION  ALL
SELECT	'TEST_007', '测试组007',	GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'		UNION  ALL
SELECT	'TEST_008', '测试组008',	GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'		UNION  ALL
SELECT	'TEST_009', '测试组009',	GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'		UNION  ALL
SELECT	'TEST_010', '测试组010',	GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'		UNION  ALL
SELECT	'TEST_011', '测试组011',	GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'		UNION  ALL
SELECT	'TEST_012', '测试组012',	GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'		UNION  ALL
SELECT	'TEST_013', '测试组013',	GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'		UNION  ALL
SELECT	'TEST_014', '测试组014',	GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'		UNION  ALL
SELECT	'TEST_015', '测试组015',	GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'		UNION  ALL
SELECT	'TEST_016', '测试组016',	GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'		UNION  ALL
SELECT	'TEST_017', '测试组017',	GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'		UNION  ALL
SELECT	'TEST_018', '测试组018',	GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'		UNION  ALL
SELECT	'TEST_019', '测试组019',	GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'		UNION  ALL
SELECT	'TEST_020', '测试组020',	GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'
;
