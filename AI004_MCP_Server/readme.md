## AI004_MCP_Server 用于测试的 MCP Server 项目（使用 SSE）

项目引用
ModelContextProtocol (0.1.0-preview.8)
ModelContextProtocol.AspNetCore (0.1.0-preview.8)


这个项目，是创建一个 SSE 的 MCP Server，然后发布到测试服务器上。
然后，用 AI 客户端，来进行测试。

假设这个网站，被发布在 http://192.168.1.242:8080/ 上面。



### 客户端 Dify 1.2.0
前往插件市场，搜索 MCP
安装 MCP SSE
通过 HTTP with SSE 传输使用 MCP 协议来发现和调用工具。

创建 Agent
工具的地方，[添加]-->[插件]-->[通过SSE发现和调用MCP工具]

获取MCP工具列表
调用MCP工具

点击右边的[工具未授权]按钮。
在MCP 服务配置的地方， 修改 "url" 的数值为  "http://192.168.1.242:8080/sse"
然后，确保这个工具，处于“启用”的模式。
Agent 使用的模型，为 "Deepseek-reasoner"

然后，测试提问
上海现在几点钟



### 追踪处理步骤

#### LLM
好的，用户再次询问上海当前的时间。之前我告诉他我不能直接提供实时时间，并建议他查看设备或在线查询。现在他又问同样的问题，可能需要更具体的帮助。
首先，我需要确认是否有工具可以获取实时时间。查看可用工具列表，之前有mcp_sse_call_tool和mcp_sse_list_tools。可能mcp_sse_call_tool可以用来调用某个时间相关的工具。但之前回复中没有相关工具的信息，可能需要先使用mcp_sse_list_tools获取可用工具列表。
执行mcp_sse_list_tools，查看是否有时间相关的工具。如果返回的工具有获取时间的，比如“get_current_time”，那么可以用它来查询上海时间。如果没有，可能需要再次告知用户无法直接获取，并建议其他方法。
假设调用mcp_sse_list_tools后，返回的工具列表中有获取时间的工具。接下来调用该工具，参数设为上海时区或CST。例如，参数可能是{"timezone": "Asia/Shanghai"}。然后解析返回的时间数据，生成用户友好的回答。
如果没有相关工具，只能重复之前的回复，或者建议用户手动检查时间。需要确保回答准确且有帮助，同时不提供错误信息。


#### mcp_sse_list_tools
MCP Server tools list:
[
  {
    "name": "GetCurrentTime",
    "description": "Get the current time for a city",
    "parameters": {
      "title": "GetCurrentTime",
      "description": "Get the current time for a city",
      "type": "object",
      "properties": {
        "city": {
          "type": "string"
        }
      },
      "required": [
        "city"
      ]
    }
  }
]


#### LLM
好的，用户再次询问上海的时间，之前已经回复过无法直接提供实时时间，但用户可能希望得到更直接的帮助。现在工具列表里有一个GetCurrentTime工具，可以用来获取城市的时间。
首先，我需要调用这个工具。根据工具的参数，需要提供城市名称。用户问的是上海，所以参数应该是{"city": "Shanghai"}。但需要注意拼写是否正确，比如应该是"Shanghai"而不是"Shanghai"。不过工具可能对参数有严格的校验，所以必须确保城市名称正确。
然后，使用mcp_sse_call_tool调用GetCurrentTime，参数为{"city": "Shanghai"}。假设调用成功，返回当前时间的数据，比如时间字符串。接着将结果转换为用户友好的格式，例如“上海当前的时间是YYYY年MM月DD日 HH:MM”。如果有错误，比如参数无效，则需要检查拼写或工具是否支持该城市。
如果调用成功，就可以把结果告诉用户。如果失败，可能需要提示用户确认城市名称或工具不可用，再次建议手动查看时间。需要确保回答准确，避免误导用户。


#### mcp_sse_call_tool
Tool execution result:
[
  {
    "type": "text",
    "text": "It is 15:6 in Shanghai."
  }
]


#### LLM
好的，现在我需要继续处理用户的问题。用户再次询问上海的时间，而我之前已经通过调用GetCurrentTime工具得到了结果，显示当前时间是15:06。接下来需要整理这个结果，并以用户友好的方式回应。
首先，确认工具返回的结果是否正确。观察到的结果是“It is 15:6 in Shanghai.”，这里的分钟数只有一位数，可能需要格式化为两位数的分钟，比如15:06，这样更符合常规时间显示习惯。不过也可能工具返回的数据本身就是这样的格式，所以需要保持原样还是调整一下？
其次，用户使用的是中文，所以在回应时应该用中文回复，并且时间格式可能需要调整成中国常用的格式，例如下午3点06分，或者15:06。需要确保时间信息准确无误，并且表达清晰。
另外，是否需要补充时区信息？比如提到上海使用的是中国标准时间（CST），UTC+8，这样可以增强回答的完整性。但用户可能只需要当前时间，所以这部分可以视情况添加。
最后，确保回答礼貌且有帮助，可能还需要提醒用户如果需要更精确的时间，可以自行查询在线资源，但根据当前工具的结果，已经能够提供准确的时间了。需要避免任何可能的信息错误，比如时间格式不正确或者城市名称错误。

总结步骤：
1. 解析工具返回的时间结果。
2. 格式化时间，确保分钟两位显示。
3. 用中文回应用户，包含城市名称和时间。
4. 可选添加时区信息。
5. 确保回答简洁准确。

上海当前的时间是15:06。






## AI004_Stdio_MCP_Server 用于测试的 MCP Server 项目 （使用 studio）


项目引用
ModelContextProtocol (0.2.0-preview.2)
Microsoft.Extensions.Hosting (9.0.5)

参考
https://learn.microsoft.com/en-us/dotnet/ai/quickstarts/build-mcp-server
创建代码。


在 Cherry Studio 中，新建一个 MCP 服务器.
类型：标准输入/输出(studio)
命令：dotnet
参数：
run
--project
D:\My-Github\my-dotnetcore-sample\AI004_MCP_Server\AI004_Stdio_MCP_Server\AI004_Stdio_MCP_Server.csproj

注意：参数的最后一行，是我的 C# 代码，在这个目录下。

保存成功后，前往工具的 Tab，看看是否成功识别到了 C# 中的写的工具代码。





