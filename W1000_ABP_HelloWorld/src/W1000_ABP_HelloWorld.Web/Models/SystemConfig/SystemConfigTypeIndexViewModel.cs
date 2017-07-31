using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using W1000_ABP_HelloWorld.SystemConfig.Dtos;


namespace W1000_ABP_HelloWorld.Web.Models.SystemConfig
{


    public class SystemConfigTypeIndexViewModel
    {


        public IReadOnlyList<SystemConfigTypeDto> SystemConfigTypes { get; }



        public SystemConfigTypeIndexViewModel(IReadOnlyList<SystemConfigTypeDto> systemConfigTypes)
        {
            SystemConfigTypes = systemConfigTypes;
        }

    }
}
