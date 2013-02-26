$(function () {
    module("ChargeViewModelTests");


    
    var asset = new AssetViewModel(null,null);
    var vm = new ChargeViewModel(asset,null);


    test("Set selected", function () {
        vm.select();

        QUnit.equal(vm.selected(), true, "Selected is set to true");
        QUnit.equal(asset.selectedCharge(), vm, "Selected charge of asset is set to the selected one");
    });

    test("Default values", function () {
        QUnit.equal(vm.isNew(), true);
        QUnit.equal(vm.isBusy(), false);
    });

    test("test", function () {
        vm.isNew(true);
        QUnit.equal(vm.updateText(), "Create");

        vm.isNew(false);
        QUnit.equal(vm.updateText(), "Update");
    });
}
);