
using FarazWare.Application.UseCases;
using FarazWare.Infrastructure.Clients;
using FarazWare.Infrastructure.Configuration;
using FarazWare.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.BearerToken;

namespace FarazWare.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // بارگذاری تنظیمات
            builder.Services.Configure<BearerTokenOptions>(
                builder.Configuration.GetSection("BearerTokenOptions"));
            builder.Services.Configure<AuthSettings>(
                builder.Configuration.GetSection("AuthSettings"));
            builder.Services.Configure<KeySettings>(
                builder.Configuration.GetSection("Keys"));

            // ثبت سرویس‌ها
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            builder.Services.AddSingleton<IEncryptionService, RsaEncryptionService>();
            builder.Services.AddHttpClient<IAuthClient, AuthClient>();
            builder.Services.AddHttpClient<ICardClient, CardClient>();
            builder.Services.AddTransient<AcquireTokenUseCase>();
            builder.Services.AddTransient<GetCardsUseCase>();

            var app = builder.Build();

            // پیکربندی Middlewareها
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
