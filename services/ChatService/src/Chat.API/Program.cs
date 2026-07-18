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
builder.Services.AddSwaggerDocumentation();
builder.Services.AddSignalR();

var app = builder.Build();

app.UseSwaggerDocumentation();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapConversationEndpoints();

app.MapMessageEndpoints();

app.MapHub<ChatHub>("/hubs/chat");

app.Run();