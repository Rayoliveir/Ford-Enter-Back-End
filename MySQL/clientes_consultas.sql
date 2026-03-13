-- SELECT * FROM clientes;

# Calculamos o gasto médio de cada cliente dentro da plataforma
SELECT cliente_id, AVG(preco_total) AS ticket_medio
FROM alugueis
GROUP BY cliente_id; 

# média de dias de estadia de cada cliente
SELECT cliente_id, AVG(DATEDIFF(data_fim, data_inicio)) AS media_dias_estadia
FROM alugueis
GROUP BY cliente_id
ORDER BY media_dias_estadia DESC;