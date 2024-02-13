using System.Text.Json.Nodes;

namespace DatabaseDownloadAssistant.Backend
{
    public class Record<T> where T : class
    {
        #region Static Methods

        public void CreateRecord(string recordName, int fieldCount)
        {
            Console.WriteLine("Creating a new record...");
            this.RecordName = recordName;
            for (int i = 0; i < fieldCount; i++)
            {
                this.Fields.Add(new Field<T>());
                this.Fields[i].FieldName = string.Concat("field", i);
            }
        }

        public static void DownloadRecord()
        {
            Console.WriteLine("Downloading the record...");

        }

        public static void UpdateRecord()
        {
            Console.WriteLine("Updating the record...");

        }

        #endregion

        #region Instance Methods

        #endregion

        #region Constructors

        public Record()
        {
            Console.WriteLine("Record object created...");

        }

        #endregion

        #region Fields 

        public string RecordName { get; set; } = "defalut.db";

        public List<Field<T>> Fields { get; set; } = []; // This is a list of fields in the record

        #endregion
    }
}