using Microsoft.EntityFrameworkCore;
using MinimalApi.Infraestrutura.Database;

var builder = WebApplication.CreateBuilder(args);

// Usar SQLite em vez de MySQL
builder.Services.AddDbContext<DatabaseContexto>(options =>
{
    options.UseSqlite("Data Source=minimal_api.db");
});

var app = builder.Build();

app.MapGet("/", () => "API está rodando com SQLite!");

// Rota de login simples
app.MapPost("/login", (LoginDTO loginDTO) =>
{
    if (loginDTO.Email == "admin@teste.com" && loginDTO.Senha == "123456")
        return Results.Ok(new { message = "Login successful" });
    else
        return Results.Json(new { message = "Invalid credentials" }, statusCode: 401);
});

app.Run();

public class LoginDTO
{
    public string Email { get; set; } = default!;
    public string Senha { get; set; } = default!;
}

