-- Criando schema
CREATE DATABASE oxenteburguer;
USE oxenteburguer;

-- Criando tabelas
CREATE TABLE categorias(
	ID_categoria INT PRIMARY KEY AUTO_INCREMENT,
    nome VARCHAR(50) NOT NULL,
    descricao TEXT
);
CREATE TABLE fornecedores(
	ID_fornecedor INT PRIMARY KEY AUTO_INCREMENT,
	nome_empresa VARCHAR(50) NOT NULL,
	cnpj_cpf VARCHAR(20) NOT NULL,
	telefone VARCHAR(20) NOT NULL,
	email VARCHAR(100) NOT NULL
);
CREATE TABLE produtos(
	ID_produto INT PRIMARY KEY AUTO_INCREMENT,
	nome_item VARCHAR(100) NOT NULL,
	preco_unitario DECIMAL(10,2) NOT NULL,
	estoque_atual INT NOT NULL,
    descricao TEXT,
	FK_categoria INT,
	FK_fornecedor INT,
    
    FOREIGN KEY(FK_categoria) REFERENCES categorias(ID_categoria),
    FOREIGN KEY(FK_fornecedor) REFERENCES fornecedores(ID_fornecedor)
);
CREATE TABLE pedidos(
	ID_pedido INT PRIMARY KEY AUTO_INCREMENT,
	data_hora DATETIME DEFAULT CURRENT_TIMESTAMP NOT NULL,
    mesa_cliente varchar(20),
	Status ENUM('Pendente', 'Em Preparo', 'Pronto', 'Entregue', 'Cancelado') NOT NULL DEFAULT 'Pendente', 
	total_pedido DECIMAL(10,2) NOT NULL,
	metodo_pagamento ENUM('Dinheiro', 'Cartao', 'Pix') NOT NULL
);
CREATE TABLE itens_pedido(
	ID_item INT PRIMARY KEY AUTO_INCREMENT,
	quantidade INT NOT NULL,
	subtotal DECIMAL(10,2) NOT NULL,
	FK_pedido INT,
	FK_produto INT,
	
	FOREIGN KEY(FK_pedido) REFERENCES pedidos(ID_pedido),
	FOREIGN KEY(FK_produto) REFERENCES produtos(ID_produto)
);

-- INSERT
INSERT INTO categorias (nome, descricao) VALUES 
('Lanche', 'Hambúrgueres artesanais e sanduíches quentes'),
('Bebida', 'Sucos naturais, refrigerantes e água');

INSERT INTO fornecedores (nome_empresa, cnpj_cpf, telefone, email) VALUES 
('Distribuidora Oxente', '12.345.678/0001-90', '(71) 99999-9999', 'contato@distribuidoraoxente.com'),
('Frigorífico Boi na Brasa', '98.765.432/0001-10', '(71) 98888-7777', 'vendas@boinabrasa.com.br'),
('Hortifruti Frescor da Terra', '45.678.912/0001-33', '(71) 97777-6666', 'pedidos@frescor.com.br'),
('Panificadora Central', '11.222.333/0001-44', '(71) 3344-5566', 'paes@panicentral.com'),
('Mega Bebidas Distribuidora', '22.333.444/0001-55', '(71) 3322-1100', 'comercial@megabebidas.com');

