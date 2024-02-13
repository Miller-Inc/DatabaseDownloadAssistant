using MillerInc.Convert.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseDownloadAssistant.Backend
{
    internal class Database<T> where T : class 
    {
        #region Methods

        public void CreateDatabase(string databaseName, int pages, int records, int fields)
        {
            Console.WriteLine("Creating a new database...");
            this.DatabaseName = databaseName;
            for (int i = 0; i < pages; i++)
            {
                this.Pages.Add(new Page<T>());
                this.Pages[i].CreatePage(string.Concat("page", i), records, fields);
            }
        }

        [RequiresDynamicCode("Calls System.Text.Json.JsonSerializer.Serialize<TValue>(TValue, JsonSerializerOptions)")]
        public void SaveDatabase(string filePath)
        {
            Console.WriteLine("Saving the database...");
            string? json = System.Text.Json.JsonSerializer.Serialize(this);
            System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(filePath) ?? "");
            File.WriteAllText(filePath, json);
        }

        public void LoadDataBase(string filePath, bool encrypted = false, string key = "")
        {
            string? json = "";
            if (encrypted)
            {
                json = this.DecryptDatabase(key, File.ReadAllText(filePath));
            }
            else
            {
                json = File.ReadAllText(filePath);
            }
            json ??= "";
            Newtonsoft.Json.JsonConvert.PopulateObject(json, this);
        }

        public string EncryptDatabase(string password)
        {
            Console.WriteLine("Encrypting the database...");
            string? json = System.Text.Json.JsonSerializer.Serialize(this);
            json ??= "";
            var encryptedString = AesOperation.EncryptString(password, json);
            return encryptedString;
        }

        public string DecryptDatabase(string password, string encryptedString)
        {
            Console.WriteLine("Decrypting the database...");
            string? json = AesOperation.DecryptString(password, encryptedString);
            return json;
        }

        #endregion

        #region Instance Methods

        #endregion

        #region Constructors

        public Database()
        {
            Console.WriteLine("Database object created...");

        }

        #endregion

        #region Fields 

        public string DatabaseName { get; set; } = "defalut.db";

        public List<Page<T>> Pages { get; set; } = []; // This is a list of pages in the database

        #endregion
    }
}
