window.renderVegaFromSpec = (elementId, spec) => {
    vegaEmbed(`#${elementId}`, spec, { actions: false });
};