function BaseViewModel() {
    var self = this;
    self.id = ko.observable();
    self.isNew = ko.observable(true);
    self.selected = ko.observable(false);
    self.isBusy = ko.observable(false);
    self.message = ko.observable();
    

    self.setDefaults = function () {
        self.isNew(true);
        self.isBusy(false);
    }

    self.updateText = ko.computed(function () {
        if (self.isNew())
            return resourceProvider.create;
        else
            return resourceProvider.update;
    }, this);

    

}