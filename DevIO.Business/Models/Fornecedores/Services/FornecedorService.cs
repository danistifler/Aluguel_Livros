using DevIO.Business.Core.Services;
using DevIO.Business.Models.Fornecedores.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models.Fornecedores.Services
{
    public class FornecedorService : BaseServices, IFornecedorService
    {

        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IEnderecoRepository _enderecorRepository;

        public FornecedorService(IFornecedorRepository fornecedorRepository, IEnderecoRepository enderecorRepository)
        {
            _fornecedorRepository = fornecedorRepository;
            _enderecorRepository = enderecorRepository;
        }

        public async Task Adicionar(Fornecedor fornecedor)
        {
            if (ExecutarValidacao(new FornecedorValidation(), fornecedor)
                || !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)) return;

            await _fornecedorRepository.Adicionar(fornecedor);
        }

        public async Task Atualizar(Fornecedor fornecedor)
        {
            if (!ExecutarValidacao(new FornecedorValidation(), fornecedor)) return;

            if (await FornecedorExistente(fornecedor)) return;


            await _fornecedorRepository.Atualizar(fornecedor);
        }


        public async Task Remove(Guid id)
        {
            var fornecedor = await _fornecedorRepository.ObterFornecedorProdutosEndereco(id);

            if (fornecedor.Produtos.Any()) return;

            if(fornecedor.Endereco != null)
            {
                await _enderecorRepository.Remover(fornecedor.Endereco.Id);
            }

            await _fornecedorRepository.Remover(id);
        }

        public async Task AtualizarEndereco(Endereco endereco)
        {
            if (!ExecutarValidacao(new EnderecoValidation(), endereco)) return;

            await _enderecorRepository.Atualizar(endereco);
        }

        private async Task<bool> FornecedorExistente(Fornecedor fornecedor)
        {
            var fornecedorAtual = await _fornecedorRepository.Buscar(f => f.Documento == fornecedor.Documento && f.Id != fornecedor.Id);
            return fornecedorAtual.Any();
        }

        public void Dispose()
        {
            _fornecedorRepository?.Dispose();
            _enderecorRepository?.Dispose();
        }
    }
}
