using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorpManagement.Model;
using CorpManagement.ToolBox;
using CorpManagement.ViewModel;

namespace CorpManagement.DB
{
    class RequestDBArticleAttribution : FactoryDB<ArticleAttribution>
    {
        /// <summary>
        /// Converty the SqlDataReader to ArticleAttribution
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public override ArticleAttribution ConvertSqlDataReaderToClass(SqlDataReader dr)
        {
            RequestDBStateArticleAttribution RSAA = new RequestDBStateArticleAttribution();
            RequestDBUser RU = new RequestDBUser();
            RequestDBArticle RA = new RequestDBArticle();
            ArticleAttribution articleAttribution = null;
            int attributionId = 0;
            User iduser = null;
            Article idarticle = null;
            string serialnumber = "";
            string specialnumber = "";
            string description = "";
            StateArticleAttribution state = null;

            try
            {
                attributionId = Convert.ToInt32(dr["AttributionId"]);
                if (!string.IsNullOrEmpty(dr["IdUser"].ToString()))
                    iduser = RU.SelectElementById(Convert.ToInt32(dr["IdUser"]));
                if (!string.IsNullOrEmpty(dr["Serial"].ToString()))
                    serialnumber = Convert.ToString(dr["Serial"]);
                if (!string.IsNullOrEmpty(dr["Special"].ToString()))
                    specialnumber = Convert.ToString(dr["Special"]);
                if (!string.IsNullOrEmpty(dr["Description"].ToString()))
                    description = Convert.ToString(dr["Description"]);
                if (!string.IsNullOrEmpty(dr["IdArticle"].ToString()))
                    idarticle = RA.SelectElementById(Convert.ToInt32(dr["IdArticle"]));
                if (!string.IsNullOrEmpty(dr["State"].ToString()))
                    state = RSAA.SelectElementById(Convert.ToInt32(dr["State"]));

                articleAttribution = new ArticleAttribution(attributionId, iduser, idarticle, serialnumber, specialnumber, description, state);
            }
            catch (Exception ex)
            {
                ShowDebugInfo();
                throw new MyErrorException(ex.Message, GetType().Name, method, line);
            }
            return articleAttribution;
        }

        /// <summary>
        /// Return all ArticleAttribution
        /// </summary>
        /// <returns></returns>
        public override List<ArticleAttribution> SelectAllElement()
        {
            string query = "SELECT id As AttributionId, iduser As IdUser, idarticle As IdArticle, serialnumber As Serial, specialnumber As Special, description As Description, state As State FROM article_attribution;";
            List<ArticleAttribution> allArticleAttribution = new List<ArticleAttribution>();

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
                            allArticleAttribution.Add(ConvertSqlDataReaderToClass(reader));
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

            return allArticleAttribution;
        }

        /// <summary>
        /// Return ArticleAttribution by Id : type int
        /// </summary>
        /// <param name="AttributionId"></param>
        /// <returns></returns>
        public override ArticleAttribution SelectElementById(int AttributionId)
        {
            string query = "SELECT id As AttributionId, iduser As IdUser, idarticle As IdArticle, serialnumber As Serial, specialnumber As Special, description As Description, state As State FROM article_attribution WHERE id = @AttributionId;";
            ArticleAttribution attribution = null;

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@AttributionId", AttributionId);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            attribution = ConvertSqlDataReaderToClass(reader);
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

            return attribution;
        }

        /// <summary>
        /// Insert new ArticleAttribution in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool InsertNewElement(ArticleAttribution value)
        {
            string query = "INSERT INTO article_attribution (iduser, idarticle, serialnumber, specialnumber, description, state, createddate, createdby) VALUES"
                + "(@IdUser, @IdArticle, @Serial, @Special, @Description, @State, @CreatedDate, @CreatedBy);";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@IdUser", value.iduser != null ? value.iduser.id : SqlInt32.Null);
                cmd.Parameters.AddWithValue("@IdArticle", value.idarticle.id);
                cmd.Parameters.AddWithValue("@Serial", value.serialnumber);
                cmd.Parameters.AddWithValue("@Special", value.specialnumber);
                cmd.Parameters.AddWithValue("@Description", value.description);
                cmd.Parameters.AddWithValue("@State", value.state.id);
                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@CreatedBy", CurrentUser.userId.id);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    ShowDebugInfo();
                    throw new MyErrorException(ex.Message, GetType().Name, method, line);
                }

                CloseConnection();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Update ArticleAttribution in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool UpdateElement(ArticleAttribution value)
        {
            string query = "UPDATE article_attribution SET iduser = @IdUser, idarticle = @IdArticle, serialnumber = @Serial, specialnumber = @Special, description = @Description, state = @State, modifieddate = @ModifiedDate, modifiedby = @ModifiedBy WHERE id = @AttributionId;";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@AttributionId", value.id);
                cmd.Parameters.AddWithValue("@IdUser", value.iduser != null ? value.iduser.id : SqlInt32.Null);
                cmd.Parameters.AddWithValue("@IdArticle", value.idarticle.id);
                cmd.Parameters.AddWithValue("@Serial", value.serialnumber);
                cmd.Parameters.AddWithValue("@Special", value.specialnumber);
                cmd.Parameters.AddWithValue("@Description", value.description);
                cmd.Parameters.AddWithValue("@State", value.state.id);
                cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@ModifiedBy", CurrentUser.userId.id);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    ShowDebugInfo();
                    throw new MyErrorException(ex.Message, GetType().Name, method, line);
                }

                CloseConnection();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Delete ArticleAttribution in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool DeleteElement(ArticleAttribution value)
        {
            string query = "DELETE FROM article_attribution WHERE id = @AttributionId;";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@AttributionId", value.id);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    ShowDebugInfo();
                    throw new MyErrorException(ex.Message, GetType().Name, method, line);
                }

                CloseConnection();
                return true;
            }

            return false;
        }
    }
}
