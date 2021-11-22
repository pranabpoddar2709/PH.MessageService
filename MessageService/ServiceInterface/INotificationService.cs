using PHDataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PH.MessageService
{
   public interface INotificationService
    {
        Task SendNotification(MessageMaster messageMaster);
    }
}
