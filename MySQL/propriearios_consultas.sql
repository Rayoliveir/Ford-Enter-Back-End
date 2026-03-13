-- SELECT * FROM proprietarios;

#  top 10 proprietários com mais hospedagens ativas na plataforma
SELECT p.nome AS nome_proprietario, COUNT(h.hospedagem_id)
AS total_hospedagens_ativas
FROM proprietarios p
JOIN hospedagens h ON p.proprietario_id = h.proprietario_id
WHERE h.ativo = 1
GROUP BY p.nome
ORDER BY total_hospedagens_ativas DESC
LIMIT 10;

#  número de hospedagens inativas por proprietário
SELECT p.nome AS nome_proprietario, COUNT(*) AS total_hospedagens_inativas
FROM proprietarios p
JOIN hospedagens h ON p.proprietario_id = h.proprietario_id
WHERE h.ativo = 0 
GROUP BY p.nome;