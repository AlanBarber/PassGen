using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PassGen
{
    class Program
    {
        static void Main(string[] args)
        {
            ArgumentParser argumentParser = new ArgumentParser();

            Dictionary<string, string> arguments = argumentParser.Parse(args);


            
            
            PasswordGen passwordGen = new PasswordGen();

            int passwordLength = 16;
            PasswordCase passwordCase = PasswordCase.Mixed;
            PasswordPattern passwordPattern = PasswordPattern.AlphaNumericExtended;
            PasswordPreset? passwordPreset = null;

            const string errorMessageUnknownOption = "unknown option: {0}";
            const string errorMessageMissingOptionValue = "Missing value for option : {0}";
            const string errorMessageInvalidOptionValue = "Invalid value '{1}' for option : {0}";
            string errorMessage = "";
            bool showHelp = false;

            // Parse command args
            foreach (string s in args)
            {
                Console.WriteLine(s);
            }

            for(int i = 0; i < args.Length; i++)
            {
                // Case
                if (args[i] == "-c")
                {
                    i++;
                    if(i >= args.Length)
                    {
                        errorMessage = String.Format(errorMessageMissingOptionValue,args[i]);
                    } 
                    else
                    {
                        switch(args[i].ToUpper())
                        {
                            case "LOWER":
                                passwordCase = PasswordCase.Lowercase;
                                break;
                            case "UPPER":
                                passwordCase = PasswordCase.Uppercase;
                                break;
                            case "MIXED":
                                passwordCase = PasswordCase.Mixed;
                                break;
                            default:
                                errorMessage = String.Format(errorMessageInvalidOptionValue,args[i-1],args[i]);
                                break;
                        }
                    }
                }
                if (args[i].StartsWith("--case"))
                {
                    if(!args[i].Contains("="))
                    {
                        errorMessage = String.Format(errorMessageMissingOptionValue, args[i]);
                    }
                    else
                    {
                        
                    }
                }
                // Help
                if (args[i] == "--help")
                {
                    showHelp = true;
                    errorMessage = "";
                    break;
                }
                // Length
                if (args[i] == "-l")
                {
                    
                }
                if (args[i].StartsWith("--length"))
                {
                    
                }
                // Pattern
                if (args[i] == "-p")
                {

                }
                if (args[i].StartsWith("--pattern"))
                {
                    
                }
                // Preset
                if (args[i].StartsWith("--preset"))
                {
                    
                }
                // Break out of loop if we have any error message
                if (showHelp || errorMessage.Length > 0)
                    break;
            }

            if(showHelp)
            {
                displayHelp(errorMessage);
            } else
            {
                if (passwordPreset != null)
                {
                    Console.WriteLine(passwordGen.Generate((PasswordPreset) passwordPreset));
                } else
                {
                    Console.WriteLine(passwordGen.Generate(passwordLength, passwordPattern, passwordCase));
                }
            }
                


#if DEBUG
            Console.Write("Press any key to continue...");
            Console.ReadKey();
#endif
        }

        static void displayHelp(string errorMessage = "")
        {
            Console.WriteLine("PassGen");
            Console.WriteLine("Copyright (c) 2012 Alan Barber");
            Console.WriteLine("");
            if (errorMessage.Length > 0)
            {
                Console.WriteLine("Unknown option: " + errorMessage + "\n");
            }
            Console.WriteLine("Usage: passgen [options]");
            Console.WriteLine("");
            Console.WriteLine("-c CASE, --case=CASE");
            Console.WriteLine("");
            Console.WriteLine("    Sets the expected case of Alpha (a-z/A-Z) chars generated password.");
            Console.WriteLine("");
            Console.WriteLine("    The following are valid values for CASE:");
            Console.WriteLine("    Lower - Only lowercase letters");
            Console.WriteLine("    Upper - Only uppercase letters");
            Console.WriteLine("    Mixed - Use both lowercase and uppercase letters");
            Console.WriteLine("");
            Console.WriteLine("--help ");
            Console.WriteLine("");
            Console.WriteLine("    Output a brief help message.");
            Console.WriteLine("");
            Console.WriteLine("-l NUM, --length=NUM");
            Console.WriteLine("");
            Console.WriteLine("    Sets how long of a password to generate.");
            Console.WriteLine("");
            Console.WriteLine("-p PATTERN, --pattern=PATTERN");
            Console.WriteLine("");
            Console.WriteLine("    Defines the set of chars to generate the password from.");
            Console.WriteLine("");
            Console.WriteLine("    The following are valid values for PATTERN:");
            Console.WriteLine("    Alpha                - Letters only");
            Console.WriteLine("    Numeric              - Numbers only");
            Console.WriteLine("    AlphaNumeric         - Letters and numbers");
            Console.WriteLine("    AlphaNumericExtended - Letters, numbers and other chars");
            Console.WriteLine("");
            Console.WriteLine("--preset=PRESET");
            Console.WriteLine("");
            Console.WriteLine("    Preset allows for specifying a pre-designed password that includes");
            Console.WriteLine("    the case, pattern and length. Using the preset option will cause the");
            Console.WriteLine("    program to ignore other options provided.");
            Console.WriteLine("");
            Console.WriteLine("    The following are valid values for PESET:");
            Console.WriteLine("    BasicNumeric");
            Console.WriteLine("        Minimum length (8) with only numbers");
            Console.WriteLine("    BasicAlphaNumeric");
            Console.WriteLine("        Minimum length (8) with lowercase letters and numbers");
            Console.WriteLine("    BasicAlphaNumericExtended");
            Console.WriteLine("        Minimum length (8) with lowercase letters, numbers and other chars");
            Console.WriteLine("    StrongNumeric");
            Console.WriteLine("        Secure length (16) with only numbers");
            Console.WriteLine("    StrongAlphaNumeric");
            Console.WriteLine("        Secure length (16) with mixed-case letters and numbers");
            Console.WriteLine("    StrongAlphaNumericExtended");
            Console.WriteLine("        Secure length (16) with mixed-case letters, numbers and other chars");
            Console.WriteLine("    UltraStrongAlphaNumericExtended");
            Console.WriteLine("        Long length (32) with mixed case letters, numbers and other chars");
            Console.WriteLine("");
            Console.WriteLine("If no command line options are specified the program will generate a password");
            Console.WriteLine("using the default preset of BasicAlphaNumericExtended.");
            Console.WriteLine("");
            Console.WriteLine("Please make feature requests and/or bug reports to");
            Console.WriteLine("https://github.com/AlanBarber/PassGen/issues");
            Console.WriteLine("");
        }
    }
}
