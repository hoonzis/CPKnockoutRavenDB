function obligationToChart(d) {
    return { x: d.name(), y: d.amount() };
}