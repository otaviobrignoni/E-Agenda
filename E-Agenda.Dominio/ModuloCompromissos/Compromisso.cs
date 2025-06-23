using E_Agenda.Dominio.Compartilhado;
using E_Agenda.Dominio.ModuloContatos;

namespace E_Agenda.Dominio.ModuloCompromissos;
public class Compromisso : EntidadeBase<Compromisso>
{
    public string Assunto { get; set; }
    public DateOnly DataOcorrencia { get; set; }
    public TimeOnly HoraInicio { get; set; }
    public TimeOnly HoraTermino { get; set; }
    public TipoCompromisso Tipo { get; set; }
    public string LocalOuLink { get; set; }
    public Contato? Contato { get; set; } = null;

    public Compromisso()
    {
    }
    public Compromisso(string assunto, DateOnly dataOcorrencia, TimeOnly inicio, TimeOnly termino, bool ehRemoto, string localOuLink, Contato? contato) : this()
    {
        Id = Guid.NewGuid();
        Assunto = assunto;
        DataOcorrencia = dataOcorrencia;
        HoraInicio = inicio;
        HoraTermino = termino;
        Tipo = ehRemoto ? TipoCompromisso.Remoto : TipoCompromisso.Presencial;
        LocalOuLink = localOuLink;
        Contato = contato;
    }

    public override void Atualizar(Compromisso registroEditado)
    {
        Assunto = registroEditado.Assunto;
        DataOcorrencia = registroEditado.DataOcorrencia;
        HoraInicio = registroEditado.HoraInicio;
        HoraTermino = registroEditado.HoraTermino;
        Tipo = registroEditado.Tipo;
        LocalOuLink = registroEditado.LocalOuLink;
        Contato = registroEditado.Contato;
    }
}

public enum TipoCompromisso
{
    Presencial,
    Remoto
}
