﻿using FarmerApp.API.Utils;
using FarmerApp.API.Utils.Swagger;
using FarmerApp.Core.Extensions;
using FarmerApp.Core.Utils;
using FarmerApp.Data.DAO;
using FarmerApp.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

#region Services

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FarmerAPI", Version = "v1.3.4" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] { }
        }
    });
    c.CustomSchemaIds(x => x.FullName);
    c.DocumentFilter<SwaggerRouteExtenderDocumentFilter>();
});

builder.Services.AddHttpContextAccessor();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    Environment.GetEnvironmentVariable("JWT_SECRET_KEY", EnvironmentVariableTarget.Process)
                        ?? builder.Configuration["JwtSecretKey"]))
        };
        options.IncludeErrorDetails = false;
    });

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var appSettings = new ApplicationSettings(builder.Configuration);
builder.Services.AddDbContext<FarmerDbContext>(opts =>opts.UseSqlServer(appSettings.ConnectionString), ServiceLifetime.Scoped);
builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSeq());

builder.Services.AddCustomServices();

builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

var app = builder.Build();

await DbMigrator.MigrateDbAndPopulate(app, builder);

#endregion

#region Pipeline

// Configure the HTTP request pipeline.
app.UseCors("MyPolicy");

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion
