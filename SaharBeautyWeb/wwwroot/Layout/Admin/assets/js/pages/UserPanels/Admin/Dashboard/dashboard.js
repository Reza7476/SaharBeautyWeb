

//var labels = labels;
//var data = data;

const ctx = document.getElementById('appointmentsChart').getContext('2d');
const appointmentsChart = new Chart(ctx, {
    type: 'pie', // نوع نمودار: bar, line, pie و غیره
    data: {
        labels: labels,
        datasets: [{
            label: 'تعداد نوبت‌ها',
            data: data,
            backgroundColor: 'rgba(54, 162, 235, 0.6)',
            borderColor: 'rgba(54, 162, 235, 1)',
            borderWidth: 1
        }]
    },
    options: {
        responsive: true,
        plugins: {
            legend: {
                display: true,
                position: 'top'
            },
            tooltip: {
                enabled: true
            }
        },
        scales: {
            y: {
                beginAtZero: true,
                ticks: {
                    precision: 0
                }
            }
        }
    }
});