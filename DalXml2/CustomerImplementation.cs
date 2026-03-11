using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
//using Tools;
namespace Dal
{
    internal class CustomerImplementation : ICustomer
    {
        XElement fileXmlName = XElement.Load("customers");

        const string CUSTOMER = "customers";
        const string CUSTOMERID = "CustomerId";
        const string NAME = "Name";
        const string ADDRESS = "Adress";
        const string PHONE = "Phone";
        //private fileXmlName="data-config";

        public int Create(Customer item)
        {
            if (fileXmlName.Descendants(CUSTOMER).Any(c => c.Element(CUSTOMERID) == item.customerId))
                fileXmlName.Add(new XElement(CUSTOMERID, item.id),
                    new XElement(NAME, item.name),
                    new XElement(ADDRESS, item.adress),
                    new XElement(PHONE, item.phone));
            fileXmlName.Save(CUSTOMER);
            return item.CustomerId;
        }

        public Customer? Read(Func<Customer, bool>? filter)
        {

        }

        public Customer? Read(int id)
        {
            if (fileXmlName.Descendants(CUSTOMER).Any(c => c.Element(CUSTOMERID) == null))
                throw new Exception("id not found");
            var customer = fileXmlName.Descendants(CUSTOMER).FirstOrDefault(C=>(int?)C.Element(CUSTOMERID)==id);
            return new Customer(
                customerId= 
                
                
                );
        }

        public List<Customer> ReadAll(Func<Customer, bool>? filter = null)
        {

        }


        public void Update(Customer item)
        {

        }
        public void Delete(int id)
        {

        }
    }
}
