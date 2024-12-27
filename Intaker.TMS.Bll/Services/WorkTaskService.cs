using Intaker.TMS.Bll.Events;
using Intaker.TMS.Bll.Models;
using Intaker.TMS.Dal.Models;
using Intaker.TMS.Dal.Repositories;

namespace Intaker.TMS.Bll.Services;


class WorkTaskService : IWorkTaskService
{
    private readonly IWorkTaskRepository _repository;
    private readonly IServiceBusHandler _serviceBusHandler;

    public WorkTaskService(IWorkTaskRepository repository, IServiceBusHandler serviceBusHandler)
    {
        _repository = repository;
        _serviceBusHandler = serviceBusHandler;
    }

    public async Task<WorkTaskDto> CreateAsync(WorkTaskDto workTask)
    {
        var dao = MapToDao(workTask);
        await _repository.CreateAsync(dao);
        await _serviceBusHandler.NotifyWorkTaskCreated(new WorkTaskCreatedEvent(dao.Id, dao.Name));
        return MapToDto(dao);
    }

    public async Task DeleteByIdAsync(int id)
    {
        var workTaskDto = await GetByIdAsync(id);
        if (workTaskDto == null) return;
        await _repository.DeleteAsync(MapToDao(workTaskDto));
    }

    public async Task<WorkTaskDto> GetByIdAsync(int id)
    {
        var item = await _repository.GetOneAsync(x => x.Id == id, tracking: false);
        return MapToDto(item);
    }

    public async Task<WorkTaskDto> UpdateAsync(WorkTaskDto workTask)
    {
        var dao = MapToDao(workTask);
        var result = await _repository.UpdateAsync(dao);
        return MapToDto(result);
    }

    private WorkTask MapToDao(WorkTaskDto dto) => new WorkTask()
    {
        Id = dto.Id,
        Name = dto.Name,
        Description = dto.Description,
        AssignedTo = dto.AssignedTo,
        Status = (WorkTaskStatus)dto.Status
    };

    private WorkTaskDto MapToDto(WorkTask dao) => new WorkTaskDto()
    {
        Id = dao.Id,
        Name = dao.Name,
        Description = dao.Description,
        AssignedTo = dao.AssignedTo,
        Status = (WorkTaskStatusDto)dao.Status
    };
}