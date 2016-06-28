/*!
 * Start Bootstrap - Grayscale Bootstrap Theme (http://startbootstrap.com)
 * Code licensed under the Apache License v2.0.
 * For details, see http://www.apache.org/licenses/LICENSE-2.0.
 */

// jQuery to collapse the navbar on scroll
function collapseNavbar() {
    if ($(".navbar").offset().top > 50) {
        $(".navbar-fixed-top").addClass("top-nav-collapse");
    } else {
        $(".navbar-fixed-top").removeClass("top-nav-collapse");
    }
}

$(window).scroll(collapseNavbar);
$(document).ready(collapseNavbar);

$("#submit").click(function(event){
    event.preventDefault();
    alert("Handler for .click() called.");
});

// jQuery for page scrolling feature - requires jQuery Easing plugin
$(function() {
    $('a.page-scroll').bind('click', function(event) {
        var $anchor = $(this);
        $('html, body').stop().animate({
            scrollTop: $($anchor.attr('href')).offset().top
        }, 1500, 'easeInOutExpo');
        event.preventDefault();
    });

     $.get("https://olympicsmeter.azurewebsites.net/api/country?code=x3v5zkb7ebm7ct2djx20529a8yjvy6axruml5b5niphnnrk9l8g8grwis1x6qmqqqvo9od2t9", function (result) {
        
        result.sort(function(a, b) {
            return parseFloat(b.Score) - parseFloat(a.Score);
        });       

        new Morris.Bar({
            element: 'chart',
            data: result.slice(0,7),
            xkey: 'Name',
            ykeys: ['Score'],
            barColors: ["#009E3D"],
            labels: ['Score']
        });

        $.each(result , function (index, value){
            $('#select').append($('<option>').text(value["Name"]).attr('value', value["Name"]));
        });
    }); 
});