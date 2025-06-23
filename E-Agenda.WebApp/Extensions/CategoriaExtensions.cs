using E_Agenda.Dominio.ModuloCategorias;
using E_Agenda.WebApp.Models;

namespace E_Agenda.WebApp.Extensions;

public static class CategoriaExtensions
{
    public static DetalhesCategoriaViewModel ParaVM(this Categoria categoria)
    {
        return new DetalhesCategoriaViewModel(categoria.Id, categoria.Titulo, categoria.Despesas);
    }

    public static Categoria ParaEntidade(this FormularioCategoriaViewModel formVM)
    {
        return new Categoria(formVM.Titulo);
    }
}
