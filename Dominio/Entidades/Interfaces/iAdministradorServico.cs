using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.DTOs;


namespace MinimalApi.Dominio.Interfaces;

public interface IAdministradorServico
{
    Administrador? Login(LoginDTO loginDTO);
    Administrador Incluir(Administrador administrador);
    Administrador? BuscaPorId(int id);
    List<Administrador> Todos(int? pagina);
    void Apagar(Administrador administrador);
}