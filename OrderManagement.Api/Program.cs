using OrderManagement.Logic;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(builder =>
    builder.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

builder.Services.AddControllers(options => options.ReturnHttpNotAcceptable = true)
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    })
    .AddXmlDataContractSerializerFormatters();

builder.Services.AddScoped<IOrderManagementLogic, OrderManagementLogic>();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddOpenApiDocument(settings => settings.Title = "Order Management API");

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.UseOpenApi();
app.UseSwaggerUi(settings => settings.Path = "/swagger");
app.UseReDoc(settings => settings.Path = "/redoc");

app.Run();
