using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using Studenda.Core.Data;
using Studenda.Core.Data.Configuration;
using Studenda.Core.Server.utils;
using Studenda.Core.Server.utils.Token;


#if DEBUG
const bool isDebugMode = true;
#else
const bool isDebugMode = false;
#endif
var builder = WebApplication.CreateBuilder(args);
//��������� �����������
builder.Services.AddControllers();
//��������� �  builder ������������ ��� ���� ������(���������� ���  sqlite)
builder.Services.AddSingleton<ContextConfiguration>(_ => new SqliteConfiguration("Data Source=000_debug_storage.db", isDebugMode));
//��������� ���� ������
builder.Services.AddDbContext<DataContext>(o=>o.UseSqlite());

builder.Services.AddScoped<ITokenService, TokenService>();
//��������� ����
builder.Services.AddCors(c => c.AddPolicy("cors", opt =>
{
    opt.AllowAnyHeader();
    opt.AllowCredentials();
    opt.AllowAnyMethod();
    opt.WithOrigins(builder.Configuration.GetSection("Cors:Urls").Get<string[]>()!);
}));
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // ���������, ����� �� �������������� �������� ��� ��������� ������
            ValidateIssuer = true,
            // ������, �������������� ��������
            ValidIssuer = AuthOptions.ISSUER,
            // ����� �� �������������� ����������� ������
            ValidateAudience = true,
            // ��������� ����������� ������
            ValidAudience = AuthOptions.AUDIENCE,
            // ����� �� �������������� ����� �������������
            ValidateLifetime = true,
            // ��������� ����� ������������
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            // ��������� ����� ������������
            ValidateIssuerSigningKey = true,
            ClockSkew=TimeSpan.FromMinutes(2),
        };
    });


var app = builder.Build();
app.UseCors("cors");
app.MapControllers();
app.Run();