using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.DTOs;
using MinimalApi.Infraestrutura.Database;
using MinimalApi.Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MinimalApi.Dominio.Servicos;

public class AdministradorServico : IAdministradorServico
{
    private readonly DatabaseContexto _context;

    public AdministradorServico(DatabaseContexto context)
    {
        _context = context;
    }

    public Administrador? BuscaPorId(int id)
    {
        return _context.Administradores.FirstOrDefault(v => v.Id == id);
    }

    public Administrador Incluir(Administrador administrador)
    {
        _context.Administradores.Add(administrador);
        _context.SaveChanges();

        return administrador;
    }

    public Administrador? Login(LoginDTO loginDTO)
    {
        return _context.Administradores
            .FirstOrDefault(a => a.Email == loginDTO.Email && a.Senha == loginDTO.Senha);
    }

    public List<Administrador> Todos(int? pagina)
    {
        var query = _context.Administradores.AsQueryable();

        int itensPorPagina = 10;

        if (pagina != null)
            query = query.Skip(((int)pagina - 1) * itensPorPagina).Take(itensPorPagina);

        return query.ToList();
    }
    public void Apagar(Administrador administrador)
{
    _context.Administradores.Remove(administrador);
    _context.SaveChanges();
}

}
