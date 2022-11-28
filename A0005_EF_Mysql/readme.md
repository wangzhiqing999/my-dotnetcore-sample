# .Net Core Entity Framework

.Net Core, NuGet ������а���

MySql.Data.EntityFrameworkCore
MySql.Data.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.Tools
Microsoft.EntityFrameworkCore.Tools.DotNet


�ڴ�״̬�£���������ݿⲻ���ڣ� �״����У� �ܹ��Զ�������
����һ�����ݿⴴ���� ���뷢���仯�� ���޷�ͨ������ȥ�������ݿ��ˡ�



### �����Ĵ���.

Add-Migration MyFirstMigration
������������������ݿ�ͱ��C#����

Script-Migration
���ɴ������ SQL ���.

Update-Database
ִ�����ɵĴ��룬�������ݿ��ṹ.


ע�⣺ִ�� Update-Database ��ʱ�򣬿��ܻᱨ�� ��ʾ  __EFMigrationsHistory' doesn't exist

����취��
���� Script-Migration ���ɴ������ SQL ���.
�ֶ����� __EFMigrationsHistory ��. ���ֶ������������.

CREATE TABLE `__EFMigrationsHistory` (
    `MigrationId` varchar(150) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    PRIMARY KEY (`MigrationId`)
);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`) ......



### һ�Զ�Ĳ��Դ���.

���һ�Զ����ش��롣
��Ҫ�� DocumentTypes �� Documents �γ�һ�Զ�.

Add-Migration AddOneToManyCode
Update-Database





### ��Զ�Ĳ��Դ���.

��Ӷ�Զ����ش��롣
��Ҫ�� MrRole �� MrUser �γɶ�Զ�.  �м������Ϊ MrUserRole
ʵ���������൱������һ�Զ�Ĵ���
�м����Ҫ������һ�� �������������á�

Add-Migration AddManyToManyCode
Update-Database




### һ��һ�Ĳ��Դ���.
���һ��һ����ش��롣
��Ҫ�� MrUser �� MrUserExp �γ�һ��һ

Add-Migration AddOneToOneCode
Update-Database








# 2021-12-09
����һ���µ���Ŀ�� ʹ�� .Net 6.0

NuGet ������а���
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.Tools
MySql.EntityFrameworkCore


����֮ǰ ��Ŀ�Ĵ��룬 ����ͨ����


ִ��
Add-Migration MyFirstMigration

����
Method 'AppendIdentityWhereCondition' in type 'MySql.EntityFrameworkCore.MySQLUpdateSqlGenerator' from assembly 'MySql.EntityFrameworkCore, Version=5.0.8.0, Culture=neutral, PublicKeyToken=c5687fc88969c44d' does not have an implementation.


���� NuGet �����

�Ƴ� 
MySql.EntityFrameworkCore

���
Pomelo.EntityFrameworkCore.MySql


�������

��
UseMySql(connectionString);
�޸�Ϊ
UseMySql(connectionString, serverVersion);



����ִ��
Add-Migration MyFirstMigration
���.

Script-Migration
���ɴ������ SQL ���.

Update-Database
ִ�����ɵĴ��룬�������ݿ��ṹ.


��������








# 2022-11-28

�Ƴ� Migrations Ŀ¼��
�Ƴ� 
Pomelo.EntityFrameworkCore.MySql

���
MySql.EntityFrameworkCore 6.0.7


���� 
MysqlEntityFrameworkDesignTimeServices
�Ĵ��롣


��
UseMySql(connectionString, serverVersion);
�޸�Ϊ
UseMySQL(connectionString);



����ִ��
Add-Migration MyFirstMigration
���.


Script-Migration
���ɴ������ SQL ���.

Update-Database
ִ�����ɵĴ��룬�������ݿ��ṹ.


��������

