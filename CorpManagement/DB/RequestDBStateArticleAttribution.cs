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
    class RequestDBStateArticleAttribution : FactoryDB<StateArticleAttribution>
    {
        /// <summary>
        /// Converty the SqlDataReader to StateArticleAttribution
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public override StateArticleAttribution ConvertSqlDataReaderToClass(SqlDataReader dr)
        {
            StateArticleAttribution statusattribution = null;
            int id = 0;
            string name = "";

            try
            {
                id = Convert.ToInt32(dr["StatusAttributionId"]);
                if (!string.IsNullOrEmpty(dr["Name"].ToString()))
                    name = Convert.ToString(dr["Name"]);

                statusattribution = new StateArticleAttribution(id, name);
            }
            catch (Exception ex)
            {
                ShowDebugInfo();
                throw new MyErrorException(ex.Message, GetType().Name, method, line);
            }
            return statusattribution;
        }

        /// <summary>
        /// Return all StateArticleAttribution
        /// </summary>
        /// <returns></returns>
        public override List<StateArticleAttribution> SelectAllElement()
        {
            string query = "SELECT id As StatusAttributionId, name As Name FROM state_article_att;";
            List<StateArticleAttribution> allStatusAttribution = new List<StateArticleAttribution>();

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
                            allStatusAttribution.Add(ConvertSqlDataReaderToClass(reader));
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

            return allStatusAttribution;
        }

        /// <summary>
        /// Return StateArticleAttribution by Id : type int
        /// </summary>
        /// <param name="StatusAttributionId"></param>
        /// <returns></returns>
        public override StateArticleAttribution SelectElementById(int StatusAttributionId)
        {
            string query = "SELECT id As StatusAttributionId, name As Name FROM state_article_att WHERE id = @StatusAttributionId;";
            StateArticleAttribution StatusAttribution = null;

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@StatusAttributionId", StatusAttributionId);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            StatusAttribution = ConvertSqlDataReaderToClass(reader);
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

            return StatusAttribution;
        }

        /// <summary>
        /// Insert new StateArticleAttribution in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool InsertNewElement(StateArticleAttribution value)
        {
            return false;
        }

        /// <summary>
        /// Update StateArticleAttribution in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool UpdateElement(StateArticleAttribution value)
        {
            return false;
        }

        /// <summary>
        /// Delete StateArticleAttribution in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool DeleteElement(StateArticleAttribution value)
        {
            return false;
        }
    }
}
