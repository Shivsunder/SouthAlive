var moPlay= function () {
    var first = new mojs.Shape({
        shape: 'circle',
        radius: {
            0: 20
        },
        stroke: '#2BBBAD',
        strokeWidth: {
            10: 0
        },
        fill: 'none',
        left: 0,
        top: 0,
        duration: 300
    });
    var seconds = [];
    var colors = ['#B71C1C', '#292b2c', '#ffc107', '#ffe57f'];
    for (var i = 0; i < 4; i++) {
        var second = new mojs.Shape({
            parent: first.el,
            shape: 'circle',
            radius: {
                0: 'rand(5,15)'
            },
            stroke: colors[i],
            strokeWidth: {
                5: 0
            },
            fill: 'none',
            left: '50%',
            top: '50%',
            x: 'rand(-50, 50)',
            y: 'rand(-50, 50)',
            delay: 250
        });
        seconds.push(second);
    }

    document.addEventListener('click', function (e) {
        first.tune({
            x: e.pageX,
            y: e.pageY
        }).replay();
        for (var i = 0; i < seconds.length; i++) {
            seconds[i].generate().replay();
        }
    });
};

if (window.innerWidth > 760) {
    moPlay();
}