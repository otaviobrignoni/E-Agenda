using E_Agenda.Dominio.ModuloCategorias;
using E_Agenda.Dominio.ModuloDespesas;
using E_Agenda.WebApp.Models;

namespace E_Agenda.WebApp.Extensions;

public static class DespesaExtensions
{

    public static DetalhesDespesaViewModel ParaVM(this Despesa despesa)
    {
        return new DetalhesDespesaViewModel(despesa.Id, despesa.Descricao, despesa.DataOcorrencia, despesa.Valor, despesa.FormaPagamento, despesa.Categorias);
    }

    public static Despesa ParaEntidade(this FormularioDespesaViewModel formVM, List<Categoria> categorias)
    {
        return new Despesa(formVM.Descricao, formVM.DataOcorrencia, formVM.Valor, formVM.FormaPagamento, categorias);
    }
}
