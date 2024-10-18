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

