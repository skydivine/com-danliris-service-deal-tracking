using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Com.DanLiris.Service.DealTracking.WebApi.Utilities;
using Com.DanLiris.Service.DealTracking.Lib.Models;
using Com.DanLiris.Service.DealTracking.Lib.BusinessLogic.Facades;
using AutoMapper;
using Com.DanLiris.Service.DealTracking.Lib.Services;
using Com.DanLiris.Service.DealTracking.Lib.ViewModels;

namespace Com.DanLiris.Service.DealTracking.WebApi.Controllers.v1
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/deal-tracking-reasons")]
    [Authorize]
    public class ReasonController : BaseController<Reason, ReasonViewModel, ReasonFacade>
    {
        private readonly static string apiVersion = "1.0";
        public ReasonController(IMapper mapper, IdentityService identityService, ValidateService validateService, ReasonFacade bookingOrderFacade) : base(mapper, identityService, validateService, bookingOrderFacade, apiVersion)
        {
        }
    }
}