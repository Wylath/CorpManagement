using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorpManagement.Model;
using CorpManagement.ToolBox;

namespace CorpManagement.DB
{
    class RequestDBInvoiceType : FactoryDB<InvoiceType>
    {
        /// <summary>
        /// Converty the SqlDataReader to InvoiceType
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public override InvoiceType ConvertSqlDataReaderToClass(SqlDataReader dr)
        {
            InvoiceType invoicetype = null;
            int id = 0;
            string name = "";

            try
            {
                id = Convert.ToInt32(dr["Id"]);
                if (!string.IsNullOrEmpty(dr["Name"].ToString()))
                    name = Convert.ToString(dr["Name"]);

                invoicetype = new InvoiceType(id, name);
            }
            catch (Exception ex)
            {
                ShowDebugInfo();
                throw new MyErrorException(ex.Message, GetType().Name, method, line);
            }
            return invoicetype;
        }

        /// <summary>
        /// Return all InvoiceType
        /// </summary>
        /// <returns></returns>
        public override List<InvoiceType> SelectAllElement()
        {
            string query = "SELECT id As Id, name As Name FROM invoice_type;";
            List<InvoiceType> allInvoiceType = new List<InvoiceType>();

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            allInvoiceType.Add(ConvertSqlDataReaderToClass(reader));
                        }
                    }

                    reader.Close();
                }
                catch (SqlException ex)
                {
                    ShowDebugInfo();
                    throw new MyErrorException(ex.Message, GetType().Name, method, line);
                }

                CloseConnection();
            }

            return allInvoiceType;
        }

        /// <summary>
        /// Return InvoiceType by Id : type int
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public override InvoiceType SelectElementById(int Id)
        {
            string query = "SELECT id As Id, name As Name FROM invoice_type WHERE id = @Id;";
            InvoiceType invoicetype = null;

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@Id", Id);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            invoicetype = ConvertSqlDataReaderToClass(reader);
                        }
                    }

                    reader.Close();
                }
                catch (SqlException ex)
                {
                    ShowDebugInfo();
                    throw new MyErrorException(ex.Message, GetType().Name, method, line);
                }

                CloseConnection();
            }

            return invoicetype;
        }

        /// <summary>
        /// Insert new InvoiceType in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool InsertNewElement(InvoiceType value)
        {
            return false;
        }

        /// <summary>
        /// Update InvoiceType in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool UpdateElement(InvoiceType value)
        {
            return false;
        }

        /// <summary>
        /// Delete InvoiceType in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool DeleteElement(InvoiceType value)
        {
            return false;
        }
    }
}
