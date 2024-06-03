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
    class RequestDBSizeClothing : FactoryDB<SizeClothing>
    {
        /// <summary>
        /// Converty the SqlDataReader to SizeClothing
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public override SizeClothing ConvertSqlDataReaderToClass(SqlDataReader dr)
        {
            RequestDBArticle RA = new RequestDBArticle();
            SizeClothing articletype = null;
            int id = 0;
            string size = "";

            try
            {
                id = Convert.ToInt32(dr["IdSize"]);
                if (!string.IsNullOrEmpty(dr["Size"].ToString()))
                    size = Convert.ToString(dr["Size"]);

                articletype = new SizeClothing(id, size);
            }
            catch (Exception ex)
            {
                ShowDebugInfo();
                throw new MyErrorException(ex.Message, GetType().Name, method, line);
            }
            return articletype;
        }

        /// <summary>
        /// Return all SizeClothing
        /// </summary>
        /// <returns></returns>
        public override List<SizeClothing> SelectAllElement()
        {
            string query = "SELECT id As IdSize, size As Size FROM size_clothing;";
            List<SizeClothing> allSizeClothing = new List<SizeClothing>();

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
                            allSizeClothing.Add(ConvertSqlDataReaderToClass(reader));
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

            return allSizeClothing;
        }

        /// <summary>
        /// Return SizeClothing by Id : type int
        /// </summary>
        /// <param name="IdArticle"></param>
        /// <returns></returns>
        public override SizeClothing SelectElementById(int IdSize)
        {
            string query = "SELECT id As IdSize, size As Size FROM size_clothing WHERE id = @IdSize;";
            SizeClothing sizeclothing = null;

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@IdSize", IdSize);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            sizeclothing = ConvertSqlDataReaderToClass(reader);
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

            return sizeclothing;
        }

        /// <summary>
        /// Insert new SizeClothing in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool InsertNewElement(SizeClothing value)
        {
            return false;
        }

        /// <summary>
        /// Update SizeClothing in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool UpdateElement(SizeClothing value)
        {
            return false;
        }

        /// <summary>
        /// Delete SizeClothing in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool DeleteElement(SizeClothing value)
        {
            return false;
        }
    }
}
