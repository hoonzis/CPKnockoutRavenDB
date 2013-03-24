function OwnerViewModel() {
    var self = this;
    $.extend(self, new BaseViewModel());
    self.assets = ko.observableArray([]);
    self.selectedAsset = ko.observable();
    
    self.isBusy(true);
    self.message(resourceProvider.loading);

    $.ajax("/../api/assets", {
        type: "get", contentType: "application/json",
        statusCode: {
            401: function () { window.location = "/en/Account/LogOn" }
        },
        success: function (data) {
            var mappedAssets = $.map(data, function (item) {
                return new AssetViewModel(self, item);
            });
            self.assets(mappedAssets);
            self.isBusy(false);
            showTab(requestedAsset, requestedTab);
        }
    });

    self.newAsset = function () {
        var newAsset = new AssetViewModel(self);
        newAsset.mode(); //put in edit mode
        self.selectedAsset(newAsset);
    };

    self.assetCount = ko.computed(function () {
        return self.assets().length;
    }, self);

    self.avarageRoi = ko.computed(function () {
        var total = 0;
        ko.utils.arrayForEach(self.assets(), function (item) {
            var value = parseFloat(item.roi());
            if (value!=NaN) {
                total += value;
            }
        });
        return (total / self.assetCount()).toFixed(2);
    }, self);

    self.revenuesPerYear = ko.computed(function () {
        var total = 0;
        ko.utils.arrayForEach(self.assets(), function (item) {
            var value = parseFloat(item.totalReturn());
            if (value != NaN) {
                total += value;
            }
        });
        return total.toFixed(2);
    }, self);
}