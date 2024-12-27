namespace Intaker.TMS.Bll.Events;

public record WorkTaskCreatedEvent(int WorkTaskId, string WorkTaskName) : IWorkTaskCreatedEvent
{
}
