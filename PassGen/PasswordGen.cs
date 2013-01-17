using System;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;

namespace PassGen
{
    #region Public Enums

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

    public enum PasswordPreset
    {
        BasicNumeric,
        BasicAlphaNumeric,
        BasicAlphaNumericExtended,
        StrongNumeric,
        StrongAlphaNumeric,
        StrongAlphaNumericExtended,
        UltraStrongAlphaNumericExtended
    }

    #endregion  

    public class PasswordGen
    {

        #region Variables and Configuration Data

        RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();

        private char[] numericValues = new char[] {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};
        private char[] alphaLowValues = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        private char[] alahaUpValues = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        private char[] extendedValues = new char[] { '?', '!', '@', '#', '$', '%', '^', '&', '*', '_', '-', '~', '.' };

        private const int presetBasicLength = 8;
        private const int presetStrongLength = 16;
        private const int presetUltraLength = 32;

        #endregion

        #region Public Functions

        /// <summary>
        /// Generates a password for a specified password preset.
        /// </summary>
        /// <param name="passwordPreset">The password preset.</param>
        /// <returns></returns>
        public string Generate(PasswordPreset passwordPreset)
        {
            string retPassword = "";
            bool validPassword = false;
            while (!validPassword)
            {
                switch (passwordPreset)
                {
                    case PasswordPreset.BasicNumeric: // numbers only, minimal length
                        retPassword = Generate(presetBasicLength, PasswordPattern.Numeric, PasswordCase.Lowercase);
                        validPassword = true;
                        break;
                    case PasswordPreset.BasicAlphaNumeric: // numbers and lowercase letters, minimal size
                        retPassword = Generate(presetBasicLength, PasswordPattern.AlphaNumeric, PasswordCase.Lowercase);
                        if (containsOneFromList(retPassword, alphaLowValues) && containsOneFromList(retPassword, numericValues))
                            validPassword = true;
                        break;
                    case PasswordPreset.BasicAlphaNumericExtended: // numbers, lowercase letters and extended chars, minimal size
                        retPassword = Generate(presetBasicLength, PasswordPattern.AlphaNumericExtended, PasswordCase.Lowercase);
                        if (containsOneFromList(retPassword, alphaLowValues) && containsOneFromList(retPassword, numericValues) && containsOneFromList(retPassword,extendedValues))
                            validPassword = true;
                        break;
                    case PasswordPreset.StrongNumeric: // numbers only, secure length
                        retPassword = Generate(presetStrongLength, PasswordPattern.Numeric, PasswordCase.Lowercase);
                        validPassword = true;
                        break;
                    case PasswordPreset.StrongAlphaNumeric: // numbers and lowercase letters, secure length
                        retPassword = Generate(presetStrongLength, PasswordPattern.AlphaNumeric, PasswordCase.Mixed);
                        if (containsOneFromList(retPassword, alphaLowValues) && containsOneFromList(retPassword, alahaUpValues) && containsOneFromList(retPassword, numericValues))
                            validPassword = true;
                        break;
                    case PasswordPreset.StrongAlphaNumericExtended: // numbers, lowercase letters and extended chars, secure length
                        retPassword = Generate(presetStrongLength, PasswordPattern.AlphaNumericExtended, PasswordCase.Mixed);
                        if (containsOneFromList(retPassword, alphaLowValues) && containsOneFromList(retPassword, alahaUpValues) && containsOneFromList(retPassword, numericValues) && containsOneFromList(retPassword, extendedValues))
                            validPassword = true;
                        break;
                    case PasswordPreset.UltraStrongAlphaNumericExtended: // numbers, lowercase letters and extended chars, very long length
                        retPassword = Generate(presetUltraLength, PasswordPattern.AlphaNumericExtended, PasswordCase.Mixed);
                        if (containsOneFromList(retPassword, alphaLowValues) && containsOneFromList(retPassword, alahaUpValues) && containsOneFromList(retPassword, numericValues) && containsOneFromList(retPassword, extendedValues))
                            validPassword = true;
                        break;
                    default:
                        throw new InvalidEnumArgumentException("passwordPreset", (int)passwordPreset, typeof (PasswordPreset));
                }

            }
            return retPassword;
        }

