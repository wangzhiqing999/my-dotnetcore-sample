using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI002_Ollama
{
    class Constant
    {

        /// <summary>
        /// 服务器地址.
        /// <br/>
        /// 这里是测试环境地址.
        /// 实际使用时，如果是ollama是安装在其它服务器上的，请替换为实际的地址.
        /// 或者是放到配置文件里面去.
        /// </summary>
        public const string OLLAMA_HOST = "http://localhost:11434/";


        /// <summary>
        /// 模型名称.
        /// <br/>
        /// 这个是要求ollama 上面，有这个模型.
        /// </summary>
        public const string OLLAMA_MODEL = "qwen2.5:1.5b";

    }
}
