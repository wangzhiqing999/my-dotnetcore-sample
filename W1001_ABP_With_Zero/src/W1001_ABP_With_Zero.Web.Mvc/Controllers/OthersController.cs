using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using W1001_ABP_With_Zero.Authorization;
using W1001_ABP_With_Zero.Controllers;
using W1001_ABP_With_Zero.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace W1001_ABP_With_Zero.Web.Mvc.Controllers
{


    [AbpMvcAuthorize(PermissionNames.Pages_Others)]
    public class OthersController : W1001_ABP_With_ZeroControllerBase
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