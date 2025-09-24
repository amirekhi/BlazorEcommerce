window.renderTrafficSourceChart = (sources) => {
    const ctx = document.getElementById("trafficSourceChart").getContext("2d");

    if (window.trafficSourceChartInstance) {
        window.trafficSourceChartInstance.destroy();
    }

    const labels = Object.keys(sources);
    const data = Object.values(sources);

    window.trafficSourceChartInstance = new Chart(ctx, {
        type: 'pie',
        data: {
            labels: labels,
            datasets: [{
                label: 'Traffic Sources',
                data: data,
                backgroundColor: [
                    '#4F46E5',
                    '#22C55E',
                    '#F59E0B',
                    '#EF4444',
                    '#0EA5E9',
                    '#A855F7'
                ],
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false, // <-- THIS IS THE FIX
            plugins: {
                legend: {
                    position: 'bottom',
                }
            }
        }
    });
};






window.renderDeviceChart = function (devices) {
    const ctx = document.getElementById('deviceChart');

    // Destroy existing chart if re-rendering
    if (window.deviceChartInstance) {
        window.deviceChartInstance.destroy();
    }

    const labels = Object.keys(devices);
    const values = Object.values(devices);

    const backgroundColors = [
        '#4ade80', // green
        '#60a5fa', // blue
        '#facc15', // yellow
        '#f472b6', // pink
        '#a78bfa', // purple
        '#f87171', // red
    ];

    if (!ctx) {
        console.error('Canvas element not found');
        return;
    }

    window.deviceChartInstance = new Chart(ctx, {
        type: 'doughnut',
        data: {
            labels: labels,
            datasets: [{
                label: 'Devices',
                data: values,
                backgroundColor: backgroundColors.slice(0, labels.length),
                borderColor: '#fff',
                borderWidth: 2
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: {
                    position: 'bottom',
                    labels: {
                        color: '#333'
                    }
                }
            }
        }
    });
};
