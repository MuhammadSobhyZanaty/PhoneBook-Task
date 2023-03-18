using Microsoft.AspNetCore.Mvc.Formatters;
using Phonebook.Models;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Text;

namespace Phonebook.Core
{
    public class DatabaseOperations
    {
        private IConfiguration Configuration;
        private string ConnectionString;
        public DatabaseOperations(IConfiguration Config)
        {
            this.Configuration = Config;
            ConnectionString = this.Configuration.GetConnectionString("DefaultConnectionString");
        }
        public void AddNumber(Numbers Number)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            string Query = String.Format("insert into number values ({0},{1})", Number.Number,Number.ContactID);
            SqlCommand command = new SqlCommand(Query, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }        
        public void AddContact(Contact Contact)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            string Query = String.Format("insert into Contact (ContactName, MaxNumbers) values ('{0}',{1})", Contact.ContactName,Contact.MaxNumbers);
            SqlCommand command = new SqlCommand(Query, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
        public List<ContactNumbers> GetAllData()
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            string Query = String.Format("SELECT * FROM Contact");
            SqlCommand command = new SqlCommand(Query, connection);
            SqlDataReader dr = command.ExecuteReader();
            List<ContactNumbers> data = new List<ContactNumbers>();
            while (dr.Read())
            {
                data.Add(new ContactNumbers
                {
                    ContactID = Convert.ToInt32(dr["ContactID"]),
                    ContactName = dr["ContactName"].ToString(),
                    MaxNumbers= Convert.ToInt32(dr["MaxNumbers"]),
                    Number= dr["Number"].ToString(),
                    //NumberID= Convert.ToInt32(dr["NumberID"])
                });
            }
            connection.Close();
            return data;
        }
        public int GetMaxNumbers(int ContactId)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            string Query = String.Format("SELECT MaxNumbers FROM Contact where ContactID = {0}",ContactId);
            SqlCommand command = new SqlCommand(Query, connection);
            return (int)command.ExecuteScalar();
        }
        public int GetCoutOfNumbers(int ContactId)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            string Query = String.Format("SELECT count(NumberID) FROM Number where ContactID = {0}",ContactId);
            SqlCommand command = new SqlCommand(Query, connection);
            return (int)command.ExecuteScalar();
        }
        public List<Contact> GetContact()
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            string Query = String.Format("SELECT * FROM Contact");
            SqlCommand command = new SqlCommand(Query, connection);
            SqlDataReader dr = command.ExecuteReader();
            List<Contact> data = new List<Contact>();
            while (dr.Read())
            {
                data.Add(new Contact
                {
                    ContactID = Convert.ToInt32(dr["ContactID"]),
                    ContactName = dr["ContactName"].ToString(),
                    MaxNumbers = Convert.ToInt32(dr["MaxNumbers"])
                });
            }
            connection.Close();
            return data;
        }
    }
}
