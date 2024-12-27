namespace Intaker.TMS.Dal.Models;

public enum WorkTaskStatus {
    NotStarted,
    InProgress,
    Completed
}

public class WorkTask
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public WorkTaskStatus Status { get; set; }
    public string AssignedTo { get; set; }
}
