function ChargeViewModel(parent, data) {
    var self = this;
    $.extend(self, new ObligationViewModel());
    self.parent = parent;
    self.counterparty = ko.observable();
    self.type = "charge";

    self.errors = ko.validation.group(self);

    self.canSave = ko.computed(function () {
        var isOK = self.errors().length == 0;
        return isOK;
    }, self);

    //Update the current charge with the recieved data
    self.update = function (data) {
        //map charge specific values
        self.counterparty(data.Counterparty);
        //map common properties inside the ObligationViewModel
        self.construct(data);        
    }

    if (data != null)
        self.update(data);
    
    self.save = function () {
        if (self.errors().length != 0) {
            self.errors().showAllMessages();
            return;
        }

        self.isBusy(true);
        data = self.toDto();
        var rUrl = "/../api/charges?assetID=" + self.parent.id();
        if (self.isNew())
            var opType = "post";
        else
            var opType = "put";

        $.ajax(rUrl, {
            data: JSON.stringify(data),
            type: opType, contentType: "application/json",
            success: function (result) {
                self.isBusy(false);
                self.message(result.message);
                if (self.isNew()) {
                    self.update(result.dto);
                    parent.charges.push(self);
                }
            }
        });
    }

    self.remove = function () {
        $.ajax("/../api/charges/" + self.id() + "?assetID=" + self.parent.id(), {
            type: "delete", contentType: "application/json",
            success: function (result) {
                self.isBusy(false);
                parent.charges.remove(self);
                parent.selectedCharge(null);
            }
        });
    };

    self.select = function () {
        if (parent.selectedCharge() != null) {
            parent.selectedCharge().selected(false);
        }
        parent.selectedCharge(self);
        self.selected(true);
    };

    self.toDto = function () {
        var dto = self.constructDto();
        dto.Counterparty = self.counterparty();
        return dto;
    };
}