using Inquiry.Core.Domain.Models.Response.Entities;
using Inquiry.EndPoints.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddResponseFramework();
builder.Services.AddEndpointsApiExplorer();


var app = builder.ConfigureService();

app.ConfigurePipeline();

app.Run();
