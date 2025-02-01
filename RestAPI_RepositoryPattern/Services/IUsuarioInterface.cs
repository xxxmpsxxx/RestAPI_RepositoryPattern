using RestAPI_RepositoryPattern.Dto;
using RestAPI_RepositoryPattern.Models;

namespace RestAPI_RepositoryPattern.Services
{
    public interface IUsuarioInterface
    {
        Task<ResponseModel<List<UsuarioListaDto>>> GetUsuarios();
        Task<ResponseModel<List<UsuarioListaDto>>> CriarUsuario(UsuarioCriarDto instance);
        Task<ResponseModel<List<UsuarioListaDto>>> EditarUsuario(UsuarioEditarDto instance);
        Task<ResponseModel<List<UsuarioListaDto>>> RemoverUsuario(int id);
    }
}
