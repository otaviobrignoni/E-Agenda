using E_Agenda.Dominio.Compartilhado;
using E_Agenda.Dominio.ModuloCategorias;

namespace E_Agenda.Dominio.ModuloDespesas;
public class Despesa : EntidadeBase<Despesa>
{
    public string Descricao { get; set; }
    public DateOnly DataOcorrencia { get; set; }
    public decimal Valor { get; set; }
    public TipoPagamento FormaPagamento { get; set; }
    public List<Categoria> Categorias { get; set; }
    
    public Despesa()
    {
         Categorias = new();
    }

    public Despesa(string descricao, DateOnly dataOcorrencia, decimal valor, TipoPagamento formaPagamento) : this()
    {
        Id = Guid.NewGuid();
        Descricao = descricao;
        DataOcorrencia = dataOcorrencia;
        Valor = valor;
        FormaPagamento = formaPagamento;
    }

    public override void Atualizar(Despesa registroEditado)
    {
        Descricao = registroEditado.Descricao;
        DataOcorrencia = registroEditado.DataOcorrencia;
        Valor = registroEditado.Valor;
        FormaPagamento = registroEditado.FormaPagamento;
        Categorias = registroEditado.Categorias;
    }

    public void AdicionarCategoria(Categoria categoria)
    {
        Categorias.Add(categoria);
    }

    public void RemoverCategoria(Categoria categoria)
    {
        Categorias.Remove(categoria);
    }

    public enum TipoPagamento
    {
        Dinheiro,
        Credito,
        Debito
    }
}
