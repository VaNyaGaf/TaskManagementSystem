using Microsoft.AspNetCore.Mvc;
using Intaker.TMS.Bll.Services;
using Intaker.TMS.Bll.Models;

namespace Intaker.TMS.Api.Controllers;

[ApiController]
[Route("api/v1/tasks")]
public class TasksController : ControllerBase
{

    private readonly IWorkTaskService _workTaskService;

    public TasksController(IWorkTaskService workTaskService)
    {
        _workTaskService = workTaskService;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetTask(int id)
    {
        var workTask = await _workTaskService.GetByIdAsync(id);
        return Ok(workTask);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask(WorkTaskDto workTaskDto)
    {
        var workTask = await _workTaskService.CreateAsync(workTaskDto);
        return Ok(workTask);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateTask(int id, WorkTaskDto workTaskDto)
    {
        var workTask = await _workTaskService.UpdateAsync(workTaskDto);
        return Ok(workTask);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        await _workTaskService.DeleteByIdAsync(id);
        return NoContent();
    }
}