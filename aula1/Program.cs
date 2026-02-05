class Principal {
    static void Main(string[] args) {
        double Divida = 5000;

        Console.Write("Digite seu nome: ");
        string Nome = Console.ReadLine() !;

        do
        {
        valorpagar:
            Console.WriteLine("Digite o valor que você quer pagar da dívida: ");
            double ValorPago = Convert.ToDouble(Console.ReadLine());
            if (ValorPago <= Divida)
            {
                Divida -= ValorPago;
                Console.WriteLine($"\nValor pago: {ValorPago:C}. \nDívida restante: {Divida:C}.");
                if (Divida > 0) {
                    Console.WriteLine("Deseja pagar mais alguma coisa? (s/n)");
                    string opcao = Console.ReadLine();

                    switch (opcao.ToLower()) {
                        case "s":
                            goto valorpagar;
                        default:
                            Console.WriteLine($"Obrigado, {Nome}. Até a próxima!");
                            break;
                    }
                }
            }
            else if (ValorPago > Divida) {
                Console.WriteLine("O valor pago é maior do que a dívida. Por favor, tente novamente digitando um valor válido.");
            }
            else {
                Console.WriteLine("Valor inválido. Por favor, tente novamente.");
            }
        } while (Divida > 0);

    }
}