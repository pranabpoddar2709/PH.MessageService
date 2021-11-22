using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data;
using System.Linq;
using PHDataAccessLayer.Models;
using PHDataAccessLayer.DataInterfaces;
using PH.MessageService.ServiceImp;
using Dasync.Collections;
using System.Collections.Generic;

namespace MessageService
{
    public class PHFunctionApp
    {
        private readonly IGenericRepository<MessageMaster> _genericRepository = null;
        private readonly INotificationServiceFactory _notificationServiceFactory;

        public PHFunctionApp(IGenericRepository<MessageMaster> genericRepository, INotificationServiceFactory notificationServiceFactory)
        {
            _genericRepository = genericRepository;
            _notificationServiceFactory = notificationServiceFactory;
        }

        [FunctionName("SendNotification")]
        public  async Task<IActionResult> SendNotification([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<List<MessageMaster>>(requestBody);
            try
            {
                await _genericRepository.BulkInsert(data);
                return new OkResult();
            }
            catch (Exception e)
            {
                log.LogError(e.ToString());
                return new BadRequestResult();
            }
        }

       


        [FunctionName("InsertMessage")]
        public async Task<IActionResult> InsertMessage([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            MessageMaster data = JsonConvert.DeserializeObject<MessageMaster>(requestBody);
            try
            {
                var result = await _genericRepository.Create(data);
                return new OkObjectResult(result);
            }
            catch (Exception e)
            {
                log.LogError(e.ToString());
                return new BadRequestResult();
            }
        }

        [FunctionName("UpdateMessage")]
        public async Task<IActionResult> UpdateMessage([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            MessageMaster data = JsonConvert.DeserializeObject<MessageMaster>(requestBody);
            try
            {
                await _genericRepository.Update(data.MessageId, data);
                return new OkResult();
            }
            catch (Exception e)
            {
                log.LogError(e.ToString());
                return new BadRequestResult();
            }
        }

        [FunctionName("DeleteMessage")]
        public async Task<IActionResult> DeleteMessage([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            MessageMaster data = JsonConvert.DeserializeObject<MessageMaster>(requestBody);
            try
            {
                await _genericRepository.Delete(data.MessageId);
                return new OkResult();
            }
            catch (Exception e)
            {
                log.LogError(e.ToString());
                return new BadRequestResult();
            }
        }

        [FunctionName("SendNotification")]
        public async Task Run([TimerTrigger("*/30 */1 * * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

             var result = await _genericRepository.GetAllAsync();
            if (result != null && result.Count() > 0)
            {
                var data = result.Where(x => x.Status == Status.Failed.ToString() || x.Status == Status.InProgress.ToString()).ToList();
                await data.ParallelForEachAsync(async message =>
                {
                    try
                    {
                        await _notificationServiceFactory.SendNotification((NotificationMode)Enum.Parse(typeof(NotificationMode), message.Mode));
                        message.Status = Status.Success.ToString();
                        await _genericRepository.Update(message.MessageId, message);
                    }
                    catch (Exception ex)
                    {
                        message.Status = Status.Failed.ToString();
                        await _genericRepository.Update(message.MessageId, message);
                        log.LogError("Sending Failed " + ex.Message, ex.StackTrace);
                    }
                });
            }
        }
    }
}

