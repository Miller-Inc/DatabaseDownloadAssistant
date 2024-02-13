using DatabaseDownloadAssistant.Backend;
using MillerInc.UI.OutputFile;

namespace DatabaseDownloadAssistant
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string key; // The key to encrypt the database
            if (args.Length > 0)
            {
                key = args[0];
                if (args[1] == "new")
                {
                    BuildNewDatabase();
                }
                else if (args[0] == "validate")
                {
                    string username = args[1];
                    string password = args[2];
                    Console.WriteLine(username + " " + password);
                    UserValidation.ValidateUser(username, password);
                }
            }
        }

        static void BuildNewDatabase()
        {
            // Create a new database
            var db = new Database<string>();
            db.CreateDatabase("Users", 2, 3, 1);
            db.EncryptDatabase("b14ca5898a4e4133bbce2ea2315a1916");
            Console.WriteLine("Database created...");
            Console.WriteLine(db.EncryptDatabase("b14ca5898a4e4133bbce2ea2315a1916"));
            Output.WriteLine("output.db",db.DecryptDatabase("b14ca5898a4e4133bbce2ea2315a1916", db.EncryptDatabase("b14ca5898a4e4133bbce2ea2315a1916")));
        }
    }
}
