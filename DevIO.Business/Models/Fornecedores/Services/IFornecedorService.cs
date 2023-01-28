using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models.Fornecedores.Services
{
    public interface IFornecedorService : IDisposable
    {
        Task Adicionar(Fornecedor fornecedor);

        Task Atualizar(Fornecedor fornecedor);

        Task Remove(Guid id);

        Task AtualizarEndereco(Endereco endereco);
    }
}
