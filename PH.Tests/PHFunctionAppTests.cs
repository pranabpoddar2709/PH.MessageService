using MessageService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using PH.MessageService.ServiceImp;
using PHDataAccessLayer.DataInterfaces;
using PHDataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit.Sdk;

namespace PH.Tests
{
    [TestClass]
    public class PHFunctionAppTests
    {
        private readonly ILogger logger = null;
        PHFunctionApp phFunctionApp = null;
        private readonly Mock<IGenericRepository<MessageMaster>> _genericRepository = null;
        private readonly Mock<INotificationServiceFactory> _notificationServiceFactory = null;
        public PHFunctionAppTests()
        {
            _notificationServiceFactory = new Mock<INotificationServiceFactory>();
            _genericRepository = new Mock<IGenericRepository<MessageMaster>>();
            logger = NullLoggerFactory.Instance.CreateLogger("Test");
            phFunctionApp = new PHFunctionApp(_genericRepository.Object,_notificationServiceFactory.Object);
        }
        [TestMethod]
        public void InsertMessage_Success()
        {
            var query = new Dictionary<String, StringValues>();
            var body = JsonConvert.SerializeObject(CreateRequest());
            var result = phFunctionApp.InsertMessage(HttpRequestSetup(query, body), logger);
            var resultObject = (OkObjectResult)result.Result;
            Assert.IsNotNull(resultObject.Value);
        }

        [TestMethod]
        public void UpdateMessage_Success()
        {
            var query = new Dictionary<String, StringValues>();
            var body = JsonConvert.SerializeObject(CreateRequest());
            var result = phFunctionApp.UpdateMessage(HttpRequestSetup(query, body), logger);
            var resultObject = (OkObjectResult)result.Result;
            Assert.IsNotNull(resultObject.Value);
        }

        public HttpRequest HttpRequestSetup(Dictionary<String, StringValues> query, string body)
        {
            var reqMock = new Mock<HttpRequest>();

            reqMock.Setup(req => req.Query).Returns(new QueryCollection(query));
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(body);
            writer.Flush();
            stream.Position = 0;
            reqMock.Setup(req => req.Body).Returns(stream);
            return reqMock.Object;
        }
        private MessageMaster CreateRequest()
        {
            var data = new MessageMaster();
            data.To = "";
            data.From = "";
            data.Details = "";
            data.MessageDate = DateTime.Now;
            data.Mode = "SMS";
            data.Status = "InProgress";
            return data;
        }
    }
}
