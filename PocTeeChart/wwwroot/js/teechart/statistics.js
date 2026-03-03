export function calculateStats(data) {

    const mean = data.reduce((a, b) => a + b) / data.length;

    const variance = data.reduce((a, b) =>
        a + Math.pow(b - mean, 2), 0) / data.length;

    const sd = Math.sqrt(variance);

    return { mean, variance, sd };
}

export function normalDistribution(x, mean, sd) {
    return (1 / (sd * Math.sqrt(2 * Math.PI))) *
        Math.exp(-0.5 * Math.pow((x - mean) / sd, 2));
}