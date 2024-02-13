namespace DatabaseDownloadAssistant.Backend
{
    public class Field<T> where T : class
    {

        #region Instance Methods

        #endregion

        #region Constructors

        public Field()
        {
            Console.WriteLine("Field object created...");
        }

        #endregion

        #region Fields 

        public string FieldName { get; set; } = "defalut.db";

        public T? FieldValue { get; set; } // This is the value of the field

        #endregion
    }
}