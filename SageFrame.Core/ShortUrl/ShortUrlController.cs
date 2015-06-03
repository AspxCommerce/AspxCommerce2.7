using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SageFrame.Core
{
    /// <summary>
    /// Business logic for URL shortening.
    /// </summary>
    public class ShortUrlController
    {
        /// <summary>
        /// Encodes the provided url by mixing with random character.
        /// </summary>
        /// <param name="url">User friendly URL.</param>
        /// <returns>Encoded URL.</returns>
        public string EncodeUrl(string url)
        {
            string shortUrlkey = string.Empty;
            string code = string.Empty;
            ShortUrlProvider provider = new ShortUrlProvider();
            while (true)
            {
                string randomText = GenerateRandomCode();
                string random = GetRandomText(randomText);
                code = provider.EncodeUrl(url, random);
                if (code != "-1")
                    break;
            }
            string encodedUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/suid/" + code;
            return encodedUrl;
        }

        /// <summary>
        /// Decodes the  encrypted URL.
        /// </summary>
        /// <param name="key">encrpted URL.</param>
        /// <returns>User friendly URL.</returns>
        public string DecodeUrl(string key)
        {
            ShortUrlProvider provider = new ShortUrlProvider();
            string decodedUrl = provider.DecodeUrl(key);
            return decodedUrl;
        }

        /// <summary>
        /// Checks if the random text is valid or not.
        /// </summary>
        /// <param name="ramdomText">Random text.</param>
        /// <returns>True if the text is valid.</returns>
        public bool CheckValidCode(string ramdomText)
        {
            bool isValid = false;
            string checkValue = GetRandomText(ramdomText);
            if (ramdomText.Equals(checkValue))
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Generates random text for any string.
        /// </summary>
        /// <param name="randomText">String to be randomized.</param>
        /// <returns>Random text.</returns>
        protected string GetRandomText(string randomText)
        {

            char[] randomChars = randomText.ToCharArray();

            int[] randomCharAsci = new int[7];
            for (int i = 0; i < 7; i++)
            {
                randomCharAsci[i] = (int)randomChars[i];
            }

            int asciSum = 0;

            for (int i = 0; i < 7; i++)
            {
                asciSum += randomCharAsci[i];
            }

            int divider = randomCharAsci[1];
            int subtractValue = asciSum % divider;

            int[] randomCharAsciSubtracted = new int[7];
            for (int i = 0; i < 7; i++)
            {
                randomCharAsciSubtracted[i] = ((randomCharAsci[i] - subtractValue) > 0) ? (randomCharAsci[i] - subtractValue) : randomCharAsci[i];
            }

            asciSum = 0;
            for (int i = 0; i < 7; i++)
            {
                asciSum += randomCharAsciSubtracted[i];
            }
            divider = randomCharAsciSubtracted[3];
            int lastCharAsciValue = asciSum % divider;
            char lastValue;
            if ((lastCharAsciValue >= 48 && lastCharAsciValue <= 57) || (lastCharAsciValue >= 65 && lastCharAsciValue <= 90) || (lastCharAsciValue >= 97 && lastCharAsciValue <= 122))
            {
                lastValue = (char)lastCharAsciValue;
            }
            else
            {
                if (lastCharAsciValue < 48)
                {
                    lastCharAsciValue = (lastCharAsciValue % 10) + 48;
                }
                else if (lastCharAsciValue < 65)
                {
                    lastCharAsciValue = (lastCharAsciValue % 26) + 65;
                }
                else
                {
                    lastCharAsciValue = (lastCharAsciValue % 26) + 97;
                }
                lastValue = (char)lastCharAsciValue;
            }
            randomText = randomText + lastValue;
            return randomText;

        }

        /// <summary>
        /// Generates random string.
        /// </summary>
        /// <returns>Random string.</returns>
        private string GenerateRandomCode()
        {
            Random random = new Random();
            string s = "";
            string[] CapchaValue = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "a", "B", "b", "C", "c", "D", "d", "E", "e", "F", "f", "G", "g", "H", "h", "I", "i", "J", "j", "K", "k", "L", "l", "M", "m", "N", "n", "O", "o", "P", "p", "Q", "q", "R", "r", "S", "s", "T", "t", "U", "u", "V", "v", "W", "w", "X", "x", "Y", "y", "Z", "z" };
            for (int i = 0; i < 7; i++)
                s = String.Concat(s, CapchaValue[random.Next(36)]);
            return s;
        }
    }
}
