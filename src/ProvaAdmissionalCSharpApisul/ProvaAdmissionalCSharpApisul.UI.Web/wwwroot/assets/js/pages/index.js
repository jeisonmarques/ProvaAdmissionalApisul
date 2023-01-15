//[custom Javascript]
//Project:	Compass - Responsive Bootstrap 4 Template
//Version:  1.0
//Last change:  15/12/2017
//Primary use:	Compass - Responsive Bootstrap 4 Template
//should be included in all pages. It controls some layout
$(function() {
    "use strict";
    initSparkline();
    initDonutChart();
    MorrisArea();
    Jknob();
});

// Start
function initSparkline() {
    $(".sparkline").each(function() {
        var $this = $(this);
        $this.sparkline('html', $this.data());
    });
}
//End
//Start
function initDonutChart() {
    Morris.Donut({
        element: 'donut_chart',
        data: [{
                label: 'Chrome',
                value: 37
            }, {
                label: 'Firefox',
                value: 30
            }, {
                label: 'Safari',
                value: 18
            }, {
                label: 'Opera',
                value: 12
            },
            {
                label: 'Other',
                value: 3
            }
        ],
        colors: ['#93e3ff', '#b0dd91', '#ffe699', '#f8cbad', '#a4a4a4'],
        formatter: function(y) {
            return y + '%'
        }
    });
}//End
//Start
function MorrisArea() {
    Morris.Area({
        element: 'area_chart',
        data: [{
                period: '2011',
                iphone: 10,
                ipad: 0,
                itouch: 0
            }, {
                period: '2012',
                iphone: 30,
                ipad: 68,
                itouch: 5
            }, {
                period: '2013',
                iphone: 85,
                ipad: 50,
                itouch: 23
            }, {
                period: '2014',
                iphone: 45,
                ipad: 12,
                itouch: 28
            }, {
                period: '2015',
                iphone: 30,
                ipad: 32,
                itouch: 98
            }, {
                period: '2016',
                iphone: 125,
                ipad: 80,
                itouch: 40
            }, {
                period: '2017',
                iphone: 40,
                ipad: 10,
                itouch: 26
            }

        ],
        lineColors: ['#f7cf68', '#666666', '#a890d3'],
        xkey: 'period',
        ykeys: ['iphone', 'ipad', 'itouch'],
        labels: ['iphone', 'ipad', 'itouch'],
        pointSize: 0,
        lineWidth: 0,
        resize: true,
        fillOpacity: 0.8,
        behaveLikeLine: true,
        gridLineColor: '#e0e0e0',
        hideHover: 'auto'
    });
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
    
        }],
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
}
//End
//Start
function Jknob() {
    $('.knob').knob({
        draw: function() {
            // "tron" case
            if (this.$.data('skin') == 'tron') {

                var a = this.angle(this.cv) // Angle
                    ,
                    sa = this.startAngle // Previous start angle
                    ,
                    sat = this.startAngle // Start angle
                    ,
                    ea // Previous end angle
                    , eat = sat + a // End angle
                    ,
                    r = true;

                this.g.lineWidth = this.lineWidth;

                this.o.cursor &&
                    (sat = eat - 0.3) &&
                    (eat = eat + 0.3);

                if (this.o.displayPrevious) {
                    ea = this.startAngle + this.angle(this.value);
                    this.o.cursor &&
                        (sa = ea - 0.3) &&
                        (ea = ea + 0.3);
                    this.g.beginPath();
                    this.g.strokeStyle = this.previousColor;
                    this.g.arc(this.xy, this.xy, this.radius - this.lineWidth, sa, ea, false);
                    this.g.stroke();
                }

                this.g.beginPath();
                this.g.strokeStyle = r ? this.o.fgColor : this.fgColor;
                this.g.arc(this.xy, this.xy, this.radius - this.lineWidth, sat, eat, false);
                this.g.stroke();

                this.g.lineWidth = 2;
                this.g.beginPath();
                this.g.strokeStyle = this.o.fgColor;
                this.g.arc(this.xy, this.xy, this.radius - this.lineWidth + 1 + this.lineWidth * 2 / 3, 0, 2 * Math.PI, false);
                this.g.stroke();

                return false;
            }
        }
    });
}
//End
//Start
$(window).on('scroll',function() {
    $('.card .sparkline').each(function() {
        var imagePos = $(this).offset().top;

        var topOfWindow = $(window).scrollTop();
        if (imagePos < topOfWindow + 400) {
            $(this).addClass("pullUp");
        }
    });
});
//End
//Start
$(".dial1").knob();
$({
    animatedVal: 0
}).animate({
    animatedVal: 66
}, {
    duration: 3000,
    easing: "swing",
    step: function() {
        $(".dial1").val(Math.ceil(this.animatedVal)).trigger("change");
    }
});
$(".dial2").knob();
$({
    animatedVal: 0
}).animate({
    animatedVal: 26
}, {
    duration: 3800,
    easing: "swing",
    step: function() {
        $(".dial2").val(Math.ceil(this.animatedVal)).trigger("change");
    }
});
$(".dial3").knob();
$({
    animatedVal: 0
}).animate({
    animatedVal: 76
}, {
    duration: 3200,
    easing: "swing",
    step: function() {
        $(".dial3").val(Math.ceil(this.animatedVal)).trigger("change");
    }
});
$(".dial4").knob();
$({
    animatedVal: 0
}).animate({
    animatedVal: 88
}, {
    duration: 3500,
    easing: "swing",
    step: function() {
        $(".dial4").val(Math.ceil(this.animatedVal)).trigger("change");
    }
});
//End
//Start
$(function() {
    $('#world-map-markers').vectorMap({
        map: 'world_mill_en',
        normalizeFunction: 'polynomial',
        hoverOpacity: 0.7,
        hoverColor: false,
        backgroundColor: 'transparent',
        regionStyle: {
            initial: {
                fill: '#49cdd0',
                "fill-opacity": 1,
                stroke: 'none',
                "stroke-width": 0,
                "stroke-opacity": 1
            },
            hover: {
                fill: 'rgba(255, 193, 7, 2)',
                cursor: 'pointer'
            },
            selected: {
                fill: 'yellow'
            },
            selectedHover: {}
        },
        markerStyle: {
            initial: {
                fill: '#fff',
                stroke: '#FFC107',                
            }
        },
        markers: [{
                latLng: [37.09, -95.71],
                name: 'America'
            },
            {
                latLng: [51.16, 10.45],
                name: 'Germany'
            },
            {
                latLng: [-25.27, 133.77],
                name: 'Australia'
            },
            {
                latLng: [56.13, -106.34],
                name: 'Canada'
            },
            {
                latLng: [20.59, 78.96],
                name: 'India'
            },
            {
                latLng: [55.37, -3.43],
                name: 'United Kingdom'
            },
        ]
    });
});
//End

