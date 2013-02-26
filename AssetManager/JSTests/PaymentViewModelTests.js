$(function () {
    module("PaymentViewModelTests");

    var asset = new AssetViewModel(null, null);
    var vm = new ChargeViewModel(asset, null);
    var vm = new PaymentViewModel(vm,  null);

    test("Set selected", function () {
        vm.select();

        QUnit.equal(vm.selected(), true, "Selected is set to true");
        QUnit.equal(asset.selectedPayment(), vm, "Selected payment of charge or rent is set to the selected one");
    });

    test("Default values", function () {
        QUnit.equal(vm.isNew(), true);
        QUnit.equal(vm.isBusy(), false);
    });
}
);