// Screen Sound 
string mensagemDeBoasVindas = "\n\t\t\t\t\t\tBem-vindo ao Screen Sound!\n\n";
List<string> ListaDasBandas = new List<string> { "U2", "SlipKnot", "Calipso"};


void ExibirLogo() {
    Console.WriteLine(@"
        
        ░██████╗░█████╗░██████╗░███████╗███████╗███╗░░██╗  ░██████╗░█████╗░██╗░░░██╗███╗░░██╗██████╗░░██████╗
        ██╔════╝██╔══██╗██╔══██╗██╔════╝██╔════╝████╗░██║  ██╔════╝██╔══██╗██║░░░██║████╗░██║██╔══██╗██╔════╝
        ╚█████╗░██║░░╚═╝██████╔╝█████╗░░█████╗░░██╔██╗██║  ╚█████╗░██║░░██║██║░░░██║██╔██╗██║██║░░██║╚█████╗░
        ░╚═══██╗██║░░██╗██╔══██╗██╔══╝░░██╔══╝░░██║╚████║  ░╚═══██╗██║░░██║██║░░░██║██║╚████║██║░░██║░╚═══██╗
        ██████╔╝╚█████╔╝██║░░██║███████╗███████╗██║░╚███║  ██████╔╝╚█████╔╝╚██████╔╝██║░╚███║██████╔╝██████╔╝
        ╚═════╝░░╚════╝░╚═╝░░╚═╝╚══════╝╚══════╝╚═╝░░╚══╝  ╚═════╝░░╚════╝░░╚═════╝░╚═╝░░╚══╝╚═════╝░╚═════╝░

    ");
    Console.WriteLine(mensagemDeBoasVindas);
}

void ExibirOpcoesDoMenu()
{
    ExibirLogo();
    Console.WriteLine("1 - Registrar uma banda");
    Console.WriteLine("2 - Mostrar todas as bandas");
    Console.WriteLine("3 - Avaliar uma banda");
    Console.WriteLine("4 - Exibir a média de uma banda");
    Console.WriteLine("0 - sair");

    Console.Write("\nDigite uma opção: ");
    string opcaoEscolhida = Console.ReadLine();
    int opcaoEscolhidaNumerica = int.Parse(opcaoEscolhida);

    switch (opcaoEscolhidaNumerica)
    {
        case 1:
            RegistrarBanda();
            break;
        case 2:
            MostrarBandasRegistradas();
            break;
        case 3:
            Console.WriteLine("Você escolheu a opção 3");
            break;
        case 4:
            Console.WriteLine("Você escolheu a opção 4");
            break;
        case 0:
            Console.WriteLine("Programa Encerrado!");
            break;
        default:
            Console.WriteLine("Opção inválida!");
            break;
    }
}

void RegistrarBanda() {
    Console.Clear();

    Console.WriteLine("**********************************");
    Console.WriteLine("\tRegistro de Bandas");
    Console.WriteLine("**********************************\n");

    Console.Write("Digite o nome da banda que deseja registrar: ");
    string nomeDaBanda = Console.ReadLine()!;

    ListaDasBandas.Add(nomeDaBanda);

    Console.WriteLine($"A banda {nomeDaBanda} foi registrada com sucesso.");

    Console.WriteLine("\nDigite uma tecla para voltar ao menu.");
    Console.ReadKey();
    Console.Clear();
    ExibirOpcoesDoMenu();
}

void MostrarBandasRegistradas() {
    Console.Clear();

    Console.WriteLine("**********************************");
    Console.WriteLine("\tBandas Registradas");
    Console.WriteLine("**********************************");

    for (int i = 0; i < ListaDasBandas.Count; i++)
    {
        Console.WriteLine($"Banda {ListaDasBandas[i]}");
    }

    Console.WriteLine("Digite uma tecla para voltar ao menu.");
    Console.ReadKey();
    Console.Clear();

    ExibirOpcoesDoMenu();
}

ExibirOpcoesDoMenu();