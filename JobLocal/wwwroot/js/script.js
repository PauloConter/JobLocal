// JobLocal - JavaScript Simplificado
class JobLocalApp {
    constructor() {
        this.init();
    }

    init() {
        console.log('JobLocal App inicializado');
        this.setupEventListeners();
    }

    setupEventListeners() {
        // Sistema de avaliação por estrelas
        this.setupStarRating();

        // Filtro de vagas
        const btnFiltrar = document.getElementById('btnFiltrar');
        if (btnFiltrar) {
            btnFiltrar.addEventListener('click', () => this.filtrarVagas());
        }
    }

    setupStarRating() {
        const starRatings = document.querySelectorAll('.star-rating');
        starRatings.forEach(rating => {
            const stars = rating.querySelectorAll('input');
            stars.forEach(star => {
                star.addEventListener('change', function () {
                    const ratingValue = this.value;
                    console.log('Avaliação:', ratingValue);
                });
            });
        });
    }

    filtrarVagas() {
        const cidade = document.getElementById('cidade')?.value || '';
        const servico = document.getElementById('tipoServico')?.value || '';

        // Em uma aplicação real, isso faria uma requisição para o backend
        console.log('Filtrando vagas:', { cidade, servico });

        // Simulação de filtro
        const vagasCards = document.querySelectorAll('.vaga-card');
        vagasCards.forEach(card => {
            const cardCidade = card.querySelector('.cidade')?.textContent.toLowerCase() || '';
            const cardServico = card.querySelector('.servico')?.textContent || '';

            const matchCidade = !cidade || cardCidade.includes(cidade.toLowerCase());
            const matchServico = !servico || cardServico === servico;

            card.style.display = (matchCidade && matchServico) ? 'block' : 'none';
        });
    }

    // Função para processar pagamento
    processarPagamento(metodo) {
        alert(`Pagamento via ${metodo} processado com sucesso!`);
        return true;
    }

    // Função para mostrar notificações
    showNotification(mensagem, tipo = 'info') {
        const notification = document.createElement('div');
        notification.className = `notification notification-${tipo}`;
        notification.innerHTML = `
            <div class="notification-content">
                <span class="notification-message">${mensagem}</span>
                <button class="notification-close" onclick="this.parentElement.parentElement.remove()">×</button>
            </div>
        `;

        // Estilos para a notificação
        notification.style.cssText = `
            position: fixed;
            top: 20px;
            right: 20px;
            background: white;
            padding: 1rem;
            border-radius: 8px;
            box-shadow: 0 4px 12px rgba(0,0,0,0.15);
            z-index: 10000;
            border-left: 4px solid ${this.getNotificationColor(tipo)};
            animation: slideIn 0.3s ease-out;
        `;

        document.body.appendChild(notification);

        setTimeout(() => {
            if (notification.parentElement) {
                notification.remove();
            }
        }, 5000);
    }

    getNotificationColor(tipo) {
        const colors = {
            success: '#28a745',
            error: '#dc3545',
            warning: '#ffc107',
            info: '#17a2b8'
        };
        return colors[tipo] || colors.info;
    }
}

// Inicializar aplicação quando o DOM estiver carregado
document.addEventListener('DOMContentLoaded', function () {
    window.jobLocal = new JobLocalApp();

    // Adicionar CSS para animações
    const style = document.createElement('style');
    style.textContent = `
        @keyframes slideIn {
            from {
                transform: translateX(100%);
                opacity: 0;
            }
            to {
                transform: translateX(0);
                opacity: 1;
            }
        }
        
        .notification-close {
            background: none;
            border: none;
            font-size: 1.5rem;
            cursor: pointer;
            margin-left: 1rem;
            color: #6c757d;
        }
        
        .notification-content {
            display: flex;
            align-items: center;
            justify-content: space-between;
        }
    `;
    document.head.appendChild(style);
});

// Funções globais para uso nos onclick
function processarPagamento() {
    const metodo = document.querySelector('input[name="metodo"]:checked').value;
    if (window.jobLocal && window.jobLocal.processarPagamento) {
        window.jobLocal.processarPagamento(metodo);
    } else {
        alert(`Pagamento via ${metodo} processado com sucesso!`);
    }
}