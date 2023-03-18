using System.Data;

namespace Phonebook.Core
{
    public class FileOperations
    {
        public DataTable ReadFile()
        {
            DataTable datatable = new DataTable();
            StreamReader streamreader = new StreamReader(@"C:\Users\lulud\OneDrive\Desktop\PhoneBook-Data.txt");
            char[] delimiter = new char[] { '\t' };
            string[] columnheaders = streamreader.ReadLine().Split(delimiter);
            foreach (string columnheader in columnheaders)
            {
                datatable.Columns.Add(columnheader); // I've added the column headers here.
            }
            while (streamreader.Peek() > 0)
            {
                DataRow datarow = datatable.NewRow();
                datarow.ItemArray = streamreader.ReadLine().Split(delimiter);
                datatable.Rows.Add(datarow);
            }
            return datatable;
        }
    }
}
