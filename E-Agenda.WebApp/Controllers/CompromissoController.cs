using E_Agenda.Dominio.ModuloCompromissos;
using E_Agenda.Dominio.ModuloContatos;
using E_Agenda.Infraestrutura.Compartilhado;
using E_Agenda.Infraestrutura.ModuloCompromissos;
using E_Agenda.Infraestrutura.ModuloContatos;
using E_Agenda.WebApp.Extensions;
using E_Agenda.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Agenda.WebApp.Controllers;

[Route("compromisso")]
public class CompromissoController : Controller
{
    private readonly ContextoDados contexto;
    private readonly IRepositorioCompromisso repositorioCompromisso;
    private readonly IRepositorioContato repositorioContato;

    public CompromissoController()
    {
        contexto = new(true);
        repositorioCompromisso = new RepositorioCompromisso(contexto);
        repositorioContato = new RepositorioContato(contexto);
    }

    [HttpGet]
    public IActionResult Index()
    {
        var registros = repositorioCompromisso.ObterTodos();

        var visualizarVM = new VisualizarCompromissoViewModel(registros);

        return View(visualizarVM);
    }

    [HttpGet("cadastrar")]
    public IActionResult Cadastrar()
    {
        var contatos = repositorioContato.ObterTodos();
        var cadastrarVM = new CadastrarCompromissoViewModel(contatos);

        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    [ValidateAntiForgeryToken]
    public IActionResult Cadastrar(CadastrarCompromissoViewModel cadastrarVM)
    {
        var registros = repositorioCompromisso.ObterTodos();

        foreach (var item in registros)
        {
            if (registros.FirstOrDefault(c => c.DataOcorrencia == cadastrarVM.DataOcorrencia && c.HoraInicio < cadastrarVM.HoraTermino && c.HoraTermino > cadastrarVM.HoraInicio) != null)
            {
                ModelState.AddModelError("CadastroUnico", "Já existe um compromisso no horário selecionado");
                break;
            }
        }

        if (!ModelState.IsValid)
            return View(cadastrarVM);

        var contatos = repositorioContato.ObterTodos();

        var entidade = cadastrarVM.ParaEntidade(contatos);

        repositorioCompromisso.Cadastrar(entidade);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("editar/{id:guid}")]
    public IActionResult Editar(Guid id)
    {
        var registroSelecionado = repositorioCompromisso.ObterPorId(id);

        var contatos = repositorioContato.ObterTodos();

        string localOuLink;

        if (registroSelecionado.Tipo == TipoCompromisso.Remoto) localOuLink = registroSelecionado.Link;
        else localOuLink = registroSelecionado.Local;

        var editarVM = new EditarCompromissoViewModel(registroSelecionado.Id, registroSelecionado.Assunto, registroSelecionado.DataOcorrencia, registroSelecionado.HoraInicio, registroSelecionado.HoraTermino, registroSelecionado.Tipo, localOuLink, registroSelecionado.Id, contatos);
        return View(editarVM);
    }

    [HttpPost("editar/{id:guid}")]
    [ValidateAntiForgeryToken]
    public IActionResult Editar(Guid id, EditarCompromissoViewModel editarVM)
    {
        var registros = repositorioCompromisso.ObterTodos();
        var contatos = repositorioContato.ObterTodos();

        foreach (var item in registros)
        {
            if (registros.FirstOrDefault(c => c.DataOcorrencia == editarVM.DataOcorrencia && c.HoraInicio < editarVM.HoraTermino && c.HoraTermino > editarVM.HoraInicio && !item.Id.Equals(id)) != null)
            {
                ModelState.AddModelError("CadastroUnico", "Já existe um compromisso no horário selecionado");
                break;
            }
        }

        if (!ModelState.IsValid)
            return View(editarVM);

        var entidadeEditada = editarVM.ParaEntidade(contatos);

        repositorioCompromisso.Editar(id, entidadeEditada);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("excluir/{id:guid}")]
    public IActionResult Excluir(Guid id)
    {
        var registroSelecionado = repositorioCompromisso.ObterPorId(id);

        var excluirVM = new ExcluirCompromissoViewModel(registroSelecionado.Id, registroSelecionado.Assunto);

        return View(excluirVM);
    }

    [HttpPost("excluir/{id:guid}")]
    public IActionResult ExcluirConfirmado(Guid id)
    {
        repositorioCompromisso.Excluir(id);

        return RedirectToAction(nameof(Index));
    }

}
