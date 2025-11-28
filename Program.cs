using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using TagManagement.Data;
using TagManagement.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Configure JSON serializer to be case-insensitive
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TagManagement API",
        Version = "v1",
        Description = "API for managing vehicle tags"
    });
    
    // Resolve conflicts when multiple actions have the same route but different content types
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

// DbContext
builder.Services.AddDbContext<CaseDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Tag service
builder.Services.AddScoped<IItagService, TagService>();

// File upload service
builder.Services.AddScoped<IFileUploadService, FileUploadService>();

var app = builder.Build();

// Swagger middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TagManagement API v1");
    });
}

app.UseHttpsRedirection();

// Enable static files serving (for uploaded images)
app.UseStaticFiles();

app.UseAuthorization();
app.MapControllers();
app.Run();
