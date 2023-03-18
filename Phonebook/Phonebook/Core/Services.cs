using Phonebook.Controllers;
using Phonebook.Models;
using System.Data;

namespace Phonebook.Core
{
    public class Services
    {
        private IConfiguration Configuration;
        char[] SpecialCharacters = { '§', '®', '™', '©', 'ʬ', '@' };
        List<Contact> ContactList = new List<Contact>();
        public Services( IConfiguration Config)
        {
            this.Configuration = Config;
        }
        public void AddFromFile()
        {
            DatabaseOperations databaseOperations = new DatabaseOperations(Configuration);
            FileOperations fileOperations = new FileOperations();
            var DataFromFile = fileOperations.ReadFile();
            int index;
            for (int i = 0; i < DataFromFile.Rows.Count; i++)
            {
                index = DataFromFile.Rows[i]["#Contact"].ToString().IndexOfAny(SpecialCharacters);
                if (index != -1)
                {
                    ContactList = databaseOperations.GetContact();
                    if (index == 0 || ContactList.Where(e => e.ContactName == DataFromFile.Rows[i]["#Contact"].ToString()).Count() > 0 || !char.IsDigit(DataFromFile.Rows[i]["#Contact"].ToString()[index - 1]))
                        continue;
                    else 
                    {
                        databaseOperations.AddContact(new Contact
                        {
                            ContactName= DataFromFile.Rows[i]["#Contact"].ToString(),
                            MaxNumbers= Convert.ToInt32(DataFromFile.Rows[i]["#MaxNumbers"])
                        });
                    }
                }
                else
                    continue;
            }
        }
        public void AddContact(Contact Contact)
        {
            DatabaseOperations databaseOperations = new DatabaseOperations(Configuration);
            int index = Contact.ContactName.IndexOfAny(SpecialCharacters);
            ContactList = databaseOperations.GetContact();
            if (index != -1)
            {
                if (index != 0 && ContactList.Where(e => e.ContactName == Contact.ContactName).Count() == 0 && char.IsDigit(Contact.ContactName[index - 1]))
                    databaseOperations.AddContact(Contact);
            }
        }
        public void AddNumber(Numbers Number)
        {
            DatabaseOperations databaseOperations = new DatabaseOperations(Configuration);
            int MaxNumbers = databaseOperations.GetMaxNumbers(Number.ContactID);
            int CountOfNumbers = databaseOperations.GetCoutOfNumbers(Number.ContactID);
            if(CountOfNumbers<MaxNumbers)
                databaseOperations.AddNumber(Number);
        }
    }
}
