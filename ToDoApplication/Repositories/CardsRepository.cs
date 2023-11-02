using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using ToDoApplication.Models;

namespace ToDoApplication.Repositories;
public class CardsRepository
{
    private readonly IConfiguration _configuration;
    public CardsRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public List<CardModel> GetCards()
    {
        List<CardModel> cards = new List<CardModel>();
        string connectString = _configuration.GetConnectionString("MySqlConnection");
        string query = "Select * from tb_tarefa";
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
                            CardModel card = new CardModel
                            {
                                Id = int.Parse(reader["id"].ToString()),
                                IdUsuario = int.Parse(reader["id_usuario"].ToString()),
                                Nome = reader["nome"].ToString(),
                                Descricao = reader["descricao"].ToString(),
                                DataInicio = reader["data_inicio"].ToString(),
                                DataConclusao = reader["data_conclusao"].ToString(),
                                Prioridade = int.Parse(reader["prioridade"].ToString()),
                                StatusCard = int.Parse(reader["status_card"].ToString())
                            };
                            cards.Add(card);
                        }
                    }
                }   
            }
            return cards;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void InsereCard(CardModel card)
    {
        string connectString = _configuration.GetConnectionString("MySqlConnection");
        string query = "Insert into tb_tarefa(nome, descricao, data_inicio, data_conclusao, prioridade, status_card, id_usuario) " +
                       "values (@nome, @descricao, @data_inicio, @data_conclusao, @prioridade, @status_card, @id_usuario)";

        using (MySqlConnection connection = new MySqlConnection(connectString))
        {
            try
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nome", card.Nome);
                    command.Parameters.AddWithValue("@id_usuario", card.IdUsuario);
                    command.Parameters.AddWithValue("@descricao", card.Descricao);
                    command.Parameters.AddWithValue("@data_inicio", card.DataInicio);
                    command.Parameters.AddWithValue("@data_conclusao", card.DataConclusao);
                    command.Parameters.AddWithValue("@prioridade", card.Prioridade);
                    command.Parameters.AddWithValue("@status_card", card.StatusCard);
                    
                    using (MySqlTransaction tr = connection.BeginTransaction())
                    {
                        try
                        {
                            command.Transaction = tr;
                            command.ExecuteNonQuery();
                            tr.Commit();
                        }
                        catch (MySqlException ex)
                        {
                            tr.Rollback();
                            Console.WriteLine(ex);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex);
            }
        }
    }

    public bool AtualizaStatusCard(int id, int idUsuario,int status)
    {
        string connectString = _configuration.GetConnectionString("MySqlConnection");
        string query = "Update tb_tarefa set status_card = @status_card where id = @id and id_usuario = @id_usuario";
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query,connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@id_usuario", idUsuario);
                    command.Parameters.AddWithValue("@status_card", status);
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
        catch (MySqlException e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public bool DeletaCard(int id)
    {
        string connectString = _configuration.GetConnectionString("MySqlConnection");
        string query = "Delete from tb_tarefa where id = @id";
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query,connection))
                {
                    command.Parameters.AddWithValue("@id", id);
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
        catch (MySqlException e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}