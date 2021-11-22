using Azure;
using Azure.Communication.Identity;
using Azure.Communication.Sms;
using Azure.Core;
using Azure.Identity;
using PHDataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PH.MessageService.ServiceImp
{
    public class SMSNotificationService : INotificationService
    {
        private static readonly DefaultAzureCredential credential = new DefaultAzureCredential();

        public async Task SendNotification(MessageMaster messageMaster)
        {
            SmsSendResult result = await SendSms(new Uri(Environment.GetEnvironmentVariable("AcsUrl")), messageMaster.From, messageMaster.To, messageMaster.Details);
        }

        public  Response<AccessToken> CreateIdentityAndGetTokenAsync(Uri resourceEndpoint)
        {
            var client = new CommunicationIdentityClient(resourceEndpoint, credential);
            var identityResponse = client.CreateUser();
            var identity = identityResponse.Value;

            var tokenResponse = client.GetToken(identity, scopes: new[] { CommunicationTokenScope.VoIP });

            return tokenResponse;
        }
        public async Task<SmsSendResult> SendSms(Uri resourceEndpoint, string from, string to, string message)
        {
            SmsClient smsClient = new SmsClient(resourceEndpoint, credential);
            SmsSendResult sendResult = await smsClient.SendAsync(
                 from: from,
                 to: to,
                 message: message,
                 new SmsSendOptions(enableDeliveryReport: true) // optional
            );

            return sendResult;
        }
    }
}
