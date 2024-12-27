
using Intaker.TMS.Dal.Models;

namespace Intaker.TMS.Dal.Repositories;

public interface IWorkTaskRepository : IBasicRepository<int, WorkTask> {

}
public class WorkTaskRepository : BasicRepository<int, WorkTask>, IWorkTaskRepository
{
    public WorkTaskRepository(WorkTaskContext dbContext) : base(dbContext)
    {
    }
}
