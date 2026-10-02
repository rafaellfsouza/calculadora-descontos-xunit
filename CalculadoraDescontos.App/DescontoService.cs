namespace CalculadoraDescontos.App;

/// <summary>
/// Serviço responsável pelas regras de negócio da calculadora de descontos:
/// categoria do cliente, cálculo de desconto percentual e elegibilidade de cupom.
/// </summary>
public class DescontoService
{
    /// <summary>
    /// Retorna a categoria do cliente com base no total de compras realizadas.
    /// </summary>
    /// <param name="totalCompras">Quantidade total de compras do cliente.</param>
    /// <returns>
    /// "BRONZE" para menos de 5 compras,
    /// "PRATA" para compras entre 5 e 10 (inclusive),
    /// "OURO" para mais de 10 compras.
    /// </returns>
    public string ObterCategoriaCliente(int totalCompras)
    {
        if (totalCompras < 5)
        {
            return "BRONZE";
        }

        if (totalCompras <= 10)
        {
            return "PRATA";
        }

        return "OURO";
    }

    /// <summary>
    /// Calcula o valor final de uma compra após a aplicação de um desconto percentual.
    /// </summary>
    /// <param name="valorOriginal">Valor original da compra.</param>
    /// <param name="percentualDesconto">Percentual de desconto a ser aplicado (ex.: 10 = 10%).</param>
    /// <returns>O valor final com o desconto aplicado (ex.: 100 com 10% retorna 90).</returns>
    public int CalcularDescontoPorPercentual(int valorOriginal, int percentualDesconto)
    {
        return valorOriginal - (valorOriginal * percentualDesconto / 100);
    }

    /// <summary>
    /// Verifica se o cliente é elegível para receber o cupom de desconto.
    /// </summary>
    /// <param name="idade">Idade do cliente.</param>
    /// <param name="primeiraCompra">Indica se é a primeira compra do cliente.</param>
    /// <returns>
    /// true se o cliente tiver 18 anos ou mais OU se for a primeira compra;
    /// caso contrário, false.
    /// </returns>
    public bool EValidoParaCupom(int idade, bool primeiraCompra)
    {
        return idade >= 18 || primeiraCompra;
    }
}
