using System.Threading.Tasks;
using Northwind.Application.Common.Interfaces;
using Northwind.Application.Notifications.Models;

namespace Northwind.Application
{
  public class NotificationService : INotificationService
  {
    public Task SendAsync(MessageDto message)
    {
      return Task.CompletedTask;
    }

  }
}