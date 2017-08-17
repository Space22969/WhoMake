$(document).ready(function () {
    console.log("You had been hacked!");
    jQuery.validator.setDefaults({
        debug: true,
        success: "valid"
    });

    $('#form1').validate({
        ignore: [],
        rules: {
            Name:
            {
                required: true,
                minlength: 3,
                maxlength: 40
            },
            SecName:
            {
                required: true,
                minlength: 3,
                maxlength: 40
            },
            Email:
            {
                required: true,
                email: true
            },
            password:
            {
                required: true,
                minlength: 3,
                maxlength: 20
            },
            password_again: {
                equalTo: "#password"

            },

           
                hiddenrecapctha: {
                    required: function () {
                        if (grecaptcha.getResponse() == '') {
                            return true;
                        } else {
                            return false;
                        }
                    }
                }
        },
        messages: {
            hiddenrecapctha: {
                required: ""
             
            }
        },
        highlight: function (element) {
            $(element).closest('.form-group').addClass('has-error');

        },
        unhighlight: function (element) {
            $(element).closest('.form-group').removeClass('has-error');
        },
        errorElement: 'span',
        errorClass: 'help-block',
        errorPlacement: function (error, element) {
            if (element.parent('.input-group').length) {
                error.insertAfter(element.parent());
            } else {
                error.insertAfter(element);
            }
        }

    })
});