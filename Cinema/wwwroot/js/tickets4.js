var posti = '.cinema-seats .seat';

$(posti).on('click', function () {
    $(this).toggleClass('active');
});