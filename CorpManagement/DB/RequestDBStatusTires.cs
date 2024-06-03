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
    class RequestDBStatusTires : FactoryDB<StatusTire>
    {
        /// <summary>
        /// Converty the SqlDataReader to StatusTire
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public override StatusTire ConvertSqlDataReaderToClass(SqlDataReader dr)
        {
            StatusTire statustire = null;
            int statustireid = 0;
            string name = "";

            try
            {
                statustireid = Convert.ToInt32(dr["StatusTireId"]);
                if (!string.IsNullOrEmpty(dr["Name"].ToString()))
                    name = Convert.ToString(dr["Name"]);

                statustire = new StatusTire(statustireid, name);
            }
            catch (Exception ex)
            {
                ShowDebugInfo();
                throw new MyErrorException(ex.Message, GetType().Name, method, line);
            }
            return statustire;
        }

        /// <summary>
        /// Return all StatusTire
        /// </summary>
        /// <returns></returns>
        public override List<StatusTire> SelectAllElement()
        {
            string query = "SELECT id As StatusTireId, name As Name FROM state;";
            List<StatusTire> allStatusTire = new List<StatusTire>();

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
                            allStatusTire.Add(ConvertSqlDataReaderToClass(reader));
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

            return allStatusTire;
        }

        /// <summary>
        /// Return StatusTire by Id : type int
        /// </summary>
        /// <param name="StatusTireId"></param>
        /// <returns></returns>
        public override StatusTire SelectElementById(int StatusTireId)
        {
            string query = "SELECT id As StatusTireId, name As Name FROM state WHERE id = @StatusTireId;";
            StatusTire statustire = null;

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@StatusTireId", StatusTireId);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            statustire = ConvertSqlDataReaderToClass(reader);
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

            return statustire;
        }

        /// <summary>
        /// Insert new StatusTire in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool InsertNewElement(StatusTire value)
        {
            return false;
        }

        /// <summary>
        /// Update StatusTire in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool UpdateElement(StatusTire value)
        {
            return false;
        }

        /// <summary>
        /// Delete StatusTire in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool DeleteElement(StatusTire value)
        {
            return false;
        }
    }
}
