const API_URL = window.location.origin + '/api';
let todosProdutos = [];
let todosPedidos = [];

document.addEventListener("DOMContentLoaded", () => {
    inicializarSistema();
});

async function inicializarSistema() {
    await carregarProdutos();
    await carregarPedidosEMonitores();
    carregarBI();
}

// ALTERNADOR DE ABAS
function switchTab(tabId) {
    document.querySelectorAll('.tab-content').forEach(tab => tab.classList.remove('active'));
    document.querySelectorAll('.menu-item').forEach(btn => btn.classList.remove('active'));

    document.getElementById(tabId).classList.add('active');
    event.currentTarget.classList.add('active');

    if (tabId === 'tab-bi') carregarBI();
    if (tabId === 'tab-historico') carregarPedidosEMonitores();
}

// SISTEMA DE NOTIFICAÇÕES (SEM ALERTS)
function showNotification(message, type = 'success') {
    const container = document.getElementById('notification-container');
    const toast = document.createElement('div');
    toast.className = `toast ${type}`;

    const icon = type === 'success' ? 'fa-circle-check' : 'fa-circle-xmark';
    toast.innerHTML = `
        <i class="fa-solid ${icon}"></i>
        <div>${message}</div>
    `;

    container.appendChild(toast);
    setTimeout(() => {
        toast.style.animation = 'fadeIn 0.3s reverse forwards';
        setTimeout(() => toast.remove(), 300);
    }, 4000);
}

// BUSCAR CARDÁPIO E SEPARAÇÃO DE CATEGORIAS
async function carregarProdutos() {
    try {
        const response = await fetch(`${API_URL}/Produtos`);
        if (!response.ok) throw new Error();
        todosProdutos = await response.json();

        const gridLanches = document.getElementById('grid-lanches');
        const gridBebidas = document.getElementById('grid-bebidas');
        const listaSelecao = document.getElementById('lista-selecao-produtos');

        gridLanches.innerHTML = '';
        gridBebidas.innerHTML = '';
        listaSelecao.innerHTML = '';

        todosProdutos.forEach(p => {
            // Renderização no Dashboard
            const card = document.createElement('div');
            card.className = 'product-card';
            card.innerHTML = `
                <div class="product-info">
                    <h4>${p.nome}</h4>
                    <p>${p.descricao || 'Item de alta qualidade Oxente Burguer.'}</p>
                </div>
                <div class="product-price">R$ ${p.preco.toFixed(2)}</div>
            `;

            if (p.categoria.toLowerCase() === 'lanche') {
                gridLanches.appendChild(card);
            } else {
                gridBebidas.appendChild(card);
            }

            // Renderização na caixa de seleção de novos pedidos
            const isLanche = p.categoria.toLowerCase() === 'lanche';
            const itemSelecao = document.createElement('div');
            itemSelecao.className = 'selection-wrapper';
            itemSelecao.innerHTML = `
                <div class="selection-item">
                    <input type="checkbox" id="check-${p.id}" value="${p.id}" onchange="atualizarPreviewPedido()">
                    <label for="check-${p.id}">${p.nome} - R$ ${p.preco.toFixed(2)}</label>
                </div>
                ${isLanche ? `
                    <div class="custom-input-group" id="custom-${p.id}" style="display:none;">
                        <select id="ponto-${p.id}">
                            <option value="Ao Ponto">Ao Ponto</option>
                            <option value="Mal Passada">Mal Passada</option>
                            <option value="Bem Passada">Bem Passada</option>
                        </select>
                        <input type="text" id="add-${p.id}" placeholder="Adicionais (sep. por vírgula)">
                    </div>
                ` : ''}
            `;
            listaSelecao.appendChild(itemSelecao);
        });
    } catch {
        showNotification("Falha crítica ao conectar com catálogo de produtos.", "error");
    }
}

