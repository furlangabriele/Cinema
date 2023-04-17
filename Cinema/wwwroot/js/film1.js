var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    console.log("request start");
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url":"/Admin/Film/GetAll"
        },
        "columns": [
            { data: "titolo", width: "15%" },
            { data: "genere", width: "15%" },
            { data: "descrizione", width: "15%" },
            { data: "durata", width: "15%" },
            { data: "annoProd", width: "15%" },
            {
                data: "titolo",
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                        <a href="/Admin/Film/Upsert?id=${encodeURI(data)}"
                        class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edit</a>
                        <a onClick=Delete('/Admin/Film/Delete/${encodeURI(data)}')
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