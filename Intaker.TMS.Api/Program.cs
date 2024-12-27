using Intaker.TMS.Api;
using Intaker.TMS.Bll.Extensions;
using Intaker.TMS.Dal;
using Intaker.TMS.Dal.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddBllServices();
builder.Services.AddRepositories(builder.Configuration.GetConnectionString("TaskManagerDb"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<WorkTaskContext>();
    context.Database.EnsureCreated();
    DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
