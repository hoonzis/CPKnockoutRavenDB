var time = new Rickshaw.Fixtures.Time();
var months = time.unit('month');
var papers = [];
var graphs = [];
    
function mapToAmount(collection){
    var mapped = ko.utils.arrayMap(collection, function (item) {
        var parsedValue = parseFloat(item.amount());
        if (parsedValue > 0)
            return parsedValue;

        return 0;
    });
    return mapped;
};

function showPieChart(collection, elementName, d, amountMap) {
    if (papers[elementName] == null) {
        papers[elementName] = Raphael(elementName);
    }

    if (graphs[elementName] != null) {
        graphs[elementName].remove();
    }
    
    var legend = $.map(collection, function (i) {
        return i.name();
    }, self);

    if (amountMap == null)
        var graphData = mapToAmount(collection);
    else
        var graphData = $.map(collection, amountMap);

    var settings = { legend: legend, legendpos: "west" };
    graphs[elementName] = papers[elementName].piechart(d, 50, 50, graphData, settings);
}

function showRentsGraph(asset) {
    showPieChart(asset.rents(), 'rentsGraph', 150);
}

function showChargesGraph(asset) {
    showPieChart(asset.charges(), 'chargesGraph', 150);
}