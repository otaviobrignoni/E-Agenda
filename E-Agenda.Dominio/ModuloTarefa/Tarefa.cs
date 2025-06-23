using E_Agenda.Dominio.Compartilhado;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace E_Agenda.Dominio.ModuloTarefa;

public class Tarefa : EntidadeBase<Tarefa>
{
    public string Titulo { get; set; }
    public Prioridade NivelPrioridade { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime DataConclusao { get; set; }
    public Status StatusTarefa { get; set; }
    public decimal PercentualConcluido { get; set; }
    public List<Item> Itens { get; set; }


    public Tarefa() 
    {
        Itens = new List<Item>();
    }

    public Tarefa(string titulo, Prioridade nivelPrioridade): this()
    {
        Id = Guid.NewGuid();
        Titulo = titulo;
        NivelPrioridade = nivelPrioridade;
        DataCriacao = DateTime.Now;
        StatusTarefa = Status.Pendente;
        PercentualConcluido = 0;
    }

    public override void Atualizar(Tarefa registroEditado)
    {
        Titulo = registroEditado.Titulo;
        NivelPrioridade = registroEditado.NivelPrioridade;
    }

    public void AdicionarItem(Item item)
    {
        Itens.Add(item);
    }

    public void RemoverItem(Item item)
    {
        Itens.Remove(item);
    }

    public void ConcluirTarefa()
    {
        StatusTarefa = Status.Concluida;
        DataConclusao = DateTime.Now;

        foreach (var item in Itens)
        {
            item.Concluir();
        }

        AtualizarPercentualConcluido();
    }

    public void AtualizarPercentualConcluido()
    {
        if (Itens.Count == 0)
        {
            PercentualConcluido = 0;
            return;
        }

        int itensConcluidos = Itens.Count(item => item.status == Item.statusItem.Concluido);

        PercentualConcluido = (decimal)itensConcluidos / Itens.Count * 100;
    }

    public enum Prioridade
    {
        Baixa,
        Normal,
        Alta
    }

    public enum Status
    {
        Pendente,
        Concluida
    }
}