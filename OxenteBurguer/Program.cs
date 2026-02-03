// See https://aka.ms/new-console-template for more information
using OxenteBurguer.Models;
using OxenteBurguer.Enums;

// Criando um cardápio fixo (Data Seeding)
List<Produto> cardapio = new List<Produto>
{
    new Lanche { Id = 1, Nome = "X-Bacon", Preco = 25.00m, Categoria = "Lanche", PontoCarne = "Ao ponto" },
    new Produto { Id = 2, Nome = "Suco de Acerola", Preco = 8.00m, Categoria = "Bebida" },
    new Lanche { Id = 3, Nome = "Misto Quente", Preco = 12.50m, Categoria = "Lanche" }
};

// Objeto do pedido atual
Pedido pedidoAtual = new Pedido { NumeroPedido = 1, NomeCliente = "Visitante", Status = StatusPedido.Recebido };

// Início do Menu
bool executando = true;

while (executando) // Estrutura de repetição para o menu não fechar
{
    Console.Clear();
    Console.WriteLine("=== 🍔 OXENTE BURGUER SYSTEM ===");
    Console.WriteLine("1. Ver Cardápio");
    Console.WriteLine("2. Adicionar Item ao Pedido");
    Console.WriteLine("3. Ver Carrinho e Total");
    Console.WriteLine("4. Finalizar Pedido");
    Console.WriteLine("0. Sair");
    Console.Write("\nEscolha uma opção: ");

    string opcao = Console.ReadLine();

    switch (opcao) // Estrutura condicional para o menu
    {
        case "1":
            ExibirCardapio(cardapio);
            break;
        case "2":
            Console.Write("\nDigite o ID do produto que deseja adicionar: ");
            if (int.TryParse(Console.ReadLine(), out int idEscolhido))
            {
                // Busca o produto no cardápio usando o ID digitado
                var produtoEncontrado = cardapio.FirstOrDefault(p => p.Id == idEscolhido);

                if (produtoEncontrado != null)
                {
                    pedidoAtual.Itens.Add(produtoEncontrado);
                    Console.WriteLine($"\n{produtoEncontrado.Nome} adicionado com sucesso!");
                }
                else
                {
                    Console.WriteLine("\nProduto não encontrado, meu rei!");
                }
            }
            else
            {
                Console.WriteLine("\nPor favor, digite um número de ID válido.");
            }
            Console.WriteLine("\nPressione Enter para continuar...");
            Console.ReadLine();
            break;
        case "3":
            ExibirCarrinho(pedidoAtual);
            break;
        case "4":
            FinalizarPedido(pedidoAtual);
            break;
        case "0":
            executando = false;
            break;
        default:
            Console.WriteLine("Opção inválida! Aperte Enter e tente de novo.");
            Console.ReadLine();
            break;
    }
}

// Método modularizado para exibir o cardápio
static void ExibirCardapio(List<Produto> lista)
{
    Console.WriteLine("\n--- NOSSO CARDÁPIO ---");
    foreach (var p in lista)
    {
        Console.WriteLine($"{p.Id} - {p.Nome} | R$ {p.Preco}");
    }
    Console.WriteLine("\nPressione qualquer tecla para voltar...");
    Console.ReadKey();
}

static void ExibirCarrinho(Pedido pedido)
{
    Console.WriteLine($"\n--- CARRINHO DE {pedido.NomeCliente} ---");
    if (pedido.Itens.Count == 0)
    {
        Console.WriteLine("O carrinho está vazio, brocado!");
    }
    else
    {
        foreach (var item in pedido.Itens)
        {
            Console.WriteLine($"- {item.Nome}: R$ {item.Preco}");
        }
        // Aqui a mágica do seu método CalcularTotal acontece!
        Console.WriteLine($"\nTOTAL ATUAL: R$ {pedido.CalcularTotal()}");
    }
    Console.WriteLine("\nPressione qualquer tecla para voltar...");
    Console.ReadKey();
}

static void FinalizarPedido(Pedido pedido)
{
    if (pedido.Itens.Count == 0)
    {
        Console.WriteLine("\nNão dá para finalizar pedido vazio! Vá escolher um lanche.");
    }
    else
    {
        pedido.Status = StatusPedido.Pronto; // Usando o seu Enum!
        Console.WriteLine($"\nPedido finalizado com sucesso!");
        Console.WriteLine($"Total a pagar: R$ {pedido.CalcularTotal()}");
        Console.WriteLine("Obrigado pela preferência no OxenteBurguer!");
        pedido.Itens.Clear(); // Limpa para o próximo cliente
    }
    Console.WriteLine("\nPressione qualquer tecla para continuar...");
    Console.ReadKey();
}