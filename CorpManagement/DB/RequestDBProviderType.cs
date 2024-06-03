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
    class RequestDBProviderType : FactoryDB<ProviderType>
    {
        /// <summary>
        /// Converty the SqlDataReader to ProviderType
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public override ProviderType ConvertSqlDataReaderToClass(SqlDataReader dr)
        {
            ProviderType providertype = null;
            int id = 0;
            string name = "";

            try
            {
                id = Convert.ToInt32(dr["Id"]);
                if (!string.IsNullOrEmpty(dr["Name"].ToString()))
                    name = Convert.ToString(dr["Name"]);

                providertype = new ProviderType(id, name);
            }
            catch (Exception ex)
            {
                ShowDebugInfo();
                throw new MyErrorException(ex.Message, GetType().Name, method, line);
            }
            return providertype;
        }

        /// <summary>
        /// Return all ProviderType
        /// </summary>
        /// <returns></returns>
        public override List<ProviderType> SelectAllElement()
        {
            string query = "SELECT id As Id, name As Name FROM provider_type;";
            List<ProviderType> allProviderType = new List<ProviderType>();

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
                            allProviderType.Add(ConvertSqlDataReaderToClass(reader));
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

            return allProviderType;
        }

        /// <summary>
        /// Return ProviderType by Id : type int
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public override ProviderType SelectElementById(int Id)
        {
            string query = "SELECT id As Id, name As Name FROM provider_type WHERE id = @Id;";
            ProviderType providertype = null;

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
                            providertype = ConvertSqlDataReaderToClass(reader);
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

            return providertype;
        }

        /// <summary>
        /// Insert new ProviderType in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool InsertNewElement(ProviderType value)
        {
            return false;
        }

        /// <summary>
        /// Update ProviderType in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool UpdateElement(ProviderType value)
        {
            return false;
        }

        /// <summary>
        /// Delete ProviderType in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool DeleteElement(ProviderType value)
        {
            return false;
        }
    }
}
