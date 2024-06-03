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
    class RequestDBArticleType : FactoryDB<ArticleType>
    {
        /// <summary>
        /// Converty the SqlDataReader to ArticleType
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public override ArticleType ConvertSqlDataReaderToClass(SqlDataReader dr)
        {
            ArticleType articletype = null;
            int articletypeid = 0;
            string name = "";

            try
            {
                articletypeid = Convert.ToInt32(dr["ArticleTypeId"]);
                if (!string.IsNullOrEmpty(dr["Name"].ToString()))
                    name = Convert.ToString(dr["Name"]);

                articletype = new ArticleType(articletypeid, name);
            }
            catch (Exception ex)
            {
                ShowDebugInfo();
                throw new MyErrorException(ex.Message, GetType().Name, method, line);
            }
            return articletype;
        }

        /// <summary>
        /// Return all ArticleType
        /// </summary>
        /// <returns></returns>
        public override List<ArticleType> SelectAllElement()
        {
            string query = "SELECT id As ArticleTypeId, name As Name FROM type_article;";
            List<ArticleType> allStatusTire = new List<ArticleType>();

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
        /// Return ArticleType by Id : type int
        /// </summary>
        /// <param name="ArticleTypeId"></param>
        /// <returns></returns>
        public override ArticleType SelectElementById(int ArticleTypeId)
        {
            string query = "SELECT id As ArticleTypeId, name As Name FROM type_article WHERE id = @ArticleTypeId;";
            ArticleType articletype = null;

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@ArticleTypeId", ArticleTypeId);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            articletype = ConvertSqlDataReaderToClass(reader);
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

            return articletype;
        }

        /// <summary>
        /// Insert new ArticleType in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool InsertNewElement(ArticleType value)
        {
            return false;
        }

        /// <summary>
        /// Update ArticleType in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool UpdateElement(ArticleType value)
        {
            return false;
        }

        /// <summary>
        /// Delete ArticleType in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool DeleteElement(ArticleType value)
        {
            return false;
        }
    }
}