        /// <summary>
        /// Generates a password according to provided parameters.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <param name="passwordPattern">The password pattern.</param>
        /// <param name="passwordCase">The password case.</param>
        /// <returns></returns>
        public string Generate(int length, PasswordPattern passwordPattern, PasswordCase passwordCase)
        {
            StringBuilder sb = new StringBuilder(length);
            char[] passwordValues = new char[0];
            int oldArrayLength = 0;

            switch(passwordPattern)
                {
                    case PasswordPattern.AlphaNumericExtended:
                        // Add Extended Chars
                        oldArrayLength = passwordValues.Length;
                        Array.Resize(ref passwordValues, extendedValues.Length);
                        extendedValues.CopyTo(passwordValues, oldArrayLength);
                        goto case PasswordPattern.Alpha;
                    case PasswordPattern.AlphaNumeric:
                    case PasswordPattern.Alpha:
                        oldArrayLength = passwordValues.Length;
                        switch(passwordCase)
                        {
                            case PasswordCase.Lowercase:
                                Array.Resize(ref passwordValues, passwordValues.Length + alphaLowValues.Length);
                                alphaLowValues.CopyTo(passwordValues, oldArrayLength);
                                break;
                            case PasswordCase.Uppercase:
                                Array.Resize(ref passwordValues, passwordValues.Length + alahaUpValues.Length);
                                alahaUpValues.CopyTo(passwordValues, oldArrayLength);
                                break;
                            case PasswordCase.Mixed:
                                Array.Resize(ref passwordValues, passwordValues.Length + alphaLowValues.Length + alahaUpValues.Length);
                                alphaLowValues.CopyTo(passwordValues, oldArrayLength);
                                alahaUpValues.CopyTo(passwordValues, oldArrayLength + alphaLowValues.Length);
                                break;
                        }
                        if(passwordPattern == PasswordPattern.AlphaNumeric || passwordPattern == PasswordPattern.AlphaNumericExtended)
                            goto case PasswordPattern.Numeric;
                        break;
                    case PasswordPattern.Numeric:
                        oldArrayLength = passwordValues.Length;
                        Array.Resize(ref passwordValues, passwordValues.Length + numericValues.Length);
                        numericValues.CopyTo(passwordValues, oldArrayLength);
                        break;
                }

            for (int i = 0; i < length; i++)
            {
                sb.Append(selectRandomChar(passwordValues));
            }

            return sb.ToString();
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// Determines whether specified password has at least one character from given data set.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="dataSet">The data set.</param>
        /// <returns>
        ///   <c>true</c> if [contains one from list] [the specified password]; otherwise, <c>false</c>.
        /// </returns>
        private bool containsOneFromList(string password, char[] dataSet)
        {
            for(int i = 0; i < dataSet.Length; i++)
            {
                if (password.Contains(dataSet[i].ToString()))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Selects and returns at random one char from a provided char array of possible values.
        /// </summary>
        /// <param name="charSet">The character data set.</param>
        /// <returns></returns>
        private char selectRandomChar(char[] charSet)
        {
            if (charSet.Length > 256)
                throw new ArgumentOutOfRangeException("charSet", "The size of the character data set is larger than allowed");

            int randomValue = getRandomVal(charSet.Length - 1);
            return charSet[randomValue];
        }

        /// <summary>
        /// Gets a single random value from 0 to 255 as returned from RNGCryptoServiceProvider.GetBytes()
        /// </summary>
        /// <returns></returns>
        private int getRandomVal()
        {
            byte[] randomByte = new byte[1];
            rngCsp.GetBytes(randomByte);
            return (int)randomByte[0];
        }

        /// <summary>
        /// Gets a single random value from 0 to and including "maxValue".
        /// </summary>
        /// <param name="maxValue">The max value.</param>
        /// <returns></returns>
        private int getRandomVal(int maxValue)
        {
            int randomValue = getRandomVal();
            while (randomValue > maxValue)
            {
                randomValue = getRandomVal();
            }
            return randomValue;
        }

        #endregion
    }
}