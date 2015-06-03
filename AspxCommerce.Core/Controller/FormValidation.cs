/*
AspxCommerce® - http://www.aspxcommerce.com
Copyright (c) 2011-2015 by AspxCommerce

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OF OTHER DEALINGS IN THE SOFTWARE. 
*/



using System;
using System.Text.RegularExpressions;
namespace AspxCommerce.Core
{
    /// <summary>
    /// Summary description for FormValidation
    /// </summary>
    public class FormValidation
    {
        public FormValidation()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public bool ValidateValue(string id, int attValType, string val)
        {
            bool isValid = false;
            string[] parsedata = id.Split('_');
            bool isRequired = bool.Parse(parsedata[3]);
            if (isRequired)
            {
                if (val == null && val == "" && val == "undefined")
                {
                    return false;
                }
            }
            switch (attValType)
            {
                case 1: //AlphabetsOnly
                    isValid = IsOnlyAlphabets(val);
                    break;
                case 2: //AlphaNumeric
                    isValid = IsAlphaNumeric(val);
                    break;
                case 3: //DecimalNumber
                    isValid = IsNumber(val);
                    break;
                case 4: //Email
                    isValid = IsValidEmail(val);
                    break;
                case 5: //IntegerNumber
                    isValid = IsInteger(val);
                    break;
                case 6: //Price
                    isValid = IsPrice(val);
                    break;
                case 7: //WebURL
                    isValid = IsURL(val);
                    break;
                case 8:
                    isValid = true;
                    break;
            }
            return isValid;
        }

