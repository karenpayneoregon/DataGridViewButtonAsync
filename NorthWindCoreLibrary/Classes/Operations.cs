using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NorthWindCoreLibrary.Data;


namespace NorthWindCoreLibrary.Classes
{
    public class Operations
    {
        public static void WarmUp()
        {
            using var context = new Context();
            var _ = context.Customers.Count();
        }

        public static List<CustomerItem> LoadCustomerData()
        {
            using var context = new Context();

            return context.Customers
                .Include(customer => customer.ContactTypeIdentifierNavigation)
                .Select(customer => new CustomerItem
                {
                    CustomerIdentifier = customer.CustomerIdentifier, CompanyName = customer.CompanyName, ContactName = customer.ContactName,
                    ContactTitle = customer.ContactTypeIdentifierNavigation.ContactTitle,
                    City = customer.City,
                    Country = customer.Country
                }).ToList();
        }

        public static List<ContactTypeItem> LoadContactTypes()
        {
            using var context = new Context();

            return context.ContactType
                .Select(contact => new ContactTypeItem
                {
                    ContactTypeIdentifier = contact.ContactTypeIdentifier, 
                    ContactTitle = contact.ContactTitle
                })
                .ToList();

        }
    }
}
