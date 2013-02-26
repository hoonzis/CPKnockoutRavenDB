function isEmpty(str) {
    return (!str || 0 === str.length);
}

function sum(kArray) {
    var total = new Number;
    for (var p = 0; p < kArray.length; p++) {
        total += parseFloat(kArray[p].amount());
    }
    return total;
}

function find(data, predicate) {
    var i = 0;
    for (i = 0; i < data.length; i++)
        if(predicate(data[i]))
            return data[i];
}

function positiveRounded(d) {
    if (d > 0)
        return Math.round(d);
    return 0;
}

function prTotalReturn(d) {
    return positiveRounded(d.eTotalReturn);
}