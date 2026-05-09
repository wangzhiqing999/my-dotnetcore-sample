using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace A5500_AvaloniaApp.Models
{

    /// <summary>
    /// 配置数据.
    /// https://docs.avaloniaui.net/docs/how-to/data-persistence-how-to 
    /// </summary>
    public class AppSettings
    {
        public string Theme { get; set; } = "Default";
        public double WindowWidth { get; set; } = 800;
        public double WindowHeight { get; set; } = 600;

    }



}
