using Intaker.TMS.Bll.Models;

namespace Intaker.TMS.Bll.Services;

public interface IWorkTaskService
{
    Task<WorkTaskDto> CreateAsync(WorkTaskDto workTask);
    Task<WorkTaskDto> UpdateAsync(WorkTaskDto workTask);
    Task<WorkTaskDto> GetByIdAsync(int id);
    Task DeleteByIdAsync(int id);
}
