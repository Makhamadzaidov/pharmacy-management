using Microsoft.EntityFrameworkCore;
using PharmacyAppExam.WebApi.Commons.Middlewares;
using PharmacyAppExam.WebApi.DbContexts;
using PharmacyAppExam.WebApi.Helpers;
using PharmacyAppExam.WebApi.Interfaces.Services;
using PharmacyAppExam.WebApi.Mappers;
using PharmacyAppExam.WebApi.Services;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("PostgresProductionDb");
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseNpgsql(connectionString);
    option.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IDrugService, DrugService>();
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddMemoryCache();

var botToken = builder.Configuration.GetSection("TelegramBotToken")["Production"];
builder.Services.AddSingleton<ITelegramBotClient, TelegramBotClient>(p =>
    new TelegramBotClient(botToken));


var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

HttpContextHelper.Accessor = app.Services.GetRequiredService<IHttpContextAccessor>();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
