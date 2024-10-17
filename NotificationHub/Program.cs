using NotificationHub.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add Swagger for API documentation (if needed)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add SignalR
builder.Services.AddSignalR();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueApp",
        builder => builder
            .WithOrigins("http://localhost:8081") // allow requests from your Vue.js app
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials() // Allow credentials
        );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();

// Enable CORS (this should be placed before app.UseEndpoints)
app.UseCors("AllowVueApp");
app.UseRouting();



app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<NotificationPush>("/notificationHub");  // Map your SignalR hub here
    endpoints.MapControllers();  // Map the controllers
});

app.MapControllers();

app.Run();
