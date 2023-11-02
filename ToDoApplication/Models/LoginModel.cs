using System.ComponentModel.DataAnnotations;

namespace ToDoApplication.Models;

public class LoginModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Senha { get; set; }
    public string Cpf { get; set; }
    public string Email { get; set; }
}