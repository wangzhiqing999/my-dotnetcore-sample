# 基于 `microsoft/aspnetcore:latest` 来构建我们的镜像
# 注意，这个名字不是乱来的，/后面是 docker images 中的 Tag
FROM microsoft/aspnetcore:latest

# 拷贝项目publish文件夹中的所有文件到 docker容器中的publish文件夹中
COPY . /publish

#设置工作目录为 `/publish` 文件夹，即容器启动默认的文件夹
WORKDIR /publish

# 对外暴露的端口
EXPOSE 5000

# 使用`dotnet D0001_Docker_HelloWorld.dll`来运行应用程序
CMD ["dotnet", "D0001_Docker_HelloWorld.dll" ]