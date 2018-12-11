# .Net Core Entity Framework + web api


### DataAccess ��Ŀ

.Net Core, NuGet ������а���
  Microsoft.EntityFrameworkCore.Sqlite
  Microsoft.EntityFrameworkCore.Design
  Microsoft.EntityFrameworkCore.Tools
  Microsoft.EntityFrameworkCore.Tools.DotNet



### Service ��Ŀ

���� DataAccess ��Ŀ.






### Web ��Ŀ.

���� DataAccess��DataAccess ��Ŀ.

.Net Core, NuGet ������а���
  Microsoft.EntityFrameworkCore.Sqlite


���Ӵ�����
  ValuesController �������� ���½���Ŀ��ʱ�� �Զ����ɵģ�ɶҲû�޸ġ�
  CommodityPricesController ����������ֱ�ӵ��� EntityFramework�� �����ز����ġ� ������룬�� VS �������ɵģ�ɶҲû�޸ġ�
  TradableCommoditysController ���������ǵ����Լ�д�ķ��� �������ȥ���� EntityFramework�� �����ز����ġ�

ע�⣺CommodityPrice �࣬�Ǹ��������ģ���ˣ����ɵ� CommodityPricesController �������� ʵ��ʹ�õĻ��� ����Ҫ���޸ĵġ�


���ݿ��������Ϣ�� �Լ�����/�ӿڵ�����ע���������ã�д�� Startup.cs ����




### ����/����

�� VS2017 �в������к󣬷������ļ�ϵͳ�� bin\Release\PublishOutput Ŀ¼�¡�
���ڳ�ʼ����£� û�����ݣ� ʹ�� ���ݿ�����ߣ� �ֶ����뼸�����ݣ� ���в��ԡ�


#### ����̨����
...\MyMiniTradingSystem.Web\bin\Release\PublishOutput>dotnet MyMiniTradingSystem.Web.dll
Hosting environment: Production
Content root path: ...\MyMiniTradingSystem.Web\bin\Release\PublishOutput
Now listening on: http://localhost:5000
Application started. Press Ctrl+C to shut down.

Ȼ��������У�http://localhost:5000/api/CommodityPrices   �鿴ҳ�档



#### ������ IIS �¡�

���ȣ� �� IIS ʲô�ģ����Ѿ���װ��ɵ�����¡�
Ϊ������ .Net Core����Ҫ���ⰲװһ�� .NET Core Windows Server Hosting bundle
���ص�ַ�� https://aka.ms/dotnetcore.2.0.0-windowshosting
���غ���ļ���Ϊ DotNetCore.2.0.0-WindowsHosting.exe
��������ļ���Ȼ������ IIS ����
IIS �������У� ������վ�� Ŀ¼Ϊǰ�淢���� bin\Release\PublishOutput Ŀ¼�£� �˿� 8082
��Ӧ�ó�����У� �ҵ�����´�������վ�� ˫���� �� .NET CLR �汾�� �޸�Ϊ �����йܴ��롿

Ȼ��������У�http://localhost:8082/api/CommodityPrices  ȥ�鿴ҳ�档

�ο�ҳ�棺
https://docs.microsoft.com/en-us/aspnet/core/publishing/iis?tabs=aspnetcore2x



#### Linux ����̨������

�����������CentOS-7-x86_64-Minimal-1611.iso ��װ.

��װ .NetCore. �ο������ҳ��.
http://www.microsoft.com/net/learn/get-started/linuxcentos

�� Program.cs �ļ��У�WebHost.CreateDefaultBuilder(args)  ���棬 ׷��һ�� .UseUrls("http://*:5000")
���·���һ�Σ� Ȼ���ļ����Ƶ� Linux ϵͳ�¡�

[root@localhost PublishOutput]#dotnet MyMiniTradingSystem.Web.dll
Hosting environment: Production
Content root path: /home/wang/PublishOutput
Now listening on: http://[::]:5000
Application started. Press Ctrl+C to shut down.

Ȼ��������У�http://�����ip��ַ:5000/api/CommodityPrices   �鿴ҳ�档

