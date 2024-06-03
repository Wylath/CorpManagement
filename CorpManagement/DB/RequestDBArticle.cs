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
    class RequestDBArticle : FactoryDB<Article>
    {
        /// <summary>
        /// Converty the SqlDataReader to Article
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public override Article ConvertSqlDataReaderToClass(SqlDataReader dr)
        {
            RequestDBProvider RP = new RequestDBProvider();
            RequestDBArticleType RAT = new RequestDBArticleType();
            Article article = null;
            int articleid = 0;
            string name = "";
            string refArticle = "";
            Provider idprovider = null;
            float price = 0.0f;
            int amount = 0;
            int maxquantity = 0;
            ArticleType idtype = null;
            string description = "";
            int credit = 0;

            try
            {
                articleid = Convert.ToInt32(dr["ArticleId"]);
                if (!string.IsNullOrEmpty(dr["Name"].ToString()))
                    name = Convert.ToString(dr["Name"]);
                if (!string.IsNullOrEmpty(dr["RefArticle"].ToString()))
                    refArticle = Convert.ToString(dr["RefArticle"]);
                if (!string.IsNullOrEmpty(dr["Price"].ToString()))
                    float.TryParse(Convert.ToString(dr["Price"]), out price);
                if (!string.IsNullOrEmpty(dr["Amount"].ToString()))
                    amount = Convert.ToInt32(dr["Amount"]);
                if (!string.IsNullOrEmpty(dr["MaxQuantity"].ToString()))
                    maxquantity = Convert.ToInt32(dr["MaxQuantity"]);
                if (!string.IsNullOrEmpty(dr["Description"].ToString()))
                    description = Convert.ToString(dr["Description"]);
                if (!string.IsNullOrEmpty(dr["Credit"].ToString()))
                    credit = Convert.ToInt32(dr["Credit"]);
                if (!string.IsNullOrEmpty(dr["IdProvider"].ToString()))
                    idprovider = RP.SelectElementById(Convert.ToInt32(dr["IdProvider"]));
                if (!string.IsNullOrEmpty(dr["IdType"].ToString()))
                    idtype = RAT.SelectElementById(Convert.ToInt32(dr["IdType"]));

                article = new Article(articleid, name, refArticle, idprovider, price, amount, maxquantity, idtype, description, credit);
            }
            catch (Exception ex)
            {
                ShowDebugInfo();
                throw new MyErrorException(ex.Message, GetType().Name, method, line);
            }
            return article;
        }

        /// <summary>
        /// Return all Article
        /// </summary>
        /// <returns></returns>
        public override List<Article> SelectAllElement()
        {
            string query = "SELECT id As ArticleId, name As Name, ref As RefArticle, idprovider As IdProvider, price As Price, amount As Amount, maxquantity As MaxQuantity, idtype As IdType, description As Description, credit As Credit FROM Article;";
            List<Article> allArticle = new List<Article>();

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
                            allArticle.Add(ConvertSqlDataReaderToClass(reader));
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

            return allArticle;
        }

        /// <summary>
        /// Return all Article by type
        /// </summary>
        /// <returns></returns>
        public List<Article> SelectAllElementByType(int IdType)
        {
            string query = "SELECT id As ArticleId, name As Name, ref As RefArticle, idprovider As IdProvider, price As Price, amount As Amount, maxquantity As MaxQuantity, idtype As IdType, description As Description, credit As Credit FROM Article WHERE idtype = @IdType;";
            List<Article> allArticle = new List<Article>();

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@IdType", IdType);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            allArticle.Add(ConvertSqlDataReaderToClass(reader));
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

            return allArticle;
        }

        /// <summary>
        /// Return Article by Id : type int
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <returns></returns>
        public override Article SelectElementById(int ArticleId)
        {
            string query = "SELECT id As ArticleId, name As Name, ref As RefArticle, idprovider As IdProvider, price As Price, amount As Amount, maxquantity As MaxQuantity, idtype As IdType, description As Description, credit As Credit FROM Article WHERE id = @ArticleId;";
            Article article = null;

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@ArticleId", ArticleId);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            article = ConvertSqlDataReaderToClass(reader);
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

            return article;
        }

        /// <summary>
        /// Insert new article in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool InsertNewElement(Article value)
        {
            string query = "INSERT INTO Article (name, ref, idprovider, price, amount, maxquantity, idtype, description, credit, createddate, createdby) VALUES"
                + "(@Name, @RefArticle, @IdProvider, @Price, @Amount, @MaxQuantity, @IdType, @Description, @Credit, @CreatedDate, @CreatedBy);";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@Name", value.name);
                cmd.Parameters.AddWithValue("@RefArticle", value.refArticle);
                cmd.Parameters.AddWithValue("@IdProvider", value.idprovider.id);
                cmd.Parameters.AddWithValue("@Price", value.price);
                cmd.Parameters.AddWithValue("@Amount", value.amount);
                cmd.Parameters.AddWithValue("@MaxQuantity", value.maxquantity);
                cmd.Parameters.AddWithValue("@IdType", value.idtype.id);
                cmd.Parameters.AddWithValue("@Description", value.description);
                cmd.Parameters.AddWithValue("@Credit", value.credit);
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
        /// Update article in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool UpdateElement(Article value)
        {
            string query = "UPDATE Article SET name = @Name, ref = @RefArticle, idprovider = @IdProvider, price = @Price, amount = @Amount, maxquantity = @MaxQuantity, idtype = @IdType, description = @Description, credit = @Credit, modifieddate = @ModifiedDate, modifiedby = @ModifiedBy WHERE id = @ArticleId;";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@ArticleId", value.id);
                cmd.Parameters.AddWithValue("@Name", value.name);
                cmd.Parameters.AddWithValue("@RefArticle", value.refArticle);
                cmd.Parameters.AddWithValue("@IdProvider", value.idprovider.id);
                cmd.Parameters.AddWithValue("@Price", value.price);
                cmd.Parameters.AddWithValue("@Amount", value.amount);
                cmd.Parameters.AddWithValue("@MaxQuantity", value.maxquantity);
                cmd.Parameters.AddWithValue("@IdType", value.idtype.id);
                cmd.Parameters.AddWithValue("@Description", value.description);
                cmd.Parameters.AddWithValue("@Credit", value.credit);
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
        /// Delete Article in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool DeleteElement(Article value)
        {
            string query = "DELETE FROM Article WHERE id = @ArticleId;";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@ArticleId", value.id);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    if (ex.Errors.Count > 0)
                    {
                        switch (ex.Errors[0].Number)
                        {
                            case 547: // Foreign Key violation
                                ShowDebugInfo();
                                throw new MyErrorException("Erreur de suppression, une liaison est toujours présente, vérifier que l'article n'est plus utilisé pour les commandes et les attributions avant sa suppression.", GetType().Name, method, line, true);
                            case 2601: // Primary key violation
                            default:
                                ShowDebugInfo();
                                throw new MyErrorException(ex.Message, GetType().Name, method, line);
                        }
                    }
                }

                CloseConnection();
                return true;
            }

            return false;
        }
    }
}
