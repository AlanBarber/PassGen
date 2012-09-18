using System;
using System.Security.Cryptography;
using System.Text;

namespace PassGen
{
    public class PasswordGen
    {
        RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();

        public enum PasswordPattern
        {
            Alpha,
            Numeric,
            AlphaNumeric,
            AlphaNumericExtended
        }

        public enum PasswordCase
        {
            Lowercase,
            Uppercase,
            Mixed
        }

        private char[] numericValues = new char[] {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};
        private char[] alphaLowValues = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        private char[] alahaHighValues = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        private char[] extendedValues = new char[] { '?', '!', '@', '#', '$', '%', '^', '&', '*', '_', '-', '~', '.' };

        public string Generate(int length, PasswordPattern passwordPattern, PasswordCase passwordCase)
        {
            StringBuilder sb = new StringBuilder(length);
            char[] passwordValues = new char[0];

            switch(passwordPattern)
                {
                    case PasswordPattern.Alpha:
                        switch(passwordCase)
                        {
                            case PasswordCase.Lowercase:
                                Array.Resize(ref passwordValues, passwordValues.Length + alphaLowValues.Length);
                                alphaLowValues.CopyTo(passwordValues, passwordValues.Length);
                                break;
                            case PasswordCase.Uppercase:
                                Array.Resize(ref passwordValues, passwordValues.Length + alahaHighValues.Length);
                                alahaHighValues.CopyTo(passwordValues, passwordValues.Length);
                                break;
                            case PasswordCase.Mixed:
                                Array.Resize(ref passwordValues, passwordValues.Length + alphaLowValues.Length + alahaHighValues.Length);
                                alphaLowValues.CopyTo(passwordValues, passwordValues.Length);
                                alahaHighValues.CopyTo(passwordValues, passwordValues.Length);
                                break;
                        }
                        break;
                    case PasswordPattern.Numeric:
                        Array.Resize(ref passwordValues, passwordValues.Length + numericValues.Length);
                        numericValues.CopyTo(passwordValues, passwordValues.Length);
                        break;
                    case PasswordPattern.AlphaNumeric:
                        switch (passwordCase)
                        {
                            case PasswordCase.Lowercase:
                                Array.Resize(ref passwordValues, passwordValues.Length + alphaLowValues.Length);
                                alphaLowValues.CopyTo(passwordValues, passwordValues.Length);
                                break;
                            case PasswordCase.Uppercase:
                                Array.Resize(ref passwordValues, passwordValues.Length + alahaHighValues.Length);
                                alahaHighValues.CopyTo(passwordValues, passwordValues.Length);
                                break;
                            case PasswordCase.Mixed:
                                Array.Resize(ref passwordValues, passwordValues.Length + alphaLowValues.Length + alahaHighValues.Length);
                                alphaLowValues.CopyTo(passwordValues, passwordValues.Length);
                                alahaHighValues.CopyTo(passwordValues, passwordValues.Length);
                                break;
                        }
                        Array.Resize(ref passwordValues, passwordValues.Length + numericValues.Length);
                        numericValues.CopyTo(passwordValues, passwordValues.Length);
                        break;
                    case PasswordPattern.AlphaNumericExtended:
                        switch (passwordCase)
                        {
                            case PasswordCase.Lowercase:
                                Array.Resize(ref passwordValues, passwordValues.Length + alphaLowValues.Length);
                                alphaLowValues.CopyTo(passwordValues, passwordValues.Length);
                                break;
                            case PasswordCase.Uppercase:
                                Array.Resize(ref passwordValues, passwordValues.Length + alahaHighValues.Length);
                                alahaHighValues.CopyTo(passwordValues, passwordValues.Length);
                                break;
                            case PasswordCase.Mixed:
                                Array.Resize(ref passwordValues, passwordValues.Length + alphaLowValues.Length + alahaHighValues.Length);
                                alphaLowValues.CopyTo(passwordValues, passwordValues.Length);
                                alahaHighValues.CopyTo(passwordValues, passwordValues.Length);
                                break;
                        }
                        Array.Resize(ref passwordValues, passwordValues.Length + numericValues.Length);
                        numericValues.CopyTo(passwordValues, passwordValues.Length);
                        Array.Resize(ref passwordValues, passwordValues.Length + passwordValues.Length);
                        extendedValues.CopyTo(passwordValues,passwordValues.Length);
                        break;
                }
            return "";
        }

        /// <summary>
        /// Gets a random value from 0 to 255
        /// </summary>
        /// <returns></returns>
        private int getRandomVal()
        {
            byte[] randomByte = new byte[1];
            rngCsp.GetBytes(randomByte);
            return (int)randomByte[0];
        }

        /// <summary>
        /// Gets a random value with a set upper limit
        /// </summary>
        /// <param name="highValue">The high value.</param>
        /// <returns></returns>
        private int getRandomVal(int highValue)
        {
            int randomValue = getRandomVal();
            while (randomValue > highValue)
            {
                randomValue = getRandomVal();
            }
            return randomValue;
        }
    }
}