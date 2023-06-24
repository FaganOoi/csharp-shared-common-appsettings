using SharedSettingsAcrossMultipleProjects;
using SharedSettingsAcrossMultipleProjects.Extensions;

var builder = WebApplication.CreateBuilder(args);
IWebHostEnvironment environment = builder.Environment;
builder = builder.SetSharedAppSettingJson(environment);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<AppSettingModel>(builder.Configuration.GetSection(nameof(AppSettingModel)));
builder.Services.Configure<SharedSettingModel>(builder.Configuration.GetSection(nameof(SharedSettingModel)));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
