namespace ToDoApplication.Models;

public class CardModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public string DataInicio { get; set; }
    public string DataConclusao { get; set; }
    public int Prioridade { get; set; }
    public int StatusCard { get; set; }
    
}