using E_Agenda.Dominio.ModuloContatos;
using E_Agenda.Infraestrutura.Compartilhado;
using E_Agenda.Infraestrutura.ModuloContatos;
using E_Agenda.WebApp.Extensions;
using E_Agenda.WebApp.Models.Contatos;
using Microsoft.AspNetCore.Mvc;

namespace E_Agenda.WebApp.Controllers
{
    [Route("contatos")]
    public class ContatoController : Controller
    {
        private readonly ContextoDados contextoDados;
        private readonly IRepositorioContato repositorioContato;

        public ContatoController()
        {
            contextoDados = new ContextoDados(true);
            repositorioContato = new RepositorioContato(contextoDados);
        }

        public IActionResult Index()
        {
            var registros = repositorioContato.ObterTodos();

            var visualizarVM = new VisualizarContatoViewModel(registros);

            return View(visualizarVM);
        }

        [HttpGet("cadastrar")]
        public IActionResult Cadastrar()
        {
            var cadastrarVM = new CadastrarContatoViewModel();

            return View(cadastrarVM);
        }


        [HttpPost("cadastrar")]
        [ValidateAntiForgeryToken]
        public IActionResult Cadastrar(CadastrarContatoViewModel cadastrarVM)
        {
            var registros = repositorioContato.ObterTodos();

            foreach (var item in registros)
            {
                if (item.Email.Equals(cadastrarVM.Email))
                {
                    ModelState.AddModelError("CadastroUnico", "Já existe um contato registrado com este email.");
                    break;
                }
                if (item.Telefone.Equals(cadastrarVM.Telefone))
                {
                    ModelState.AddModelError("CadastroUnico", "Já existe um contato registrado com este telefone.");
                    break;
                }
            }

            if (!ModelState.IsValid)
                return View(cadastrarVM);

            var entidade = cadastrarVM.ParaEntidade();

            repositorioContato.Cadastrar(entidade);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("editar/{id}")]
        public IActionResult Editar(Guid id)
        {
            var registros = repositorioContato.ObterPorId(id);

            var editarVM = new EditarContatoViewModel(
                id,
                registros.Nome,
                registros.Email,
                registros.Telefone,
                registros.Cargo,
                registros.Empresa
            );

            return View(editarVM);
        }

        [HttpPost("editar/{id:guid}")]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Guid id, EditarContatoViewModel editarVM)
        {
            var registros = repositorioContato.ObterTodos();

            foreach (var item in registros)
            {
                
                if (item.Id.Equals(editarVM.Email) && !item.Id.Equals(id))
                {
                    ModelState.AddModelError("CadastroUnico", "Já existe um contato registrado com este email.");
                    break;
                }

                if (item.Id.Equals(editarVM.Telefone) && !item.Id.Equals(id))
                {
                    ModelState.AddModelError("CadastroUnico", "Já existe um contato registrado com este telefone.");
                    break;
                }        
            }

            if (!ModelState.IsValid)
                return View(editarVM);

            var entidadeEditada = editarVM.ParaEntidade();

            repositorioContato.Editar(id, entidadeEditada);

            return RedirectToAction(nameof(Index));
        }


        [HttpGet("excluir/{id:guid}")]
        public IActionResult Excluir(Guid id)
        {
            var registroSelecionado = repositorioContato.ObterPorId(id);

            var excluirVM = new ExcluirContatoViewModel(registroSelecionado.Id, registroSelecionado.Nome);

            return View(excluirVM);
        }

        [HttpPost("excluir/{id:guid}")]
        [ValidateAntiForgeryToken]
        public IActionResult ExcluirConfirmado(Guid id) //Adicionar validação de exclusão do enunciado "Não permitir excluir um contato caso tenha compromissos vinculados"
        {
            repositorioContato.Excluir(id);

            return RedirectToAction(nameof(Index));
        }
    }
}