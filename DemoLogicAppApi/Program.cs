/* using DemoAngularCrudApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AngCustDBContext>(options => options.UseInMemoryDatabase(
   "SmartAirDB"));
builder.Services.AddCors(options
=> {
    options.AddPolicy("SmartCors",
 builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
 */
 using DemoAngularCrudApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AngCustDBContext>(options => options.UseInMemoryDatabase(
   "SmartAirDB"));
builder.Services.AddCors(options
=> {
    options.AddPolicy("SmartCors",
 builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "DemoLogicAppApi",
        Version = "1.0"
    });

    options.CustomOperationIds(apiDesc =>
    {
        var controller = apiDesc.ActionDescriptor.RouteValues.TryGetValue("controller", out var c) ? c : "Api";
        var action = apiDesc.ActionDescriptor.RouteValues.TryGetValue("action", out var a) ? a : "Action";
        return $"{controller}_{action}";
    });

    options.OperationFilter<JsonOnlyContentTypesOperationFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();

public sealed class JsonOnlyContentTypesOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.RequestBody?.Content is { Count: > 0 } requestContent)
        {
            var mediaType = requestContent.TryGetValue("application/json", out var json)
                ? json
                : requestContent.Values.FirstOrDefault();

            operation.RequestBody.Content = mediaType is null
                ? new Dictionary<string, OpenApiMediaType>()
                : new Dictionary<string, OpenApiMediaType>
                {
                    ["application/json"] = mediaType
                };
        }

        foreach (var response in operation.Responses.Values)
        {
            if (response.Content is not { Count: > 0 } responseContent)
            {
                continue;
            }

            var mediaType = responseContent.TryGetValue("application/json", out var json)
                ? json
                : responseContent.Values.FirstOrDefault();

            response.Content = mediaType is null
                ? new Dictionary<string, OpenApiMediaType>()
                : new Dictionary<string, OpenApiMediaType>
                {
                    ["application/json"] = mediaType
                };
        }
    }
}
