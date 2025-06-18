using E_Agenda.Dominio.ModuloCategorias;
using E_Agenda.Infraestrutura.Compartilhado;
using E_Agenda.Infraestrutura.ModuloCategorias;
using E_Agenda.WebApp.Extensions;
using E_Agenda.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace E_Agenda.WebApp.Controllers;

[Route("categoria")]
public class CategoriaController : Controller
{
    private readonly ContextoDados contexto;
    private readonly IRepositorioCategoria repositorioCategoria;

    public CategoriaController()
    {
        contexto = new(true);
        repositorioCategoria = new RepositorioCategoria(contexto);
    }
    public IActionResult Index()
    {
        var registros = repositorioCategoria.ObterTodos();

        var visualizarVM = new VisualizarCategoriaViewModel(registros);

        return View(visualizarVM);
    }

    [HttpGet("cadastrar")]
    public IActionResult Cadastrar()
    {
        var cadastrarVM = new CadastrarCategoriaViewModel();

        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    [ValidateAntiForgeryToken]
    public IActionResult Cadastrar(CadastrarCategoriaViewModel cadastrarVM)
    {
        var registros = repositorioCategoria.ObterTodos();

        foreach (var item in registros)
        {
            if (item.Titulo.Equals(cadastrarVM.Titulo))
            {
                ModelState.AddModelError("CadastroUnico", "Já existe uma categoria registrada com este título.");
                break;
            }
        }
        if (!ModelState.IsValid)
            return View(cadastrarVM);

        var entidade = cadastrarVM.ParaEntidade();

        repositorioCategoria.Cadastrar(entidade);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("editar/{id:guid}")]
    public ActionResult Editar(Guid id)
    {
        var registroSelecionado = repositorioCategoria.ObterPorId(id);

        var editarVM = new EditarCategoriaViewModel(id, registroSelecionado.Titulo);

        return View(editarVM);
    }

    [HttpPost("editar/{id:guid}")]
    [ValidateAntiForgeryToken]
    public ActionResult Editar(Guid id, EditarCategoriaViewModel editarVM)
    {
        var registros = repositorioCategoria.ObterTodos();

        foreach (var item in registros)
        {
            if (!item.Id.Equals(id) && item.Titulo.Equals(editarVM.Titulo))
            {
                ModelState.AddModelError("CadastroUnico", "Já existe uma categoria registrada com este título.");
                break;
            }
        }
        if (!ModelState.IsValid)
            return View(editarVM);

        var entidadeEditada = editarVM.ParaEntidade();

        repositorioCategoria.Editar(id, entidadeEditada);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("excluir/{id:guid}")]
    public IActionResult Excluir(Guid id)
    {
        var registroSelecionado = repositorioCategoria.ObterPorId(id);

        var excluirVM = new ExcluirCategoriaViewModel(registroSelecionado.Id, registroSelecionado.Titulo);

        return View(excluirVM);
    }

    [HttpPost("excluir/{id:guid}")]
    [ValidateAntiForgeryToken]
    public IActionResult ExcluirConfirmado(Guid id)
    {
        var registros = repositorioCategoria.ObterTodos();
        foreach (var item in registros)
        {
            if (item.Despesas.Count != 0)
            {
                ModelState.AddModelError("CadastroUnico", "Não é possível excluir um categoria com despesas vinculadas");
                break;
            }
        }
        repositorioCategoria.Excluir(id);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("detalhes/{id:guid}")]
    public IActionResult Detalhes(Guid id)
    {
        var registroSelecionado = repositorioCategoria.ObterPorId(id);

        var detalhesVM = new DetalhesCategoriaViewModel(id, registroSelecionado.Titulo, registroSelecionado.Despesas);

        return View(detalhesVM);
    }
}
