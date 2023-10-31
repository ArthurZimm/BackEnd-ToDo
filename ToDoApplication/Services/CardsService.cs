using ToDoApplication.Models;
using ToDoApplication.Repositories;

namespace ToDoApplication.Services;

public class CardsService
{
    private CardsRepository _cardsRepository;
    
    public CardsService(CardsRepository cardsRepository)
    {
        _cardsRepository = cardsRepository;
    }

    public List<CardModel> ListaCards()
    {
        return _cardsRepository.GetCards();
    }

    public bool VerificaListaCards(List<CardModel> lista)
    {
        if (lista != null)
        {
            return true;
        }

        return false;
    }
    public bool ValidaCardParaInsercao(CardModel card)
    {
        if (card.Nome != null || card.StatusCard != null || card.Descricao != null || card.Prioridade != null)
        {
            _cardsRepository.InsereCard(card);
            return true;
        }
        return false;
    }

    public bool AtualizaStatusCard(int id, int status)
    {
        if(_cardsRepository.AtualizaStatusCard(id,status))
            return true;
        return false;
    }
}