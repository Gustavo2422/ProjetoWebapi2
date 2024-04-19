using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebapiPessoa.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
       public PessoaController(ILogger<PessoaController> logger)
        {

        }

        [HttpPost]
        public PessoaResponse ProcessarInformacoesPessoa([FromBody] PessoaRequest request) 
        {
            var anoAtual = DateTime.Now.Year;
            var idadePessoa = anoAtual - request.DataNascimento.Year;

            var imc = Math.Round (request.Peso / (request.Altura * request.Altura), 2);
            var classificacao = "";

            if (imc < (decimal) 18.5)
            {
                classificacao = "Abaixo do peso ideal";
            }

            else if (imc >= (decimal) 18.5 && imc <= (decimal) 24.99)
            {
                classificacao = "Peso ideal";

            }
            
            else if (imc >= (decimal) 24.99 && imc <= (decimal) 29.99)
            {
                classificacao = "Pré obesidade";
            }

            else if (imc >= (decimal) 30.00 && imc <= (decimal) 34.99)
            {
                classificacao = "Obesidade Grau 1";
            }

            else if (imc >= (decimal) 35.00 && imc <= (decimal) 39.99)
            {
                classificacao = "Obesidade Grau 2";
            }

            else  
            {
                classificacao = "Obesidade Grau 3";
            }

            var aliquota = 7.5;
            if (request.Salario <=1212)
            {
                aliquota = 7.5;
            }
            else if (request.Salario >= 1212 && request.Salario <= 2427.35)
            {
                aliquota = 9;
            }
            else if (request.Salario >= 2427.36 && request.Salario <= 3641.03)
            {
                aliquota = 12;
            }
            else
            {
                aliquota = 14;
            }

            var inss = (request.Salario * aliquota) / 100;
            var salarioLiquido = request.Salario - inss;

            var dolar = (decimal)5.14;
            var saldoDolar = Math.Round (request.Saldo / dolar, 2);

            var resposta = new PessoaResponse();
            resposta.SaldoDolar = saldoDolar;
            resposta.Aliquota = aliquota;
            resposta.SalarioLiquido = salarioLiquido;
            resposta.Classificacao = classificacao;
            resposta.Idade = idadePessoa;
            resposta.Imc = imc;
            resposta.Inss = inss;
            resposta.Nome = request.Nome;
            return resposta;
        }

    }
}
