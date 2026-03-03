import * as meanChart from './meanChart.js';
import * as histogramChart from './histogramChart.js';

const charts = {};

export function createChart(type, canvasId, data, options) {

    // Si ya existe, limpiamos en vez de destroy()
    if (charts[canvasId]) {
        try {
            charts[canvasId].removeAllSeries();
            charts[canvasId].draw();
        } catch (e) {
            console.warn("No se pudo limpiar el chart anterior:", e);
        }
    }

    let chart;

    switch (type) {
        case "mean":
            chart = meanChart.render(canvasId, data, options);
            break;

        case "histogram":
            chart = histogramChart.render(canvasId, data, options);
            break;

        default:
            throw new Error("Tipo no soportado: " + type);
    }

    charts[canvasId] = chart;
}

export function updateChart(canvasId, newData, type, options) {

    createChart(type, canvasId, newData, options);
}