INSERT INTO produtos (nome_item, preco_unitario, estoque_atual, descricao, FK_categoria, FK_fornecedor) VALUES 
('X-Bacon', 25.00, 50, 'Hambúrguer artesanal, bacon crocante, queijo e molho especial', 1, 1),
('X-Salada', 22.00, 50, 'Hambúrguer suculento, queijo, alface, tomate e maionese da casa', 1, 1),
('X-Frango', 23.50, 40, 'Filé de frango grelhado, queijo, alface e molho especial', 1, 1),
('X-Tudo', 30.00, 30, 'Hambúrguer, bacon, ovo, presunto, queijo, alface e tomate', 1, 1),
('Cheeseburger', 20.00, 60, 'Hambúrguer clássico com queijo derretido e pão macio', 1, 1),
('Misto Quente', 12.50, 100, 'Pão Artesanal, Queijo, Presunto', 1, 1),
('Refrigerante Lata', 6.00, 200, 'Lata 350ml', 2, 1),
('Suco de Laranja', 7.00, 80, 'Suco natural 400ml', 2, 1),
('Suco de Abacaxi', 7.00, 80, 'Suco natural 400ml', 2, 1),
('Suco de Maracujá', 7.00, 80, 'Suco natural 400ml', 2, 1),
('Água Mineral', 4.00, 150, 'Garrafa 500ml sem gás', 2, 1),
('Água com Gás', 4.50, 150, 'Garrafa 500ml com gás', 2, 1),
('Suco de Acerola', 8.00, 60, 'Suco natural 400ml rico em Vitamina C', 2, 1);

INSERT INTO pedidos (mesa_cliente, Status, total_pedido, metodo_pagamento) VALUES 
('Mesa 01', 'Entregue', 31.00, 'Pix'),
('Mesa 05', 'Entregue', 50.00, 'Cartao'),
('Mesa 02', 'Pronto', 22.00, 'Dinheiro'),
('Balcão - João', 'Entregue', 12.50, 'Pix'),
('Mesa 03', 'Em Preparo', 60.00, 'Cartao'),
('Mesa 10', 'Pendente', 25.00, 'Cartao'),
('Mesa 04', 'Cancelado', 0.00, 'Dinheiro'),
('Mesa 01', 'Entregue', 47.00, 'Pix'),
('Mesa 07', 'Em Preparo', 23.50, 'Cartao'),
('Balcão - Maria', 'Entregue', 10.50, 'Dinheiro');

INSERT INTO itens_pedido (FK_pedido, FK_produto, quantidade, subtotal) VALUES 
(1, 1, 1, 25.00), (1, 7, 1, 6.00),   -- Itens do Pedido 1
(2, 2, 2, 44.00), (2, 7, 1, 6.00),   -- Itens do Pedido 2
(3, 2, 1, 22.00),                    -- Itens do Pedido 3
(4, 6, 1, 12.50),                    -- Itens do Pedido 4
(5, 4, 2, 60.00),                    -- Itens do Pedido 5 (2 X-Tudo)
(6, 1, 1, 25.00),                    -- Itens do Pedido 6
(8, 1, 1, 25.00), (8, 2, 1, 22.00),  -- Itens do Pedido 8
(9, 3, 1, 23.50),                    -- Itens do Pedido 9
(10, 8, 1, 7.00), (10, 12, 1, 3.50); -- Itens do Pedido 10
-- UPDATE
# Desativando safe mode para evitar erros de segurança
SET SQL_SAFE_UPDATES = 0;

# 1. Atualizando Bebidas para o Novo Fornecedor
UPDATE produtos 
SET FK_fornecedor = 5 
WHERE FK_categoria = 2;

# 2. Atualizando Lanches Específicos (Carnes)
UPDATE produtos 
SET FK_fornecedor = 2 
WHERE nome_item IN ('X-Bacon', 'X-Salada', 'X-Tudo');

# 3. Atualizando o Misto Quente (Padaria)
UPDATE produtos 
SET FK_fornecedor = 4 
WHERE nome_item = 'Misto Quente';

# 4. Atualização de Preços (Inflação ou Promoção)
UPDATE produtos 
SET preco_unitario = preco_unitario * 1.10 
WHERE FK_categoria = 1;

-- SELECT
# 1. Auditoria de Suprimentos
SELECT p.nome_item, f.nome_empresa 
FROM produtos p
JOIN fornecedores f ON p.FK_fornecedor = f.ID_fornecedor;

# 2. Espelho de Vendas Detalhado
SELECT 
    p.mesa_cliente, 
    prod.nome_item, 
    ip.quantidade, 
    p.total_pedido,
    p.Status
FROM pedidos p
JOIN itens_pedido ip ON p.ID_pedido = ip.FK_pedido
JOIN produtos prod ON ip.FK_produto = prod.ID_Produto;

