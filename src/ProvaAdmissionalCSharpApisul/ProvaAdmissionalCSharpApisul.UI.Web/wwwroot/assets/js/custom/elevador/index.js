async function enviarArquivo(frm) {
    try {
        $(".loading").fadeIn("slow");
        const response = await fetch(frm.action, {
            method: 'POST',
            body: new FormData(frm)
        });

        if (response.ok) {
            $("#frmUpload").each(function () { this.reset(); });
            carregarEstatisticas();
            toastr.success("Operação realizada com sucesso!");
        }
        else {
            toastr.error("Não foi possível concluir a operação");
        }
    } catch (error) {
        toastr.error(error);
    }
    finally {
        $(".loading").fadeOut("slow");
    }
}

carregarEstatisticas();
function carregarEstatisticas() {
    $.get("/Elevador/Elevador/ObterEstatisticas", function (retorno) {
        $("#dvEstatisticas").empty();
        $("#dvEstatisticas").append(retorno);
    });
}

$("#btnExcluir").on("click", function () {
    bootbox.confirm({
        message: 'Deseja realmente excluir todos os registros da base de dados ?',
        buttons: {
            confirm: {
                label: 'Sim',
                className: 'btn-success'
            },
            cancel: {
                label: 'Não',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            if (result) {
                $(".loading").fadeIn("slow");
                $.get("/Elevador/Elevador/Excluir", function (retorno) {
                    if (retorno.flSucesso) {
                        carregarEstatisticas();
                        toastr.success(retorno.mensagem);
                    }
                    else {
                        toastr.error(retorno.mensagem);
                    }
                    $(".loading").fadeOut("slow");
                });
            }
        }
    });
});

setInterval(function () { carregarEstatisticas() }, 60000);
