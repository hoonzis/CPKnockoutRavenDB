function obligationToChart(d) {
    return { x: d.name(), y: d.amount() };
}

function getAmount(d) {
    return d.amount();
}