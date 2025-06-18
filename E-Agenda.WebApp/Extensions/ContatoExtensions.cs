using E_Agenda.Dominio.ModuloContatos;
using E_Agenda.WebApp.Models.Contatos;

namespace E_Agenda.WebApp.Extensions
{
    public static class ContatoExtensions
    {
        public static Contato ParaEntidade(this FormularioContatoViewModel formularioVM)
        {
            return new Contato(formularioVM.Nome, formularioVM.Email, formularioVM.Telefone, formularioVM.Cargo, formularioVM.Empresa);
        }

        public static DetalhesContatoViewModel ParaDetalhesVM(this Contato contato)
        {
            return new DetalhesContatoViewModel(
                contato.Id,
                contato.Nome,
                contato.Email,
                contato.Telefone,
                contato.Cargo,
                contato.Empresa
            );
        }
    }
}



