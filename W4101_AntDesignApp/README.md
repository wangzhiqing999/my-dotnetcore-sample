# W4101_AntDesignApp





安装 node.js

完成后，运行 node --version 来验证.
v20.18.0


先切换源.
npm config set registry https://registry.npmmirror.com/


看看有没有安装过 gulp
gulp --version


没有安装过的话，安装 gulp
npm install --global gulp-cli




安装模板
dotnet new --install AntDesign.Templates


使用模板，创建项目

dotnet new antdesign -o W4101_AntDesignApp






如果要卸载已安装的模板。

先运行下面的命令，进行查询。
dotnet new uninstall


当前已安装项:
   AntDesign.Templates
      版本: 0.20.2.2
      详细信息:
         Author: ant-design-blazor
         Owners: ElderJames
         Reserved: ✘
         NuGetSource: https://api.nuget.org/v3/index.json
      模板:
         Ant Design Pro Blazor App (antdesign) C#
      卸载命令:
         dotnet new uninstall AntDesign.Templates






### W4111_AntDesignProWebApp

在 Visual Studio 2022 中， 创建项目时， 选择模板 “Ant Design Pro Blazor App (AntDesign Blazor Team)”

full 上不打勾
host 选择 webapp
styles 选择 css


结果是创建两个项目， 结果与 W4101_AntDesignApp 的代码， 基本一致.





### W4112_AntDesignProWasm

在 Visual Studio 2022 中， 创建项目时， 选择模板 “Ant Design Pro Blazor App (AntDesign Blazor Team)”

full 上打勾
host 选择 wasm
styles 选择 css


结果是生成一个 项目，Wasm 的。




### W4113_AntDesignProServer

在 Visual Studio 2022 中， 创建项目时， 选择模板 “Ant Design Pro Blazor App (AntDesign Blazor Team)”

full 上打勾
host 选择 server
styles 选择 css


结果是生成一个 项目，WebApp 的。






### 组件使用的例子代码

W4113_AntDesignProServer 项目

W4101_AntDesignApp.Client 项目

