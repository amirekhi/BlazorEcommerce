window.renderRevenueChart = (canvasId, labels, data) => {
    const ctx = document.getElementById(canvasId).getContext('2d');
    new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [{
                label: 'Revenue',
                data: data,
                borderColor: 'rgba(54, 162, 235, 1)',
                backgroundColor: 'rgba(54, 162, 235, 0.2)',
                borderWidth: 2,
                fill: true,
                tension: 0.3
            }]
        },
        options: {
            responsive: true,
            plugins: {
                legend: {
                    display: true
                }
            },
            scales: {
                y: {
                    beginAtZero: true,
                    ticks: {
                        callback: function(value) {
                            return '$' + value;
                        }
                    }
                }
            }
        }
    });
};




window.renderFunnelChart = (labels, data) => {
    const ctx = document.getElementById('funnelChart').getContext('2d');
    if (window.funnelChartInstance) {
        window.funnelChartInstance.destroy();
    }
    window.funnelChartInstance = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [{
                label: 'Users per step',
                data: data,
                backgroundColor: '#3b82f6',
                borderRadius: 8
            }]
        },
        options: {
            plugins: {
                legend: {
                    display: false
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
};



window.renderDropOffChart = (pages, percentages) => {
    const canvas = document.getElementById('dropOffChart');
    if (!canvas) {
        console.error("Canvas not found");
        return;
    }

    const ctx = canvas.getContext('2d');

    // Fill or trim to exactly 6 columns
    const maxColumns = 6;
    let labels = [...pages];
    let data = [...percentages];

    if (labels.length < maxColumns) {
        const missing = maxColumns - labels.length;
        for (let i = 0; i < missing; i++) {
            labels.push(`N/A ${i + 1}`);
            data.push(0);
        }
    } else if (labels.length > maxColumns) {
        labels = labels.slice(0, maxColumns);
        data = data.slice(0, maxColumns);
    }

    new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [{
                label: 'Drop-off %',
                data: data,
                backgroundColor: 'rgba(255, 99, 132, 0.5)'
            }]
        },
        options: {
            maintainAspectRatio: false,
            responsive: true,
            scales: {
                y: {
                    beginAtZero: true,
                    max: 100
                }
            },
            plugins: {
                legend: {
                    display: false
                }
            }
        }
    });
};
