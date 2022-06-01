using System.Text;

namespace PasswordManager
{
    /*
    * Creates a unique String of characters for a strong password
    */
    public class Password
    {
        protected readonly String password;

        public Password(int length = 16, bool includeNumbers = true, bool includeUppercase = true, bool includeSymbols = true)
        {
            this.password = generatePassword(length, createValidChars(includeNumbers, includeUppercase, includeSymbols));
        }

        public Password(String password) { this.password = password; }

        /*
        * Creates a random String of characters that utilizes a List
        * of allowed/valid characters
        */
        protected String generatePassword(int length, List<char> validChars)
        {
            StringBuilder password = new StringBuilder("");

            Random rng = new Random();
            int i = 0;
            // creates the randomized String
            while (i < length)
            {
                int charIndex = rng.Next(0, validChars.Count);
                // this ensures that we don't add a character that is
                // a direct ascension or descension of the character
                // most recently added
                if (i == 0 || (password[i - 1] != validChars[charIndex] - 1 && 
                    password[i - 1] != validChars[charIndex] + 1))
                {
                    password.Append(validChars[charIndex]);
                    validChars.RemoveAt(charIndex);
                    i++;
                }
            }

            return password.ToString();
        }

        /*
        * Creates a List of valid characters that can be used for the 
        * generation of the password
        */ 
        private static List<char> createValidChars(bool includeNumbers, bool includeUppercase, bool includeSymbols)
        {
            List<char> validChars = new List<char>();

            // By default, we include all lowercase characters within the 
            // allowed pool of characters
            for (char i = 'a'; i <= 'z'; i++)
                validChars.Add(i);

            // All of the below add different characters to the pool we
            // can pull from
            if (includeNumbers)
                for (char i = '0'; i <= '9'; i++)
                    validChars.Add(i);
            
            if (includeUppercase)
                for (char i = 'A'; i <= 'Z'; i++)
                    validChars.Add(i);

            if (includeSymbols)
            {
                for (char i = '!'; i <= '/'; i++)
                    validChars.Add(i);

                for (char i = ':'; i <= '@'; i++)
                    validChars.Add(i);

                for (char i = '['; i <= '`'; i++)
                    validChars.Add(i);

                for (char i = '{'; i <= '~'; i++)
                    validChars.Add(i);
            }
            
            return validChars;
        }

        /*
        * Overrides the toString method in order to return this password
        * as a String
        */
        override public String ToString() { return this.password; }

        /*
        * Overrides the + operator in order to allow this Password to be
        * concatenated onto other Strings 
        */ 
        public static String operator+(Password password, String str) { return password.ToString() + str; }
        public static String operator+(String str, Password password) { return str + password.ToString(); }
    }
}