using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Com.DanLiris.Service.DealTracking.Lib.Services;
using Com.DanLiris.Service.DealTracking.Lib.BusinessLogic.Facades;
using Com.DanLiris.Service.DealTracking.Lib.ViewModels;
using Microsoft.EntityFrameworkCore;
using Com.DanLiris.Service.DealTracking.WebApi.Utilities;

namespace Com.DanLiris.Service.DealTracking.WebApi.Controllers.v1
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/deal-tracking-move-activities")]
    [Authorize]
    public class MoveActivityController : Controller
    {
        private readonly IdentityService IdentityService;
        private readonly DealFacade Facade;
        private const string ApiVersion = "1.0";

        public MoveActivityController(IdentityService identityService, ValidateService validateService, DealFacade facade)
        {
            this.IdentityService = identityService;
            this.Facade = facade;
        }

        private void ValidateUser()
        {
            IdentityService.Username = User.Claims.Single(p => p.Type.Equals("username")).Value;
            IdentityService.Token = Request.Headers["Authorization"].First().Replace("Bearer ", "");
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] MoveActivityViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                ValidateUser();

                await Facade.MoveActivity(viewModel);

                return NoContent();
            }
            catch (DbUpdateConcurrencyException e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(Common.INTERNAL_ERROR_STATUS_CODE, Result);
            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(Common.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }
    }
}