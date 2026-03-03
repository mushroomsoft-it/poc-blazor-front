import { calculateStats, normalDistribution } from './statistics.js';

export function render(canvasId, data, options = {}) {

    const chart = new Tee.Chart(canvasId);
    const stats = calculateStats(data);

    if (chart.legend)
        chart.legend.visible = true;

    if (chart.animation?.active !== undefined) {
        chart.animation.active = false;
    }

    const bins = options.bins ?? 10;

    const min = Math.min(...data);
    const max = Math.max(...data);
    const binSize = (max - min) / bins;

    const frequencies = new Array(bins).fill(0);

    data.forEach(value => {
        let index = Math.floor((value - min) / binSize);
        if (index >= bins) index = bins - 1;
        frequencies[index]++;
    });

    // Histograma
    const histSeries = chart.addSeries(new Tee.Bar());
    histSeries.title = "Frecuencia";
    histSeries.data.values = frequencies;
    histSeries.format.fill = "rgba(0,123,255,0.6)";

    // Curva normal
    const normalSeries = chart.addSeries(new Tee.Line());
    normalSeries.title = "Curva Normal";
    normalSeries.format.stroke.fill = "red";
    normalSeries.format.stroke.size = 3;
    normalSeries.pointer.visible = false;

    const normalValues = [];

    for (let i = 0; i < bins; i++) {
        const x = min + i * binSize;
        const y = normalDistribution(x, stats.mean, stats.sd);
        normalValues.push(y * data.length * binSize);
    }

    normalSeries.data.values = normalValues;

    chart.title.text = options.title ?? "Histograma + Normal";

    chart.draw();

    return chart;
}