﻿/////////////////////////////////////////////////tamanho fonte////////////////////////////////////////

document.addEventListener("DOMContentLoaded", function () {
    const fonteMaior = document.getElementById("fonteMaior");
    const fonteMenor = document.getElementById("fonteMenor");
    const texto = document.getElementById("bulaCompleta");

    fonteMaior.addEventListener("click", function () {
        const fontSize = window.getComputedStyle(texto).fontSize;
        const newSize = parseFloat(fontSize) + 2;
        texto.style.fontSize = newSize + "px";
    });

    fonteMenor.addEventListener("click", function () {
        const fontSize = window.getComputedStyle(texto).fontSize;
        const newSize = parseFloat(fontSize) - 2;
        texto.style.fontSize = newSize + "px";
    });

});



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




function selectOption(option) {
    var buttons = document.querySelectorAll('.button');
    var summaries = document.querySelectorAll('.summary');

    buttons.forEach(function (button) {
        button.classList.remove('selected');
    });

    summaries.forEach(function (summary) {
        summary.style.display = 'none';
    });

    var selectedButton = document.querySelector('.button.option' + option);
    var selectedSummary = document.querySelector('.summary.option' + option);

    selectedButton.classList.add('selected');
    selectedSummary.style.display = 'block';
}


