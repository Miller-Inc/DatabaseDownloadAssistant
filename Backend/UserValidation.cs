using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace DatabaseDownloadAssistant.Backend
{
    internal class UserValidation
    {
        public static User ValidateUser(string username, string password, string key)
        {
            if (username == null || password == null)
            {
                return new User() { Username = "guest", Role = Role.Guest, IsLoggedIn = false }; // This is a validation error
            }

            // Download the database
            string filePath = Environment.GetFolderPath((Environment.SpecialFolder.ApplicationData)) + @"\MillerInc\RecipeDB\";
            if (Directory.Exists(filePath) == false)
            {
                Directory.CreateDirectory(filePath);
            }
            filePath += "db.db";
            MillerInc.Net.Downloader.DownloadFileAsync(DatabaseURL, filePath);
            while (System.IO.File.Exists(filePath) == false)
            {
                System.Threading.Thread.Sleep(1000);
            }
            Database<string> db = new();
            db.LoadDataBase(filePath, true, key);
            System.IO.File.Delete(filePath);
            
            // Validate user
            for (int i = 0; i < db.Pages.Count; i++)
            {
                if (db.Pages[i].PageName == "Users")
                {
                    for (int j = 0; j < db.Pages[i].Records.Count; j++)
                    {
                        if (db.Pages[i].Records[j].RecordName == "Usernames")
                        {
                           for (int k = 0; k < db.Pages[i].Records[j].Fields.Count; k++)
                            {
                               if (db.Pages[i].Records[j].Fields[k].FieldValue == username)
                               {
                                    if (db.Pages[i].Records[j + 1].Fields[k].FieldValue == password)
                                    {
                                        string? role = db.Pages[i].Records[j + 2].Fields[k].FieldValue;
                                        role ??= "Guest"; 
                                        Role roleType = Enum.Parse<Role>(role);
                                        Console.WriteLine("user logged in..."); 
                                        return new User() { Username = username, Role = roleType, IsLoggedIn = true }; // The user is valid
                                    }
                               }
                           }
                        }
                    }
                }
            }


            return new User() { Username = "guest", Role = Role.Guest, IsLoggedIn = false }; // This is a validation error; // The user is not valid
        }

        private static string DatabaseURL = @"https://raw.githubusercontent.com/Miller-Inc/DatabaseDownloadAssistant/master/Database/UserDB.db";
    }
}
