var srPlay = function () { // Start of use strict

    // Initialize and Configure Scroll Reveal Animation
    window.sr = ScrollReveal();

    sr.reveal('.sr-map', {
    origin: "left",
    duration: 500,
    easing: 'ease-in-out',
    scale: 1,
    distance: '50px'
    }, 200);

    sr.reveal('.sr-zero-btn1', {
        origin: "right",
        duration: 500,
        easing: 'ease-in-out',
        scale: 1,
        distance: '80px'
    }, 200);

    sr.reveal('.sr-zero-btn2', {
        origin: "right",
        duration: 550,
        easing: 'ease-in-out',
        scale: 1,
        distance: '80px'
    }, 200);

    sr.reveal('.sr-zero-btn3', {
        origin: "right",
        duration: 600,
        easing: 'ease-in-out',
        scale: 1,
        distance: '80px'
    }, 200);

    sr.reveal('.sr-zero-btn4', {
        origin: "right",
        duration: 650,
        easing: 'ease-in-out',
        scale: 1,
        distance: '80px'
    }, 200);

    sr.reveal('.sr-zero-btn5', {
        origin: "bottom",
        duration: 500,
        easing: 'ease-in-out',
        scale: 1,
        distance: '50px'
    }, 200);

    sr.reveal('.sr-titleR', {
        origin: "right",
        duration: 500,
        easing: 'ease-in-out',
        scale: 1,
        reset: true,
        distance: '80px'
    }, 200);

    sr.reveal('.sr-bottom', {
        origin: "bottom",
        duration: 500,
        easing: 'ease-in-out',
        scale: 1,
        reset: true,
        distance: '80px'
    }, 200);
};

srPlay();