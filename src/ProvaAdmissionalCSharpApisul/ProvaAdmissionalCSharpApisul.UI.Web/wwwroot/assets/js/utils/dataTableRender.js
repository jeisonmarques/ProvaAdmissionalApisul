function DataTable_Render(identificador, pageLength = 100) {
    $(identificador).dataTable({
        //dom: "<'row'<'col-md-12'tr>>",
        stateSave: true,
        pageLength: pageLength,
        "pagingType": "full_numbers",
        "paging": true,
        "ordering": true,
        "order": [],
        "columnDefs": [{
            "targets": 'no-sort',
            "orderable": false
        }],
        "autoWidth": false,
        "stateDuration": 60 * 60 * 24,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Portuguese-Brasil.json"
        },
        initComplete: function () {

            this.api().columns().every(function () {

                var column = this;
                var columnIndex = this.index();

                switch ($(".filter th:eq(" + columnIndex + ")").attr('class')) {

                    case "dropdownlist":

                        var select = $('<select class="form-control filtros"><option value="">-- Filtrar --</option></select>')
                            .appendTo($(".filter th:eq(" + columnIndex + ")").empty())
                            .on('change', function () {
                                var val = $.fn.dataTable.util.escapeRegex(
                                    $(this).val()
                                );

                                column
                                    .search(val ? '^' + val + '$' : '', true, false)
                                    .draw();
                            });

                        $(select).click(function (e) {
                            e.stopPropagation();
                        });

                        column.data().unique().sort().each(function (d, j) {

                            var val = $.fn.dataTable.util.escapeRegex(d);
                            if (column.search() === "^" + val + "$") {
                                select.append(
                                    '<option value="' + d + '" selected="selected">' + d + "</option>"
                                );
                            } else {
                                select.append('<option value="' + d + '">' + d + "</option>");
                            }
                        });

                        //$(".filtros").selectpicker();

                        break;

                    case 'search':
                        $(".filter th:eq(" + columnIndex + ")").html('<input type="text" class="form-control" />');

                        $('input', $(".filter th:eq(" + columnIndex + ")")).on('keyup change', function () {
                            if (column.search() !== this.value) {
                                column
                                    .search(this.value)
                                    .draw();
                            }
                        });
                        break;
                }
            });
        }
    });
}  