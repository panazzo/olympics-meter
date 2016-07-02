var APIContry = "";
var APIMessage = "";

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
    var country = $("#select").val();
    var text = $("#textarea").val();

    var settings = {
    "async": true,
    "crossDomain": true,
    "url": APIMessage,
    "method": "POST",
    "headers": {
        "content-type": "application/json"
    },
    "data": JSON.stringify({name: country, message: text})
    }

    $.ajax(settings).done(function (response) {
        $("#resultdiv").show();
        $("#scoreResult").text(String(response));
        $("#formdiv").hide();
    });
});

// jQuery for page scrolling feature - requires jQuery Easing plugin
$(function() {
    $("#resultdiv").hide();

    $('a.page-scroll').bind('click', function(event) {
        var $anchor = $(this);
        $('html, body').stop().animate({
            scrollTop: $($anchor.attr('href')).offset().top
        }, 1500, 'easeInOutExpo');
        event.preventDefault();
    });

     $.get(APIContry, function (result) {
        
        result.sort(function(a, b) {
            return parseFloat(b.Score) - parseFloat(a.Score);
        });

        new Morris.Bar({
            element: 'chart',
            data: result.slice(0,5),
            xkey: 'Name',
            ykeys: ['Score'],
            barColors: ["#009E3D"],
            labels: ['Score']
        });

        result.sort(function(a, b) {
            return compareStrings(a.Name, b.Name);
        })

        $.each(result , function (index, value){
            if(value["Name"] == "Brazil"){
                $('#select').append($('<option selected>').text(value["Name"]).attr('value', value["Name"]));
            }
            else{
                $('#select').append($('<option>').text(value["Name"]).attr('value', value["Name"]));
            }

        });
    }); 
});

function compareStrings(a, b) {
  // Assuming you want case-insensitive comparison
  a = a.toLowerCase();
  b = b.toLowerCase();

  return (a < b) ? -1 : (a > b) ? 1 : 0;
}