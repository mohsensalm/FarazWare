
using FarazWare.Application.UseCases;
using FarazWare.Infrastructure.Clients;
using FarazWare.Infrastructure.Configuration;
using FarazWare.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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
            builder.Services.AddSingleton<IEncryptionService, RsaEncryptionService>();
            builder.Services.AddHttpClient<IAuthClient, AuthClient>();
            builder.Services.AddHttpClient<ICardClient, CardClient>();
            builder.Services.AddTransient<AcquireTokenUseCase>();
            builder.Services.AddTransient<GetCardsUseCase>();
            builder.Services.AddHttpClient<OAuthClient>();

            //builder.Services.AddHttpClient<FakeBankApiClient>();

            //builder.Services.AddScoped<IClientCredentialsService, ClientCredentialsService>();
            //builder.Services.AddScoped<IAuthorizationCodeService, AuthorizationCodeService>();
            //builder.Services.AddScoped<IRevokeService, RevokeService>();
            //builder.Services.AddScoped<IRefreshService, RefreshService>();
            //builder.Services.AddScoped<ILoginService, LoginService>();


            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession();


            //builder.Services.AddAuthentication(options => {
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(options => {
            //    options.Authority = builder.Configuration["OAuth:Authority"];
            //    options.Audience = builder.Configuration["OAuth:ClientId"];
            //    options.RequireHttpsMetadata = true;
            //});

            var app = builder.Build();

            // پیکربندی Middlewareها
            if (app.Environment.IsDevelopment())
            {
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
