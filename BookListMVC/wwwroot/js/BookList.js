let dataTable;

(function () {
    loadDataTable();
})();

function loadDataTable() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/Books/getall",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "25%" },
            { "data": "author", "width": "25%" },
            { "data": "isbn", "width": "25%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href="/Books/Upsert?id=${data}" class="btn btn-success text-white" style="cursor: pointer; width: 100px; margin: 10px 0;">
                            Edit
                        </a>
                        &nbsp;
                        <a class="btn btn-danger text-white" style="cursor: pointer; width: 100px;margin: 10px 0;"
                            onclick=Delete('/Books/Delete?id='+${data})
                        >
                            Delete
                        </a>
                    </div>
                    `
                },
                "width": "25%",
            }
        ],
        "language": {
            "emptyTable": "No data available in table"
        },
        "width": "100%"
    });
}

//delete action (ref line 27)
function Delete(url) {
    swal({
        title: 'Are you sure?',
        text: 'Once deleted, you will not be able to recover',
        icon: 'warning',
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}
