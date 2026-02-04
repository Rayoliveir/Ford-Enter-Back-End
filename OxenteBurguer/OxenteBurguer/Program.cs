using OxenteBurguer.Models;
using OxenteBurguer.Enums;

List<Produto> cardapio = new List<Produto> {
    new Lanche { Id = 1, Nome = "X-Bacon", Preco = 25.00m, Categoria = "Lanche", Descricao = "Hambúrguer artesanal, bacon crocante, queijo e molho especial" },
    new Lanche { Id = 2, Nome = "X-Salada", Preco = 22.00m, Categoria = "Lanche", Descricao = "Hambúrguer suculento, queijo, alface, tomate e maionese da casa"},
    new Lanche { Id = 3, Nome = "X-Frango", Preco = 23.50m, Categoria = "Lanche", Descricao = "Filé de frango grelhado, queijo, alface e molho especial" },
    new Lanche { Id = 4, Nome = "X-Tudo", Preco = 30.00m, Categoria = "Lanche", Descricao = "Hambúrguer, bacon, ovo, presunto, queijo, alface e tomate"  },
    new Lanche { Id = 5, Nome = "Cheeseburger", Preco = 20.00m, Categoria = "Lanche", Descricao = "Hambúrguer clássico com queijo derretido e pão macio" },
    new Lanche { Id = 6, Nome = "Misto Quente", Preco = 12.50m, Categoria = "Lanche", Descricao = "Pão Artesanal, Queijo, Presunto" },
    new Produto { Id = 7, Nome = "Refrigerante Lata", Preco = 6.00m, Categoria = "Bebida" },
    new Produto { Id = 8, Nome = "Suco de Laranja", Preco = 7.00m, Categoria = "Bebida" },
    new Produto { Id = 9, Nome = "Suco de Abacaxi", Preco = 7.00m, Categoria = "Bebida" },
    new Produto { Id = 10, Nome = "Suco de Maracujá", Preco = 7.00m, Categoria = "Bebida" },
    new Produto { Id = 11, Nome = "Água Mineral", Preco = 4.00m, Categoria = "Bebida" },
    new Produto { Id = 12, Nome = "Água com Gás", Preco = 4.50m, Categoria = "Bebida" },
    new Produto { Id = 13, Nome = "Suco de Acerola", Preco = 8.00m, Categoria = "Bebida" }

};

Console.Write("Digite seu nome: ");

Pedido pedidoAtual = new Pedido { 
    NumeroPedido = 1, 
    NomeCliente = Console.ReadLine() ?? "Cliente", 
    Status = StatusPedido.Recebido, 
};


bool executando = true;

