var RandomNumber = 0;
$.fn.quickValidate = function()
{
    var $form = this,
        $inputs = $form.find('input:text, input:password'),
    $submit = $form.find('input:button');
    var filters = {
        firstname: {
            regex: /^[A-Za-z]{2,}$/,
            error: 'must only contain letters and must be at lest 2 characters long'
        },
        lastname: {
            regex: /^[A-Za-z]{2,}$/,
            error: 'must only contain letters and must be at lest 2 characters long'
        },
        fullname: {
            regex: /^[A-Za-z]{4,}$/,
            error: 'Must be at least 4 characters long, and must only contain letters.'
        },
        pass: {
            regex: /(?=.*\d).{6,}/,
            error: 'Must be at least 6 characters long and contain at least one number.'
        },

        Repass: {
            regex: /(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,}/,
            error: 'the two password doesnot match'
        },
        email: {
            regex: /^[\w\-\.\+]+\@[a-zA-Z0-9\.\-]+\.[a-zA-z0-9]{2,4}$/,
            error: 'Must be a valid e-mail address (user@gmail.com)'
        },
        phone: {
            regex: /^[2-9]\d{2}-\d{3}-\d{4}$/,
            error: 'Must be a valid US phone number (999-999-9999)'
        }
    };
    var validate = function(klass, value)
    {
        var isValid = true,
            error = '';
        if (!value && /required/.test(klass)) {
            error = 'This field is required';
            isValid = false;
        } else {
            klass = klass.split(/\s/);
            $.each(klass, function(i, k)
            {
                if (filters[k]) {
                    if (value && !filters[k].regex.test(value)) {
                        isValid = false;
                        error = filters[k].error;
                    }
                }
            });
        }
        return {
            isValid: isValid,
            error: error
        }
    };
    var printError = function($input)
    {
        var klass = $input.attr('class'),
                value = $input.val(),
                test = validate(klass, value),
                $error = $('<span class="error">' + test.error + '</span>'),
                $icon = $('<i class="error-icon"></i>');
        $input.removeClass('invalid').siblings('.error, .error-icon').remove();
        if ($input.attr('class') == 'required Repass') {

            if ($('.Repass').val() == $('.pass').val() && $('.Repass').val().length > 0) {
                $input.removeClass('invalid').siblings('.error, .error-icon').remove();
            }
            else {
                $input.addClass('invalid');
                $error.add($icon).insertAfter($input);
                $icon.hover(function()
                {
                    $(this).siblings('.error').toggle();
                });
            }
        }
        else {

            if (!test.isValid) {
                $input.addClass('invalid');
                $error.add($icon).insertAfter($input);
                $icon.hover(function()
                {
                    $(this).siblings('.error').toggle();
                });

            }
        }
    };
    $inputs.each(function()
    {
        if ($(this).is('.required')) {
            printError($(this));
        }
    });
    $inputs.keyup(function()
    {
        printError($(this));
        $(this).next().hover();

    });
    $submit.click(function(e)
    {
        if ($form.find('input.invalid').length) {
            e.preventDefault();
            alert('The form does not validate! Check again...');
        }
        else {
            if (RandomNumber == $('#txtSum').val().trim()) {
                alert('The sum is correct');
            }
            else {
                alert('all Form are good');
            }
        }
    });

    return this;
};
function sum()
{
    var Randomnumber1 = Math.floor(Math.random() * 10);
    var Randomnumber2 = Math.floor(Math.random() * 10);

    $('#Sum').text(' ' + Randomnumber1 + '+' + Randomnumber2);
    RandomNumber = Randomnumber1 + Randomnumber2;
}
sum();
