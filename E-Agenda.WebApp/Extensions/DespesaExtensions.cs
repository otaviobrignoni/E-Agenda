using E_Agenda.Dominio.ModuloDespesas;
using E_Agenda.WebApp.Models;

namespace E_Agenda.WebApp.Extensions;

public static class DespesaExtensions
{
    
    public static DetalhesDespesaViewModel ParaVM(this Despesa despesa)
    {
        return new DetalhesDespesaViewModel(despesa.Id, despesa.Descricao, despesa.DataOcorrencia, despesa.Valor, despesa.FormaPagamento, despesa.Categorias);
    }
}
