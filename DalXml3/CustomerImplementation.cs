using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO; // חובה עבור File.Exists
using DO;
using DalApi;
using Tools;   // תיקון: using למרחב השמות Tools בלבד

namespace Dal
{
    internal class CustomerImplementation : DalApi.ICustomer
    {
        const string CUSTOMER_TAG = "Customer";
        const string CUSTOMERID_TAG = "CustomerId";
        const string NAME_TAG = "Name";
        const string ADDRESS_TAG = "Adress";
        const string PHONE_TAG = "Phone";

        private string fileXmlName = "customers-data.xml";
        private XElement? rootElement;

        private List<Customer> LoadFromXml()
        {
            if (!File.Exists(fileXmlName)) return new List<Customer>();

            rootElement = XElement.Load(fileXmlName);
            return (from c in rootElement.Elements(CUSTOMER_TAG)
                    select new Customer
                    (
                        int.Parse(c.Element(CUSTOMERID_TAG)?.Value ?? "0"),
                        c.Element(NAME_TAG)?.Value ?? "",
                        c.Element(ADDRESS_TAG)?.Value ?? "",
                        c.Element(PHONE_TAG)?.Value ?? ""
                    )).ToList();
        }

        public int Create(Customer item)
        {
            LogManager.writeToLog("DalXml", "Create", "start");

            XElement root = File.Exists(fileXmlName) ? XElement.Load(fileXmlName) : new XElement("Customers");

            root.Add(new XElement(CUSTOMER_TAG,
                new XElement(CUSTOMERID_TAG, item.CustomerId),
                new XElement(NAME_TAG, item.Name),
                new XElement(ADDRESS_TAG, item.Adress),
                new XElement(PHONE_TAG, item.Phone)));

            root.Save(fileXmlName);

            LogManager.writeToLog("DalXml", "Create", "end");
            return item.CustomerId;
        }

        // --- פונקציות Read הנדרשות לפי הממשק ---

        // 1. קריאה לפי פילטר
        public Customer? Read(Func<Customer, bool>? filter)
        {
            LogManager.writeToLog("DalXml", "Read", "start");
            var customers = LoadFromXml();
            var customer = customers.FirstOrDefault(filter ?? (c => true));
            LogManager.writeToLog("DalXml", "Read", "end");
            return customer;
        }

        // 2. קריאה לפי ID (פותר את CS0535)
        public Customer? Read(int id)
        {
            return Read(c => c.CustomerId == id);
        }

        // 3. קריאה של האובייקט הראשון/כללי (פותר את CS0535)
        public Customer? Read()
        {
            var customers = LoadFromXml();
            return customers.FirstOrDefault();
        }

        public List<Customer> ReadAll(Func<Customer, bool>? filter = null)
        {
            LogManager.writeToLog("DalXml", "ReadAll", "start");
            var customers = LoadFromXml();
            var result = filter == null ? customers : customers.Where(filter).ToList();
            LogManager.writeToLog("DalXml", "ReadAll", "end");
            return result;
        }

        public void Update(Customer item)
        {
            LogManager.writeToLog("DalXml", "Update", "start");
            XElement root = XElement.Load(fileXmlName);
            XElement? toUpdate = (from c in root.Elements(CUSTOMER_TAG)
                                  where int.Parse(c.Element(CUSTOMERID_TAG)!.Value) == item.CustomerId
                                  select c).FirstOrDefault();

            if (toUpdate == null)
                throw new IdNotFoundException($"Customer with ID {item.CustomerId} not found for update.");

            toUpdate.Element(NAME_TAG)!.Value = item.Name;
            toUpdate.Element(ADDRESS_TAG)!.Value = item.Adress;
            toUpdate.Element(PHONE_TAG)!.Value = item.Phone;

            root.Save(fileXmlName);
            LogManager.writeToLog("DalXml", "Update", "end");
        }

        public void Delete(int id)
        {
            LogManager.writeToLog("DalXml", "Delete", "start");
            XElement root = XElement.Load(fileXmlName);
            XElement? toDelete = (from c in root.Elements(CUSTOMER_TAG)
                                  where int.Parse(c.Element(CUSTOMERID_TAG)!.Value) == id
                                  select c).FirstOrDefault();

            if (toDelete == null)
                throw new IdNotFoundException($"Customer with ID {id} not found for deletion.");

            toDelete.Remove();
            root.Save(fileXmlName);
            LogManager.writeToLog("DalXml", "Delete", "end");
        }
    }
}