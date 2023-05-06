var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Analitica/GetAnaliticaFilm"
        },
        "columns": [
            { data: "film", width: "33%" },
            { data: "guadagni", width: "33%" },
            { data: "numBigliettiAcq", width: "50%" },
        ]
    });
}