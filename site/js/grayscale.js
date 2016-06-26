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

// jQuery for page scrolling feature - requires jQuery Easing plugin
$(function() {
    $('a.page-scroll').bind('click', function(event) {
        var $anchor = $(this);
        $('html, body').stop().animate({
            scrollTop: $($anchor.attr('href')).offset().top
        }, 1500, 'easeInOutExpo');
        event.preventDefault();
    });

     $.get("https://azuresolutionslets.azurewebsites.net/api/olympics-meter2?code=r4a3qqlia9cbq1cj9uftakyb9jhbpslx0youwmfwml84pr2j4i20qsa9p4fmd4q7qv8ku88mpldi", function (result) {

        for (var i = 0; i < result.length; ++i) {
            if (result[i]['Name'] === 'Brazil') {
                result[i]['Score'] = parseFloat(0.9);
            }
            if (result[i]['Name'] === 'Chile') {
                result[i]['Score'] = parseFloat(0.8);
            }
            if (result[i]['Name'] === 'Russia') {
                result[i]['Score'] = parseFloat(0.7);
            }
            if (result[i]['Name'] === 'Spain') {
                result[i]['Score'] = parseFloat(0.6);
            }
            if (result[i]['Name'] === 'Ireland') {
                result[i]['Score'] = parseFloat(0.5);
            }
            if (result[i]['Name'] === 'Mexico') {
                result[i]['Score'] = parseFloat(0.4);
            }
            if (result[i]['Name'] === 'Canada') {
                result[i]['Score'] = parseFloat(0.3);
            }
        }
        
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
    }); 
});