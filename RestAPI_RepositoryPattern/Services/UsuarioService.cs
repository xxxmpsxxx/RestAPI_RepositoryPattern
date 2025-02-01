using AutoMapper;
using Dapper;
using RestAPI_RepositoryPattern.Dto;
using RestAPI_RepositoryPattern.Models;
using System.Data.SqlClient;

namespace RestAPI_RepositoryPattern.Services
{
    public class UsuarioService : IUsuarioInterface
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public UsuarioService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<ResponseModel<List<UsuarioListaDto>>> GetUsuarios()
        {
            var response = new ResponseModel<List<UsuarioListaDto>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuariosDB = await connection.QueryAsync<Usuario>("SELECT * FROM Usuarios");

                if (usuariosDB == null || !usuariosDB.Any())
                {
                    response.Message = "Nenhum usuário localizado";
                    response.Status = false;
                    return response;
                }

                var usuarioMapped = _mapper.Map<List<UsuarioListaDto>>(usuariosDB);

                response.Data = usuarioMapped;
                response.Status = true;
                return response;
            }
        }

        public async Task<ResponseModel<List<UsuarioListaDto>>> CriarUsuario(UsuarioCriarDto instance)
        {
            var response = new ResponseModel<List<UsuarioListaDto>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuariosDB = await connection.ExecuteAsync(@"
INSERT INTO
Usuarios (Nome, Email, Salario, CPF, Senha, Status)
VALUES (@Nome, @Email, @Salario, @CPF, @Senha, @Status)", instance);

                if (usuariosDB == 0)
                {
                    response.Message = "Ocorreu um erro ao realizar o registro";
                    response.Status = false;
                    return response;
                }

                var usuarios = await ListarUsuarios(connection);

                var usuarioMapped = _mapper.Map<List<UsuarioListaDto>>(usuarios);

                response.Data = usuarioMapped;
                response.Status = true;
                response.Message = "Usuário criado com sucesso";

                return response;
            }
        }

        public async Task<ResponseModel<List<UsuarioListaDto>>> EditarUsuario(UsuarioEditarDto instance)
        {
            var response = new ResponseModel<List<UsuarioListaDto>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuariosDB = await connection.ExecuteAsync(@"
UPDATE Usuarios
   SET Nome = @Nome, Email = @Email, Salario = @Salario, CPF = @CPF, Status = @Status
 WHERE Id = @Id", instance);

                if (usuariosDB == 0)
                {
                    response.Message = "Ocorreu um erro ao realizar a edição do registro";
                    response.Status = false;
                    return response;
                }

                var usuarios = await ListarUsuarios(connection);

                var usuarioMapped = _mapper.Map<List<UsuarioListaDto>>(usuarios);

                response.Data = usuarioMapped;
                response.Status = true;
                response.Message = "Usuário alterado com sucesso";

                return response;
            }
        }

        public async Task<ResponseModel<List<UsuarioListaDto>>> RemoverUsuario(int id)
        {
            var response = new ResponseModel<List<UsuarioListaDto>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuariosDB = await connection.ExecuteAsync(@"
DELETE FROM Usuarios
 WHERE Id = @Id", new { Id = id });

                if (usuariosDB == 0)
                {
                    response.Message = "Ocorreu um erro ao realizar a exclusão do registro";
                    response.Status = false;
                    return response;
                }

                var usuarios = await ListarUsuarios(connection);

                var usuarioMapped = _mapper.Map<List<UsuarioListaDto>>(usuarios);

                response.Data = usuarioMapped;
                response.Status = true;
                response.Message = "Usuário excluído com sucesso";

                return response;
            }
        }

        private static async Task<IEnumerable<Usuario>> ListarUsuarios(SqlConnection connection)
        {
            return await connection.QueryAsync<Usuario>("SELECT * FROM Usuarios");
        }
    }
}
