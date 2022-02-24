using BlazorPerformance.Api.BackgroundServices;
using BlazorPerformance.Api.Data;
using BlazorPerformance.Api.Hubs;
using BlazorPerformance.Api.Services;
using BlazorPerformance.Api.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProtoBuf.Grpc.Server;
using System.IO.Compression;

var corsPolicy = "CorsPolicy";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicy,
        builder =>
        {
            builder
                .AllowCredentials()
                .AllowAnyHeader()
                .WithHeaders(new[] { "GET", "HEAD", "PUT", "POST", "DELETE" })
                .WithOrigins("https://localhost:7191");
        });
});

builder.Services.AddScoped(sp => new HttpClient());
builder.Services.AddScoped<ContributionsService>();
builder.Services.AddScoped<ConferencesService>();
builder.Services.AddScoped<SpeakersService>();

builder.Services.AddDbContext<SampleDatabaseContext>(
                options => options.UseInMemoryDatabase(databaseName: "BlazorPerformance"));

builder.Services.AddControllers();

builder.Services.AddCodeFirstGrpc(config => { config.ResponseCompressionLevel = CompressionLevel.Optimal; });

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Net6Features.Api", Version = "v1" });
});

builder.Services.AddSignalR();
builder.Services.AddHostedService<RealTimeSyncService>();

var app = builder.Build();
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blazor.WASM.Api v1"));
}

// ONLY FOR DEMO
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DataGenerator.InitializeAsync(services);
}

app.UseCors(corsPolicy);
app.UseHttpsRedirection();
app.UseRouting();

app.UseGrpcWeb();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<ContributionsService>().EnableGrpcWeb();
    endpoints.MapGrpcService<ConferencesService>().EnableGrpcWeb();
    endpoints.MapGrpcService<SpeakersService>().EnableGrpcWeb();
    endpoints.MapHub<RealtimeHub>("/count");
    endpoints.MapControllers();
});

app.Run();