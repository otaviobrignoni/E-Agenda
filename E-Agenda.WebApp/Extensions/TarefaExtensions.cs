using E_Agenda.Dominio.ModuloTarefa;
using E_Agenda.WebApp.Models;

namespace E_Agenda.WebApp.Extensions;

public static class TarefaExtensions
{
    public static Tarefa ParaEntidade(this FormTarefaViewModel formVM) 
    {
        return new Tarefa(formVM.Titulo, formVM.Prioridade);
    }

    public static DetalhesTarefaViewModel DetalhesVM(this Tarefa tarefa) 
    {
        return new DetalhesTarefaViewModel(
            tarefa.Id,
            tarefa.Titulo,
            tarefa.NivelPrioridade,
            tarefa.DataCriacao,
            tarefa.DataConclusao,
            tarefa.StatusTarefa,
            tarefa.PercentualConcluido,
            tarefa.Itens
        );
    }
}
