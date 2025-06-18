using E_Agenda.Dominio.ModuloCompromissos;
using E_Agenda.Dominio.ModuloContatos;
using E_Agenda.WebApp.Models;

namespace E_Agenda.WebApp.Extensions;

public static class CompromissoExtensions
{
    public static DetalhesCompromissoViewModel ParaVM(this Compromisso c)
    {
        string localOuLink;

        if (c.Tipo == TipoCompromisso.Remoto) localOuLink = c.Link!;
        else localOuLink = c.Local!;

        return new DetalhesCompromissoViewModel(c.Id, c.Assunto, c.DataOcorrencia, c.HoraInicio, c.HoraTermino, c.Tipo == TipoCompromisso.Remoto, localOuLink, c.Contato);
    }

    public static Compromisso ParaEntidade(this FormularioComprimissoViewModel formVm, List<Contato> contatos)
    {
        // Procura pelo primeiro registro na lista que satisfaz a condição. Se não encontrar nenhum, retorna null.
        Contato? contatoSelecionado = contatos.FirstOrDefault(c => c.Id == formVm.ContatoId); 

        return new Compromisso(formVm.Assunto, formVm.DataOcorrencia, formVm.HoraInicio, formVm.HoraTermino, formVm.Tipo == TipoCompromisso.Remoto, formVm.LocalOuLink, contatoSelecionado);
    }
}
