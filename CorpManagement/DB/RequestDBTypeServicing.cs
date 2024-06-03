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
    class RequestDBTypeServicing : FactoryDB<TypeServicing>
    {
        /// <summary>
        /// Converty the SqlDataReader to TypeServicing
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public override TypeServicing ConvertSqlDataReaderToClass(SqlDataReader dr)
        {
            TypeServicing typeservicing = null;
            int typeServicingid = 0;
            string name = "";

            try
            {
                typeServicingid = Convert.ToInt32(dr["TypeServicingId"]);
                if (!string.IsNullOrEmpty(dr["Name"].ToString()))
                    name = Convert.ToString(dr["Name"]);

                typeservicing = new TypeServicing(typeServicingid, name);
            }
            catch (Exception ex)
            {
                ShowDebugInfo();
                throw new MyErrorException(ex.Message, GetType().Name, method, line);
            }
            return typeservicing;
        }

        /// <summary>
        /// Return all TypeServicing
        /// </summary>
        /// <returns></returns>
        public override List<TypeServicing> SelectAllElement()
        {
            string query = "SELECT id As TypeServicingId, name As Name FROM type_servicing;";
            List<TypeServicing> allTypeServicing = new List<TypeServicing>();

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
                            allTypeServicing.Add(ConvertSqlDataReaderToClass(reader));
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

            return allTypeServicing;
        }

        /// <summary>
        /// Return TypeServicing by Id : type int
        /// </summary>
        /// <param name="TypeServicingId"></param>
        /// <returns></returns>
        public override TypeServicing SelectElementById(int TypeServicingId)
        {
            string query = "SELECT id As TypeServicingId, name As Name FROM type_servicing WHERE id = @TypeServicingId;";
            TypeServicing typeservicing = null;

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@TypeServicingId", TypeServicingId);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            typeservicing = ConvertSqlDataReaderToClass(reader);
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

            return typeservicing;
        }

        /// <summary>
        /// Insert new TypeServicing in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool InsertNewElement(TypeServicing value)
        {
            return false;
        }

        /// <summary>
        /// Update TypeServicing in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool UpdateElement(TypeServicing value)
        {
            return false;
        }

        /// <summary>
        /// Delete TypeServicing in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool DeleteElement(TypeServicing value)
        {
            return false;
        }
    }
}
