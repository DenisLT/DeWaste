using DeWasteApi.Data;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMvc(c => c.Conventions.Add(new ApiExplorerIgnores()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DeWasteDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DeWasteApiContext")));


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();

//custom middleware for error handling
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        await context.Response.WriteAsync(ex.Message);
    }
});

app.MapControllers();

app.Run();


public class ApiExplorerIgnores : IActionModelConvention
{
    public void Apply(ActionModel action)
    {
        if (action.Controller.ControllerName.Equals("Pwa"))
            action.ApiExplorer.IsVisible = false;
    }
}