//Coloca o botão em uma varivel
var btn_subir = $(".btn");


//Faz a primeira verificacao ao carregar a pagina
$(document).ready(function () {
    var minhaposicao = $(this).scrollTop();
    if (minhaposicao >= 100) {
        btn_subir.fadeIn();
    }
    else {
        btn_subir.fadeOut();
    }
});

//Fica monitorando a rolagem de pagina
$(window).scroll(function () {
    var minhaposicao = $(this).scrollTop();

    if (minhaposicao >= 100) {
        btn_subir.fadeIn();
    }
    else {
        btn_subir.fadeOut();
    }
});

btn_subir.click(function () {
    $('html,body').animate({ scrollTop: 0 }, 500);
})