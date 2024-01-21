using Domain.Interfaces.InterfaceProduct;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ServiceProduct : IServiceProduct
    {

        private readonly IProduct _IProduct;

        public ServiceProduct(IProduct IProduct)
        {
            _IProduct = IProduct;
        }
        public async Task AddProduct(Produto produto)
        {
            var validaNome = produto.ValidarPropiedadeString(produto.Nome, "Nome");

            var validaValor = produto.ValidarPropiedadeDecimal(produto.Valor, "Valor");

            var validaQtdEstoque = produto.ValidarPropiedadeInt(produto.QtdEstoque, "QtdEstoque");

            if (validaNome && validaValor && validaQtdEstoque)
            {
                produto.DataCadastro = DateTime.Now;
                produto.DataAlteracao = DateTime.Now;

                produto.Estado = true;
                await _IProduct.Add(produto);
            }
        }

        public async Task UpdateProduct(Produto produto)
        {
            var validaNome = produto.ValidarPropiedadeString(produto.Nome, "Nome");

            var validaValor = produto.ValidarPropiedadeDecimal(produto.Valor, "Valor");

            var validaQtdEstoque = produto.ValidarPropiedadeInt(produto.QtdEstoque, "QtdEstoque");

            if (validaNome && validaValor && validaQtdEstoque)
            {
                produto.DataAlteracao = DateTime.Now;
               
                await _IProduct.Update(produto);
            }
        }
    }
}
