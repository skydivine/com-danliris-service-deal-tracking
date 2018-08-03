using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Com.DanLiris.Service.DealTracking.WebApi.Utilities;
using Com.DanLiris.Service.DealTracking.Lib.Models;
using Com.DanLiris.Service.DealTracking.Lib.ViewModels;
using Com.DanLiris.Service.DealTracking.Lib.BusinessLogic.Facades;
using AutoMapper;
using Com.DanLiris.Service.DealTracking.Lib.Services;
using System.IO;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Com.DanLiris.Service.DealTracking.WebApi.Controllers.v1
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/deal-tracking-activities")]
    [Authorize]
    public class ActivityController : BaseController<Activity, ActivityViewModel, ActivityFacade>
    {
        private readonly static string apiVersion = "1.0";
        private readonly AzureStorageService azureStorageService;
        public ActivityController(IMapper mapper, IdentityService identityService, ValidateService validateService, ActivityFacade ActivityFacade, AzureStorageService azureStorageService) : base(mapper, identityService, validateService, ActivityFacade, apiVersion)
        {
            this.azureStorageService = azureStorageService;
        }

        [HttpPost("upload/file")]
        public async Task<ActionResult> UploadFile()
        {
            try
            {
                this.ValidateUser();
                var isUpdate = Convert.ToBoolean(Request.Form["update"]);
                var files = Request.Form.Files;

                List<Task> uploadTasks = new List<Task>();
                List<ActivityAttachment> attachments = new List<ActivityAttachment>();

                foreach (var file in files)
                {
                    var inputStream = new MemoryStream();
                    file.CopyTo(inputStream);

                    string fileName = this.azureStorageService.GenerateFileName(file.FileName);

                    attachments.Add(new ActivityAttachment() { FileName = file.FileName, FilePath = fileName });
                    uploadTasks.Add(this.azureStorageService.UploadFile(inputStream, "Activity", fileName));
                }

                
                await Task.WhenAll(uploadTasks);

                if (isUpdate == false)
                {
                    Activity model = new Activity()
                    {
                        Type = "NOTES",
                        DealId = Convert.ToInt64(Request.Form["dealId"]),
                        DealCode = Request.Form["dealCode"].ToString(),
                        DealName = Request.Form["dealName"].ToString(),
                        Notes = Request.Form["notes"].ToString(),
                        Attachments = attachments,
                    };

                    await this.Facade.Create(model);

                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, Common.CREATED_STATUS_CODE, Common.OK_MESSAGE)
                        .Ok();
                    return Created(String.Concat(Request.Path, "/", model.Id), Result);
                }
                else
                {
                    long activityId = Convert.ToInt64(Request.Form["Id"]);
                    Activity model = await this.Facade.ReadById(activityId);
                    model.Notes = Request.Form["notes"].ToString();

                    await this.Facade.Update(activityId, model);
                    await this.Facade.CreateAttachment(activityId, attachments);

                    return NoContent();
                }
            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(Common.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }

        [HttpGet("attachment/{fileName}")]
        public async Task<IActionResult> GetFile([FromRoute] string fileName)
        {
            var pieces = fileName.Split(new[] { '_' }, 2);
            MemoryStream stream = await this.azureStorageService.DownloadFile("Activity", fileName);
            stream.Position = 0;

            return File(stream, "application/octet-stream", pieces[1]);
        }

        [HttpPut("attachment/delete")]
        public async Task<IActionResult> GetFile([FromBody] ActivityAttachmentViewModel activityAttachment)
        {
            ValidateUser();

            await this.Facade.DeleteAttachment(activityAttachment.Id);
            await this.azureStorageService.DeleteImage("Activity", activityAttachment.FilePath);
            
            return NoContent();
        }
    }
}