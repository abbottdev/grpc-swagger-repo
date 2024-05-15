using grpc_request_repo.Services;
using Microsoft.AspNetCore.Server;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddGrpc()
    .AddJsonTranscoding(o =>
        {
            o.JsonSettings.WriteEnumsAsIntegers = false;
        }).Services
        .AddGrpcSwagger()
        .AddGrpcReflection()
        .AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1",
                    new OpenApiInfo { Title = "My API", Version = "v1" });

            var filePath = Path.Combine(AppContext.BaseDirectory, "grpc-request-repo.xml");
            c.IncludeXmlComments(filePath);
            c.IncludeGrpcXmlComments(filePath, includeControllerXmlComments: true);
        });

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.MapGrpcReflectionService();
app.Run();
