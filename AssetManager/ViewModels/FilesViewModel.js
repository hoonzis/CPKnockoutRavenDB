
var CLIENT_ID = '752004182866-ea3qupvn9t9j6h18ge1iebqqt73pfm7o.apps.googleusercontent.com';
var SCOPES = 'https://www.googleapis.com/auth/drive';

function FilesViewModel(parent) {
    var self = this;
    
    $.extend(self, new BaseViewModel());

    self.result = ko.observable();
    self.message = ko.observable();

    /**
    * Check if the current user has authorized the application.
    */
    self.checkAuth = function () {
        gapi.auth.authorize(
            { 'client_id': CLIENT_ID, 'scope': SCOPES, 'immediate': true },
            self.handleAuthResult);
    }

    /**
    * Called when authorization server replies.
    *
    *  {Object} authResult Authorization result.
    */
    self.handleAuthResult = function(authResult) {
        if (authResult && !authResult.error) {
            self.message('Appartee is authorized to access GDrive');
            getOrCreateApparteeFolder();
        } else {
            self.message('Could not access the Appartee folder inside your GDrive account');
        }
    }

    function getOrCreateApparteeFolder() {
        var request = gapi.client.request({
            'path': 'drive/v2/files',
            'method': 'GET',
            'params': {
                'maxResults': '20',
                'q': "mimeType = 'application/vnd.google-apps.folder' and title = '" + "appartee" + "'"
            }
        });

        request.execute(apparteeFolderRecieved);
    }

    function apparteeFolderRecieved(result) {
        self.result(result);
        var apparteeFolderID = result.id;
        getAllFilesInFolder(id);
    }
}