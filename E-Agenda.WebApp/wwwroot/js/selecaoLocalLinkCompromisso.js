document.addEventListener('DOMContentLoaded', function () {
    let tipo = document.querySelectorAll('input[name="Tipo"]');
    let label = document.getElementById('localOuLinkLabel');
    let input = document.getElementById('localOuLinkInput');

    function updateLabel() {
        let selected = document.querySelector('input[name="Tipo"]:checked');
        if (!selected) return;

        if (selected.value === 'Presencial') {
            label.textContent = 'Local*';
            input.placeholder = 'Digite um local...';
        }
        else if (selected.value === 'Remoto') {
            label.textContent = 'Link*';
            input.placeholder = 'Digite um link...';
        }
    }

    tipo.forEach(function (radio) {
        radio.addEventListener('change', updateLabel);
    });

    updateLabel();
})