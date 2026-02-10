/*
04. Agência de Viagens – Conversor de Moedas
Você está desenvolvendo um sistema de apoio para uma agência de viagens. 
Uma das funcionalidades mais solicitadas é um conversor de moedas. 
O usuário informa um valor em reais (R$) e o sistema precisa mostrar quanto isso representa em dólares (US$), 
usando uma taxa de câmbio definida pela empresa.

Sua tarefa é criar um algoritmo em linguagem natural que use uma função para fazer essa conversão.
A função deve receber o valor em reais e a taxa de câmbio como entrada, e retornar o valor convertido.
 */



Console.WriteLine("Informe o valor em reais(R$) que deseja converter: ");
double valor = double.Parse(Console.ReadLine()!);

void ConverterMoedas() {

    double taxaCambio = 5.2;

    double valorConvertido = valor * taxaCambio;
    Console.WriteLine($"O valor convertido em dólares é: US$ {valorConvertido}");
}

ConverterMoedas();