using E_Agenda.Dominio.ModuloCategorias;
using E_Agenda.Dominio.ModuloDespesas;
using E_Agenda.Infraestrutura.Compartilhado;
using E_Agenda.Infraestrutura.ModuloCategorias;
using E_Agenda.Infraestrutura.ModuloDespesas;
using E_Agenda.WebApp.Extensions;
using E_Agenda.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Agenda.WebApp.Controllers;

[Route("despesas")]
public class DespesaController : Controller
{
    private readonly ContextoDados contexto;
    private readonly IRepositorioDespesa repositorioDespesa;
    private readonly IRepositorioCategoria repositorioCategoria;

    public DespesaController()
    {
        contexto = new(true);
        repositorioDespesa = new RepositorioDespesa(contexto);
        repositorioCategoria = new RepositorioCategoria(contexto);
    }

    public IActionResult Index()
    {
        ViewBag.Title = "Despesas";
        ViewBag.Header = "Visualizando Despesa";

        var registros = repositorioDespesa.ObterTodos();

        var visualizarVM = new VisualizarDespesaViewModel(registros);

        return View(visualizarVM);
    }

    [HttpGet("cadastrar")]
    public IActionResult Cadastrar()
    {
        ViewBag.Title = "Despesas | Cadastrar";
        ViewBag.Header = "Cadastro de Despesa";

        var categorias = repositorioCategoria.ObterTodos();

        var cadastrarVM = new CadastrarDespesaViewModel(categorias);

        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    [ValidateAntiForgeryToken]
    public IActionResult Cadastrar(CadastrarDespesaViewModel cadastrarVM)
    {
        ViewBag.Title = "Despesas | Cadastrar";
        ViewBag.Header = "Cadastro de Despesa";

        var registros = repositorioDespesa.ObterTodos();

        foreach (var item in registros)
        {
            if (item.Categorias.Count == 0)
            {
                ModelState.AddModelError("CadastroUnico", "Selecione pelo menos uma categoria");
                break;
            }
        }

        if (!ModelState.IsValid)
        {
            var categorias = repositorioCategoria.ObterTodos();
            cadastrarVM.CategoriasDisponiveis = categorias.Select(c => new SelectListItem(c.Titulo, c.Id.ToString())).ToList();
            return View(cadastrarVM);
        }

        var categoriasSelecionadas = repositorioCategoria.ObterTodos();

        categoriasSelecionadas = categoriasSelecionadas.GroupBy(c => c.Id).Select(g => g.First()).ToList();

        var entidade = cadastrarVM.ParaEntidade(categoriasSelecionadas);

        repositorioDespesa.Cadastrar(entidade);

        return RedirectToAction(nameof(Index));

    }

    [HttpGet("editar/{id:guid}")]
    public IActionResult Editar(Guid id)
    {
        ViewBag.Title = "Despesas | Editar";
        ViewBag.Header = "Edição de Despesa";

        var registroSelecionado = repositorioDespesa.ObterPorId(id);

        var editarVM = new EditarDespesaViewModel(id, registroSelecionado.Descricao, registroSelecionado.DataOcorrencia, registroSelecionado.Valor, registroSelecionado.FormaPagamento, registroSelecionado.Categorias);
        
        return View(editarVM);
    }

    [HttpPost("editar/{id:guid}")]
    [ValidateAntiForgeryToken]
    public IActionResult Editar(Guid id, EditarDespesaViewModel editarVM)
    {
        ViewBag.Title = "Despesas | Editar";
        ViewBag.Header = "Edição de Despesa";

        var registros = repositorioDespesa.ObterTodos();

        foreach (var item in registros)
        {
            if (item.Categorias.Count <= 0)
            {
                ModelState.AddModelError("CadastroUnico", "Selecione pelo menos uma categoria");
                break;
            }
        }

        if (!ModelState.IsValid)
        {
            var categorias = repositorioCategoria.ObterTodos();
            editarVM.CategoriasDisponiveis = categorias.Select(c => new SelectListItem(c.Titulo, c.Id.ToString())).ToList();
            return View(editarVM);
        }

        var categoriasSelecionadas = repositorioCategoria.ObterTodos();

        categoriasSelecionadas = categoriasSelecionadas.GroupBy(c => c.Id).Select(g => g.First()).ToList();

        var entidade = editarVM.ParaEntidade(categoriasSelecionadas);

        repositorioDespesa.Cadastrar(entidade);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("excluir/{id:guid}")]
    public IActionResult Excluir(Guid id)
    {
        ViewBag.Title = "Despesas | Excluir";
        ViewBag.Header = "Exclusão de Despesa";

        var registroSelecionado = repositorioDespesa.ObterPorId(id);

        var excluirVM = new ExcluirDespesaViewModel(registroSelecionado.Id, registroSelecionado.Descricao);

        return View(excluirVM);
    }

    [HttpPost("excluir/{id:guid}")]
    [ValidateAntiForgeryToken]
    public IActionResult ExcluirConfirmado(Guid id)
    {
        ViewBag.Title = "Despesas | Excluir";
        ViewBag.Header = "Exclusão de Despesa";

        repositorioDespesa.Excluir(id);

        return RedirectToAction(nameof(Index));
    }
}
