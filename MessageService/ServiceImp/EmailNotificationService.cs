using PHDataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PH.MessageService.ServiceImp
{
    public class EmailNotificationService : INotificationService
    {
        private readonly string _acsURLEndPoint;
        public EmailNotificationService(string acsURLEndPoint)
        {
            _acsURLEndPoint = acsURLEndPoint;
        }
        public Task SendNotification(MessageMaster messageMaster)
        {
            throw new NotImplementedException();
        }
    }
}
