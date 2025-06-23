using System.ComponentModel.DataAnnotations;
using E_Agenda.Dominio.ModuloTarefa;
using E_Agenda.WebApp.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using static E_Agenda.Dominio.ModuloTarefa.Item;
using static E_Agenda.Dominio.ModuloTarefa.Tarefa;

namespace E_Agenda.WebApp.Models;

public class FormTarefaViewModel
{
    [Required(ErrorMessage = "O campo \"Titulo\" é obrigatório.")]
    [MinLength(2, ErrorMessage = "O campo \"Titulo\" precisa conter ao menos 2 caracteres.")]
    [MaxLength(100, ErrorMessage = "O campo \"Titulo\" precisa conter no máximo 100 caracteres.")]
    public string Titulo { get; set; }

    [Required(ErrorMessage = "O campo \"Prioridade\" é obrigatório.")]
    public Prioridade Prioridade { get; set; }
}

public class CriarTarefaViewModel : FormTarefaViewModel
{
    public CriarTarefaViewModel() { }

    public CriarTarefaViewModel(string titulo, Prioridade prioridade) : this()
    {
        Titulo = titulo;
        Prioridade = prioridade;
    }
}

public class EditarTarefaViewModel : FormTarefaViewModel
{
    public Guid Id { get; set; }
    public EditarTarefaViewModel() { }
    public EditarTarefaViewModel(Guid id, string titulo, Prioridade prioridade) : this()
    {
        Id = id;
        Titulo = titulo;
        Prioridade = prioridade;
    }
}

public class ExcluirTarefaViewModel
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public ExcluirTarefaViewModel() { }
    public ExcluirTarefaViewModel(Guid id, string titulo) : this()
    {
        Id = id;
        Titulo = titulo;
    }
}

public class VisualizarTarefaViewModel
{
    public List<DetalhesTarefaViewModel> Tarefas { get; set; }

    public VisualizarTarefaViewModel() 
    {
        Tarefas = new List<DetalhesTarefaViewModel>();
    }
    public VisualizarTarefaViewModel(List<Tarefa> tarefas) : this()
    {
        foreach (Tarefa t in tarefas)
        {
            DetalhesTarefaViewModel detalheVM = t.DetalhesVM();

            Tarefas.Add(detalheVM);
        }
    }
}

public class DetalhesTarefaViewModel
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public Prioridade NivelPrioridade { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataConclusao { get; set; }
    public Status StatusTarefa { get; set; }
    public decimal PercentualConcluido { get; set; }
    public List<ItemViewModel> Itens { get; set; }

    public DetalhesTarefaViewModel()
    {
        Itens = new List<ItemViewModel>();
    }

    public DetalhesTarefaViewModel(Guid id, string titulo, Prioridade nivelPrioridade, DateTime dataCriacao, DateTime? dataConclusao, Status statusTarefa, decimal percentualConcluido, List<Item> itens) : this()
    {
        Id = id;
        Titulo = titulo;
        NivelPrioridade = nivelPrioridade;
        DataCriacao = dataCriacao;
        DataConclusao = dataConclusao;
        StatusTarefa = statusTarefa;
        PercentualConcluido = percentualConcluido;
        
        foreach (var i in itens)
        {
            Itens.Add(new ItemViewModel(i.Id, i.Titulo, i.status));
        }
    }
}

public class GerenciarTarefaViewModel
{
    DetalhesTarefaViewModel Tarefa { get; set; }
    public List<SelectListItem> Itens { get; set; }

    [Required(ErrorMessage = "O campo \"Titulo\" é obrigatório.")]
    public string Titulo { get; set; }

    public GerenciarTarefaViewModel() { }
    public GerenciarTarefaViewModel(Tarefa tarefa, List<Item> itens) : this()
    {
        Tarefa = tarefa.DetalhesVM();

        foreach (var item in itens)
        {
            Itens.Add(new SelectListItem(item.Titulo, item.Id.ToString()));
        }
    }
}

public class ItemViewModel
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public statusItem Status { get; set; }
    public ItemViewModel() { }
    public ItemViewModel(Guid id, string titulo, statusItem status)
    {
        Id = id;
        Titulo = titulo;
        Status = status;
    }
}
