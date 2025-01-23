//using WebSocketApiExample.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
//services.AddSingleton<WebSocketService>();

services.AddCors(options =>
{
  options.AddPolicy(name: "MyPolicy",
      builder =>
      {
        builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
      });
});

WebApplication app = builder.Build();

app.UseHttpsRedirection();
app.UseCors(option => option.AllowAnyOrigin().AllowAnyMethod());
app.UseWebSockets();
app.MapControllers();
await app.RunAsync();
app.Run();
