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


//////////////////////////////////////////////////////////


const videoPlayer = document.getElementById("videoPlayer");

videoPlayer.addEventListener('timeupdate', function (event) {
    if (videoPlayer.currentTime >= 3) { //defina traves de segundos. Ex: 120s = 2 minutos do video
        document.querySelector('.text_baixar').style.display = "block";
    } else {
        document.querySelector('.text_baixar').style.display = "none";

     
    }
});


$('.switch label').on('click', function () {
    var indicator = $(this).parent('.switch').find('span');
    if ($(this).hasClass('right')) {
        $(indicator).addClass('right');
    } else {
        $(indicator).removeClass('right');
    }
});

//////////////////////////////////////////////////////////

function trocarTexto() {
    var botao = document.getElementById("meuBotao");
    var texto1 = document.getElementById("texto1");
    var texto2 = document.getElementById("texto2");

    if (texto1.style.display !== "none") {
        texto1.style.display = "none";
        texto2.style.display = "block";
        botao.innerHTML = "Clique novamente";
    } else {
        texto1.style.display = "block";
        texto2.style.display = "none";
        botao.innerHTML = "Clique aqui";
    }
}
