-- SELECT * FROM alugueis;

# períodos de maior e menor demanda de aluguel na plataforma
SELECT YEAR(data_inicio) AS ano,
MONTH(data_inicio) AS mes,
COUNT(*) AS total_alugueis
FROM alugueis
GROUP BY ano, mes
ORDER BY total_alugueis DESC;