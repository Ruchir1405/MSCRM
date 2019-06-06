// JavaScript source code


function ContactLoad(executionContext) {
    //get formContext
    var formContext = executionContext.getFormContext();

    // get attribute data
    var firstName = formContext.getAttribute("firstname").getValue();

    //set attribute data
    formContext.getAttribute("description").setValue("First name is" + firstName);

}

function OnEmailChange(executionContext) {
    //get formContext
    var formContext = executionContext.getFormContext();

    alert("you have changed the email")
}

function OnSSNUpdate(executionContext) {
    //get formContext
    var formContext = executionContext.getFormContext();

    var ssn = formContext.getAttribute("rkj_ssn").getValue();

    if (isNaN(ssn) || ssn.length != 10) {
        // show field notifications, this  acts as error message.
        formContext.getControl("rkj_ssn").setNotification("enter only numbers", "message1");
        formContext.ui.setFormNotification("Instructions: SSN is 10 digit number only", "Warning", "message2");
    }
    else {
        formContext.getControl("rkj_ssn").clearNotification("message1");
    }
}





