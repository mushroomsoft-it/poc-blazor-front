import { calculateStats } from './statistics.js';

export function render(canvasId, data, options = {}) {

    const chart = new Tee.Chart(canvasId);
    const stats = calculateStats(data);

    // ===============================
    // Configuración visual segura
    // ===============================

    if (chart.panel?.format) {
        chart.panel.format.fill = "white";
        chart.panel.format.stroke.fill = "#dee2e6";
        if (chart.panel.format.shadow)
            chart.panel.format.shadow.visible = false;
    }

    if (chart.axes?.left) {
        chart.axes.left.title.text = "Valor";
        chart.axes.left.grid.visible = true;
        if (chart.axes.left.grid?.format?.stroke)
            chart.axes.left.grid.format.stroke.fill = "#e6e6e6";
    }

    if (chart.axes?.bottom) {
        chart.axes.bottom.title.text = "Muestra";
    }

    if (chart.legend)
        chart.legend.visible = true;

    // ===============================
    // Zoom y Scroll (seguros)
    // ===============================

    if (chart.zoom) {
        chart.zoom.enabled = true;
        chart.zoom.direction = "horizontal";
    }

    if (chart.scroll) {
        chart.scroll.enabled = true;
        chart.scroll.direction = "horizontal";
    }

    // ===============================
    // Animación segura
    // ===============================

    if (chart.animation?.active !== undefined) {
        chart.animation.active = false;
    }

    // ===============================
    // Serie principal
    // ===============================

    const mainSeries = chart.addSeries(new Tee.Line());
    mainSeries.title = "Valores";
    mainSeries.data.values = data;
    mainSeries.format.stroke.fill = options.lineColor ?? "green";

    if (data.length > 500) {
        mainSeries.pointer.visible = false;
    }

    // ===============================
    // Tooltips seguros
    // ===============================

    if (chart.tools) {
        const tip = chart.tools.add(new Tee.ToolTip(chart));
        tip.render = "dom";
        tip.autoHide = true;
    }

    // ===============================
    // Colorear fuera de control
    // ===============================

    mainSeries.onGetPointerStyle = function (series, index) {

        const value = series.data.values[index];

        if (value > stats.mean + 3 * stats.sd ||
            value < stats.mean - 3 * stats.sd) {
            series.pointer.format.fill = "red";
        } else {
            series.pointer.format.fill = "green";
        }
    };

    // ===============================
    // Líneas estadísticas
    // ===============================

    addHorizontalLine(chart, data.length, stats.mean, "blue", "Mean");
    addHorizontalLine(chart, data.length, stats.mean + stats.sd, "green", "+1 SD");
    addHorizontalLine(chart, data.length, stats.mean - stats.sd, "green", "-1 SD");
    addHorizontalLine(chart, data.length, stats.mean + 2 * stats.sd, "orange", "+2 SD");
    addHorizontalLine(chart, data.length, stats.mean - 2 * stats.sd, "orange", "-2 SD");
    addHorizontalLine(chart, data.length, stats.mean + 3 * stats.sd, "red", "+3 SD");
    addHorizontalLine(chart, data.length, stats.mean - 3 * stats.sd, "red", "-3 SD");

    chart.title.text = options.title ?? "Control Chart Enterprise";

    chart.draw();

    return chart;
}

function addHorizontalLine(chart, length, value, color, title) {

    const lineSeries = chart.addSeries(new Tee.Line());
    lineSeries.title = title;
    lineSeries.data.values = new Array(length).fill(value);
    lineSeries.format.stroke.fill = color;
    lineSeries.format.stroke.size = 2;
    lineSeries.pointer.visible = false;
}