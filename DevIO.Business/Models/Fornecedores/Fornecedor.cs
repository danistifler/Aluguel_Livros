using DevIO.Business.Core.Models;
using DevIO.Business.Models.Fornecedores.Validations;
using DevIO.Business.Models.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models.Fornecedores
{
    public class Fornecedor : Entity
    {
        public string Nome { get; set; }
        public string Documento { get; set; }

        public TipoFornecedor TipoFornecedor { get; set; }

        public Endereco Endereco { get; set; }

        public bool Ativo { get; set; }

        /* Ef Relations*/

        public ICollection<Produto> Produtos { get; set; }

        public bool Validacao()
        {
            var validacao = new FornecedorValidation();
            var resultado = validacao.Validate(this);

            return resultado.IsValid;
        }

    }
}