while (executando) {
    Console.Clear();
    Console.WriteLine($"\nOlá, {pedidoAtual.NomeCliente}! Bem-vindo ao OxenteBurguer!\n");
    Console.WriteLine("===== OXENTE BURGUER SYSTEM =====");
    Console.WriteLine("1. Ver Cardápio");
    Console.WriteLine("2. Adicionar Item ao Pedido");
    Console.WriteLine("3. Ver Carrinho e Total");
    Console.WriteLine("4. Finalizar Pedido");
    Console.WriteLine("5. Remover Item");
    Console.WriteLine("0. Sair");
    Console.WriteLine("=================================");
    Console.Write("\nEscolha uma opção: ");

    string opcao = Console.ReadLine();

    switch (opcao) {
        case "1":
            ExibirCardapio(cardapio);
            break;
        case "2":
            AdicionarItemAoPedido(pedidoAtual, cardapio);
            break;
        case "3":
            ExibirCarrinho(pedidoAtual);
            break;
        case "4":
            FinalizarPedido(pedidoAtual);
            break;
        case "5":
            RemoverItem(pedidoAtual);
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

static void ExibirCardapio(List<Produto> lista)
{
    Console.Clear();
    Console.WriteLine("\n============ NOSSO CARDÁPIO ============ ");

    Console.WriteLine("\n--- LANCHES ---");
    var lanches = lista.Where(p => p.Categoria == "Lanche");
    foreach (var p in lanches)
    {
        Console.WriteLine($"{p.Id:D2} - {p.Nome.PadRight(20)} R$ {p.Preco:F2}");
    }

    Console.WriteLine("\n--- BEBIDAS ---");
    var bebidas = lista.Where(p => p.Categoria == "Bebida");
    foreach (var p in bebidas)
    {
        Console.WriteLine($"{p.Id:D2} - {p.Nome.PadRight(20)} R$ {p.Preco:F2}");
    }

    Console.WriteLine("\n========================================");
    Console.WriteLine("Pressione qualquer tecla para continuar.");
    Console.ReadKey();
}

static void AdicionarItemAoPedido(Pedido pedido, List<Produto> cardapio)
{
    ExibirCardapio(cardapio);
    Console.Write("\nDigite o ID do produto: ");

    if (int.TryParse(Console.ReadLine(), out int id))
    {
        var produtoBase = cardapio.FirstOrDefault(p => p.Id == id);

        if (produtoBase != null)
        {
            Console.WriteLine($"\n--- VOCÊ ESCOLHEU: {produtoBase.Nome} ---");
            Console.WriteLine($"Composição: {produtoBase.Descricao}");
            Console.WriteLine("------------------------------------------");

            if (produtoBase is Lanche lancheNoCardapio)
            {
                Lanche novoLanche = new Lanche
                {
                    Id = lancheNoCardapio.Id,
                    Nome = lancheNoCardapio.Nome,
                    Preco = lancheNoCardapio.Preco,
                    Descricao = lancheNoCardapio.Descricao,
                    Categoria = lancheNoCardapio.Categoria,
                    Adicionais = new List<string>()
                };

                Console.Write("Qual o ponto da carne? ");
                novoLanche.PontoCarne = Console.ReadLine() ?? "Ao ponto";

                Console.Write("Adicionais (separe por vírgula ou deixe vazio): ");
                string extras = Console.ReadLine() ?? "";

                if (!string.IsNullOrWhiteSpace(extras))
                {
                    novoLanche.Adicionais = extras.Split(',')
                                                  .Select(a => a.Trim())
                                                  .ToList();
                }

                pedido.Itens.Add(novoLanche);
            }
            else
            {
                pedido.Itens.Add(produtoBase);
            }

            Console.WriteLine($"\n{produtoBase.Nome} adicionado ao pedido!");
        }
        else
        {
            Console.WriteLine("\nProduto não encontrado!");
        }
    }
    else
    {
        Console.WriteLine("\nID inválido. Digite apenas números.");
    }

    Console.WriteLine("\nPressione qualquer tecla para continuar...");
    Console.ReadKey();
}

static void ExibirCarrinho(Pedido pedido) {
    Console.WriteLine($"\n--- CARRINHO DE {pedido.NomeCliente} ---");
    if (pedido.Itens.Count == 0) {
        Console.WriteLine("O carrinho está vazio!");
    }
    else {
        foreach (var item in pedido.Itens) {
            Console.WriteLine($"- {item.Nome}: R$ {item.Preco}");
            if (item is Lanche lanche && lanche.Adicionais.Count > 0) {
                Console.WriteLine("  Adicionais: " + string.Join(", ", lanche.Adicionais));
                Console.WriteLine(lanche.Adicionais);
            }
        }
        Console.WriteLine($"\nTOTAL ATUAL: R$ {pedido.CalcularTotal()}");
    }
    Console.WriteLine("\nPressione qualquer tecla para voltar.");
    Console.ReadKey();
}

static void FinalizarPedido(Pedido pedido) {
    if (pedido.Itens.Count == 0) {
        Console.WriteLine("\nNão dá para finalizar pedido vazio! Vá escolher um lanche.");
    }
    else {
        pedido.Status = StatusPedido.Pronto; 
        Console.WriteLine($"\nPedido finalizado com sucesso!");
        Console.WriteLine($"Total a pagar: R$ {pedido.CalcularTotal()}");
        Console.WriteLine("Obrigado pela preferência no OxenteBurguer!");
        pedido.Itens.Clear(); 
    }
    Console.WriteLine("\nPressione qualquer tecla para continuar.");
    Console.ReadKey();
}

static void RemoverItem(Pedido pedido) {
    if (pedido.Itens.Count == 0) {
        Console.WriteLine("O carrinho está vazio, nada para remover!");
    }
    else {
        ExibirCarrinho(pedido);
        Console.Write("\nDigite o ID do produto que deseja remover: ");
        if (int.TryParse(Console.ReadLine(), out int idRemover))
        {
            var itemRemover = pedido.Itens.FirstOrDefault(p => p.Id == idRemover);
            if (itemRemover != null)
            {
                pedido.Itens.Remove(itemRemover);
                Console.WriteLine($"\n{itemRemover.Nome} removido com sucesso!");
            }
            else
            {
                Console.WriteLine("\nProduto não encontrado no carrinho!");
            }
        }
        else
        {
            Console.WriteLine("\nPor favor, digite um número de ID válido.");
        }
    }
    Console.WriteLine("\nPressione Enter para continuar.");
    Console.ReadLine();
}