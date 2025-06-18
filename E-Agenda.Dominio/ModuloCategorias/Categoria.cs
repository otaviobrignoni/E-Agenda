using E_Agenda.Dominio.Compartilhado;
using E_Agenda.Dominio.ModuloDespesas;

namespace E_Agenda.Dominio.ModuloCategorias;
public class Categoria : EntidadeBase<Categoria>
{
    public string Titulo { get; set; }
    public List<Despesa> Despesas { get; set; }

    public Categoria() { }

    public Categoria(string titulo)
    {
        Id = Guid.NewGuid();
        Titulo = titulo;
    }

    public override void Atualizar(Categoria registroEditado)
    {
        Titulo = registroEditado.Titulo;
    }

    public void AdicionarDespesa(Despesa despesa)
    {
        Despesas.Add(despesa);
    }

    public void RemoverDespesa(Despesa despesa)
    {
        Despesas.Remove(despesa);
    }
}
