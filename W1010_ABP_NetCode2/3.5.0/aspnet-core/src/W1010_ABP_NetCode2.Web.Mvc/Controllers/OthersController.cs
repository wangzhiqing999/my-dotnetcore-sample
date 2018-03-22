using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using W1010_ABP_NetCode2.Authorization;
using W1010_ABP_NetCode2.Controllers;
using W1010_ABP_NetCode2.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace W1010_ABP_NetCode2.Web.Mvc.Controllers
{


    [AbpMvcAuthorize(PermissionNames.Pages_Others)]
    public class OthersController : W1010_ABP_NetCode2ControllerBase
    {


        private readonly IOtherAppService _otherAppService;

        public OthersController(IOtherAppService otherAppService)
        {
            _otherAppService = otherAppService;
        }



        public async System.Threading.Tasks.Task<ActionResult> Index()
        {
            var output = await _otherAppService.GetAll(new PagedResultRequestDto { MaxResultCount = int.MaxValue }); //Paging not implemented yet
            return View(output);
        }



        public async System.Threading.Tasks.Task<ActionResult> EditOtherModal(long id)
        {
            var otherDto = await _otherAppService.Get(new EntityDto<long>(id));
            return View("_EditOtherModal", otherDto);
        }


    }
}