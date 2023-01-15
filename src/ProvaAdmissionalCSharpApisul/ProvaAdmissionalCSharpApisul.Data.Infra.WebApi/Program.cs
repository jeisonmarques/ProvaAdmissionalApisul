using ProvaAdmissionalCSharpApisul.Data.Infra.IoC.App_Start;
using ProvaAdmissionalCSharpApisul.Infra.Data.Contexts;
using MediatR;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using System.IO.Compression;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .AddCommandLine(args)
            .AddUserSecrets<Program>(true)
            .Build();

        builder.Services.AddResponseCompression();
        builder.Services.Configure<GzipCompressionProviderOptions>(p => p.Level = CompressionLevel.Fastest);

        builder.Services.AddDbContext<HsElevadorContext>(options => options.UseSqlServer(config["ConnectionStrings:ProvaAdmissionalCSharpApisul"]));
        builder.Services.AddMediatR(AppDomain.CurrentDomain.Load("ProvaAdmissionalCSharpApisul.Domain.Events"));
        InjectionDependencyCore.ConfigureServices(builder.Services);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}