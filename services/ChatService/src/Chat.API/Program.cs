using Chat.API.Endpoints;
using Chat.API.Hubs;
using Chat.Application;
using Chat.Infrastructure;
using Chat.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.MapConversationEndpoints();

app.MapMessageEndpoints();

app.MapHub<ChatHub>("/hubs/chat");

app.Run();