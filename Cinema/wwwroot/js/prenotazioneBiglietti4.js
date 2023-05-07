var listabiglietti = [];

function AggiungiItem(id) {
    if (listabiglietti.length >= 4) {
        alert("Puoi acquistare massimo 4 biglietti per uno spettacolo!");
        return;
    }
    var class_ = document.getElementById(id).classList;
    if (class_ == 'seat added') {
        const index = listabiglietti.indexOf(id);
        if (index > -1) { // only splice array when item is found
            listabiglietti.splice(index, 1); // 2nd parameter means remove one item only
        }
        document.getElementById(id).classList.toggle('added');
    } else if (class_ != 'seat active') {
        listabiglietti.push(id);
        document.getElementById(id).classList.toggle('added');
    }
}

function CreaBiglietti(id_spettacolo) {
    var posto_fila = listabiglietti.join(';');
    fetch("/Customer/Biglietto/CreaBiglietto?posto_fila=" + posto_fila + "&id_spettacolo=" + id_spettacolo, {
        method: 'POST',
    })
    .then(res => {
        if (res.ok) {
            console.log("success");
            window.location.reload();
        } else {
            console.log("error");
        }
    });
}