# .Net Core Entity Framework 


### ��ʼ������

.Net Core, NuGet ������а���

Microsoft.EntityFrameworkCore.Sqlite
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.Tools
Microsoft.EntityFrameworkCore.Tools.DotNet


�ڴ�״̬�£��� ������ݿⲻ���ڣ� �״����У� �ܹ��Զ�������
����һ�����ݿⴴ���� ���뷢���仯�� ���޷�ͨ������ȥ�������ݿ��ˡ�




### ��ʼ�� EF migration

��Ҫ��ɵĺ��������� �༭ A0006_EF_Sqlite.csproj
�� 
    <PackageReference  Include="Microsoft.EntityFrameworkCore.Tools" Version="2.0.0" />
    <PackageReference  Include="Microsoft.EntityFrameworkCore.Tools.DotNet" Version="2.0.0" />
�޸�Ϊ
    <DotNetCliToolReference  Include="Microsoft.EntityFrameworkCore.Tools" Version="2.0.0" />
    <DotNetCliToolReference  Include="Microsoft.EntityFrameworkCore.Tools.DotNet" Version="2.0.0" />

Ȼ�����������У� ��Ŀ�ĵ�ǰĿ¼�£� ����
...\A0006_EF_Sqlite>dotnet ef migrations add FirstMigration

�������ʾ��
Done. To undo this action, use 'ef migrations remove'

Ȼ�� �ڵ�ǰĿ¼�£�������һ�� Migrations Ŀ¼�� ������ص����ɵĴ����ļ���



Ȼ�����������У� ��Ŀ�ĵ�ǰĿ¼�£� ����
...\A0006_EF_Sqlite>dotnet ef database update

�������ʾ��Applying migration '20171102111133_FirstMigration'.
Done.

Ȼ�󣬵�ǰĿ¼�£� �������Ѿ����ɵ� sqlite ���ݿ��ļ���





### һ�Զ�Ĳ��Դ���.

���һ�Զ����ش��롣
��Ҫ�� DocumentTypes �� Documents �γ�һ�Զ�.

Ȼ�����������У� ��Ŀ�ĵ�ǰĿ¼�£� ����
...\A0006_EF_Sqlite>dotnet ef migrations add AddOneToManyCode

Ȼ�����������У� ��Ŀ�ĵ�ǰĿ¼�£� ����
...\A0006_EF_Sqlite>dotnet ef database update

�۲����ݿ��Ƿ���ȷ��������صı�.




### ��Զ�Ĳ��Դ���.

��Ӷ�Զ����ش��롣
��Ҫ�� MrRole �� MrUser �γɶ�Զ�.  �м������Ϊ MrUserRole
ʵ���������൱������һ�Զ�Ĵ���
�м����Ҫ������һ�� �������������á�

Ȼ�����������У� ��Ŀ�ĵ�ǰĿ¼�£� ����
...\A0006_EF_Sqlite>dotnet ef migrations add AddManyToManyCode

Ȼ�����������У� ��Ŀ�ĵ�ǰĿ¼�£� ����
...\A0006_EF_Sqlite>dotnet ef database update

�۲����ݿ��Ƿ���ȷ��������صı�.