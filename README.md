# 🍔 OxenteBurguer - Sistema de Gestão de Pedidos

Bem-vindo ao repositório do **OxenteBurguer**! Este é um sistema de console desenvolvido em C# para simular o fluxo de atendimento de uma lanchonete moderna. O projeto nasceu como um exercício prático para aplicar conceitos de Programação Orientada a Objetos (POO) e lógica de programação.

## 🎯 O que o sistema faz?

O sistema permite que um atendente (ou o próprio cliente) gerencie um pedido completo através de um menu interativo. As principais funcionalidades são:

* **Identificação do Cliente:** O sistema começa perguntando quem está sendo atendido.
* **Cardápio Organizado:** Exibição de produtos divididos por categorias (Lanches e Bebidas).
* **Customização Arretada:** Ao escolher um lanche, você pode definir o ponto da carne e adicionar ingredientes extras (Bacon, Queijo, Ovo, etc.).
* **Carrinho Inteligente:** O sistema calcula o valor total automaticamente e permite visualizar os detalhes de cada item adicionado.
* **Gestão de Itens:** Caso mude de ideia, é possível remover itens específicos do carrinho antes de fechar a conta.
* **Finalização de Pedido:** Limpa o carrinho e prepara o sistema para o próximo cliente.

## 🛠️ Tecnologias Utilizadas

* **Linguagem:** C#
* **Ambiente:** .NET 8 (Console Application)
* **IDE:** Visual Studio 2022

## 🧠 O que eu aprendi construindo isso?

Este projeto foi fundamental para colocar em prática conceitos importantes que todo desenvolvedor precisa saber:

1. **Organização de Dados (POO):** Criei classes para representar `Produtos`, `Lanches` e o `Pedido`, garantindo que cada parte do código cuide de uma responsabilidade.
2. **Lógica de Listas:** Aprendi a usar o **LINQ** para buscar produtos por ID, filtrar categorias e calcular somas de preços.
3. **Tratamento de Erros:** O sistema foi "blindado" contra entradas erradas (como digitar letras onde deveriam ser números) para evitar que o programa feche sozinho.
4. **Interface de Console:** Melhorei a experiência do usuário com limpezas de tela (`Console.Clear`) e formatação de textos.

---

**Desenvolvido por Marcelly Oliveira**
*Estudante de tecnologia apaixonado por resolver problemas e criar soluções criativas.*

<a href="https://marcelly-oliveira.vercel.app/" target="_blank">Marcelly Oliveira - Portifólio</a>
