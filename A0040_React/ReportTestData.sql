USE [TestReport]
GO

INSERT INTO [dbo].[cr_report](
	[report_file_name] ,[report_name]
)
SELECT 'test1.rpt',  '测试报表一'	UNION ALL
SELECT 'test2.rpt',  '测试报表二'	UNION ALL
SELECT 'test3.rpt',  '测试报表三'
GO


INSERT INTO [dbo].[auot_report_master](
	[auto_report_type],[auto_report_days],[auto_report_hour_min],	
	[auto_report_mail_to],[auto_report_mail_cc],
	[auto_report_mail_subject],[auto_report_mail_body]
) VALUES (
	1,	0,	1000,
	'testto@mytest.com', 'testcc@mytest.com',
	'测试报表邮件标题', '测试报表邮件内容'
)
GO


-- 一个邮件， N个报表.
INSERT INTO [dbo].[auot_report_detail](
	[auto_report_master_id],[report_id],[report_parameter]
) 
SELECT 1, 1, 'a=1;b=2'	UNION ALL
SELECT 1, 2, 'c=3;d=4'
GO


-- 一个报表，N个格式.
INSERT INTO [dbo].[auot_report_detail_file](
	[auto_report_detail_id],[auto_report_format],[auto_report_file_name]
)
SELECT 1, 3, '测试报表一Word'	UNION ALL
SELECT 1, 4, '测试报表一Excel'	UNION ALL
SELECT 2, 3, '测试报表二Word'	UNION ALL
SELECT 2, 4, '测试报表二Excel'	UNION ALL
SELECT 2, 5, '测试报表二Pdf'
GO



