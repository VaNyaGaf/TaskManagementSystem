using Intaker.TMS.Dal;
using Intaker.TMS.Dal.Models;

namespace Intaker.TMS.Api;

public static class DbInitializer
{
    public static void Initialize(WorkTaskContext context)
    {
        if (context.WorkTasks.Any())
        {
            return;
        }

        var workTasks = new WorkTask[]
        {
                new WorkTask
                {
                    Name = "Tech task",
                    Description = "Send tech task to intaker",
                    AssignedTo = "Ivan Hafiak",
                    Status = WorkTaskStatus.Completed,
                },
                new WorkTask
                {
                    Name = "Tech task",
                    Description = "Review tech task from Ivan Hafiak",
                    AssignedTo = "Intaker",
                    Status = WorkTaskStatus.InProgress,
                },
        };

        context.WorkTasks.AddRange(workTasks);
        context.SaveChanges();
    }
}
