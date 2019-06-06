// JavaScript source code
var Sdk = window.Sdk || {};

(function() {
        //code to run in the form Onload event
        this.AccountLoad = function (executionContext)
        {
            var formContext = executionContext.getFormContext();

            //Get Countries
            Xrm.WebApi.retrieveMultipleRecords("rkj_country", "?$select=rkj_name&$top=3").then(
                function success(result) {
                    var desc = '';
                    for (var i = 0; i < result.entities.length; i++) {
                        desc += result.entities[i].rkj_name + " ";
                        formContext.getAttribute("description").setValue(desc);
                        console.log(result.entities[i].rkj_name);

                    }
                    // perform additional operations on retrieved records
                },
                function (error) {
                    console.log(error.message);
                    // handle error conditions
                }
            );
        }
        this.formOnSave = function (executionContext)
        {

        }
}).call(Sdk);

https://rkj84.api.crm8.dynamics.com/api/data/v9.1/rkj_countries?$select=rkj_name&$filter=rkj_name eq 'India'

