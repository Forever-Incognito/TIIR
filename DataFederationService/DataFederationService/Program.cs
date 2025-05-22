using DataFederationService.Services;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the dependency injection container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // Enables API explorer for Swagger/OpenAPI
builder.Services.AddSwaggerGen();           // Configures Swagger for API documentation

// Register HttpClient for making external HTTP requests.
// AddHttpClient provides a pre-configured HttpClient instance for SpoonacularFederationService,
// including features like connection pooling and error handling.
builder.Services.AddHttpClient<SpoonacularFederationService>();

// Register SpoonacularFederationService with a 'scoped' lifetime.
// A scoped service is created once per client request (connection).
builder.Services.AddScoped<SpoonacularFederationService>();

var app = builder.Build();

// Configure the HTTP request processing pipeline.
if (app.Environment.IsDevelopment())
{
    // Enable Swagger UI in development environment for API documentation and testing.
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); // Enforces HTTPS for secure communication

app.UseAuthorization();    // Enables authorization middleware

app.MapControllers();      // Maps controller routes to API endpoints

app.Run(); // Starts the application