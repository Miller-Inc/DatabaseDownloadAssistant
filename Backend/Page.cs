using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseDownloadAssistant.Backend
{
    internal class Page<T> where T : class
    {
        #region Static Methods

        public void CreatePage(string pageName, int recordsCount, int fieldsCount)
        {
            Console.WriteLine("Creating a new page...");
            this.PageName = pageName;
            for (int i = 0; i < recordsCount; i++)
            {
                this.Records.Add(new Record<T>());
                this.Records[i].CreateRecord(string.Concat("record", i), fieldsCount);
            }
        }

        public static void UpdatePage()
        {
            Console.WriteLine("Updating the page...");

        }

        #endregion

        #region Instance Methods

        #endregion

        #region Constructors

        public Page()
        {
            Console.WriteLine("Page object created...");

        }

        #endregion

        #region Fields 

        public string PageName { get; set; } = "defalut page";

        public List<Record<T>> Records { get; set; } = []; // This is a list of records in the page

        #endregion
    }
}
