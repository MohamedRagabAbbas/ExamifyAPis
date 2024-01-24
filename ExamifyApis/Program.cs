using ExamifyApis.DB;
using ExamifyApis.Models;
using ExamifyApis.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
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

// Add EF services to the services container.
builder.Services.AddDbContext<DBContextClass>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    options.User = new UserOptions 
    { 
        RequireUniqueEmail = true,
    })
    .AddEntityFrameworkStores<DBContextClass>().AddDefaultUI().AddDefaultTokenProviders();

builder.Services.AddScoped<StudentServices>();
builder.Services.AddScoped<TeacherServices>();
builder.Services.AddScoped<CourseServices>();
builder.Services.AddScoped<ExamServices>();
builder.Services.AddScoped<QuestionServices>();
builder.Services.AddScoped<UserServices>();
builder.Services.AddScoped<QuestionServices>();
builder.Services.AddScoped<AnswerServices>();
builder.Services.AddScoped<GradeServices>();
builder.Services.AddScoped<StudentAttemptsServices>();
builder.Services.AddScoped<AuthenticationManagement>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    }); 
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireClaim("Role", "Admin"));
    options.AddPolicy("Teacher", policy => policy.RequireClaim("Role", "Teacher"));
    options.AddPolicy("Student", policy => policy.RequireClaim("Role", "Student"));
});




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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
