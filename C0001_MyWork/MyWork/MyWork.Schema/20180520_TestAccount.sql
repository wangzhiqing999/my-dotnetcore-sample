-- 测试账户.
INSERT INTO [mw_account](
	[organization_id], [account_balance],
	[create_time],[create_user],[last_update_time],[last_update_user],[status]
)
SELECT	1, 500000,	GETDATE(), 'system', GETDATE(), 'system',  'ACTIVE'
;


