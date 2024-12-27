namespace Intaker.TMS.Bll.Models;

public enum WorkTaskStatusDto
{
    NotStarted,
    InProgress,
    Completed
}

public class WorkTaskDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public WorkTaskStatusDto Status { get; set; }
    public string AssignedTo { get; set; }
}
