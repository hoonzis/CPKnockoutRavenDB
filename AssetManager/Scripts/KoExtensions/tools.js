var today = new Date();
var day = today.getUTCDate();
var year = today.getFullYear();
var month = today.getMonth();
        
function isEmpty(str) {
    return (!str || 0 === str.length);
}


/* Sums all elements in the array. The map function is applied on each element to obtain it's value. The predicate is a condition to be added to the sum*/
function sum(kArray, mapfunc,predicate) {
    var total = 0;
    for (var p = 0; p < kArray.length; p++) {
        if(predicate==null || predicate(kArray[p])){
            if (mapfunc != null)
                total += parseFloat(mapfunc(kArray[p]));
            else
                total += parseFloat(kArray[p]);
        }
    }
    return total;
}

function find(data, predicate) {
    var i = 0;
    for (i = 0; i < data.length; i++)
        if(predicate(data[i]))
            return data[i];
}

function max(kArray, predicate) {
    var max = 0;
    for (var p = 0; p < kArray.length; p++) {
        var v = predicate(kArray[p]);
        if (v >= max)
            max = v;
    }

    return max;
}

function positiveRounded(d) {
    if (d > 0)
        return Math.round(d);
    return 0;
}

function isString(x){
    return typeof(x) == 'string';
}

function toFixed(v,n){
    if (isString(v))
        return v;
    return v.toFixed(n);
}

function isDate(d) {
    return Object.prototype.toString.call(d) == "[object Date]";
}

//verify that the date is valid => object is date-time and there is a meaningful value
function isValidDate(d) {
  if (!isDate(d))
    return false;
  return !isNaN(d.getTime());
}

//not efficient comparison of arrays
function arraysAreEqual(ary1, ary2) {
    if (ary1 != null && ary2 != null)
        return (ary1.join('') == ary2.join(''));

    if (ary1 == null && ary2 == null)
        return true;

    return false;
}