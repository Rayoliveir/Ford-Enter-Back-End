// Screen Sound 
string mensagemDeBoasVindas = "\n\t\t\t\t\t\tBem-vindo ao Screen Sound!\n\n";

Dictionary<string, List<int>> bandasRegistradas = new Dictionary<string, List<int>>();
bandasRegistradas.Add("Linkin Park", new List<int> { 10, 9, 6, 3 });
bandasRegistradas.Add("The Beatles", new List<int>());

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
            AvaliarBanda();
            break;
        case 4:
            ExibirMediaDaBanda();
            break;
        case 0:
            Console.WriteLine("Programa Encerrado!");
            break;
        default:
            Console.WriteLine("Opção inválida! Tente novamente.");
            Console.ReadKey();
            Console.Clear();
            ExibirOpcoesDoMenu();
            break;
    }
}

void RegistrarBanda() {
    Console.Clear();

    ExibirTitulo("Registro de Bandas");

    Console.Write("Digite o nome da banda que deseja registrar: ");
    string nomeDaBanda = Console.ReadLine()!;

    bandasRegistradas.Add(nomeDaBanda, new List<int>());

    Console.WriteLine($"A banda {nomeDaBanda} foi registrada com sucesso.");

    Console.WriteLine("\nDigite uma tecla para voltar ao menu.");
    Console.ReadKey();
    Console.Clear();
    ExibirOpcoesDoMenu();
}

void MostrarBandasRegistradas() {
    Console.Clear();

    ExibirTitulo("Bandas Registradas");

    foreach (string banda in bandasRegistradas.Keys) {
        Console.WriteLine($"Banda {banda}");

    }

    Console.WriteLine("\nDigite uma tecla para voltar ao menu principal.");
    Console.ReadKey();
    Console.Clear();

    ExibirOpcoesDoMenu();
}


void ExibirTitulo(string titulo) {
    int quantidadeDeLetras = titulo.Length;
    string asteriscos = string.Empty.PadLeft(quantidadeDeLetras, '*');

    Console.WriteLine(asteriscos);
    Console.WriteLine(titulo);
    Console.WriteLine(asteriscos + "\n");
}

void AvaliarBanda() {
    Console.Clear();
    ExibirTitulo("Avaliar Banda");

    Console.Write("Digite o nome da banda que deseja avaliar: ");
    string nomeDaBanda = Console.ReadLine()!;

    if (bandasRegistradas.ContainsKey(nomeDaBanda)) {
        Console.Write($"Qual nota você dá para a banda {nomeDaBanda}? (0 a 10): ");
        int nota = int.Parse(Console.ReadLine()!);
        bandasRegistradas[nomeDaBanda].Add(nota);
        Console.WriteLine($"\nObrigado por avaliar a banda {nomeDaBanda} com a nota {nota}.");
        Thread.Sleep(1000);
        Console.Clear();
        ExibirOpcoesDoMenu();

    } else {
        Console.WriteLine($"\nA banda {nomeDaBanda} não está registrada. Por favor, registre a banda antes de avaliá-la.");
        Console.WriteLine("Digite uma tecla para voltar ao menu principal.");
        Console.ReadKey();
        Console.Clear();
        ExibirOpcoesDoMenu();

    }
    
}

void ExibirMediaDaBanda() {
    Console.Clear();
    ExibirTitulo("Média da Banda");

    Console.Write("Digite o nome da banda que deseja calcular a média: ");
    string nomeDaBanda = Console.ReadLine()!;
    if (bandasRegistradas.ContainsKey(nomeDaBanda)) {
        List<int> notas = bandasRegistradas[nomeDaBanda];
        if (notas.Count > 0) {
            double media = notas.Average();
            Console.WriteLine($"\nA média das avaliações da banda {nomeDaBanda} é: {media:F2}");
        } else {
            Console.WriteLine($"\nA banda {nomeDaBanda} ainda não possui avaliações.");
        }
    } else {
        Console.WriteLine($"\nA banda {nomeDaBanda} não está registrada.");
        Console.WriteLine("\nDigite uma tecla para voltar ao menu principal.");
        Console.ReadKey();
        Console.Clear();
        ExibirOpcoesDoMenu();

    }
}
ExibirOpcoesDoMenu();

