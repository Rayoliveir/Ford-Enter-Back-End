/*
02. Cinema – Verificação de Meia-Entrada
Você está desenvolvendo o sistema de bilheteria para um cinema. Os clientes podem ter direito a meia-entrada em duas situações:

Se tiverem menos de 18 anos ou Se forem estudantes

Sua tarefa é criar um algoritmo em linguagem natural ou gráfica (usando fluxogramas, por exemplo) que avalie as informações do cliente e exiba uma mensagem indicando se ele tem ou não direito ao desconto.
*/

Console.WriteLine("Bem-vindo ao cinema! Vamos verificar se você tem direito à meia entrada.");

Console.Write("Informe a sua idade: ");
int idade = int.Parse(Console.ReadLine()!);

Console.WriteLine("Informe se você é estudante(S ou N): ");
string respostaEstudante = Console.ReadLine()!.ToUpper();

if (idade < 18 ) {
    Console.WriteLine("Você tem direito à meia entrada!");
} else if (respostaEstudante == "S") {
    Console.WriteLine("Você tem direito à meia entrada!");
}
else {
    Console.WriteLine("Você não tem direito à meia entrada.");
}