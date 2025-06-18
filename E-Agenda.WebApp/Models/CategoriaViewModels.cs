using E_Agenda.Dominio.ModuloCategorias;
using E_Agenda.Dominio.ModuloDespesas;
using E_Agenda.WebApp.Extensions;
using System.ComponentModel.DataAnnotations;

namespace E_Agenda.WebApp.Models;

public class FormularioCategoriaViewModel
{
    [Required(ErrorMessage = "O campo \"Título\" é obrigatório.")]
    [MinLength(2, ErrorMessage = "O campo \"Título\" precisa ter no mínimo 2 caracteres.")]
    [MaxLength(100, ErrorMessage = "O campo \"Título\" precisa ter no máximo 100 caracteres.")]
    public string Titulo { get; set; }
}

public class CadastrarCategoriaViewModel : FormularioCategoriaViewModel
{
    public CadastrarCategoriaViewModel() { }

    public CadastrarCategoriaViewModel(string titulo)
    {
        Titulo = titulo;
    }
}

public class EditarCategoriaViewModel : FormularioCategoriaViewModel
{
    public Guid Id;
    public EditarCategoriaViewModel() { }

    public EditarCategoriaViewModel(Guid id, string titulo)
    {
        Id = id;
        Titulo = titulo;
    }
}

public class ExcluirCategoriaViewModel
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }

    public ExcluirCategoriaViewModel(Guid id, string titulo)
    {
        Id = id;
        Titulo = titulo;
    }
} 

public class VisualizarCategoriaViewModel
{
    public List<DetalhesCategoriaViewModel> Registros { get; set; }

    public VisualizarCategoriaViewModel(List<Categoria> categorias)
    {
        Registros = new();
        foreach(Categoria c in categorias)
        {
            Registros.Add(c.ParaVM());
        } 
    }
}

public class DetalhesCategoriaViewModel
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public List<Despesa> Despesas { get; set; }
    public DetalhesCategoriaViewModel(Guid id, string titulo, List<Despesa> despesas)
    {
        Id = id;
        Titulo = titulo;
        Despesas = despesas;
    }
}

public class SelecionarCategoriaViewModel
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }

    public SelecionarCategoriaViewModel(Guid id, string titulo)
    {
        Id = id;
        Titulo = titulo;
    }
}