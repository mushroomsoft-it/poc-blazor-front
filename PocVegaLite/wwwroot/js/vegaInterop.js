window.renderVegaFromSpec = (elementId, spec) => {
    console.log(`\n✅ Rendering chart for element: ${elementId}`);
    console.log(`Schema: ${spec?.$schema}`);
    console.log(`Data values count: ${spec?.data?.values?.length || 0}`);

    if (spec?.data?.values && spec.data.values.length > 0) {
        console.log(`First data point:`, JSON.stringify(spec.data.values[0]));
        console.log(`Last data point:`, JSON.stringify(spec.data.values[spec.data.values.length - 1]));
    }

    console.log(`Encoding keys:`, Object.keys(spec?.encoding || {}));
    console.log(`Full spec:`, JSON.stringify(spec, null, 2));

    vegaEmbed(`#${elementId}`, spec, { actions: false })
        .then(result => {
            console.log(`✅ Chart rendered successfully for ${elementId}`);
            console.log(`Result:`, result);
        })
        .catch(err => {
            console.error(`❌ Error rendering chart for ${elementId}:`, err);
            console.error(`Error message:`, err.message);
            console.error(`Error stack:`, err.stack);
        });
};