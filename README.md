# 🍔 OxenteBurguer - Sistema de Gestão de Hamburgueria

O **OxenteBurguer** é um projeto de banco de dados para uma hamburgueria artesanal em expansão. O sistema foi projetado para substituir o controle em papel por uma solução digital robusta, integrando desde a cadeia de suprimentos até o fluxo de produção da cozinha.

---

## 📝 Descrição do Cenário

A solução foca na rastreabilidade e eficiência operacional. O sistema gerencia:
* **Cardápio Dinâmico:** Organização por categorias.
* **Cadeia de Suprimentos:** Vinculação de insumos a fornecedores específicos.
* **Fluxo de Caixa:** Separação de pedidos e itens para histórico financeiro preciso.
* **Gestão de Cozinha:** Controle de status em tempo real.

---

## 🗂️ Dicionário de Dados (Tabelas)

| Tabela | Função | Destaque Técnico |
| :--- | :--- | :--- |
| **Categorias** | Classifica itens (Lanches, Bebidas, etc). | Facilita a gestão de impostos e filtros de estoque. |
| **Fornecedores** | Cadastro de parceiros comerciais. | `VARCHAR` em documentos (CPF/CNPJ) para manter zeros à esquerda. |
| **Produtos** | Catálogo de itens à venda. | Chaves Estrangeiras (FK) para Categorias e Fornecedores. |
| **Pedidos** | Cabeçalho/Capa da venda. | Uso de `ENUM` para Status e `CURRENT_TIMESTAMP` para data/hora. |
| **Itens_Pedido** | Tabela associativa (N:N). | Preserva o valor do item no momento da venda (imutabilidade financeira). |

---

## ⚙️ Regras de Negócio e Integridade

Para garantir a confiabilidade dos dados, o modelo segue rigorosos padrões:

* **Precisão Financeira:** Utilização de `DECIMAL(10,2)` para evitar erros de arredondamento comuns em tipos `FLOAT`.
* **Integridade Referencial:** Restrições de Chave Estrangeira (FK) que impedem a exclusão de fornecedores ou categorias que possuam vínculos ativos.
* **Padronização de Fluxo:** O uso de `ENUM` (Pendente, Em Preparo, Pronto) elimina erros de digitação e garante que a cozinha e o salão falem a mesma língua.

---

## 📐 Modelo Lógico

O desenho do banco segue a **3ª Forma Normal (3FN)**, eliminando redundâncias desnecessárias.

### Estrutura de Relacionamentos:

1.  **Categorias (1) ── (N) Produtos**
    * Um produto pertence a uma única categoria; uma categoria agrupa vários produtos.
2.  **Fornecedores (1) ── (N) Produtos**
    * Rastreabilidade total: sabemos exatamente quem fornece o insumo de cada item.
3.  **Pedidos (1) ── (N) Itens_Pedido**
    * A "capa" do pedido permite que um cliente solicite múltiplos itens em uma única transação.
4.  **Produtos (1) ── (N) Itens_Pedido**
    * Permite o levantamento de histórico de vendas e ranking de produtos mais vendidos.

---

## 🚀 Tecnologias Sugeridas
* **Banco de Dados:** MySQL / PostgreSQL
* **Modelagem:** SQL Power Architect / MySQL Workbench

---
*Este projeto faz parte do portfólio de gestão de dados da OxenteBurguer.*
