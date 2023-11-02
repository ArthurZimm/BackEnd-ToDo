using Microsoft.AspNetCore.Mvc;
using ToDoApplication.Models;
using ToDoApplication.Services;

namespace ToDoApplication.Controllers;
[ApiController]
[Route("/api/[controller]")]
public class LoginController : ControllerBase
{
    private LoginService _loginService;
    public LoginController(LoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpGet]
    public IActionResult GetUsuarios()
    {
        if (_loginService.ValidaUsuarios(_loginService.GetUsuarios()))
        {
            return Ok(_loginService.GetUsuarios());
        }

        return BadRequest("Lista Não Encontrada.");
    }
    
    /*
    [HttpGet("Usuario")]
    public IActionResult GetUsuario([FromQuery] string email)
    {
        if (_loginService.ValidaUsuario(_loginService.GetUsuario(email)))
        {
            return Ok(_loginService.GetUsuario(email));
        }

        return BadRequest("Usuario Não encotrado.");
    }
    */
    
    [HttpPost]
    public IActionResult EfetuaLogin(LoginModel login)
    {
        if (_loginService.ValidaLogin(login))
            return Ok("Usuario Inserido com sucesso");
        return BadRequest("Erro ao inserir o usuario");
    }

    [HttpPut]
    public IActionResult AtualizaSenha([FromQuery] string cpf, string senha, string senhaAtual)
    {
        if (_loginService.AtualizaSenha(cpf, senha, senhaAtual))
            return Ok("Atualizado com sucesso");
        return BadRequest("Erro ao atualizar");
    }

    [HttpDelete]
    public IActionResult DeletaUsuario([FromQuery]string cpf, string senha)
    {
        if (_loginService.DeletaUsuario(cpf, senha))
            return Ok("Deletado com sucesso");
        return BadRequest("Erro ao deletar");
    }
}