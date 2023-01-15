$(function () {
    new Chart(document.getElementById("line_chart").getContext("2d"), getChartJs('line'));
    new Chart(document.getElementById("bar_chart").getContext("2d"), getChartJs('bar'));
    new Chart(document.getElementById("radar_chart").getContext("2d"), getChartJs('radar'));
    new Chart(document.getElementById("pie_chart").getContext("2d"), getChartJs('pie'));    
});

function getChartJs(type) {
    var config = null;

    if (type === 'line') {
        config = {
            type: 'line',
            data: {
                labels: ["January", "February", "March", "April", "May", "June", "July"],
                datasets: [{
                    label: "My First dataset",
                    data: [28, 32, 39, 37, 42, 55, 50],
                    borderColor: 'rgba(102,165,226, 0.2)',
                    backgroundColor: 'rgba(102,165,226, 0.7)',
                    pointBorderColor: 'rgba(102,165,226, 0.5)',
                    pointBackgroundColor: 'rgba(102,165,226, 0.2)',
                    pointBorderWidth: 1
                }, {
                    label: "My Second dataset",
                    data: [40, 48, 50, 48, 63, 62, 71],                    
                    borderColor: 'rgba(140,147,154, 0.2)',
                    backgroundColor: 'rgba(140,147,154, 0.2)',
                    pointBorderColor: 'rgba(140,147,154, 0)',
                    pointBackgroundColor: 'rgba(140,147,154, 0.9)',
                    pointBorderWidth: 1
                }]
            },
            options: {
                responsive: true,
                legend: false,
                
            }
        }
    }
    else if (type === 'bar') {
        config = {
            type: 'bar',
            data: {
                labels: ["January", "February", "March", "April", "May", "June", "July"],
                datasets: [{
                    label: "My First dataset",
                    data: [28, 48, 40, 19, 86, 27, 90],
                    backgroundColor: '#7cb5ec',
                    strokeColor: "rgba(255,118,118,0.1)",
                }, {
                        label: "My Second dataset",
                        data: [10, 30, 80, 61, 26, 75, 40],
                        backgroundColor: '#8a8a8b',
                        strokeColor: "rgba(255,118,118,0.1)",
                    }]
            },
            options: {
                responsive: true,
                legend: false
            }
        }
    }
    else if (type === 'radar') {
        config = {
            type: 'radar',
            data: {
                labels: ["January", "February", "March", "April", "May", "June", "July"],
                datasets: [{
                    label: "My First dataset",
                    data: [65, 25, 90, 81, 56, 55, 40],
                    borderColor: 'rgba(102,165,226, 0.8)',
                    backgroundColor: 'rgba(102,165,226, 0.5)',
                    pointBorderColor: 'rgba(102,165,226, 0)',
                    pointBackgroundColor: 'rgba(102,165,226, 0.8)',
                    pointBorderWidth: 1
                }, {
                        label: "My Second dataset",
                        data: [72, 48, 40, 19, 96, 27, 100],
                        borderColor: 'rgba(140,147,154, 0.8)',
                        backgroundColor: 'rgba(140,147,154, 0.5)',
                        pointBorderColor: 'rgba(140,147,154, 0)',
                        pointBackgroundColor: 'rgba(140,147,154, 0.8)',
                        pointBorderWidth: 1
                    }]
            },
            options: {
                responsive: true,
                legend: false
            }
        }
    }
    else if (type === 'pie') {
        config = {
            type: 'pie',
            data: {
                datasets: [{
                    data: [150, 53, 121, 87, 45],
                    backgroundColor: [
                        "#2a8ceb",
                        "#58a3eb",
                        "#6fa6db",
                        "#86b8e8",
                        "#9dc7f0"
                    ],
                }],
                labels: [
                    "Pia A",
                    "Pia B",
                    "Pia C",
                    "Pia D",
                    "Pia E"
                ]
            },
            options: {
                responsive: true,
                legend: false
            }
        }
    }   
    return config;
}