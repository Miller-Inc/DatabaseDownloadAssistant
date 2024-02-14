using DatabaseDownloadAssistant.Backend;
using MillerInc.UI.OutputFile;

namespace DatabaseDownloadAssistant
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                if (args[0] == "new")
                {
                    BuildNewDatabase();
                }
                else if (args[0] == "validate")
                {
                    string username = args[1];
                    string password = args[2];
                    string key = args[3];
                    if (key == "")
                    {
                        key = "b14ca5898a4e4133bbce2ea2315a1916";
                    }
                    Console.WriteLine(username + " " + password);
                    User  user = UserValidation.ValidateUser(username, password, key);
                    string filePath = Environment.GetFolderPath((Environment.SpecialFolder.ApplicationData)) + @"\MillerInc\RecipeDB\user.exe"; 
                    user.SaveUser(filePath);
                }
                else if (args[0] == "download")
                {
                    
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
