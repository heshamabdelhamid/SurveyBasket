namespace SurveyBasket.Services.Notification;

public interface INotificationService
{
    Task SendNewPollNotification(int? pollId = null);
}
