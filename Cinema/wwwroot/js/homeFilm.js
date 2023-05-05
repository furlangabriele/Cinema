var dataTable;
//esempio tratto da: https://jsfiddle.net/gyrocode/mo6eLak4/ con alcuni cambiamenti
$(document).ready(function () {
    //https://stackoverflow.com/questions/574944/how-to-load-up-css-files-using-javascript
    var cssId = 'myCss';  // you could encode the css path itself to generate id..
    if (!document.getElementById(cssId)) {
        var head = document.getElementsByTagName('head')[0];
        var link = document.createElement('link');
        link.id = cssId;
        link.rel = 'stylesheet';
        link.type = 'text/css';
        link.href = '/css/homeFilm.css';
        link.media = 'all';
        head.appendChild(link);
    }

    dataTable = $('#tblData')

        .DataTable({
            //https://datatables.net/reference/option/dom
            dom:
                "<'row'<'col-sm-12 col-md-6'l><'col-sm-12 col-md-6'<'float-md-right ml-2'B>f>>" +
                "<'row'<'col-sm-12'tr>>" +
                "<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7'p>>",
            language: {
                url: "https://cdn.datatables.net/plug-ins/1.13.2/i18n/it-IT.json"
            },
            //l'oggetto ajax di DataTable può usare tutte le properties di AJAX di Jquery'
            //https://www.tutorialsteacher.com/jquery/jquery-ajax-method
            //https://datatables.net/manual/ajax
            ajax: {
                url: "/Admin/Film/GetAll",
                //deferRender: true,
            },
            //select richiede il pacchetto select di DataTable
            //
            /* select: 'single',*/
            columns: [
                {
                    //https://datatables.net/reference/option/columns.orderable
                    orderable: false,
                    data: "urlImmPub",
                    className: 'text-center',
                    render: function (data, type, full, meta) {
                        return ` 
                          <img src="${data}" alt="Image Url" class="avatar">
                              `
                    }
                },
                { data: "titolo" },
                {
                    data: "durata",
                    render: function (data) {
                        return '<p class="text-info">Durata: ' + data + '</p>';
                    }
                },
                {
                    data: "annoProd",
                    render: function (data) {
                        return '<p class="text-info">Anno di produzione: '+ data + '</p>';
                    }
                },
                {
                    data: "titolo",
                    orderable: false,
                    className: 'align-middle',
                    render: function (data) {
                        //una multiline string deve essere racchiusa tra backtick
                        //il backtick si può ottenere sulla tastiera italiana con ALT+96
                        //https://superuser.com/questions/667622/italian-keyboard-entering-tilde-and-backtick-characters-without-changin
                        //tra i backtick mettiamo il codice HTML che deve essere renderizzato all'interno della colonna di DataTable
                        return `
                  <a href="/Customer/Home/Details/${data}" class="btn btn-primary form-control">Maggiori informazioni</a>
                  `
                    }
                },
            ],
            //https://datatables.net/reference/option/initComplete
            'initComplete': function (settings, json) {
                ////var api = this.api();
                //commuto in card view
                //$(api.table().node()).toggleClass('cards');
                //$('.bi', api.table().node()).toggleClass(['bi-table', 'bi-person-badge']);
                //ricarico i dati per rendere possibile una visualizzazione corretta delle card
                $('#tblData').DataTable().ajax.reload();


            },
            'drawCallback': function (settings) {
                var api = this.api();
                var $table = $(api.table().node());
                if ($table.hasClass('cards')) {

                    // Create an array of labels containing all table headers
                    var labels = [];
                    $('thead th', $table).each(function () {
                        labels.push($(this).text());
                    });

                    var max = 0;
                    $('tbody tr', $table).each(function () {
                        max = Math.max($(this).height(), max);
                    }).height(max);

                } else {
                    // Remove data-label attribute from each cell
                    $('tbody td', $table).each(function () {
                        $(this).removeAttr('data-label');
                    });

                    $('tbody tr', $table).each(function () {
                        $(this).height('auto');
                    });
                }
            }
        })
    //.on('select', function (e, dt, type, indexes) {
    //    var rowData = table.rows(indexes).data().toArray()
    //    $('#row-data').html(JSON.stringify(rowData));
    //})
    //.on('deselect', function () {
    //    $('#row-data').empty();
    //})
});
