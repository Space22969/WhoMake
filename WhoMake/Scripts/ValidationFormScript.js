$(document).ready(function () {
    console.log("You had been hacked!");
    jQuery.validator.setDefaults({
        debug: true,
        success: "valid"
    });

    $('#RegisForm').validate({
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


    $('#TaskForm').validate({
        ignore: [],
        rules: {
            category_id://1
            {
                required: true
            },
            service_id://2
            {
                required: true
            },
            title_id://3
            {
                minlength: 5,
                maxlength: 300,
                required: true
            },
            description://4
            {
                required: function ()
                {
                  if ($('#summernote').summernote('code') != "<p><br></p>") return false;
                    else return true;

                }
            },
            price_id://5
            {
                digits: true,
                required: true

            },
            date_exeq_begin://6
            {
              
                required: true

            },
            time_exeq_beginId://7
            {
                
                required: true

            },
            date_exeq_endId://8
            {
               
                required: true

            },
            time_exeq_endId://9
            {
                
                required: true

            },
            date_beginId://10
            {
               
                required: true

            },
            date_endId://11
            {
                
                required: true

            },
            locationId://12
            {
               
                required: true

            },
            adres_id://13
            {
                required: true,
                minlength: 5,
                maxlength: 300
            },
            phone_id://14
            {
                required: true

            }
        },
        messages: {
            description: {
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
        errorClass: 'Create-help-block',
        errorPlacement: function (error, element) {
            if (element.parent('.input-group').length) {
                error.insertAfter(element.parent());
            } else {
                error.insertAfter(element);
            }
        }

    })



});