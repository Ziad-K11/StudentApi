using StudentApi.ErrorHandlingMiddleware;
using StudentApi.Repos;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IStudentRepo, StudentRepo>();
builder.Services.AddSingleton<ICourseRepo, CourseRepo>();
builder.Services.AddSingleton<IStudentCourseRepo, StudentCourseRepo>();
var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
