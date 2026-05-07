using DevFreela.API;
using DevFreela.API.Filters;
using DevFreela.Application.Validators;
using DevFreela.Core.Interfaces;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Infrastructure.Persistence.Repositories;
using DevFreela.Infrastructure.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using DevFreela.Application.Commands.Project.CreateProject;
using DevFreela.Application.Commands.Project.DeleteProject;
using DevFreela.Application.Commands.Project.FinishProject;
using DevFreela.Application.Commands.Project.StartProject;
using DevFreela.Application.Commands.Project.UpdateProject;
using DevFreela.Application.Commands.Project.CancelProject;
using DevFreela.Application.Commands.User.CreateUser;
using DevFreela.Application.Commands.User.UpdateUser;
using DevFreela.Application.Commands.User.DeleteUser;
using DevFreela.Application.Commands.Project.CreateComment;
using DevFreela.Application.Queries.Project.GetAllProjects;
using DevFreela.Application.Queries.Project.GetProjectById;
using DevFreela.Application.Queries.Skill.GetAllSkills;
using DevFreela.Application.Queries.User.GetUserById;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DevFreelaDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DevFreelaCs")));

builder.Services.AddControllers(options => options.Filters.Add(typeof(ValidationFilter)));
builder.Services.AddFluentValidationAutoValidation()
    .AddValidatorsFromAssemblyContaining<CreateProjectCommandValidator>();

builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "DevFreela.API", Version = "v1" });
    x.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT authorization header using bearer scheme"
    });

    x.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});

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
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? string.Empty))
        };
    });

builder.Services.AddMediatR(typeof(CancelProjectCommand));
builder.Services.AddMediatR(typeof(CreateCommentCommand));
builder.Services.AddMediatR(typeof(CreateProjectCommand));
builder.Services.AddMediatR(typeof(CreateUserCommand));
builder.Services.AddMediatR(typeof(DeleteProjectCommand));
builder.Services.AddMediatR(typeof(DeleteUserCommand));
builder.Services.AddMediatR(typeof(FinishProjectCommand));
builder.Services.AddMediatR(typeof(StartProjectCommand));
builder.Services.AddMediatR(typeof(UpdateProjectCommand));
builder.Services.AddMediatR(typeof(UpdateUserCommand));

builder.Services.AddMediatR(typeof(GetAllProjectsQuery));
builder.Services.AddMediatR(typeof(GetAllSkillsQuery));
builder.Services.AddMediatR(typeof(GetProjectByIdQuery));
builder.Services.AddMediatR(typeof(GetUserByIdQuery));

FluentValidationConfig.ConfigureDisplayNames();

var app = builder.Build();
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
