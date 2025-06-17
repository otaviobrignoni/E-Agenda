using E_Agenda.Dominio.ModuloCompromissos;
using E_Agenda.Dominio.ModuloContatos;
using E_Agenda.WebApp.Extensions;
using System.ComponentModel.DataAnnotations;

namespace E_Agenda.WebApp.Models;

public class FormularioComprimissoViewModel
{
    [Required(ErrorMessage = "O campo \"Assunto\" é obrigatório.")]
    [MinLength(2, ErrorMessage = "O campo \"Assunto\" precisa ter no mínimo 2 caracteres.")]
    [MaxLength(100, ErrorMessage = "O campo \"Assunto\" precisa ter no máximo 100 caracteres.")]
    public string Assunto { get; set; }

    [Required(ErrorMessage = "O campo \"Data de Ocorrência\" é obrigatório.")]
    public DateOnly DataOcorrencia { get; set; }

    [Required(ErrorMessage = "O campo \"Hora de Início\" é obrigatório.")]
    public TimeOnly HoraInicio { get; set; }

    [Required(ErrorMessage = "O campo \"Hora de Término\" é obrigatório.")]
    public TimeOnly HoraTermino { get; set; }

    [Required(ErrorMessage = "O campo \"Tipo do Compromisso\" é obrigatório.")]
    public TipoCompromisso Tipo { get; set; }

    public string? Local { get; set; } = null;

    public string? Link { get; set; } = null;

    public Guid? ContatoId { get; set; } = null;
}

public class CadastarCompromissoViewModel : FormularioComprimissoViewModel
{
    public CadastarCompromissoViewModel() { }
    public CadastarCompromissoViewModel(string assunto, DateOnly dataOcorrencia, TimeOnly inicio, TimeOnly termino, bool ehRemoto, string localOuLink, Contato? contato) : this()
    {
        Assunto = assunto;
        DataOcorrencia = dataOcorrencia;
        HoraInicio = inicio;
        HoraTermino = termino;
        Tipo = ehRemoto ? TipoCompromisso.Remoto : TipoCompromisso.Presencial;
        if (ehRemoto) Local = localOuLink;
        else Link = localOuLink;
        ContatoId = contato.Id;
    }
}

public class EditarCompromissoViewModel : FormularioComprimissoViewModel
{
    public Guid Id { get; set; }
    public EditarCompromissoViewModel() { }

    public EditarCompromissoViewModel(Guid id, string assunto, DateOnly dataOcorrencia, TimeOnly inicio, TimeOnly termino, bool ehRemoto, string localOuLink, Contato contato) : this()
    {
        Id = id;
        Assunto = assunto;
        DataOcorrencia = dataOcorrencia;
        HoraInicio = inicio;
        HoraTermino = termino;
        Tipo = ehRemoto ? TipoCompromisso.Remoto : TipoCompromisso.Presencial;
        if (ehRemoto) Local = localOuLink;
        else Link = localOuLink;
        ContatoId = contato.Id;
    }
}
public class ExcluirCompromissoViewModel
{
    public Guid Id { get; set; }
    public string Assunto { get; set; }
    public ExcluirCompromissoViewModel(Guid id, string assunto)
    {
        Id = id;
        Assunto = assunto;
    }
}

public class VisualizarCompromissoViewModel
{
    public List<DetalhesCompromissoViewModel> Registros { get; set; }

    public VisualizarCompromissoViewModel(List<Compromisso> registros)
    {
        Registros = new();

        foreach (var c in registros)
            Registros.Add(c.ParaVM());
    }
}

public class DetalhesCompromissoViewModel
{
    public Guid Id { get; set; }
    public string Assunto { get; set; }
    public DateOnly DataOcorrencia { get; set; }
    public TimeOnly HoraInicio { get; set; }
    public TimeOnly HoraTermino { get; set; }
    public TipoCompromisso Tipo { get; set; }
    public string? Local { get; set; } = null;
    public string? Link { get; set; } = null;
    public string? Contato { get; set; } = null;

    public DetalhesCompromissoViewModel(Guid id, string assunto, DateOnly dataOcorrencia, TimeOnly inicio, TimeOnly termino, bool ehRemoto, string localOuLink, Contato? contato)
    {
        Id = id;
        Assunto = assunto;
        DataOcorrencia = dataOcorrencia;
        HoraInicio = inicio;
        HoraTermino = termino;
        Tipo = ehRemoto ? TipoCompromisso.Remoto : TipoCompromisso.Presencial;
        if (ehRemoto) Local = localOuLink;
        else Link = localOuLink;
        Contato = contato.Nome;
    }

}