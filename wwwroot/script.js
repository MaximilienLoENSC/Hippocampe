const apiUrl = 'api/film';
let films = [];

function getFilms() {
    fetch(apiUrl)
        .then(res => res.json())
        .then(data => {
            films = data;
            displayFilms();
        })
        .catch(err => console.error('Erreur de récupération :', err));
}

function addFilm() {
    const film = collectFormData();

    fetch(apiUrl, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(film)
    })
    .then(() => {
        clearForm();
        getFilms();
    })
    .catch(err => console.error('Erreur ajout :', err));
}

function deleteFilm(id) {
    fetch(`${apiUrl}/${id}`, { method: 'DELETE' })
        .then(() => getFilms())
        .catch(err => console.error('Erreur suppression :', err));
}

function displayEditForm(id) {
    const film = films.find(f => f.id === id);

    document.getElementById('edit-id').value = film.id;
    document.getElementById('edit-titre').value = film.titre;
    document.getElementById('edit-date').value = film.date_de_sortie.split('T')[0];
    document.getElementById('edit-genres').value = film.genres.join(', ');
    document.getElementById('edit-pays').value = film.pays.join(', ');
    document.getElementById('edit-realisateurs').value = film.realisateurs.join(', ');
    document.getElementById('edit-acteurs').value = film.acteurs.join(', ');
    document.getElementById('edit-compositeurs').value = film.compositeurs.join(', ');
    document.getElementById('edit-commentaire').value = film.commentaire;

    document.getElementById('editForm').style.display = 'block';
}

function updateFilm() {
    const id = parseInt(document.getElementById('edit-id').value);
    const film = {
        id: id,
        titre: document.getElementById('edit-titre').value,
        date_de_sortie: document.getElementById('edit-date').value,
        genres: splitList('edit-genres'),
        pays: splitList('edit-pays'),
        realisateurs: splitList('edit-realisateurs'),
        acteurs: splitList('edit-acteurs'),
        compositeurs: splitList('edit-compositeurs'),
        commentaire: document.getElementById('edit-commentaire').value
    };

    fetch(`${apiUrl}/${id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(film)
    })
    .then(() => {
        closeEdit();
        getFilms();
    })
    .catch(err => console.error('Erreur modification :', err));
}

function closeEdit() {
    document.getElementById('editForm').style.display = 'none';
}

function displayFilms() {
    const tbody = document.getElementById('film-list');
    tbody.innerHTML = '';

    films.forEach(film => {
        const row = tbody.insertRow();

        row.insertCell().textContent = film.titre;
        row.insertCell().textContent = new Date(film.date_de_sortie).toLocaleDateString();
        row.insertCell().textContent = film.genres.join(', ');
        row.insertCell().textContent = film.pays.join(', ');
        row.insertCell().textContent = film.realisateurs.join(', ');
        row.insertCell().textContent = film.acteurs.join(', ');
        row.insertCell().textContent = film.compositeurs.join(', ');
        row.insertCell().textContent = film.commentaire;

        const actionsCell = row.insertCell();
        const editBtn = document.createElement('button');
        editBtn.textContent = 'Modifier';
        editBtn.onclick = () => displayEditForm(film.id);

        const deleteBtn = document.createElement('button');
        deleteBtn.textContent = 'Supprimer';
        deleteBtn.onclick = () => deleteFilm(film.id);

        actionsCell.appendChild(editBtn);
        actionsCell.appendChild(deleteBtn);
    });
}

function collectFormData() {
    return {
        titre: document.getElementById('titre').value,
        date_de_sortie: document.getElementById('date_de_sortie').value,
        genres: splitList('genres'),
        pays: splitList('pays'),
        realisateurs: splitList('realisateurs'),
        acteurs: splitList('acteurs'),
        compositeurs: splitList('compositeurs'),
        commentaire: document.getElementById('commentaire').value
    };
}

function clearForm() {
    document.querySelectorAll("form input[type='text'], form input[type='date']").forEach(el => el.value = '');
}

function splitList(id) {
    return document.getElementById(id).value
        .split(',')
        .map(v => v.trim())
        .filter(v => v);
}

window.onload = getFilms;
