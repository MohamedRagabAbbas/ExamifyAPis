using ExamifyApis.DB;
using ExamifyApis.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
   // options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    // Add other JSON serialization options as needed.
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DBContextClass>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<StudentServices>();
builder.Services.AddScoped<TeacherServices>();
builder.Services.AddScoped<CourseServices>();
builder.Services.AddScoped<ExamServices>();
builder.Services.AddScoped<QuestionServices>();
builder.Services.AddScoped<UserServices>();

// builder.Services.AddScoped<AnswerServices>();
// builder.Services.AddScoped<ExamResultServices>();
// builder.Services.AddScoped<ExamQuestionServices>();
// builder.Services.AddScoped<ExamAnswerServices>();
// builder.Services.AddScoped<ExamQuestionAnswerServices>();

var app = builder.Build();

app.UseCors(builder =>
{
    builder
        .AllowAnyOrigin() // You may want to restrict this in production
        .AllowAnyMethod()
        .AllowAnyHeader();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
