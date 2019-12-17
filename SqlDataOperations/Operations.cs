using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using SqlDataOperations.Classes;
using TeamBaseLibrary;

namespace SqlDataOperations
{
    /// <summary>
    /// BaseSqlServerConnections handles connections which can be overridden
    /// along with implementing BaseExceptionhandler for implementing default 
    /// exception handling in try/catch statements
    /// </summary>
    public class Operations : BaseSqlServerConnections
    {
        public Operations()
        {
            DefaultCatalog = "NorthWindAzure";
        }

        public DataTable LoadCustomerData(bool pHidePrimaryKey = true)
        {
            mHasException = false;

            var dt = new DataTable();
            using (var cn = new SqlConnection() { ConnectionString = ConnectionString })
            {
                using (var cmd = new SqlCommand() { Connection = cn })
                {
                    try
                    {
                        cmd.CommandText = 
                            "SELECT C.CustomerIdentifier, C.CompanyName, C.ContactName, ContactType.ContactTitle, " + 
                                   "C.City, C.Country, C.ContactTypeIdentifier " + 
                             "FROM dbo.Customers AS C " + 
                             "INNER JOIN ContactType ON C.ContactTypeIdentifier = ContactType.ContactTypeIdentifier;";

                        cn.Open();
                        dt.Load(cmd.ExecuteReader());

                        dt.Columns["ContactTypeIdentifier"].ColumnMapping = MappingType.Hidden;

                        if (pHidePrimaryKey)
                        {
                            dt.Columns["CustomerIdentifier"].ColumnMapping = MappingType.Hidden;
                        }
                    }
                    catch (Exception ex)
                    {
                        mHasException = true;
                        mLastException = ex;
                    }
                }
            }

            return dt;
        }

        public async Task<List<ContactType>> LoadContactTypes()
        {
            mHasException = false;

            var contactList = new List<ContactType>();

            using (var cn = new SqlConnection() { ConnectionString = ConnectionString })
            {
                using (var cmd = new SqlCommand() { Connection = cn })
                {
                    cmd.CommandText = "SELECT ContactTypeIdentifier,ContactTitle  FROM dbo.ContactType";
                    try
                    {
                        cn.Open();
                        var reader = await cmd.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            contactList.Add(new ContactType()
                            {
                                ContactTypeIdentifier = await reader.GetFieldValueAsync<int>(0),
                                ContactTitle = await reader.GetFieldValueAsync<string>(1)

                            });

                        }
                    }
                    catch (Exception ex)
                    {
                        mHasException = true;
                        mLastException = ex;
                    }
                }
            }


            return contactList;
        }
        #region Stub methods for remove and update

        /// <summary>
        /// Here is where you would have a DELETE statement to remove
        /// the record by primary key
        /// </summary>
        /// <param name="pIdentifier"></param>
        /// <returns></returns>
        public bool FakeRemoveCustomer(int pIdentifier)
        {
            return true;
        }
        /// <summary>
        /// Here is where you would update the customer by the
        /// primary key in the DataRow followed by using field
        /// values to use in the SET part of the UPDATE statement.
        /// </summary>
        /// <param name="pRow"></param>
        /// <returns></returns>
        public bool FakeUpdateCustomer(DataRow pRow)
        {
            return true;
        }

        #endregion
    }
}
