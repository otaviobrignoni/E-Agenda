namespace E_Agenda.Dominio.ModuloTarefa;

public class Item
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public statusItem status { get; set; }
    public Tarefa Tarefa { get; set; }

    public Item() {}
    public Item(string titulo, Tarefa tarefa) : this()
    {
        Id = Guid.NewGuid();
        Titulo = titulo;
        status = statusItem.Pendente;
        Tarefa = tarefa;
    }

    public void Concluir()
    {
        status = statusItem.Concluido;
    }

    public enum statusItem
    {
        Pendente,
        Concluido
    }
}
