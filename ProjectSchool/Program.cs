var builder = WebApplication.CreateBuilder(args);

// CORS für lokale Anfragen aktivieren
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5107")
              .AllowAnyMethod() // Erlaubt GET, POST, PUT, DELETE, etc.
              .AllowAnyHeader(); // Erlaubt alle Header
    });
});

// Services hinzufügen
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowLocalhost");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();


app.Run();
