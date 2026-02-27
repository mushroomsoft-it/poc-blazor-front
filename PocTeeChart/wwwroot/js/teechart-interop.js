window.createMeanChart = function (canvasId, data) {

    var chart = new Tee.Chart(canvasId);

    // ===== Serie principal =====
    var mainSeries = chart.addSeries(new Tee.Line());
    mainSeries.data.values = data;
    mainSeries.format.stroke.fill = "green";

    // ===== Cálculos estadísticos =====
    var mean = data.reduce((a, b) => a + b) / data.length;

    var variance = data.reduce((a, b) =>
        a + Math.pow(b - mean, 2), 0) / data.length;

    var sd = Math.sqrt(variance);

    function addHorizontalLine(value, color) {
        var lineSeries = chart.addSeries(new Tee.Line());
        lineSeries.data.values = new Array(data.length).fill(value);
        lineSeries.format.stroke.fill = color;
        lineSeries.format.stroke.size = 2;
        lineSeries.pointer.visible = false;
    }

    // ===== Líneas Mean y SD =====
    addHorizontalLine(mean, "blue");

    addHorizontalLine(mean + sd, "green");
    addHorizontalLine(mean - sd, "green");

    addHorizontalLine(mean + 2 * sd, "orange");
    addHorizontalLine(mean - 2 * sd, "orange");

    addHorizontalLine(mean + 3 * sd, "red");
    addHorizontalLine(mean - 3 * sd, "red");

    chart.title.text = "QC Chart - Mean ± SD";

    chart.draw();
};

window.createHistogramWithNormal = function (canvasId, data) {

    var chart = new Tee.Chart(canvasId);

    chart.title.text = "Histograma con Curva Normal";

    // =========================
    // 1️⃣ Calcular estadísticas
    // =========================

    var mean = data.reduce((a, b) => a + b) / data.length;

    var variance = data.reduce((a, b) =>
        a + Math.pow(b - mean, 2), 0) / data.length;

    var sd = Math.sqrt(variance);

    // =========================
    // 2️⃣ Crear bins (histograma)
    // =========================

    var bins = 8;
    var min = Math.min(...data);
    var max = Math.max(...data);
    var binSize = (max - min) / bins;

    var frequencies = new Array(bins).fill(0);

    data.forEach(value => {
        var index = Math.floor((value - min) / binSize);
        if (index >= bins) index = bins - 1;
        frequencies[index]++;
    });

    // =========================
    // 3️⃣ Serie Histograma
    // =========================

    var histSeries = chart.addSeries(new Tee.Bar());
    histSeries.data.values = frequencies;
    histSeries.format.fill = "rgba(0,123,255,0.6)";
    histSeries.marks.visible = false;

    // =========================
    // 4️⃣ Curva Normal
    // =========================

    function normalDistribution(x, mean, sd) {
        return (1 / (sd * Math.sqrt(2 * Math.PI))) *
            Math.exp(-0.5 * Math.pow((x - mean) / sd, 2));
    }

    var normalSeries = chart.addSeries(new Tee.Line());
    normalSeries.format.stroke.fill = "red";
    normalSeries.format.stroke.size = 3;
    normalSeries.pointer.visible = false;

    var normalValues = [];

    for (var i = 0; i < bins; i++) {
        var x = min + i * binSize;
        var y = normalDistribution(x, mean, sd);

        // Escalamos para que coincida con histograma
        normalValues.push(y * data.length * binSize);
    }

    normalSeries.data.values = normalValues;

    chart.draw();
};