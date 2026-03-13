-- COMANDOS DE ALTERAÇÃO

-- ALTERAR
# Adicionando a coluna de qtd_hospedagens a tabela proprietarios
ALTER TABLE proprietarios 
ADD COLUMN qtd_hospedagens INT;

# Alterando nome da tabela alugueis para reservas
ALTER TABLE alugueis RENAME TO reservas;

# Alterando nome da coluna da tabela reservas
ALTER TABLE reservas RENAME COLUMN aluguel_id TO reserva_id;

-- ATUALIZAR
# atualizando tabela de hospedagens
UPDATE hospedagens
SET ativo = 1
WHERE hospedagem_id IN ('1', '10', '100');

SELECT * FROM hospedagens;

# atualizamos a informação de contato de uma pessoa proprietária na tabela de proprietarios
UPDATE proprietarios
SET contato = 'daniela_120@email.com'
WHERE proprietario_id = '1009';

SELECT * FROM proprietarios;

-- DELETAR

# apagamos os dados de duas hospedagens do banco de dados
DELETE FROM avaliacoes
WHERE hospedagem_id IN ('10000','1001');

DELETE FROM reservas
WHERE hospedagem_id IN ('10000','1001');

DELETE FROM hospedagens
WHERE hospedagem_id IN ('10000','1001');