// ATUALIZAR VALOR TOTAL E PREVIEW DO FORMULÁRIO DINAMICAMENTE
function atualizarPreviewPedido() {
    const previewContainer = document.getElementById('itens-selecionados-preview');
    const totalElement = document.getElementById('form-total-calculado');
    previewContainer.innerHTML = '';

    let total = 0;
    let selecionados = false;

    todosProdutos.forEach(p => {
        const checkbox = document.getElementById(`check-${p.id}`);
        const customDiv = document.getElementById(`custom-${p.id}`);

        if (checkbox && checkbox.checked) {
            selecionados = true;
            total += p.preco;
            if (customDiv) customDiv.style.display = 'flex';

            const pText = document.createElement('p');
            pText.style.fontSize = '0.85rem';
            pText.style.marginBottom = '0.2rem';

            if (p.categoria.toLowerCase() === 'lanche') {
                const ponto = document.getElementById(`ponto-${p.id}`).value;
                pText.innerHTML = `<i class="fa-solid fa-caret-right"></i> <strong>${p.nome}</strong> (${ponto})`;
            } else {
                pText.innerHTML = `<i class="fa-solid fa-caret-right"></i> <strong>${p.nome}</strong>`;
            }
            previewContainer.appendChild(pText);
        } else {
            if (customDiv) customDiv.style.display = 'none';
        }
    });

    if (!selecionados) {
        previewContainer.innerHTML = '<p class="empty-text">Nenhum item selecionado</p>';
    }
    totalElement.innerText = `R$ ${total.toFixed(2)}`;
}

// INSERÇÃO DE NOVO PEDIDO
async function criarPedido(event) {
    event.preventDefault();
    const nomeCliente = document.getElementById('cliente-nome').value;
    const itens = [];

    todosProdutos.forEach(p => {
        const checkbox = document.getElementById(`check-${p.id}`);
        if (checkbox && checkbox.checked) {
            const itemData = { produtoId: p.id };
            if (p.categoria.toLowerCase() === 'lanche') {
                itemData.pontoCarne = document.getElementById(`ponto-${p.id}`).value;
                const adicionaisTexto = document.getElementById(`add-${p.id}`).value;
                itemData.adicionais = adicionaisTexto ? adicionaisTexto.split(',').map(s => s.trim()) : [];
            }
            itens.push(itemData);
        }
    });

    if (itens.length === 0) {
        showNotification("Por favor, selecione pelo menos um item do menu.", "error");
        return;
    }

    const payload = { nomeCliente, itens };

    try {
        const response = await fetch(`${API_URL}/Pedidos`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(payload)
        });

        if (response.ok) {
            showNotification("Pedido enviado para a linha de produção com sucesso!");
            document.getElementById('form-pedido').reset();
            atualizarPreviewPedido();
            inicializarSistema();
        } else {
            const errData = await response.json();
            showNotification(errData.mensagem || "Erro na validação do pedido.", "error");
        }
    } catch {
        showNotification("Erro de rede ao enviar pedido.", "error");
    }
}

// ATUALIZAR FLUXO DA COZINHA E FILAS
async function carregarPedidosEMonitores() {
    try {
        const response = await fetch(`${API_URL}/Pedidos`);
        if (!response.ok) throw new Error();
        todosPedidos = await response.json();

        // Organização do Monitor KANBAN
        const divRecebidos = document.getElementById('container-recebidos');
        const divPreparo = document.getElementById('container-preparo');
        const divProntos = document.getElementById('container-prontos');

        divRecebidos.innerHTML = '';
        divPreparo.innerHTML = '';
        divProntos.innerHTML = '';

        todosPedidos.forEach(p => {
            if (p.status > 2) return; // Se já foi entregue ou encerrado, some do painel operacional

            const card = document.createElement('div');
            card.className = `kitchen-card status-${p.status}`;

            let btnLabel = '';
            let proximoStatus = 0;
            if (p.status === 0) { btnLabel = 'Iniciar Preparo'; proximoStatus = 1; }
            else if (p.status === 1) { btnLabel = 'Marcar como Pronto'; proximoStatus = 2; }
            else if (p.status === 2) { btnLabel = 'Finalizar Despacho'; proximoStatus = 3; }

            const listaItens = p.itens.map(i => `<li>- ${i.nome} ${i.pontoCarne ? `(${i.pontoCarne})` : ''}</li>`).join('');

            card.innerHTML = `
                <h5>Cod #${p.numeroPedido} - ${p.nomeCliente}</h5>
                <ul>${listaItens}</ul>
                <strong>Total: R$ ${p.total.toFixed(2)}</strong>
                <button class="btn-action-sm" onclick="mudarStatus(${p.numeroPedido}, ${proximoStatus})">${btnLabel}</button>
            `;

            if (p.status === 0) divRecebidos.appendChild(card);
            if (p.status === 1) divPreparo.appendChild(card);
            if (p.status === 2) divProntos.appendChild(card);
        });

        aplicarFiltroHistorico(); // Atualiza a tabela histórica também
    } catch {
        showNotification("Erro ao sincronizar painel operacional.", "error");
    }
}

