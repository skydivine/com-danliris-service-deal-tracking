using Microsoft.AspNetCore.Mvc;
using Com.DanLiris.Service.DealTracking.Lib.Models;
using Com.DanLiris.Service.DealTracking.Lib.BusinessLogic.Facades;
using AutoMapper;
using Com.DanLiris.Service.DealTracking.Lib.Services;
using Microsoft.AspNetCore.Authorization;
using Com.DanLiris.Service.DealTracking.WebApi.Utilities;
using Com.DanLiris.Service.DealTracking.Lib.ViewModels;

namespace Com.DanLiris.Service.DealTracking.WebApi.Controllers.v1
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/deal-tracking-boards")]
    [Authorize]
    public class BoardController : BaseController<Board, BoardViewModel, BoardFacade>
    {
        private readonly static string apiVersion = "1.0";
        public BoardController(IMapper mapper, IdentityService identityService, ValidateService validateService, BoardFacade boardFacade) : base(mapper, identityService, validateService, boardFacade, apiVersion)
        {
        }
    }
}