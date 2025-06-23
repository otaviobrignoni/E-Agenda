using E_Agenda.Dominio.Compartilhado;
using static E_Agenda.Dominio.ModuloTarefa.Tarefa;

namespace E_Agenda.Dominio.ModuloTarefa;
public interface IRepositorioTarefa : IRepositorio<Tarefa>
{
    List<Tarefa> ObterTarefasPendentes();
    List<Tarefa> ObterTarefasConcluidas();
    List<Tarefa> ObterTarefasPorPrioridade(Prioridade prioridade);
}
