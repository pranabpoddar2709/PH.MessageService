using MessageService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PH.MessageService.ServiceImp
{
    public interface INotificationServiceFactory
    {
        Task<INotificationService> SendNotification(NotificationMode mode);
    }


   public class NotificationServiceFactory : INotificationServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public NotificationServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<INotificationService> SendNotification(NotificationMode mode)
        {
            switch (mode)
            {
                case NotificationMode.SMS:
                    return new SMSNotificationService();
                case NotificationMode.Email:
                    return new EmailNotificationService();
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, $"Mode of {mode} is not supported.");
            }
        }
    }

    public enum NotificationMode
    {
        SMS,
        Email,
        Pager
    }

}
