/*
05. Cafeteria – Registro de Pedidos e Descontos
Você foi contratado por uma cafeteria que deseja automatizar o atendimento no balcão. O sistema deve permitir que o atendente registre os pedidos de cada cliente, calcule o valor total e aplique um desconto de 10% para clientes cadastrados.

O processo deve funcionar da seguinte forma:

O atendente informa quantos itens o cliente vai pedir.
Para cada item, o sistema solicita o nome e o preço.
Ao final, o sistema pergunta se o cliente é cadastrado.
Se for, aplica o desconto e exibe o valor com desconto.
Caso contrário, exibe o valor cheio.
O desafio consiste em criar um algoritmo que represente essa lógica de forma completa.
 
*/

using System;

class Program {
    static void Main() {
        double totalGeral = 0.0;
        bool adicionarMais = true;

        Console.Clear();
        Console.WriteLine("--- Sistema de Atendimento Cafeteria ---");

        
        while (adicionarMais) {
            
            Console.Write("\nInforme o nome do item: ");
            string nomeItem = Console.ReadLine() ?? "Item";

            Console.Write($"\nInforme o preço do(a) {nomeItem}: R$ ");
            if (double.TryParse(Console.ReadLine(), out double preco)) {
                totalGeral += preco; 
            }
            else {
                Console.WriteLine("\nPreço inválido! Tente novamente.");
                continue;
            }

            Console.Write("\nDeseja adicionar mais itens? (s/n): ");
            string resposta = Console.ReadLine()?.ToLower() ?? string.Empty;

            if (resposta != "s") {
                adicionarMais = false;
            }
        }

        Console.Clear();
        Console.WriteLine("\n------------------------------------");
        Console.Write("O cliente é cadastrado? (s/n): ");
        string Cadastrado = Console.ReadLine()?.ToLower() ?? string.Empty;

        Console.Clear();
        Console.WriteLine("\n--- RESUMO DO PEDIDO ---");

        if (Cadastrado == "s")
        {
            double desconto = totalGeral * 0.10;
            double valorFinal = totalGeral - desconto;

            Console.WriteLine($"Valor total bruto: R$ {totalGeral:F2}");
            Console.WriteLine($"Desconto aplicado (10%): -R$ {desconto:F2}");
            Console.WriteLine($"VALOR A PAGAR: R$ {valorFinal:F2}");
        }
        else {
            Console.Clear();
            Console.WriteLine("Cliente sem cadastro (sem desconto).");
            Console.WriteLine($"VALOR A PAGAR: R$ {totalGeral:F2}");
        }

        Console.WriteLine("------------------------------------");
        Console.WriteLine("Obrigado e volte sempre!");
    }
}