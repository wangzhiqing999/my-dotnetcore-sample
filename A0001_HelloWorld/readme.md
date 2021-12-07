.NET Core
下载 dotnet-dev-win-x64.1.0.3.exe 后， 安装。


验证安装结果
执行：

    dotnet --version



命令行下面，执行 dotnet new 命令创建一个控制台应用

执行：

    dotnet new console

运行完毕后， 在当前目录，生成一个 cs 文件与 csproj 文件.



执行：

    dotnet restore

运行完毕后， 在当前目录，生成了 obj / bin  目录.



执行：

    dotnet run


运行后， 显示

    Hello World!

	
	
运行 Visual Studio Code，选择【文件】下面的【打开文件夹】。就可以使用IDE进行基本的开发/调试处理了。
具体操作参考：

    https://docs.microsoft.com/zh-cn/dotnet/csharp/getting-started/with-visual-studio-code







## 2021年12月6日.

将其升级到  .NET 6.0
并测试发布
部署模式：依赖框架
目标运行时：linux-64
生成单个文件

测试机器为  Ubuntu 20.04.3 LTS (GNU/Linux 5.4.0-91-generic x86_64)

参考页面：https://docs.microsoft.com/zh-cn/dotnet/core/install/linux-ubuntu


先将 Microsoft 包签名密钥添加到受信任密钥列表，并添加包存储库。

wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb


目标机器仅仅安装 Runtime

sudo apt-get update; \
  sudo apt-get install -y apt-transport-https && \
  sudo apt-get update && \
  sudo apt-get install -y aspnetcore-runtime-6.0
  

在 Ubuntu 下测试运行

dotnet A0001_HelloWorld.dll




  



