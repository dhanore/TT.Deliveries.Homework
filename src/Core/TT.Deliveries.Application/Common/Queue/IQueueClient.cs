namespace TT.Deliveries.Application.Common;
public interface IQueueClient<in T>
{
    Task<long> ScheduleMessageAsync(T notificationMessage, DateTime scheduledEnqueueTime);
    Task CancelScheduledMessageAsync(long messageSequenceNumber);
}