// OPERAÇÃO PATCH: FLUXO DE PRODUÇÃO
async function mudarStatus(id, novoStatus) {
    try {
        const response = await fetch(`${API_URL}/Pedidos/${id}/status`, {
            method: 'PATCH',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(novoStatus)
        });

        if (response.ok) {
            showNotification(`Pedido #${id} avançou na linha de produção.`);
            inicializarSistema();
        } else {
            showNotification("Erro operacional ao atualizar status.", "error");
        }
    } catch {
        showNotification("Erro de comunicação com o servidor.", "error");
    }
}

// REMOÇÃO/ESTORNO DE PEDIDO (DELETE)
async function estornarPedido(id) {
    if (!confirm(`Deseja realmente estornar/excluir o pedido #${id}?`)) return;

    try {
        const response = await fetch(`${API_URL}/Pedidos/${id}`, { method: 'DELETE' });
        if (response.ok) {
            showNotification(`Pedido #${id} removido e estornado.`);
            inicializarSistema();
        } else {
            showNotification("Não foi possível excluir o registro.", "error");
        }
    } catch {
        showNotification("Erro na requisição de deleção.", "error");
    }
}

// ABA 3: BUSINESS INTELLIGENCE (MÉTRICAS)
async function carregarBI() {
    try {
        const response = await fetch(`${API_URL}/Relatorios/vendas-geral`);
        if (!response.ok) throw new Error();
        const dados = await response.json();

        document.getElementById('bi-total-pedidos').innerText = dados.totalPedidos;
        document.getElementById('bi-faturamento').innerText = `R$ ${dados.faturamentoTotal.toFixed(2)}`;
        document.getElementById('bi-cancelados').innerText = dados.pedidosCancelados;

        const tabelaProdutos = document.getElementById('bi-table-produtos');
        tabelaProdutos.innerHTML = '';

        if (dados.produtosMaisVendidos.length === 0) {
            tabelaProdutos.innerHTML = '<tr><td colspan="2" class="empty-text">Sem vendas computadas.</td></tr>';
            return;
        }

        dados.produtosMaisVendidos.forEach(p => {
            tabelaProdutos.innerHTML += `
                <tr>
                    <td><strong>${p.nome}</strong></td>
                    <td style="text-align: right;" class="text-success">${p.quantidade} und.</td>
                </tr>
            `;
        });
    } catch {
        showNotification("Erro ao puxar dados analíticos.", "error");
    }
}

// ABA 4: HISTÓRICO COM REGRAS DE FILTRO TEMPORAL
function aplicarFiltroHistorico() {
    const filtro = document.getElementById('filtro-periodo').value;
    const tabelaCorpo = document.getElementById('tabela-historico-corpo');
    tabelaCorpo.innerHTML = '';

    if (todosPedidos.length === 0) {
        tabelaCorpo.innerHTML = '<tr><td colspan="6" class="empty-text">Nenhum registro encontrado.</td></tr>';
        return;
    }

    const labelsStatus = ["Recebido", "Em Preparo", "Pronto", "Entregue"];

    // Nota: Como estamos em um ambiente simulado/in-memory ou SQLite básico sem campos robustos de data, 
    // a filtragem simula a separação por lotes de IDs ou simulação de escopo real.
    let pedidosFiltrados = [...todosPedidos];

    if (filtro === 'hoje') {
        // Exemplo: Simula exibindo os últimos 3 inseridos
        pedidosFiltrados = todosPedidos.slice(-3);
    } else if (filtro === 'mes') {
        pedidosFiltrados = todosPedidos.slice(-10);
    }

    pedidosFiltrados.forEach(p => {
        const tr = document.createElement('tr');
        const statusTexto = labelsStatus[p.status] || "Concluído";
        const itensTexto = p.itens.map(i => i.nome).join(', ');

        tr.innerHTML = `
            <td><strong>#${p.numeroPedido}</strong></td>
            <td>${p.nomeCliente}</td>
            <td><span class="text-muted" style="font-size:0.85rem;">${itensTexto}</span></td>
            <td><strong>R$ ${p.total.toFixed(2)}</strong></td>
            <td><span class="text-warning">${statusTexto}</span></td>
            <td style="text-align: center;">
                <button class="btn-primary" style="padding: 0.3rem 0.6rem; background: var(--brown-red);" onclick="estornarPedido(${p.numeroPedido})">
                    <i class="fa-solid fa-trash-can"></i>
                </button>
            </td>
        `;
        tabelaCorpo.appendChild(tr);
    });
}