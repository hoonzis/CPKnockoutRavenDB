$(function () {
    module("base view model tests");

    var vm = new BaseViewModel();

    test("initial item length", function () {
        QUnit.equal(vm.selected(),false, "selected is false");
    });
}
);