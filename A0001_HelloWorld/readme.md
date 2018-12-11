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

