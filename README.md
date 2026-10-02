# Calculadora de Descontos — Testes Unitários com xUnit ([Fact] vs [Theory])

> **Disciplina:** Garantia da Qualidade de Software — Gestão e Qualidade de Software
> **Professor:** Daniel Henrique Matos de Paiva
> **Aluno:** Rafael Luiz Ferreira de Souza
> **RA:** 32511503

Solução .NET 10 criada via **.NET CLI** que demonstra a diferença entre testes `[Fact]` (teste único, sem parâmetros) e `[Theory]` (teste parametrizado, executado várias vezes com os dados de cada `[InlineData]`), cobrindo cenários com entradas e retornos dos tipos **string**, **int** e **bool**.

## 📁 Estrutura do Repositório

```text
calculadora-descontos-xunit/
├── CalculadoraDescontos.sln                  # Solução que agrupa os dois projetos
├── CalculadoraDescontos.App/                 # Projeto de aplicação (código de produção)
│   ├── CalculadoraDescontos.App.csproj
│   ├── DescontoService.cs                    # Regras de negócio (string, int e bool)
│   └── Program.cs                            # Demonstração de uso no console
└── CalculadoraDescontos.Tests/               # Projeto de testes unitários (xUnit)
    ├── CalculadoraDescontos.Tests.csproj
    └── DescontoServiceTests.cs               # Testes com [Theory] + [InlineData]
```

## ✅ Requisitos

- [.NET SDK 10.0](https://dotnet.microsoft.com/download/dotnet/10.0) ou superior

Verifique a instalação com:

```bash
dotnet --version
```

## 🚀 Como Executar os Testes

Na raiz do repositório (pasta que contém o arquivo `CalculadoraDescontos.sln`), execute:

```bash
dotnet test
```

Cada `[InlineData]` roda como um **teste individual** (no total são 9 execuções: 3 cenários × 3 métodos `[Theory]`). Exemplo da saída:

```text
Passado!  - Com Falha: 0, Aprovado: 9, Ignorado: 0, Total: 9, Duração: < 1 ms - CalculadoraDescontos.Tests.dll (net10.0)
```

Para executar a aplicação de demonstração:

```bash
dotnet run --project CalculadoraDescontos.App
```

## 🧪 Diferença entre [Fact] e [Theory]

| Característica        | `[Fact]`                                                        | `[Theory]`                                                                     |
| --------------------- | --------------------------------------------------------------- | ------------------------------------------------------------------------------ |
| Parâmetros            | Não aceita parâmetros                                            | Aceita parâmetros no método de teste                                            |
| Execuções             | Executa **uma única vez**                                        | Executa **uma vez para cada conjunto de dados** (`[InlineData]`, `[MemberData]`, `[ClassData]`) |
| Quando usar           | Um único cenário fixo                                            | Vários cenários com entradas/saídas diferentes para a mesma lógica             |
| Vantagem              | Simplicidade                                                     | Evita duplicar métodos de teste; cada `[InlineData]` vira um teste independente |

### Exemplo com `[Fact]` (teste único, sem parâmetros)

```csharp
[Fact]
public void ObterCategoriaCliente_Com7Compras_DeveRetornarPrata()
{
    var service = new DescontoService();

    var resultado = service.ObterCategoriaCliente(7);

    Assert.Equal("PRATA", resultado);
}
```

### Exemplo com `[Theory]` (teste parametrizado com `[InlineData]`)

```csharp
[Theory]
[InlineData(2, "BRONZE")]
[InlineData(7, "PRATA")]
[InlineData(15, "OURO")]
public void ObterCategoriaCliente_DeveRetornarCategoriaEsperada(int totalCompras, string categoriaEsperada)
{
    var service = new DescontoService();

    var resultado = service.ObterCategoriaCliente(totalCompras);

    Assert.Equal(categoriaEsperada, resultado);
}
```

No exemplo acima, o mesmo método de teste é executado **3 vezes** (uma para cada `[InlineData]`), validando todos os cenários sem duplicar código.

## 📋 Regras de Negócio Testadas (`DescontoService`)

| Método                                                      | Retorno  | Regra                                                                                   |
| ----------------------------------------------------------- | -------- | --------------------------------------------------------------------------------------- |
| `ObterCategoriaCliente(int totalCompras)`                   | `string` | `"BRONZE"` (< 5 compras), `"PRATA"` (5 a 10, inclusive), `"OURO"` (> 10 compras)         |
| `CalcularDescontoPorPercentual(int valorOriginal, int pct)` | `int`    | Valor final com o desconto aplicado (ex.: 100 com 10% → 90)                              |
| `EValidoParaCupom(int idade, bool primeiraCompra)`          | `bool`   | `true` se idade ≥ 18 **OU** se for a primeira compra; caso contrário, `false`            |

## 🔧 Como a Solução Foi Criada via .NET CLI

```bash
# 1. Cria a Solução
dotnet new sln -n CalculadoraDescontos

# 2. Cria o projeto da aplicação (Código de Produção)
dotnet new console -n CalculadoraDescontos.App -f net10.0

# 3. Cria o projeto de Testes Unitários com xUnit
dotnet new xunit -n CalculadoraDescontos.Tests -f net10.0

# 4. Adiciona ambos os projetos à Solução
dotnet sln add CalculadoraDescontos.App/CalculadoraDescontos.App.csproj
dotnet sln add CalculadoraDescontos.Tests/CalculadoraDescontos.Tests.csproj

# 5. Adiciona a referência do projeto de Produção no projeto de Testes
dotnet add CalculadoraDescontos.Tests/CalculadoraDescontos.Tests.csproj reference CalculadoraDescontos.App/CalculadoraDescontos.App.csproj
```

## 📄 Licença

Este projeto está licenciado sob a [MIT License](LICENSE).
