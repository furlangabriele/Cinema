sessionStorage.setItem('listabiglietti', JSON.stringify([]));

function AggiungiItem(id) {
    var list = JSON.parse(sessionStorage.listabiglietti);
    list.push(id);
    sessionStorage.setItem('listabiglietti', JSON.stringify(list));
    document.getElementById('id').classList.add('added');
}