        public static bool AllowOnlyAlphabetsAndSpace(string str)
        {
            char[] input = str.ToCharArray();
            for (int i = 0; i < input.Length; i++)
            {
                if (!((Convert.ToInt32(input[i]) > 96 && Convert.ToInt32(input[i]) < 123) ||
                    (Convert.ToInt32(input[i]) > 64 && Convert.ToInt32(input[i]) < 91) || Convert.ToInt32(input[i]) == 32))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsStringEmpty(string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsOnlyAlphabets(string str)
        {
            Regex regex = new Regex("[^A-Z|^a-z|^ |^\t]");
            if (regex.IsMatch(str))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsNaturalNumbers(string str)
        {
            Regex regex = new Regex("0*[1-9][0-9]*");
            if (regex.IsMatch(str))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsAlphaNumeric(string str)
        {
            Regex regex = new Regex("^[a-zA-Z0-9]*$");
            if (regex.IsMatch(str))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsPositiveNumber(string str)
        {
            Regex objNotPositivePattern = new Regex("[^0-9.]");
            Regex objPositivePattern = new Regex("^[.][0-9]+$|[0-9]*[.]*[0-9]+$");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            return !objNotPositivePattern.IsMatch(str) && objPositivePattern.IsMatch(str) && !objTwoDotPattern.IsMatch(str);
        }

        public static bool IsNumber(string str)
        {
            Regex objNotNumberPattern = new Regex("[^0-9.-]");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
            String strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
            String strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
            Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");
            return !objNotNumberPattern.IsMatch(str) && !objTwoDotPattern.IsMatch(str) && !objTwoMinusPattern.IsMatch(str) && objNumberPattern.IsMatch(str);
        }

        public static bool IsValidEmail(string email)
        {
            //regular expression pattern for valid email
            //addresses, allows for the following domains:
            //com,edu,info,gov,int,mil,net,org,biz,name,museum,coop,aero,pro,tv
            string pattern = @"^[-a-zA-Z0-9][-.a-zA-Z0-9]*@[-.a-zA-Z0-9]+(\.[-.a-zA-Z0-9]+)*\.(com|edu|info|gov|int|mil|net|org|biz|name|museum|coop|aero|pro|tv|[a-zA-Z]{2})$";
            //Regular expression object
            Regex check = new Regex(pattern, RegexOptions.IgnorePatternWhitespace);
            //boolean variable to return to calling method
            bool valid = false;

            //make sure an email address was provided
            if (string.IsNullOrEmpty(email))
            {
                valid = false;
            }
            else
            {
                //use IsMatch to validate the address
                valid = check.IsMatch(email);
            }
            //return the value to the calling method
            return valid;
        }

        public static bool IsInteger(string value)
        {
            return Regex.IsMatch(value, @"^(\+|-)?\d+$");
			//return Regex.IsMatch(value, @"/^\d+$/");
        }

        public static bool IsPrice(string value)
        {
            if (Regex.IsMatch(value, @"^\$?[0-9]{0,10}(,[0-9]{3})*(\.[0-9]{0,2})?$"))
            {
                if (Regex.IsMatch(value, @"\.[0-9]$"))
                {
                    value += "0";
                }
                else if (Regex.IsMatch(value, @"\.$"))
                {
                    value += "00";
                }
                else if (!Regex.IsMatch(value, @"\.[0-9]{2}$"))
                {
                    value += ".00";
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsValidWebURL(string value)
        {
            return Regex.IsMatch(value, @"^([a-zA-Z0-9])+([.a-zA-Z0-9_-])*@([a-zA-Z0-9_-])+(.[a-zA-Z0-9_-]+)+");
        }

        public static bool IsWithinRange(decimal number, decimal min, decimal max)
        {
            if (number <= min || number >= max)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsUSPhoneNumber(string value)
        {
            Regex phoneRegex = new Regex(@"^\(?[\d]{3}\)?[\s-]?[\d]{3}[\s-]?[\d]{4}$");
            if (!phoneRegex.IsMatch(value))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsUSZipCode(string value)
        {
            Regex zipCodeRegex = new Regex(@"^(?(^00000(|-0000))|(\d{5}(|-\d{4})))$");
            if (!zipCodeRegex.IsMatch(value))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsUSSocialSecurityNumber(string value)
        {
            Regex ssnRegex = new Regex(@"^(?!000)([0-6]\d{2}|7([0-6]\d|7[012]))([ -]?)(?!00)\d\d\3(?!0000)\d{4}$");
            if (!ssnRegex.IsMatch(value))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsUSStateAbbreviation(string value)
        {
            Regex stateRegex = new Regex(@"^(?-i:A[LKSZRAP]|C[AOT]|D[EC]|F[LM]|G[AU]|HI|I[ADLN]|K[SY]|LA|M[ADEHINOPST]|N[CDEHJMVY]|O[HKR]|P[ARW]|RI|S[CD]|T[NX]|UT|V[AIT]|W[AIVY])$");
            if (!stateRegex.IsMatch(value.ToUpper()))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool IsCreditCardNumber(string value)
        {
            Regex creditCardRegex = new Regex(@"(\d{6}[-\s]?\d{12})|(\d{4}[-\s]?\d{4}[-\s]?\d{4}[-\s]?\d{4})");
            if (!creditCardRegex.IsMatch(value))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsCreditCardIDNumber(string value)
        {
            Regex idNumberRegex = new Regex(@"^([0-9]{3,4})$");
            if (!idNumberRegex.IsMatch(value))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsExperationDate(string value)
        {
            Regex expRegex = new Regex(@"^((0[1-9])|(1[0-2]))\/(\d{2})$");
            if (!expRegex.IsMatch(value))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsLongDate(string value)
        {
            Regex longDateRegex = new Regex(@"^((31(?!\ (Feb(ruary)?|Apr(il)?|June?|(Sept|Nov)(ember)?)))|((30|29)(?!\ Feb(ruary)?))|(29(?=\ Feb(ruary)?\ (((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)))))|(0?[1-9])|1\d|2[0-8])\ (Jan(uary)?|Feb(ruary)?|Ma(r(ch)?|y)|Apr(il)?|Ju((ly?)|(ne?))|Aug(ust)?|Oct(ober)?|(Sept|Nov|Dec)(ember)?)\ ((1[6-9]|[2-9]\d)\d{2})$");

            if (!longDateRegex.IsMatch(value))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsShortDate(string value)
        {
            Regex shortDateRegex = new Regex(@"(?\d{1,2})/(?\d{1,2})/(?(?:\d{4}|\d{2}))");
            if (!shortDateRegex.IsMatch(value))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsTime(string value)
        {
            Regex timeRegex = new Regex(@"(?^(?:0?[1-9]:[0-5]|1(?=[012])\d:[0-5])\d(?:[ap]m)?)");
            if (!timeRegex.IsMatch(value))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsIPAdress(string value)
        {
            Regex ipRegex = new Regex(@"^(?:(?:25[0-5]|2[0-4]\d|[01]\d\d|\d?\d)(?(?=\.?\d)\.)){4}$");
            if (!ipRegex.IsMatch(value))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool IsURL(string value)
        {
            Regex urlRegex = new Regex(@"(?(http:[/][/]|www.)([a-z]|[A-Z]|[0-9]|[/.]|[~])*)");
            if (!urlRegex.IsMatch(value))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}