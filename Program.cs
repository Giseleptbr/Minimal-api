var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();



app.MapPost("/login", (LoginDTO LoginDTO) =>
{
    if (LoginDTO.email == "admin@teste.com" && LoginDTO.Senha == "123456")
        return Results.Ok(new { message = "Login successful" });
    else
        return Results.Unauthorized(new { message = "Invalid credentials" });
});

app.Run();
