using MySql.Data.MySqlClient;
using ToDoApplication.Models;

namespace ToDoApplication.Repositories;

public class LoginRepository
{
    private readonly IConfiguration _configuration;
    public LoginRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    /*
    public List<LoginModel> GetUsuario(string email)
    {
        string connectString = _configuration.GetConnectionString("MySqlConnection");
        string query = "select * from tb_usuario a where a.email like '%' || @email || '%'";
        List<LoginModel> usuarios = new List<LoginModel>();
        
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query,connection))
                {
                    command.Parameters.Add("@email", MySqlDbType.VarChar).Value = email;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LoginModel usuario = new LoginModel
                            {
                                Cpf = reader["cpf"].ToString(),
                                Nome = reader["nome"].ToString(),
                                Email = reader["email"].ToString(),
                                Senha = reader["senha"].ToString()
                            };
                            usuarios.Add(usuario);
                        }
                    }
                }
            }
            return usuarios;
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex);
            throw;
        }  
    }
    */
    public List<LoginModel> GetUsuarios()
    {
        List<LoginModel> usuarios = new List<LoginModel>();
        string connectString = _configuration.GetConnectionString("MySqlConnection");
        string query = "select * from tb_usuario";

        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query,connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        { 
                            LoginModel usuario = new LoginModel
                            {
                                Id = int.Parse(reader["id"].ToString()),
                                Cpf = reader["cpf"].ToString(),
                                Nome = reader["nome"].ToString(),
                                Email = reader["email"].ToString(),
                                Senha = reader["senha"].ToString()
                                
                            };
                            usuarios.Add(usuario);
                        }
                    }
                }   
            }
            return usuarios;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public bool InsereUsuarioBanco(LoginModel login)
    {
        string connectString = _configuration.GetConnectionString("MySqlConnection");
        string query = "Insert into tb_usuario (nome, senha, cpf, email)" +
                       "values (@nome, @senha, @cpf, @email)";

        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query,connection))
                {
                    command.Parameters.Add("@nome", MySqlDbType.VarChar).Value = login.Nome;
                    command.Parameters.Add("@senha", MySqlDbType.VarChar).Value = login.Senha;
                    command.Parameters.Add("@cpf", MySqlDbType.VarChar).Value = login.Cpf;
                    command.Parameters.Add("@email", MySqlDbType.VarChar).Value = login.Email;
                    using (MySqlTransaction tr = connection.BeginTransaction())
                    {
                        try
                        {
                            command.Transaction = tr;
                            int rowsAffected = command.ExecuteNonQuery();
                            tr.Commit();
                            if (rowsAffected > 0)
                            {
                                return true;
                            }

                            return false;
                        }
                        catch (MySqlException ex)
                        {
                            tr.Rollback();
                            Console.WriteLine(ex);
                            return false;
                        }
                    }
                }
            }
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex);
            return false;
        }
    }

    public bool AtualizaSenha(string cpf, string senha, string senhaAtual)
    {
        string connectString = _configuration.GetConnectionString("MySqlConnection");
        string query = "Update tb_usuario Set senha = @senha where cpf = @cpf and senha = @senhaAtual ";

        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query,connection))
                {
                    command.Parameters.Add("@cpf", MySqlDbType.VarChar).Value = cpf;
                    command.Parameters.Add("@senha", MySqlDbType.VarChar).Value = senha;
                    command.Parameters.Add("@senhaAtual", MySqlDbType.VarChar).Value = senhaAtual;
                    using (MySqlTransaction tr = connection.BeginTransaction())
                    {
                        try
                        {
                            command.Transaction = tr;
                            int rowsAffected = command.ExecuteNonQuery();
                            tr.Commit();
                            if (rowsAffected > 0)
                            {
                                return true;
                            }

                            return false;
                        }
                        catch (MySqlException ex)
                        {
                            tr.Rollback();
                            Console.WriteLine(ex);
                            return false;
                        }
                    }
                }
            }
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex);
            return false;
        }
    }

    public bool DeletaUsuario(string senha, string cpf)
    {
        string connectString = _configuration.GetConnectionString("MySqlConnection");
        string query = "delete from tb_usuario where cpf = @cpf and senha = @senha";
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query,connection))
                {
                    command.Parameters.Add("@cpf", MySqlDbType.VarChar).Value = cpf;
                    command.Parameters.Add("@senha", MySqlDbType.VarChar).Value = senha;
                    using (MySqlTransaction tr = connection.BeginTransaction())
                    {
                        try
                        {
                            command.Transaction = tr;
                            int rowsAffected = command.ExecuteNonQuery();
                            tr.Commit();
                            if (rowsAffected > 0)
                            {
                                return true;
                            }

                            return false;
                        }
                        catch (MySqlException ex)
                        {
                            tr.Rollback();
                            Console.WriteLine(ex);
                            return false;
                        }
                    }
                }
            }
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex);
            return false;
        }
    }

    
}