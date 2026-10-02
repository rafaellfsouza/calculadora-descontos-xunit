using CalculadoraDescontos.App;
using Xunit;

namespace CalculadoraDescontos.Tests;

/// <summary>
/// Testes parametrizados com [Theory] + [InlineData] para o DescontoService.
/// Cada [InlineData] é executado como um teste individual, sem a necessidade
/// de duplicar métodos de teste para cada cenário.
/// </summary>
public class DescontoServiceTests
{
    private readonly DescontoService _descontoService = new();

    // Teste 1 (retorno string): valida todas as categorias de cliente.
    [Theory]
    [InlineData(2, "BRONZE")]   // menos de 5 compras -> BRONZE
    [InlineData(7, "PRATA")]    // entre 5 e 10 compras (inclusive) -> PRATA
    [InlineData(15, "OURO")]    // mais de 10 compras -> OURO
    public void ObterCategoriaCliente_DeveRetornarCategoriaEsperada(int totalCompras, string categoriaEsperada)
    {
        // Act
        var resultado = _descontoService.ObterCategoriaCliente(totalCompras);

        // Assert
        Assert.Equal(categoriaEsperada, resultado);
    }

    // Teste 2 (retorno int): valida o cálculo do desconto percentual.
    [Theory]
    [InlineData(100, 10, 90)]   // 100 com 10% de desconto -> 90
    [InlineData(200, 20, 160)]  // 200 com 20% de desconto -> 160
    [InlineData(50, 0, 50)]     // 50 sem desconto -> 50
    public void CalcularDescontoPorPercentual_DeveRetornarValorFinalComDesconto(int valorOriginal, int percentualDesconto, int valorEsperado)
    {
        // Act
        var resultado = _descontoService.CalcularDescontoPorPercentual(valorOriginal, percentualDesconto);

        // Assert
        Assert.Equal(valorEsperado, resultado);
    }

    // Teste 3 (retorno bool): valida a elegibilidade para o cupom.
    [Theory]
    [InlineData(20, false, true)]   // Maior de idade, não é primeira compra -> true
    [InlineData(16, true, true)]    // Menor de idade, primeira compra -> true
    [InlineData(17, false, false)]  // Menor de idade, não é primeira compra -> false
    public void EValidoParaCupom_DeveRetornarElegibilidadeEsperada(int idade, bool primeiraCompra, bool esperado)
    {
        // Act
        var resultado = _descontoService.EValidoParaCupom(idade, primeiraCompra);

        // Assert
        Assert.Equal(esperado, resultado);
    }
}
