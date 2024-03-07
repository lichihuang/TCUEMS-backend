using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Data;
using System.Data.SqlClient;
using TCUEMS_BackendNew.Data;
using TCUEMS_BackendNew.Models;

var builder = WebApplication.CreateBuilder(args);

// 資料庫連線字串
var connectionString = "Data Source=.;Initial Catalog=SemesterWarning;Integrated Security=true;";

builder.Services.AddScoped<IDbConnection>(c => new SqlConnection(connectionString));
builder.Services.AddTransient<ISemesterWarningRepository, SemesterWarningRepository>(provider =>
    new SemesterWarningRepository(provider.GetRequiredService<IDbConnection>().ConnectionString));

// Swagger 相關服務設定
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
});

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Semester Warning API"));
}

//app.UseHttpsRedirection();  //  HTTPS 重新導向

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();
