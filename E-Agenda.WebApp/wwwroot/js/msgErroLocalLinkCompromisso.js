document.querySelector('form').addEventListener('submit', function (e) {
    let tipo = document.querySelector('input[name="Tipo"]:checked')?.value;
    let input = document.getElementById('localOuLinkInput');

    if (input.value.trim() === "") {
        e.preventDefault();
        let errorSpan = document.querySelector('[data-valmsg-for="LocalOuLink"]');

        if (tipo === 'Presencial') {
            errorSpan.textContent = 'O campo "Local" é obrigatório para compromissos presenciais.';
        } else if (tipo === 'Remoto') {
            errorSpan.textContent = 'O campo "Link" é obrigatório para compromissos remotos.';
        }
    }
});
