/* 
01. Lanchonete – Cálculo de Pedido
Você está desenvolvendo uma funcionalidade para um aplicativo de lanchonete. 
O sistema precisa calcular o valor total de um pedido com base na quantidade de itens solicitados.

O cliente pode pedir:

Hambúrguer: R$ 12,00
Batata frita: R$ 7,00
Refrigerante: R$ 5,00

Sua tarefa é criar um algoritmo em linguagem natural ou fluxograma que armazene os dados necessários em 
variáveis e faça o cálculo do valor total com base nas quantidades informadas.

*/

// Variáveis Globais (fora das funções)
List<string> itensNoCarrinho = new List<string>();
double valorTotalPedido = 0;
bool executar = true;

void Cardapio() {
    Console.WriteLine("Bem-vindo à Lanchonete!");
    Console.WriteLine("1. hamburguer - R$ 12,00");
    Console.WriteLine("2. Batata Frita - R$ 7,00");
    Console.WriteLine("3. Refrigerante - R$ 5,00");
}

void AdicionarItemAoPedido() {
    Console.Clear();
    Cardapio();
    Console.Write("Escolha o número do item: ");
    string escolha = Console.ReadLine();
    Console.Write("Quantidade: ");
    int qtd = int.Parse(Console.ReadLine());

    if (escolha == "1")
    {
        itensNoCarrinho.Add($"{qtd}x Hambúrguer");
        valorTotalPedido += qtd * 12.00;
    }
    else if (escolha == "2")
    {
        itensNoCarrinho.Add($"{qtd}x Batata Frita");
        valorTotalPedido += qtd * 7.00;
    }
    else if (escolha == "3")
    {
        itensNoCarrinho.Add($"{qtd}x Refrigerante");
        valorTotalPedido += qtd * 5.00;
    }
    Console.WriteLine("Adicionado!");
    Console.WriteLine("\nPressione Enter para continuar...");
    Console.ReadKey();
    Console.Clear();
}

void VerCarrinho() {
    Console.Clear();
    Console.WriteLine("\n--- SEU CARRINHO ---");
    if (itensNoCarrinho.Count == 0)
    {
        Console.WriteLine("O carrinho está vazio.");
    }
    else
    {
        foreach (string item in itensNoCarrinho)
        {
            Console.WriteLine("- " + item);
        }
        Console.WriteLine("--------------------");
        Console.WriteLine($"Total Atual: R$ {valorTotalPedido:F2}");
    }
    Console.WriteLine("\nPressione Enter para continuar...");
    Console.ReadKey();
    Console.Clear();
}

void FinalizarPedido() {
    Console.Clear();
    Console.WriteLine($"\nO valor total do seu pedido é: R$ {valorTotalPedido:F2}");
    Console.WriteLine("Obrigado pela preferência!");
    executar = false;

}

void Main() {
    Console.WriteLine("========== LancheFácil ==========");
    Console.WriteLine("1. Adicionar Item ao Pedido");
    Console.WriteLine("2. Ver Carrinho e Total");
    Console.WriteLine("3. Finalizar Pedido");;
    Console.WriteLine("0. Sair");
    Console.WriteLine("=================================");

    Console.Write("\nEscolha uma opção: ");
    string opcao = Console.ReadLine();
    
    switch (opcao) {
        case "1":
            AdicionarItemAoPedido();
            break;
        case "2":
            VerCarrinho();
            break;
        case "3":
            FinalizarPedido();
            break;
        case "0":
            Console.WriteLine("Obrigado por visitar a Lanchonete!");
            executar = false;
            return;
        default:
            Console.WriteLine("Opção inválida. Tente novamente.");
            break;
    }
    

}

do {
    Main();
} while (executar);