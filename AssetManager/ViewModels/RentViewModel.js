function RentViewModel(parent, data) {
    var self = this;
    $.extend(self, new ObligationViewModel());
    self.parent = parent;
    self.deposit = ko.observable();
    self.type = "rent";
    
    self.errors = ko.validation.group(self);
    
    //Update the current rent with recieved data
    self.update = function (data) {
        //map rent specific properties
        self.deposit(data.Deposit);
        //map common properties
        self.construct(data);
    }

    if (data != null)
        self.update(data);

    self.save = function () {
        if (self.errors().length != 0) {
            self.errors().showAllMessages();
            return;
        }

        var data = self.toDto();
        self.isBusy(true);
        self.message(resourceProvider.saving);

        var opType = "post";
        var url = "/../api/rents?assetID=" + self.parent.id();

        if (!self.isNew())
            opType = "put";
        
        $.ajax(url, {
            data: JSON.stringify(data),
            type: opType, contentType: "application/json",
            success: function (result) {
                self.isBusy(false);
                self.message(result.message);
                if (self.isNew()) {
                    self.update(result.dto);
                    parent.rents.push(self);
                }
            }
        });
    }

    self.remove = function () {
        self.isBusy(true);
        $.ajax("/../api/rents/" + self.id() + "?assetID=" + self.parent.id(), {
            type: "delete", contentType: "application/json",
            success: function (result) {
                self.isBusy(false);
                parent.rents.remove(self);
                parent.selectedRent(null);
            }
        });
    };

    self.select = function () {
        if (parent.selectedRent() != null) {
            parent.selectedRent().selected(false);
        }
        parent.selectedRent(self);
    };

    self.toDto = function () {
        var dto = self.constructDto();
        dto.Deposit = self.deposit();
        return dto;
    }
}