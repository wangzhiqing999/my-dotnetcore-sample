using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using P0002_MyEtf.DataAccess;
using P0002_MyEtf.Model;
using P0002_MyEtf.Service;



namespace P0002_MyEtf.ServiceImpl
{
    public class DefaultEtfMasterService : IEtfMasterService
    {

        private readonly MyEtfContext _MyEtfContext;


        private readonly ILogger<DefaultEtfMasterService> _Logger;



        public DefaultEtfMasterService(MyEtfContext context, ILogger<DefaultEtfMasterService> logger)
        {
            this._MyEtfContext = context;
            this._Logger = logger;
        }






        public EtfMaster GetEtfMaster(string etfCode)
        {
            EtfMaster result = this._MyEtfContext.EtfMasters.Find(etfCode);
            return result;
        }




        public List<EtfMaster> GetEtfMasterList()
        {
            var query =
                from data in this._MyEtfContext.EtfMasters
                select
                    data;

            List<EtfMaster> resultList = query.ToList();
            return resultList;
        }
    }
}
