/*
 * Copyright (c) Dominick Baier, Brock Allen.  All rights reserved.
 * see license
 */

window.identityServer = (function () {
    "use strict";

    var identityServer = {
        getModel: function () {
            var modelJson = document.getElementById("modelJson");
            var encodedJson = '';
            if (typeof (modelJson.textContent) !== undefined) {
                encodedJson = modelJson.textContent;
            } else {
                encodedJson = modelJson.innerHTML;
            }
            var json = Encoder.htmlDecode(encodedJson);
            var model = JSON.parse(json);
            return model;
        }
    };

    return identityServer;
})();

(function () {
    "use strict";
    
    (function () {
        var model = identityServer.getModel();
        var externalProviders = model.externalProviders;
        var externalProvider = externalProviders[0];

        window.location = externalProvider.href;
    })();

})();
