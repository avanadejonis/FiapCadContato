using Fiap.Domain.Entities;

namespace FiapCadContato.Validators
{
    public class ContatoInput : Contato
    {
        public bool ValidarId(int id)
        {
            if (id.ToString().Length == 0) return false; return true;
        }

        public bool ValidarDDD(int ddd)
        {
            if (ddd.ToString().Length == 2) return true; return false;
        }

    }
}
