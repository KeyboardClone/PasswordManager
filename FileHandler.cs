using System.Text;

namespace PasswordManager
{
    public class FileHandler
    {
        private static readonly String fileName = "data.txt";

        /*
        * Attempts to load the Passwords from a file, if the file exists
        * to begin with
        */
        public static List<Password> loadPasswords(List<Password> passwords = null)
        {
            if (passwords == null)
                passwords = new List<Password>();

            FileInfo file = new FileInfo(fileName);
            if (!file.Exists)
                return passwords;

            // if this file exists, we assume it's been encryped and
            // therefore must be decrypted
            file.Decrypt();
            
            foreach (String line in System.IO.File.ReadLines(fileName))
                passwords.Add(new Password(line));
            
            file.Encrypt();

            return passwords;
        }

        /*
        * Saves the Passwords to the file, encrypting it as well
        */
        public static void savePasswords(List<Password> passwords)
        {
            if (passwords == null)
                return;
            
            FileInfo file = new FileInfo(fileName);
            if (!file.Exists)
                file.Create();

            StringBuilder contents = new StringBuilder();
            foreach (Password password in passwords)
                contents.Append(password.ToString() + "\n");

            System.IO.File.WriteAllText (fileName, contents.ToString());

            file.Encrypt();
        }
    }
}