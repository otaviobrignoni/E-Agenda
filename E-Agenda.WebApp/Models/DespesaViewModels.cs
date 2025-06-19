using E_Agenda.Dominio.ModuloCategorias;
using E_Agenda.Dominio.ModuloDespesas;
using E_Agenda.WebApp.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Net;
using static E_Agenda.Dominio.ModuloDespesas.Despesa;

namespace E_Agenda.WebApp.Models;

public class FormularioDespesaViewModel
{
    [Required(ErrorMessage = "O campo \"Descrição\" é obrigatório.")]
    [MinLength(2, ErrorMessage = "O campo \"Descrição\" precisa ter no mínimo 2 caracteres.")]
    [MaxLength(100, ErrorMessage = "O campo \"Descrição\" precisa ter no máximo 100 caracteres.")]
    public string Descricao { get; set; }
    public DateOnly DataOcorrencia { get; set; } = DateOnly.FromDateTime(DateTime.Now);

    [Required(ErrorMessage = "O campo \"Valor\" é obrigatório.")]
    [Range(0, double.MaxValue, ErrorMessage = "O campo \"Valor\" precisa conter um valor positivo.")]
    public decimal Valor { get; set; }
    [Required(ErrorMessage = "O campo \"Forma de Pagamento\" é obrigatório.")]
    public TipoPagamento FormaPagamento { get; set; }

    [Required(ErrorMessage = "Selecione pelo menos uma categoria.")]
    public List<Guid> CategoriasSelecionadas { get; set; }

    public List<SelectListItem> CategoriasDisponiveis { get; set; }
}

public class CadastrarDespesaViewModel : FormularioDespesaViewModel
{
    public CadastrarDespesaViewModel()
    {
        CategoriasSelecionadas = new();
        CategoriasDisponiveis = new();

    }

    public CadastrarDespesaViewModel(List<Categoria> categorias) : this()
    {
        foreach (var c in categorias)
        {
            var categoriaDisponivel = new SelectListItem(c.Titulo, c.Id.ToString());
            CategoriasDisponiveis.Add(categoriaDisponivel);
        }
    }


    public CadastrarDespesaViewModel(string descricao, DateOnly dataOcorrencia, decimal valor, TipoPagamento formaPagamento, List<Categoria> categorias) : this()
    {
        Descricao = descricao;
        DataOcorrencia = dataOcorrencia;
        Valor = valor;
        FormaPagamento = formaPagamento;

        foreach (var c in categorias)
        {
            var categoriaDisponivel = new SelectListItem(c.Titulo, c.Id.ToString());
            CategoriasDisponiveis.Add(categoriaDisponivel);
        }
    }

}

public class EditarDespesaViewModel : FormularioDespesaViewModel
{
    public Guid Id { get; set; }
    public EditarDespesaViewModel()
    {
        CategoriasSelecionadas = new();
        CategoriasDisponiveis = new();
    }

    public EditarDespesaViewModel(Guid id, string descricao, DateOnly dataOcorrencia, decimal valor, TipoPagamento formaPagamento, List<Categoria> categorias) : this()
    {
        Id = id;
        Descricao = descricao;
        DataOcorrencia = dataOcorrencia;
        Valor = valor;
        FormaPagamento = formaPagamento;

        foreach (var c in categorias)
        {
            var categoriaDisponivel = new SelectListItem(c.Titulo, c.Id.ToString());
            CategoriasDisponiveis.Add(categoriaDisponivel);
        }
    }
}

public class ExcluirDespesaViewModel
{
    public Guid Id { get; set; }
    public string Descricao { get; set; }

    public ExcluirDespesaViewModel(Guid id, string descricao)
    {
        Id = id;
        Descricao = descricao;
    }
}

public class VisualizarDespesaViewModel
{
    public List<DetalhesDespesaViewModel> Registros { get; set; }
    public VisualizarDespesaViewModel(List<Despesa> despesas)
    {
        Registros = new();
        foreach (var d in despesas)
        {
            Registros.Add(d.ParaVM());
        }
    }
}

public class DetalhesDespesaViewModel
{
    public Guid Id { get; set; }
    public string Descricao { get; set; }
    public DateOnly DataOcorrencia { get; set; }
    public decimal Valor { get; set; }
    public TipoPagamento FormaPagamento { get; set; }
    public List<Categoria> Categorias { get; set; }

    public DetalhesDespesaViewModel(Guid id, string descricao, DateOnly dataOcorrencia, decimal valor, TipoPagamento formaPagamento, List<Categoria> categorias)
    {
        Id = id;
        Descricao = descricao;
        Valor = valor;
        FormaPagamento = formaPagamento;
        Categorias = categorias;
    }
}
