/*
03. Delivery – Cálculo da Taxa de Entrega
Você está desenvolvendo um sistema para uma empresa de delivery. O valor da taxa de entrega depende da distância até o cliente e se o pedido foi feito em um dia de chuva.

As regras são:

Para entregas até 5 km, a taxa é R$ 5,00.
Entre 5 e 10 km, a taxa é R$ 8,00.
Acima de 10 km, a taxa é R$ 10,00.
Se estiver chovendo, acrescenta R$ 2,00 à taxa padrão.
O desafio desta atividade é criar um algoritmo em linguagem natural que informe o valor final da entrega.
 
*/
int valorFinal = 0;

Console.Write($"Valor do pedido: ");
int valorPedido = int.Parse(Console.ReadLine()!);

void CalcularValorFrete(int valorFinal) {
    Console.Write("Informe a distância em km: ");
    int distancia = int.Parse(Console.ReadLine()!);

    if (distancia <= 5) {
        valorFinal = valorPedido += 5;

        //Console.WriteLine($"Valor do pedido: {valorFinal}");
    } else if (distancia > 5 && distancia <= 10) {
        valorFinal = valorPedido += 8;
    } else {
        valorFinal = valorPedido += 10;
    }

    Console.WriteLine($"Valor do pedido: {valorFinal}");

}

CalcularValorFrete(valorFinal);

