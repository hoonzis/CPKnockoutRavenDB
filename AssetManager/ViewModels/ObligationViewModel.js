function ObligationViewModel() {
    var self = this;
    $.extend(self, new BaseViewModel());
    self.type = "";
    self.parent = null;
    self.name = ko.observable().extend({ required: true });
    self.paymentDay = ko.observable().extend({ number: true, min: 1});//, max: 31 });
    self.unit = ko.observable().extend({ number: true, min: 1});//, max: 12 });
    self.amount = ko.observable().extend({ required: true, number: true });
    self.notes = ko.observable();
    self.account = ko.observable();
    self.start = ko.observable();
    self.end = ko.observable();
    
    self.construct = function (data) {
        self.id(data.Id);
        self.name(data.Name);
        self.isNew(false);
        self.amount(data.Amount);
        self.notes(data.Notes);
        self.paymentDay(data.PaymentDay);
        self.account(data.AccountNumber);
        self.unit(data.Unit);
        self.start(data.Start);
        self.end(data.End);
    }

    self.constructDto = function () {
        var dto = new Object();
        dto.Id = self.id();
        dto.Name = self.name();
        dto.Amount = self.amount();
        dto.Notes = self.notes();
        dto.PaymentDay = self.paymentDay();
        dto.AccountNumber = self.account();
        dto.Unit = self.unit();
        dto.Start = self.start();
        dto.End = self.end();
        return dto;
    }
}