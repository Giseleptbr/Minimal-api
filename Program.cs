using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Infraestrutura.Database;
using MinimalApi.Dominio.Interfaces;
using MinimalApi.Dominio.Servicos;
using MinimalApi.Dominio.DTOs;
using MinimalApi.Dominio.Entidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var key = builder.Configuration["Jwt:Key"];
var issuer = builder.Configuration["Jwt:Issuer"];
var audience = builder.Configuration["Jwt:Audience"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
    };
});


var builder = WebApplication.CreateBuilder(args);

// EF Core com SQLite
builder.Services.AddDbContext<DatabaseContexto>(options =>
{
    options.UseSqlite("Data Source=minimal_api.db");
});

// Injeção de dependência dos serviços
builder.Services.AddScoped<IAdministradorServico, AdministradorServico>();
builder.Services.AddScoped<IVeiculoServico, VeiculoServico>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ----------------------------
// ROTAS ADMINISTRADORES
// ----------------------------

// GET: Listar todos
app.MapGet("/administradores", (IAdministradorServico servico) =>
{
    return Results.Ok(servico.Todos(1)); // pagina 1
});

// GET: Buscar por Id
app.MapGet("/administradores/{id}", (int id, IAdministradorServico servico) =>
{
    var admin = servico.BuscaPorId(id);
    return admin is not null ? Results.Ok(admin) : Results.NotFound();
});

// POST: Criar administrador
app.MapPost("/administradores", ([FromBody] Administrador administrador, IAdministradorServico servico) =>
{
    var novoAdmin = servico.Incluir(administrador);
    return Results.Created($"/administradores/{novoAdmin.Id}", novoAdmin);
});

// PUT: Atualizar administrador
app.MapPut("/administradores/{id}", (int id, [FromBody] Administrador adminAtualizado, IAdministradorServico servico) =>
{
    var existente = servico.BuscaPorId(id);
    if (existente is null) return Results.NotFound();

    existente.Email = adminAtualizado.Email;
    existente.Senha = adminAtualizado.Senha;
    existente.Perfil = adminAtualizado.Perfil;

    servico.Incluir(existente); // ou Update se implementado
    return Results.Ok(existente);
});

// DELETE: Remover administrador
app.MapDelete("/administradores/{id}", (int id, IAdministradorServico servico) =>
{
    var existente = servico.BuscaPorId(id);
    if (existente is null) return Results.NotFound();

    servico.Apagar(existente);
    return Results.NoContent();
});

// ----------------------------
// ROTAS VEÍCULOS
// ----------------------------

// GET: Listar todos
app.MapGet("/veiculos", (IVeiculoServico servico) =>
{
    return Results.Ok(servico.Todos());
});

// GET: Buscar por Id
app.MapGet("/veiculos/{id}", (int id, IVeiculoServico servico) =>
{
    var veiculo = servico.BuscaPorId(id);
    return veiculo is not null ? Results.Ok(veiculo) : Results.NotFound();
});

// POST: Criar veículo
app.MapPost("/veiculos", ([FromBody] Veiculo veiculo, IVeiculoServico servico) =>
{
    servico.Incluir(veiculo);
    return Results.Created($"/veiculos/{veiculo.Id}", veiculo);
});

// PUT: Atualizar veículo
app.MapPut("/veiculos/{id}", (int id, [FromBody] Veiculo veiculoAtualizado, IVeiculoServico servico) =>
{
    var existente = servico.BuscaPorId(id);
    if (existente is null) return Results.NotFound();

    existente.Nome = veiculoAtualizado.Nome;
    existente.Marca = veiculoAtualizado.Marca;

    servico.Atualizar(existente);
    return Results.Ok(existente);
});

// DELETE: Remover veículo
app.MapDelete("/veiculos/{id}", (int id, IVeiculoServico servico) =>
{
    var existente = servico.BuscaPorId(id);
    if (existente is null) return Results.NotFound();

    servico.Apagar(existente);
    return Results.NoContent();
});

// ----------------------------
// ROTA LOGIN
// ----------------------------
app.MapPost("/login", ([FromBody] LoginDTO loginDTO, IAdministradorServico administradorServico) =>
{
    if (administradorServico.Login(loginDTO) != null)
        return Results.Ok("Login com sucesso");
    else
        return Results.Unauthorized();
});

// ----------------------------
// ROTA HOME
// ----------------------------
app.MapGet("/", (IAdministradorServico administradorServico) =>
{
    var totalAdmins = administradorServico.Todos(1).Count;
    return Results.Json(new { mensagem = $"API está rodando com SQLite! Total de admins: {totalAdmins}" });
});


app.UseAuthentication();
app.UseAuthorization();


app.Run();

