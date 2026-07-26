using Chat.API.Endpoints;
using Chat.API.Extensions;
using Chat.API.Hubs;
using Chat.API.Presence;
using Chat.Application;
using Chat.Infrastructure;
using Chat.API.Services;
using Chat.Application.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddSignalR();

builder.Services.AddScoped<IChatNotificationService, ChatNotificationService>();

builder.Services.AddSingleton<IPresenceTracker, InMemoryPresenceTracker>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactClient", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();

app.UseSwaggerDocumentation();

app.UseHttpsRedirection();

app.UseCors("ReactClient");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapConversationEndpoints();

app.MapMessageEndpoints();

app.MapHub<ChatHub>("/hubs/chat");

app.MapHub<PresenceHub>("/hubs/presence");

app.Run();