# 3. Cardápio Digital (Consulta Simples) 
SELECT 
    p.nome_item AS 'Produto', 
    p.preco_unitario AS 'Preço', 
    c.nome AS 'Categoria'
FROM produtos p
JOIN categorias c ON p.FK_categoria = c.ID_categoria
ORDER BY c.nome, p.nome_item;

# 4. Painel da Cozinha (Filtro por Status)
SELECT 
    ID_pedido AS 'Nº Pedido', 
    mesa_cliente AS 'Local', 
    Status 
FROM pedidos 
WHERE Status IN ('Pendente', 'Em Preparo')
ORDER BY data_hora ASC;

# 5. Faturamento por Método de Pagamento (Agrupamento)
SELECT 
    metodo_pagamento AS 'Forma de Pagamento', 
    SUM(total_pedido) AS 'Total Arrecadado'
FROM pedidos
WHERE Status = 'Entregue'
GROUP BY metodo_pagamento;

# 6. Detalhamento de um Pedido Específico (Relacionamento Completo)
SELECT 
    p.ID_pedido,
    p.mesa_cliente,
    prod.nome_item,
    ip.quantidade,
    prod.preco_unitario AS 'Valor Unit.',
    ip.subtotal
FROM pedidos p
JOIN itens_pedido ip ON p.ID_pedido = ip.FK_pedido
JOIN produtos prod ON ip.FK_produto = prod.ID_Produto
WHERE p.mesa_cliente = 'Mesa 01';

# 7. O "Campeão de Vendas" (Ranking de Produtos)
SELECT 
    prod.nome_item AS 'Produto', 
    SUM(ip.quantidade) AS 'Total Vendido'
FROM itens_pedido ip
JOIN produtos prod ON ip.FK_produto = prod.ID_produto
GROUP BY prod.nome_item
ORDER BY SUM(ip.quantidade) DESC;

# 8. Alerta de Estoque Baixo
SELECT 
    p.nome_item AS 'Produto', 
    p.estoque_atual AS 'Qtd em Estoque', 
    f.nome_empresa AS 'Fornecedor',
    f.telefone AS 'Contato'
FROM produtos p
JOIN fornecedores f ON p.FK_fornecedor = f.ID_fornecedor
WHERE p.estoque_atual < 40;

# 9. Contagem de Itens por Categoria 
SELECT 
    c.nome AS 'Categoria', 
    COUNT(p.ID_Produto) AS 'Total de Produtos'
FROM categorias c
LEFT JOIN produtos p ON c.ID_categoria = p.FK_categoria
GROUP BY c.nome;

# 8. VIEW: Relatório de Vendas Ativas
CREATE VIEW vw_relatorio_pedidos AS
SELECT 
    p.ID_pedido AS 'Pedido',
    p.data_hora AS 'Horario',
    p.mesa_cliente AS 'Mesa',
    prod.nome_item AS 'Produto',
    ip.quantidade AS 'Qtd',
    p.Status AS 'Status_Pedido'
FROM pedidos p
JOIN itens_pedido ip ON p.ID_pedido = ip.FK_pedido
JOIN produtos prod ON ip.FK_produto = prod.ID_Produto;

-- DELETE
# 1. Removendo um item de um pedido (Ex: Remover o produto ID 7 do pedido ID 1)
DELETE FROM itens_pedido 
WHERE FK_pedido = 1 AND FK_produto = 7;

# 2. Deletando um pedido específico (Mesa 04 que estava cancelada)
# Primeiro removemos os itens (se existirem) para não dar erro de FK
DELETE FROM itens_pedido WHERE FK_pedido = 7;
# Depois removemos o cabeçalho do pedido
DELETE FROM pedidos WHERE ID_pedido = 7;

# 3. Removendo um produto do catálogo
DELETE FROM produtos 
WHERE nome_item = 'Suco de Acerola';

# Reativando o Safe mode também por motivo de segurança(Opcional)
SET SQL_SAFE_UPDATES = 1;