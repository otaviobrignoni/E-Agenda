using E_Agenda.Dominio.Compartilhado;

namespace E_Agenda.Dominio.ModuloContatos
{
    public class Contato : EntidadeBase<Contato>
    {

        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string? Cargo { get; set; } = null;
        public string? Empresa { get; set; } = null;

        public Contato() 
        {
        }

        public Contato(string nome, string email, string telefone) : this()
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Email = email;
            Telefone = telefone;
        }   

        public Contato(string nome, string email, string telefone, string? cargo, string? empresa) : this()
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Email = email;
            Telefone = telefone;
            Cargo = cargo;
            Empresa = empresa;
        }

        public override void Atualizar(Contato registroEditado)
        {
            Nome = registroEditado.Nome;
            Email = registroEditado.Email;
            Telefone = registroEditado.Telefone;
            Cargo = registroEditado.Cargo;
            Empresa = registroEditado.Empresa;
        }
    }
       
}