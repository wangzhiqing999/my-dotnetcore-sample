INSERT INTO [sso_user_category](
	[user_category_code],[user_category_name]
) VALUES (
	'TEST',  '测试'
);



INSERT INTO [sso_user](
	[user_id],
	[user_category_code],[user_name],[user_password]
)
SELECT NEWID(), 'TEST', 'TEST1', '123456'	UNION ALL
SELECT NEWID(), 'TEST', 'TEST2', '123456'	UNION ALL
SELECT NEWID(), 'TEST', 'TEST3', '123456'
;
