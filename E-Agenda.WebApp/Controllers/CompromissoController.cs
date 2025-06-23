﻿using E_Agenda.Dominio.ModuloCompromissos;
using E_Agenda.Dominio.ModuloContatos;
using E_Agenda.Infraestrutura.Compartilhado;
using E_Agenda.Infraestrutura.ModuloCompromissos;
using E_Agenda.Infraestrutura.ModuloContatos;
using E_Agenda.WebApp.Extensions;
using E_Agenda.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Agenda.WebApp.Controllers;

[Route("compromissos")]
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
        ViewBag.Title = "Compromissos";
        ViewBag.Header = "Visualizando Compromissos";

        var registros = repositorioCompromisso.ObterTodos();

        var visualizarVM = new VisualizarCompromissoViewModel(registros);

        return View(visualizarVM);
    }

    [HttpGet("cadastrar")]
    public IActionResult Cadastrar()
    {
        ViewBag.Title = "Compromissos | Cadastrar";
        ViewBag.Header = "Cadastro de Compromisso";

        var contatos = repositorioContato.ObterTodos();
        var cadastrarVM = new CadastrarCompromissoViewModel(contatos);

        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    [ValidateAntiForgeryToken]
    public IActionResult Cadastrar(CadastrarCompromissoViewModel cadastrarVM)
    {
        ViewBag.Title = "Compromissos | Cadastrar";
        ViewBag.Header = "Cadastro de Compromisso";

        var registros = repositorioCompromisso.ObterTodos();
        var contatos = repositorioContato.ObterTodos();

        foreach (var item in registros)
        {
            if (registros.FirstOrDefault(c => c.DataOcorrencia == cadastrarVM.DataOcorrencia && c.HoraInicio < cadastrarVM.HoraTermino && c.HoraTermino > cadastrarVM.HoraInicio) != null)
            {
                ModelState.AddModelError("CadastroUnico", "Já existe um compromisso no horário selecionado");
                break;
            }
        }

        if (!ModelState.IsValid)
        {
            cadastrarVM.ContatosDisponiveis.Clear();
            foreach (var c in contatos)
                cadastrarVM.ContatosDisponiveis.Add(new SelecionarContatoViewModel(c.Id, c.Nome));
            return View(cadastrarVM);
        }

        var entidade = cadastrarVM.ParaEntidade(contatos);

        repositorioCompromisso.Cadastrar(entidade);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("editar/{id:guid}")]
    public IActionResult Editar(Guid id)
    {
        ViewBag.Title = "Compromissos | Editar";
        ViewBag.Header = "Edição de Compromisso";

        var registroSelecionado = repositorioCompromisso.ObterPorId(id);

        var contatos = repositorioContato.ObterTodos();

        string localOuLink = registroSelecionado.LocalOuLink;

        var editarVM = new EditarCompromissoViewModel(
            registroSelecionado.Id,
            registroSelecionado.Assunto,
            registroSelecionado.DataOcorrencia,
            registroSelecionado.HoraInicio,
            registroSelecionado.HoraTermino,
            registroSelecionado.Tipo,
            localOuLink,
            registroSelecionado.Contato?.Id,
            contatos);
        return View(editarVM);
    }

    [HttpPost("editar/{id:guid}")]
    [ValidateAntiForgeryToken]
    public IActionResult Editar(Guid id, EditarCompromissoViewModel editarVM)
    {
        ViewBag.Title = "Compromissos | Editar";
        ViewBag.Header = "Edição de Compromisso";

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
        {
            editarVM.ContatosDisponiveis.Clear();
            foreach (var c in contatos)
                editarVM.ContatosDisponiveis.Add(new SelecionarContatoViewModel(c.Id, c.Nome));
            return View(editarVM);
        }

        var entidadeEditada = editarVM.ParaEntidade(contatos);

        repositorioCompromisso.Editar(id, entidadeEditada);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("excluir/{id:guid}")]
    public IActionResult Excluir(Guid id)
    {
        ViewBag.Title = "Compromissos | Excluir";
        ViewBag.Header = "Exclusão de Compromisso";

        var registroSelecionado = repositorioCompromisso.ObterPorId(id);

        var excluirVM = new ExcluirCompromissoViewModel(registroSelecionado.Id, registroSelecionado.Assunto);

        return View(excluirVM);
    }

    [HttpPost("excluir/{id:guid}")]
    [ValidateAntiForgeryToken]
    public IActionResult ExcluirConfirmado(Guid id)
    {
        ViewBag.Title = "Compromissos | Excluir";
        ViewBag.Header = "Exclusão de Compromisso";

        repositorioCompromisso.Excluir(id);

        return RedirectToAction(nameof(Index));
    }
}
