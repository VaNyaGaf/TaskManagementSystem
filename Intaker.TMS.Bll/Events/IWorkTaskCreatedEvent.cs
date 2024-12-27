namespace Intaker.TMS.Bll.Events;

public interface IWorkTaskCreatedEvent
{
    int WorkTaskId { get; }
    string WorkTaskName { get; }
}
