
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



-- 系统数据.
INSERT INTO [my_system](
	[system_code],[system_name]
) VALUES (
	'TEST', '测试系统'
);


-- 角色.
INSERT INTO [my_role](
	[role_code],[system_code],[role_name],
    [create_time],[create_user],[last_update_time],[last_update_user],[status]
)
SELECT	'TEST_001', 'TEST', '测试角色001',	GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'		UNION  ALL
SELECT	'TEST_002', 'TEST', '测试角色002',	GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'		UNION  ALL
SELECT	'TEST_003', 'TEST', '测试角色003',	GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'		UNION  ALL
SELECT	'TEST_004', 'TEST', '测试角色004',	GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'		UNION  ALL
SELECT	'TEST_005', 'TEST', '测试角色005',	GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'		UNION  ALL
SELECT	'TEST_006', 'TEST', '测试角色006',	GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'		UNION  ALL
SELECT	'TEST_007', 'TEST', '测试角色007',	GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'		UNION  ALL
SELECT	'TEST_008', 'TEST', '测试角色008',	GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'		UNION  ALL
SELECT	'TEST_009', 'TEST', '测试角色009',	GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'		UNION  ALL
SELECT	'TEST_010', 'TEST', '测试角色010',	GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'		UNION  ALL
SELECT	'TEST_011', 'TEST', '测试角色011',	GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'		UNION  ALL
SELECT	'TEST_012', 'TEST', '测试角色012',	GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'		UNION  ALL
SELECT	'TEST_013', 'TEST', '测试角色013',	GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'		UNION  ALL
SELECT	'TEST_014', 'TEST', '测试角色014',	GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'		UNION  ALL
SELECT	'TEST_015', 'TEST', '测试角色015',	GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'		UNION  ALL
SELECT	'TEST_016', 'TEST', '测试角色016',	GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'		UNION  ALL
SELECT	'TEST_017', 'TEST', '测试角色017',	GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'		UNION  ALL
SELECT	'TEST_018', 'TEST', '测试角色018',	GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'		UNION  ALL
SELECT	'TEST_019', 'TEST', '测试角色019',	GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'		UNION  ALL
SELECT	'TEST_020', 'TEST', '测试角色020',	GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'
;



-- 测试用户.
INSERT INTO [my_user](
	[login_user_code],[organization_id],[user_name],[user_password],
	[create_time],[create_user],[last_update_time],[last_update_user],[status]
)
SELECT 'TEST_001', 1, '测试用户001', '123456',			GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE' UNION ALL
SELECT 'TEST_002', 1, '测试用户002', '123456',			GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE' UNION ALL
SELECT 'TEST_003', 1, '测试用户003', '123456',			GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE' UNION ALL
SELECT 'TEST_004', 1, '测试用户004', '123456',			GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE' UNION ALL
SELECT 'TEST_005', 1, '测试用户005', '123456',			GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE' UNION ALL
SELECT 'TEST_006', 1, '测试用户006', '123456',			GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE' UNION ALL
SELECT 'TEST_007', 1, '测试用户007', '123456',			GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE' UNION ALL
SELECT 'TEST_008', 1, '测试用户008', '123456',			GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE' UNION ALL
SELECT 'TEST_009', 1, '测试用户009', '123456',			GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE' UNION ALL
SELECT 'TEST_010', 1, '测试用户010', '123456',			GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE' UNION ALL
SELECT 'TEST_011', 1, '测试用户011', '123456',			GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE' UNION ALL
SELECT 'TEST_012', 1, '测试用户012', '123456',			GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE' UNION ALL
SELECT 'TEST_013', 1, '测试用户013', '123456',			GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE' UNION ALL
SELECT 'TEST_014', 1, '测试用户014', '123456',			GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE' UNION ALL
SELECT 'TEST_015', 1, '测试用户015', '123456',			GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE' UNION ALL
SELECT 'TEST_016', 1, '测试用户016', '123456',			GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE' UNION ALL
SELECT 'TEST_017', 1, '测试用户017', '123456',			GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE' UNION ALL
SELECT 'TEST_018', 1, '测试用户018', '123456',			GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE' UNION ALL
SELECT 'TEST_019', 1, '测试用户019', '123456',			GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE' UNION ALL
SELECT 'TEST_020', 1, '测试用户020', '123456',			GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'
;




-- 测试模块
INSERT INTO [my_module](
	[system_code],[module_type_code],
	[module_code],[module_name],[module_expand]
)
SELECT 'TEST','API',	'TEST_API_01', '测试模块01','api/TEST/01'				UNION ALL
SELECT 'TEST','API',	'TEST_API_02', '测试模块02','api/TEST/02'				UNION ALL
SELECT 'TEST','API',	'TEST_API_03', '测试模块03','api/TEST/03'				UNION ALL
SELECT 'TEST','API',	'TEST_API_04', '测试模块04','api/TEST/04'
;

-- 测试动作.
INSERT INTO [my_action](
	[module_code],[action_code],[action_expand],[action_name],[default_useable]
)
SELECT 'TEST_API_01','TEST_API_01_01','',		'测试模块01-动作01',0		UNION ALL
SELECT 'TEST_API_01','TEST_API_01_02','',		'测试模块01-动作02',0		UNION ALL
SELECT 'TEST_API_01','TEST_API_01_03','',		'测试模块01-动作03',0		UNION ALL
SELECT 'TEST_API_01','TEST_API_01_04','',		'测试模块01-动作04',0		UNION ALL
SELECT 'TEST_API_02','TEST_API_02_01','',		'测试模块02-动作01',0		UNION ALL
SELECT 'TEST_API_02','TEST_API_02_02','',		'测试模块02-动作02',0		UNION ALL
SELECT 'TEST_API_02','TEST_API_02_03','',		'测试模块02-动作03',0		UNION ALL
SELECT 'TEST_API_02','TEST_API_02_04','',		'测试模块02-动作04',0		UNION ALL
SELECT 'TEST_API_03','TEST_API_03_01','',		'测试模块03-动作01',0		UNION ALL
SELECT 'TEST_API_03','TEST_API_03_02','',		'测试模块03-动作02',0		UNION ALL
SELECT 'TEST_API_03','TEST_API_03_03','',		'测试模块03-动作03',0		UNION ALL
SELECT 'TEST_API_03','TEST_API_03_04','',		'测试模块03-动作04',0		UNION ALL
SELECT 'TEST_API_04','TEST_API_04_01','',		'测试模块04-动作01',0		UNION ALL
SELECT 'TEST_API_04','TEST_API_04_02','',		'测试模块04-动作02',0		UNION ALL
SELECT 'TEST_API_04','TEST_API_04_03','',		'测试模块04-动作03',0		UNION ALL
SELECT 'TEST_API_04','TEST_API_04_04','',		'测试模块04-动作04',0
;