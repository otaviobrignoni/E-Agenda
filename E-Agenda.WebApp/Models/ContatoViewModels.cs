using System.ComponentModel.DataAnnotations;
using E_Agenda.Dominio.ModuloContatos;
using E_Agenda.WebApp.Extensions;

namespace E_Agenda.WebApp.Models;

public class FormularioContatoViewModel
{

    [Required(ErrorMessage = "O campo \"Nome\" é obrigatório.")]
    [MinLength(3, ErrorMessage = "O campo \"Nome\" precisa conter ao menos 3 caracteres.")]
    [MaxLength(100, ErrorMessage = "O campo \"Nome\" precisa conter no máximo 100 caracteres.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Telefone é obrigatório")]
    [RegularExpression(@"^\(\d{2}\)\s\d{4,5}-\d{4}$", 
    ErrorMessage = "Telefone deve seguir o formato (XX) XXXX-XXXX ou (XX) XXXXX-XXXX")]
    public string Telefone { get; set; }
    public string? Cargo { get; set; }
    public string? Empresa { get; set; }
}

public class CadastrarContatoViewModel : FormularioContatoViewModel
{
    public CadastrarContatoViewModel() { }  
    public CadastrarContatoViewModel(string nome, string email, string telefone, string cargo, string empresa)
    {
        Nome = nome;
        Email = email;
        Telefone = telefone;
        Cargo = cargo;
        Empresa = empresa;

    }
}
public class EditarContatoViewModel : FormularioContatoViewModel
{
    public Guid Id { get; set; }
    public EditarContatoViewModel() { }
    public EditarContatoViewModel(Guid id, string nome, string email, string telefone, string? cargo, string? empresa)
    {
        Id = id;
        Nome = nome;
        Email = email;
        Telefone = telefone;
        Cargo = cargo;
        Empresa = empresa;
    }

}
public class ExcluirContatoViewModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public ExcluirContatoViewModel(Guid id, string nome)
    {
        Id = id;
        Nome = nome;
    }
}
public class VisualizarContatoViewModel 
{
    public List<DetalhesContatoViewModel> Registros { get; set; }
    public VisualizarContatoViewModel(List<Contato> contatos)
    {
        Registros = new List<DetalhesContatoViewModel>();
        foreach (var c in contatos)
        {
            Registros.Add(c.ParaDetalhesVM());
        }
    }

}
public class DetalhesContatoViewModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public string? Cargo { get; set; }
    public string? Empresa { get; set; }
    public DetalhesContatoViewModel(Guid id, string nome, string email, string telefone, string? cargo, string? empresa)
    {
        Id = id;
        Nome = nome;
        Email = email;
        Telefone = telefone;
        Cargo = cargo;
        Empresa = empresa;
    }
}
public class SelecionarContatoViewModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public SelecionarContatoViewModel(Guid id, string nome)
    {
        Id = id;
        Nome = nome;
    }

}










