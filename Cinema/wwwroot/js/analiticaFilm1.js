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
            { data: "film", width: "50%" },
            { data: "guadagni", width: "50%" },
        ]
    });
}