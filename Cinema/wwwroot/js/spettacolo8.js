var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    console.log("request start");
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Spettacolo/GetAll"
        },
        "columns": [
            { data: "fkSala", width: "15%" },
            { data: "fkFilm", width: "15%" },
            { data: "data", width: "15%" },
            { data: "orario", width: "15%" },
            {
                data: { fkSala: "fkSala", fkFilm: "fkFilm", data: "data", orario: "orario"},
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                        <a href="/Admin/Spettacolo/Upsert?fkFilm=${encodeURI(data.fkFilm)}&fkSala=${data.fkSala}&data=${data.data}&orario=${data.orario}"
                        class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edit</a>
                        <a onClick=Delete('/Admin/Spettacolo/Delete?fkFilm=${encodeURI(data.fkFilm)}&fkSala=${data.fkSala}&data=${data.data}&orario=${data.orario}')
                        class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a>
					</div>
                        `
                },
                "width": "15%"
            }
        ]
    });
    console.log("request end");
}

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}