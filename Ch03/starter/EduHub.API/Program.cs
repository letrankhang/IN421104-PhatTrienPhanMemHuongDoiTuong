using EduHub.Application.Clubs;
using EduHub.Domain.Interfaces;
using EduHub.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ── Đăng ký services (Dependency Injection) ──────────────────────────────
// TODO 6a: đăng ký IClubRepository → InMemoryClubRepository (AddScoped)
// TODO 6b: đăng ký IMemberRepository → InMemoryMemberRepository (AddScoped)
// TODO 6c: đăng ký 4 handler: GetClubsQueryHandler, GetClubByIdQueryHandler,
//          CreateClubCommandHandler, DeleteClubCommandHandler (AddScoped)

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// TODO 7: cấu hình Swagger — SwaggerDoc "v1" với Title = "EduHub API"
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ── Middleware pipeline ───────────────────────────────────────────────────
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
