using Microsoft.AspNetCore.Mvc;
using PedroWernekNetto;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();
var app = builder.Build();

app.MapGet("/", () => "Api de Folha de Pagamento");

app.MapPost("api/funcionario/cadastrar", ([FromBody] Funcionario funcionario, [FromServices] AppDataContext ctx) =>
{   
    ctx.TabelaFuncionarios.Add(funcionario);
    ctx.SaveChanges();
    return Results.Created("", funcionario);
});

app.MapGet("api/funcionario/listar", ([FromServices] AppDataContext ctx) =>
{   
    var funcionarios = ctx.TabelaFuncionarios.ToList();
    return Results.Ok(funcionarios);
});

app.MapPost("api/folha/cadastrar", ([FromBody] Folha folha, [FromServices] AppDataContext ctx) =>
{   
    Funcionario? funcionario = ctx.TabelaFuncionarios.Find(folha.FuncionarioId);
    if(funcionario is null)
    {
        return Results.NotFound();
    }

    // lógicas de cálculo
    folha.SalarioBruto = folha.Valor * folha.Quantidade;
    folha.SalarioLiquido = folha.SalarioBruto;

    if (folha.SalarioBruto <= 1903.98)
    {
        folha.ImpostoIrrf = 0;
    } else if (folha.SalarioBruto > 1903.98 && folha.SalarioBruto <= 2826.65)
    {
        folha.ImpostoIrrf = (folha.SalarioBruto * 0.075) - 142.80;
    } else if (folha.SalarioBruto > 2826.65 && folha.SalarioBruto <= 3751.05)
    {
        folha.ImpostoIrrf = (folha.SalarioBruto * 0.15) - 354.80;
    } else if (folha.SalarioBruto > 3751.05 && folha.SalarioBruto <= 4664.68)
    {
        folha.ImpostoIrrf = (folha.SalarioBruto * 0.225) - 636.13;
    } else
    {
        folha.ImpostoIrrf = (folha.SalarioBruto * 0.275) - 869.36;
    }

    folha.SalarioLiquido -= folha.ImpostoIrrf;

        if (folha.SalarioBruto <= 1693.72)
    {
        folha.ImpostoInss = (folha.SalarioBruto * 0.08);
    } else if (folha.SalarioBruto > 1693.72 && folha.SalarioBruto <= 2822.90)
    {
        folha.ImpostoInss = (folha.SalarioBruto * 0.09);
    } else if (folha.SalarioBruto > 2822.90 && folha.SalarioBruto <= 5645.80)
    {
        folha.ImpostoInss = (folha.SalarioBruto * 0.11);
    } else
    {
        folha.ImpostoInss = 621.03;
    }

    folha.SalarioLiquido -= folha.ImpostoInss;

    folha.ImpostoFgts = folha.SalarioBruto * 8 / 100;
   

    // cadastrar folha
    ctx.TabelaFolhas.Add(folha);
    ctx.SaveChanges();
    return Results.Created("", folha);
});

app.MapGet("api/folha/listar", ([FromServices] AppDataContext ctx) =>
{   
    var folhas = ctx.TabelaFolhas.ToList();
    return Results.Ok(folhas);
});

app.MapGet("api/folha/buscar/{cpf}/{mes}/{ano}", ([FromRoute] string cpf, [FromRoute] int mes,[FromRoute] int ano, [FromServices] AppDataContext ctx) =>
{   
    Funcionario? funcionario = ctx.TabelaFuncionarios.FirstOrDefault(c => c.Cpf == cpf);
    
    if (funcionario == null)
    {
        return Results.NotFound();
    }

    var FolhaAno = funcionario.Folhas.Where(x => x.Ano == ano).Where(x => x.Mes == mes);
    
    if (FolhaAno == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(FolhaAno);
});

app.Run();
