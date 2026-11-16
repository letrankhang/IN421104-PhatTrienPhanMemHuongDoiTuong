using EduHub.Application.Clubs;
using EduHub.Domain.Interfaces;
using EduHub.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ── Đăng ký services (Dependency Injection) ──────────────────────────────
// TODO 6a: đăng ký IClubRepository → InMemoryClubRepository (AddScoped)
builder.Services.AddScoped<IClubRepository, InMemoryClubRepository>();
// TODO 6b: đăng ký IMemberRepository → InMemoryMemberRepository (AddScoped)
builder.Services.AddScoped<IMemberRepository, InMemoryMemberRepository>();
// TODO 6c: đăng ký 4 handler: GetClubsQueryHandler, GetClubByIdQueryHandler,
builder.Services.AddScoped<GetClubsQueryHandler>();
builder.Services.AddScoped<GetClubByIdQueryHandler>();
//          CreateClubCommandHandler, DeleteClubCommandHandler (AddScoped)
builder.Services.AddScoped<CreateClubCommandHandler>();
builder.Services.AddScoped<DeleteClubCommandHandler>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c => {
    c.SwaggerDoc ("v1", new () { Title = "EduHub API", Version = "v1" });
});
// TODO 7: cấu hình Swagger — SwaggerDoc "v1" với Title = "EduHub API"
var app = builder.Build();

// ── Middleware pipeline ───────────────────────────────────────────────────
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
