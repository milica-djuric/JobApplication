var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/JobApplication/GetAllCandidates/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "nameSurename", "width": "20%" },
            {"data": "email", "width": "20%"},
            { "data": "positions", "width": "40%" },
            {
                "data": "candidateID",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href="/JobApplication/Upsert?id=${data}" class='btn btn-success text-white' style='cursor:pointer; width:40%;'>
                            Edit
                        </a>
                        &nbsp;
                        <a class='btn btn-danger text-white' style='cursor:pointer; width:40%;'
                            onclick=Delete('/JobApplication/Delete?id='+${data})>
                            Delete
                        </a>
                        </div>`;
                }, "width": "20%"
            }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"
    });
}

function Delete(url) {
    swal({
        title: "Are you sure?",
        text: "Deleted candidates could not be restored!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success("The candidate is successfully deleted!");
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error("Error while deleting a candidate!");
                    }
                }
            });
        }
    });
}