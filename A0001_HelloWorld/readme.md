.NET Core
���� dotnet-dev-win-x64.1.0.3.exe �� ��װ��


��֤��װ���
ִ�У�

    dotnet --version



���������棬ִ�� dotnet new �����һ������̨Ӧ��

ִ�У�

    dotnet new console

������Ϻ� �ڵ�ǰĿ¼������һ�� cs �ļ��� csproj �ļ�.



ִ�У�

    dotnet restore

������Ϻ� �ڵ�ǰĿ¼�������� obj / bin  Ŀ¼.



ִ�У�

    dotnet run


���к� ��ʾ

    Hello World!

	
	
���� Visual Studio Code��ѡ���ļ�������ġ����ļ��С����Ϳ���ʹ��IDE���л����Ŀ���/���Դ����ˡ�
��������ο���

    https://docs.microsoft.com/zh-cn/dotnet/csharp/getting-started/with-visual-studio-code







## 2021��12��6��.

����������  .NET 6.0
�����Է���
����ģʽ���������
Ŀ������ʱ��linux-64
���ɵ����ļ�

���Ի���Ϊ  Ubuntu 20.04.3 LTS (GNU/Linux 5.4.0-91-generic x86_64)

�ο�ҳ�棺https://docs.microsoft.com/zh-cn/dotnet/core/install/linux-ubuntu


�Ƚ� Microsoft ��ǩ����Կ��ӵ���������Կ�б�����Ӱ��洢�⡣

wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb


Ŀ�����������װ Runtime

sudo apt-get update; \
  sudo apt-get install -y apt-transport-https && \
  sudo apt-get update && \
  sudo apt-get install -y aspnetcore-runtime-6.0
  

�� Ubuntu �²�������

dotnet A0001_HelloWorld.dll




  



