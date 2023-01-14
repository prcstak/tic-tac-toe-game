using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using account.Game;
using account.Hubs;
using account.Middleware;
using account.Producer;
using application;
using backgroundService.Services;
using infrastructure;
using interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddSingleton<Game>();
builder.Services.AddTransient<MessageProducer>();
builder.Services.AddTransient<IRatingService, RatingService>();

builder.Services.AddSignalR();

builder.Services.AddAuthentication().AddCookie("Cookies");

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(config =>
    {
        var secretBytes = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]);
        var key = new SymmetricSecurityKey(secretBytes);
        
        config.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey = key,
        };
        config.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];

                // If the request is for our hub...
                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) &&
                    (path.StartsWithSegments("/game")))
                {
                    // Read the token out of the query string
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "policyName",
        policyBuilder =>
        {
            policyBuilder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomExceptionHandler();

app.UseCors("policyName");

/*app.UseHttpsRedirection();*/

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapHub<GameHub>("/game");

app.Run();