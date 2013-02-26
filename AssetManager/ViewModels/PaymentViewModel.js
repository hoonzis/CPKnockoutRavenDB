function PaymentViewModel(parent, data) {
    var self = this;

    $.extend(self, new BaseViewModel());

	self.parent = parent;
	self.amount = ko.observable()
	self.paymentDate = ko.observable();
	self.month = ko.observable();
	self.day = ko.observable();
	self.year = ko.observable();
	self.paymentDetailVisible = ko.observable();

	self.parentName = ko.computed(function () {
	    return self.parent.name();
	}, self);

	self.parentAmount = ko.computed(function () {
	    return self.parent.amount();
	}, self);
	
	self.selected = ko.observable(false);
	self.sMonth = ko.computed(function () {
	    return resourceProvider.months[self.month()];
	}, self);

	self.setDefaults();
	
	self.payed = ko.computed(function () {
	    return self.amount() != 0;
	},self);
	
	if (data != null) {
	    self.amount(data.Amount);
	    self.paymentDate(data.Date);
	    self.month(data.PaymentTime.Month);
	    self.year(data.PaymentTime.Year);

        if (data.Id != 0) {
            self.isNew(false);
            self.id(data.Id);
        }
	}

	self.select = function () {
	    if (self.parent.parent.selectedPayment() != null) {
	        self.parent.parent.selectedPayment().selected(false);
	    }
	    self.selected(true);
	    self.parent.parent.selectedPayment(self);
	};

	self.fullSelect = function () {
	    //call the showDetail method of OwnerViewModel
	    self.parent.parent.parent.showDetail();

	    //select the current asset
	    self.parent.parent.select();

	    showTab("payments");

	    //now select the payment
	    self.select();
	};

	self.save = function () {
	    self.isBusy(true);
	    self.message(resourceProvider.saving);

	    var data = new Object();
	    data.Payment = self.toDto();
	    data.Type = parent.type;
	    data.ParentID = parent.id();
	    data.AssetID = self.parent.parent.id();
        
	    var rUrl = "/../api/payments/";
	    if (self.isNew()) {
	        var opType = "post";
	    }else{
	        var opType = "put";
	        rUrl +=self.id();
	    }

	    $.ajax(rUrl, {
	        data: JSON.stringify(data),
	        type: opType, contentType: "application/json",
	        success: function (result) {
	            self.isBusy(false);
	            self.isNew(false);
                self.id(result.id);
                self.message(result.message);
	        }
	    });
	};

	self.showPayment = function () {
	    self.paymentDetailVisible(true);
	};
    
	self.toDto = function () {
	    var dto = new Object();
	    dto.Payed = self.payed();
	    dto.Amount = self.amount();
	    dto.PaymentTime = new Object();
	    dto.PaymentTime.Month = self.month();
	    dto.PaymentTime.Year = self.year();
	    return dto;
	};
}
