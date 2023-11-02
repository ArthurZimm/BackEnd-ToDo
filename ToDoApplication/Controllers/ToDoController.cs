using System.Reflection.Metadata;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ToDoApplication.Models;
using ToDoApplication.Services;

namespace ToDoApplication.Controllers;
[ApiController]
[Route("/api/[controller]")]
public class ToDoController : ControllerBase
{
    private CardsService _cardsService;
    
    public ToDoController(CardsService cardsService)
    {
        _cardsService = cardsService;
    }
    [HttpGet("GetCards")]
    public IActionResult GetCards()
    {
        if (_cardsService.VerificaListaCards(_cardsService.ListaCards()))
            return Ok(_cardsService.ListaCards());
        return BadRequest("Lista Vazia");
    }

    [HttpPost]
    public IActionResult InsereCard(CardModel card)
    {
        if(_cardsService.ValidaCardParaInsercao(card))
            return Ok("Card Inserido");
        return BadRequest("Dados Invalidos");
    }

    [HttpPut]
    public IActionResult AtualizaStatusCard([FromQuery]int id, int idUsuario,int status)
    {
        if (_cardsService.AtualizaStatusCard(id, idUsuario, status))
            return Ok("Status do card atualizado com sucesso");
        return BadRequest("Ocorreu um erro ao atualizar o status do card");
    }

    [HttpDelete]
    public IActionResult DeletaCard([FromQuery] int id)
    {
        if (_cardsService.DeletaCard(id))
            return Ok("Card deletado com sucesso");
        return BadRequest("Erro ao deletar o card solicitado");
    }
}