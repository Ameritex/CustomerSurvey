using CustomerSurvey.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using CustomerSurvey.MappingProfiles;
using CustomerSurvey.Data.Repositories.Contracts;
using CustomerSurvey.Data.Repositories;
using CustomerSurvey.Services.Contracts;
using CustomerSurvey.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
var services = builder.Services;
services.AddControllersWithViews();
services.AddMvc();
var connectionString = configuration.GetValue<string>("ConnectionStrings:DbConnectionString");
services.AddDbContext<SurveyContext>(options =>
{
    options.UseSqlServer(connectionString);
});

services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
var config = new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new SurveyMappingProfile());
});
var mapper = config.CreateMapper();

services.AddSingleton(mapper);


var encrytionKey = configuration.GetValue<string>("EncrytionKey") ?? "ameritex";
var encrytionConfig = new EncryptDecrypt(encrytionKey);
services.AddSingleton(encrytionConfig);

services.AddScoped<ICustomerSurveyRepository, CustomerSurveyRepository>();
services.AddScoped<ICustomerSurveyQuestionRepository, CustomerSurveyQuestionRepository>();
services.AddScoped<ICustomerSurveyQuestionService, CustomerSurveyQuestionService>();
services.AddScoped<ICustomerSurveyService, CustomerSurveyService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
