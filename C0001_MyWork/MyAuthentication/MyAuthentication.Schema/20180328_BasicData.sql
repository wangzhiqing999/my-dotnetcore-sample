-- 模块类型.
INSERT INTO [my_module_type](
	[module_type_code],[module_type_name]
)
SELECT 'MENU','菜单'		UNION ALL
SELECT 'API','API'
;

-- 系统数据.
INSERT INTO [my_system](
	[system_code],[system_name]
) VALUES (
	'MYAUTH', '权限管理'
);

-- 角色.
INSERT INTO [my_role](
	[system_code],[role_code],[role_name],
    [create_time],[create_user],[last_update_time],[last_update_user],[status]
)
SELECT 'MYAUTH', 'MANAGER', '管理员',			GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'		UNION ALL
SELECT 'MYAUTH', 'DEMO', '演示用户',			GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'
;




-- 模块
INSERT INTO [my_module](
	[system_code],[module_type_code],
	[module_code],[module_name],[module_expand]
)
SELECT 'MYAUTH','API',	'MYAUTH_API_0001', '模块类型管理','api/MyAuth/MyModuleType'		UNION ALL
SELECT 'MYAUTH','API',	'MYAUTH_API_0002', '模块管理','api/MyAuth/MyModule'				UNION ALL
SELECT 'MYAUTH','API',	'MYAUTH_API_0003', '动作管理','api/MyAuth/MyAction'				UNION ALL
SELECT 'MYAUTH','API',	'MYAUTH_API_0004', '角色管理','api/MyAuth/MyRole'
;

INSERT INTO [my_module](
	[system_code],[module_type_code],
	[module_code],[module_name],[module_expand]
)
SELECT 'MYAUTH','MENU',	'MYAUTH_MENU_0001', '模块类型','/MyAuth/MyModuleType/'		UNION ALL
SELECT 'MYAUTH','MENU',	'MYAUTH_MENU_0002', '模块','/MyAuth//MyModule/'				UNION ALL
SELECT 'MYAUTH','MENU',	'MYAUTH_MENU_0003', '动作','/MyAuth/MyAction/'				UNION ALL
SELECT 'MYAUTH','MENU',	'MYAUTH_MENU_0004', '角色','/MyAuth/MyRole/'
;






-- 组织机构.
INSERT INTO [my_organization](
	[login_organization_code],[organization_name],
	[create_time],[create_user],[last_update_time],[last_update_user],[status]
) VALUES (
	'MANAGER', '管理组',
	GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'
);

-- 用户.
INSERT INTO [my_user](
	[login_user_code],[organization_id],[user_name],[user_password],
	[create_time],[create_user],[last_update_time],[last_update_user],[status]
)
SELECT 'admin', 1, '管理员', '123456',			GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE' UNION ALL
SELECT 'demo',  1, '演示用户', '123456',		GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'
;



DECLARE
	@userID		BIGINT;
BEGIN
	SELECT 
		@userID = user_id 
	FROM 
		my_user
	WHERE
		login_user_code = 'admin';
	
	-- 用户 and 系统. 
	INSERT INTO [my_system_user](
		[system_code],[user_id]
	) VALUES (
		'MYAUTH', @userID
	);

	-- 用户 and 角色.
	INSERT INTO [my_user_role](
		[user_id],	[role_code]
	) VALUES (
		@userID,  'MANAGER'
	);	
END;
GO








