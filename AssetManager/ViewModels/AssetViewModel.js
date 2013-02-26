function AssetViewModel(parent,data) {
    var self = this;
    $.extend(self, new BaseViewModel());

    self.files = new ko.observable(new FilesViewModel(self));
    self.city = ko.observable();
    self.country = ko.observable();
    self.zipCode = ko.observable();
    self.address = ko.observable();
    self.name = ko.observable();
    self.charges = ko.observableArray([]);
    self.rents = ko.observableArray([]);
    self.parent = parent;
    self.imageUrl = ko.observable();
    self.saved = ko.observable();
    self.selectedCharge = ko.observable();
    self.selectedPayment = ko.observable();
    self.selectedRent = ko.observable();
    self.currentYear = ko.observable(new Date().getFullYear());
    self.size = ko.observable();
    self.pms = ko.observable();
    self.price = ko.observable();
    self.incomeTax = ko.observable(0);
    self.interestRate = ko.observable(0);
    self.edit = ko.observable(false);
    self.modeText = ko.observable(resourceProvider.update);
	
	
    self.saveText = ko.computed(function () {
        if (self.isBusy())
            return resourceProvider.saving;
        else
            return resourceProvider.save;
    }, self);
    
    self.chargesSum = ko.computed(function () {
        return sum(self.charges());
    }, self);

    self.rentsSum = ko.computed(function () {
        return sum(self.rents());
    }, self);
   
    self.ebit = ko.computed(function () {
        return self.chargesSum() - self.rentsSum();
    }, self);

    self.tax = ko.computed(function () {
        return self.ebit() * self.incomeTax();
    }, self);

    self.totalReturn = ko.computed(function () {
        if (self.tax != NaN) {
            return (self.ebit() - self.tax()).toFixed(2);
        }
        return self.ebit()
    }, self);

    self.roi = ko.computed(function () {
        return ((self.totalReturn() / self.price()) * 100).toFixed(2);
    }, self);


    self.epms = ko.computed(function () {
        return (self.ebit() / self.size()).toFixed(2);
    }, self);


    if (data != null) {
        
        self.isNew(false);
        self.name(data.Name);
        self.city(data.City);
        self.address(data.Address);
        self.city(data.City);
        self.country(data.Country);
        self.zipCode(data.ZipCode);
        self.id(data.Id);
        self.size(data.Size);
        self.price(data.Price);
        self.pms(data.PMS);
        self.incomeTax(data.IncomeTax);
        self.interestRate(data.InterestRate);

        if (data.Charges != null) {
            self.charges($.map(data.Charges, function (data) {
                return new ChargeViewModel(self, data);
            }));

            self.chargesGraph = data.Charges.map(function (c) {
                return c.Amount;
            });
        }

        if (data.Rents != null) {
            self.rents($.map(data.Rents, function (data) {
                return new RentViewModel(self, data);
            }));
        }
    }
    
    self.select = function () {
        if (self.parent.selectedAsset() != null) {
            self.parent.selectedAsset().selected(false);
        }

        self.parent.selectedAsset(self);
        self.selected(true);
    };

    self.save = function () {
        var data = self.toDto();
        self.isBusy(true);
        self.message("Saving...");

        var opType = "post";
        var url = "/../api/assets"
        if (!self.isNew()) {
            opType = "put";
            url = url + "?id=" + self.id();
        }
        
        $.ajax(url, {
            data: data,
            type: opType, contentType: "application/json",
            success: function (result) {
                self.edit(false);
                self.isBusy(false);
                self.message(result.message);
                if(self.isNew()){
                    self.parent.assets.push(new AssetViewModel(self.parent, result.asset));
                    parent.selectedAsset(null);
                }
            }
        });
    };

    self.remove = function () {
        self.isBusy(true);
        $.ajax("/../api/assets/" + self.id(), {
            type: "delete", contentType: "application/json",
            success: function (result) {
                self.isBusy(false);
                parent.assets.remove(self);
                parent.selectedAsset(null);
            }
        });
    };

    self.newCharge = function () {
        self.selectedCharge(new ChargeViewModel(self,null));
    };

    self.newRent = function () {
        self.selectedRent(new RentViewModel(self, null));
    }

    self.canSave = ko.computed(function () {
        return !isEmpty(self.name()) &&
            !isEmpty(self.address());
    }, self);

    self.toDto = function () {
        var model = new Object();
        model.City = self.city();
        model.Address = self.address();
        model.Name = self.name();
        model.Id = self.id();
        model.Size = self.size();
        model.PMS = self.pms();
        model.Price = self.price();
        model.City = self.city();
        model.Country = self.country();
        model.ZipCode = self.zipCode();
        model.IncomeTax = self.incomeTax();
        
        return JSON.stringify(model);
    };
	
    self.computePMS = function () {
        self.pms((self.price() / self.size()).toFixed(2));
    };
    
    self.mode = function () {
        self.edit(!self.edit());
        if (self.edit())
            self.modeText(resourceProvider.cancel);
        else
            self.modeText(resourceProvider.update);
    };

    self.charges.subscribe(function () {
        showChargesGraph(self);
    });

    self.rents.subscribe(function () {
        showRentsGraph(self);
    });
}