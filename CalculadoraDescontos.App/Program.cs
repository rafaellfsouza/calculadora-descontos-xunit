using CalculadoraDescontos.App;

var service = new DescontoService();

Console.WriteLine("=== Calculadora de Descontos ===");
Console.WriteLine();

Console.WriteLine($"Cliente com 7 compras      -> Categoria: {service.ObterCategoriaCliente(7)}");
Console.WriteLine($"Compra de 200 com 20% off  -> Valor final: {service.CalcularDescontoPorPercentual(200, 20)}");
Console.WriteLine($"Cliente de 20 anos         -> Cupom válido: {service.EValidoParaCupom(20, false)}");
