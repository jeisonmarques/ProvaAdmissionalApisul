$(function() {
    "use strict";
    initSparkline();    
});

function initSparkline() {
    $(".sparkline").each(function() {
        var $this = $(this);
        $this.sparkline('html', $this.data());
    });
}

//===============================================================================
$(window).scroll(function() {
    $('.card .sparkline').each(function() {
        var imagePos = $(this).offset().top;

        var topOfWindow = $(window).scrollTop();
        if (imagePos < topOfWindow + 400) {
            $(this).addClass("pullUp");
        }
    });
});

//===============================================================================

Morris.Area({
    element: 'm_area_chart2',
    data: [{
            period: '2012',
            SiteA: 10,
            SiteB: 0,

        }, {
            period: '2013',
            SiteA: 105,
            SiteB: 110,

        }, {
            period: '2014',
            SiteA: 78,
            SiteB: 92,

        }, {
            period: '2015',
            SiteA: 89,
            SiteB: 185,

        }, {
            period: '2016',
            SiteA: 175,
            SiteB: 149,

        }, {
            period: '2017',
            SiteA: 126,
            SiteB: 98,

        }
    ],
    xkey: 'period',
    ykeys: ['SiteA', 'SiteB'],
    labels: ['Site A', 'Site B'],
    pointSize: 0,
    fillOpacity: 0.4,
    pointStrokeColors: ['#b6b8bb', '#a890d3'],
    behaveLikeLine: true,
    gridLineColor: '#e0e0e0',
    lineWidth: 0,
    smooth: false,
    hideHover: 'auto',
    lineColors: ['#b6b8bb', '#a890d3'],
    resize: true

});