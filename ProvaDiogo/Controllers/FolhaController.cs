using ProvaDiogo.Data;
using ProvaDiogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers;

[ApiController]
[Route("api/folha")]
public class FolhaController : ControllerBase
{
    private readonly AppDataContext _ctx;
    public FolhaController(AppDataContext ctx)
    {
        _ctx = ctx;
    }

    //GET: api/folha/listar
    [HttpGet]
    [Route("listar")]
    public IActionResult Listar()
    {
        try
        {
            List<Folha> folhas =
                _ctx.Folhas
                .Include(x => x.Funcionario)
                .ToList();
            return folhas.Count == 0 ? NotFound() : Ok(folhas);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // [HttpPost]
    // [Route("cadastrar")]
    // public IActionResult Cadastrar([FromBody] Folha folha)
    // {
    //     try
    //     {
    //         Funcionario? funcionario =
    //             _ctx.Funcionarios.Find(folha.FuncionarioId);
    //         if (funcionario == null)
    //         {
    //             return NotFound();
    //         }
    //         folha.Funcionario = funcionario;
    //         _ctx.Folhas.Add(folha);
    //         _ctx.SaveChanges();
    //         return Created("", folha);
    //     }
    //     catch (Exception e)
    //     {
    //         Console.WriteLine(e);
    //         return BadRequest(e.Message);
    //     }
    // }
    [HttpPost]
    [Route("cadastrar")]
    public IActionResult Cadastrar([FromBody] Folha folha)
    {
        try
        {
            Funcionario? funcionario =
                _ctx.Funcionarios.Find(folha.FuncionarioId);
            if (funcionario == null)
            {
                return NotFound();
            }

            // Calcular o salário bruto (número de horas trabalhadas * valor da hora)
            double salarioBruto = folha.Quantidade * folha.Valor;

            // Calcular o Imposto de Renda
            double impostoRenda = Logicas.CalcularImpostoRenda(salarioBruto);

            // Calcular o INSS
            double inss = Logicas.CalcularINSS(salarioBruto);

            // Calcular o FGTS
            double fgts = Logicas.CalcularFGTS(salarioBruto);

            // Calcular o salário líquido
            double salarioLiquido = Logicas.CalcularSalarioLiquido(salarioBruto, impostoRenda, inss);

            // Configurar os valores calculados na folha
            folha.SalarioBruto = salarioBruto;
            folha.ImpostoIrrf = impostoRenda;
            folha.ImpostoInss = inss;
            folha.ImpostoFgts = fgts;
            folha.SalarioLiquido = salarioLiquido;

            // Adicionar a folha ao contexto e salvar
            _ctx.Folhas.Add(folha);
            _ctx.SaveChanges();

            return Created("", folha);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }









    [HttpGet]
    [Route("buscar/{cpf}/{mes}/{ano}")]
    public IActionResult Buscar([FromRoute] string cpf, int mes, int ano)
    {
        try
        {
            Folha? folhaCadastrado =
                _ctx.Folhas
                .Include(x => x.Funcionario)
                .FirstOrDefault(x => x.Funcionario.CPF == cpf && x.Mes == mes && x.Ano == ano);
            if (folhaCadastrado != null)
            {
                return Ok(folhaCadastrado);
            }
            return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}