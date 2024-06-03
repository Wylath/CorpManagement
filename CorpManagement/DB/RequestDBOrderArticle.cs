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
    class RequestDBOrderArticle : FactoryDB<OrderArticle>
    {
        /// <summary>
        /// Converty the SqlDataReader to OrderArticle
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public override OrderArticle ConvertSqlDataReaderToClass(SqlDataReader dr)
        {
            RequestDBUser RU = new RequestDBUser();
            RequestDBArticle RA = new RequestDBArticle();
            RequestDBOrderStatus ROS = new RequestDBOrderStatus();
            RequestDBSizeClothing RSC = new RequestDBSizeClothing();
            OrderArticle orderarticle = null;
            int idorder = 0;
            User iduser = null;
            Article idarticle = null;
            int amount = 0;
            DateTime orderdate = DateTime.Now;
            DateTime datereceived = DateTime.Now;
            OrderStatus status = null;
            string description = string.Empty;
            SizeClothing sizeclothing = null;

            try
            {
                idorder = Convert.ToInt32(dr["IdOrder"]);
                if (!string.IsNullOrEmpty(dr["Amount"].ToString()))
                    amount = Convert.ToInt32(dr["Amount"]);
                if (!string.IsNullOrEmpty(dr["DateOrder"].ToString()))
                    orderdate = Convert.ToDateTime(dr["DateOrder"]);
                if (!string.IsNullOrEmpty(dr["DateReceived"].ToString()))
                    datereceived = Convert.ToDateTime(dr["DateReceived"]);
                if (!string.IsNullOrEmpty(dr["UserId"].ToString()))
                    iduser = RU.SelectElementById(Convert.ToInt32(dr["UserId"]));
                if (!string.IsNullOrEmpty(dr["ArticleId"].ToString()))
                    idarticle = RA.SelectElementById(Convert.ToInt32(dr["ArticleId"]));
                if (!string.IsNullOrEmpty(dr["Status"].ToString()))
                    status = ROS.SelectElementById(Convert.ToInt32(dr["Status"]));
                if (!string.IsNullOrEmpty(dr["Description"].ToString()))
                    description = Convert.ToString(dr["Description"]);
                if (!string.IsNullOrEmpty(dr["IdSize"].ToString()))
                    sizeclothing = RSC.SelectElementById(Convert.ToInt32(dr["IdSize"]));

                orderarticle = new OrderArticle(idorder, iduser, idarticle, amount, orderdate, datereceived, status, description, sizeclothing);
            }
            catch (Exception ex)
            {
                ShowDebugInfo();
                throw new MyErrorException(ex.Message, GetType().Name, method, line);
            }
            return orderarticle;
        }

        /// <summary>
        /// Return all OrderArticle
        /// </summary>
        /// <returns></returns>
        public override List<OrderArticle> SelectAllElement()
        {
            string query = "SELECT idorder As IdOrder, iduser As UserId, idarticle As ArticleId, amount As Amount, dateorder As DateOrder, datereceived As DateReceived, status As Status, description As Description, idsize As IdSize FROM order_article;";
            List<OrderArticle> allOrderArticle = new List<OrderArticle>();

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
                            allOrderArticle.Add(ConvertSqlDataReaderToClass(reader));
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

            return allOrderArticle;
        }

        /// <summary>
        /// Return OrderArticle by Id : type int
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public override OrderArticle SelectElementById(int OrderId)
        {
            string query = "SELECT idorder As IdOrder, iduser As UserId, idarticle As ArticleId, amount As Amount, dateorder As DateOrder, datereceived As DateReceived, status As Status, description As Description, idsize As IdSize FROM order_article WHERE idorder = @OrderId;";
            OrderArticle orderarticle = null;

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@OrderId", OrderId);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            orderarticle = ConvertSqlDataReaderToClass(reader);
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

            return orderarticle;
        }

        /// <summary>
        /// Return OrderArticle by ArticleId and UserId : type int
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public OrderArticle SelectElementById(int ArticleId, int UserId)
        {
            string query = "SELECT idorder As IdOrder, iduser As UserId, idarticle As ArticleId, amount As Amount, dateorder As DateOrder, datereceived As DateReceived, status As Status, description As Description, idsize As IdSize FROM order_article WHERE iduser= @UserId AND idarticle = @ArticleId;";
            OrderArticle orderarticle = null;

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@ArticleId", ArticleId);
                cmd.Parameters.AddWithValue("@UserId", UserId);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            orderarticle = ConvertSqlDataReaderToClass(reader);
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

            return orderarticle;
        }

        /// <summary>
        /// Insert new OrderArticle in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool InsertNewElement(OrderArticle value)
        {
            string query = "INSERT INTO order_article (iduser, idarticle, amount, dateorder, datereceived, status, description, idsize, createdby, createddate) VALUES"
                + "(@UserId, @ArticleId, @Amount, @DateOrder, @DateReceived, @Status, @Description, @IdSize, @CreatedBy, @CreatedBy);";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@UserId", value.iduser.id);
                cmd.Parameters.AddWithValue("@ArticleId", value.idarticle.id);
                cmd.Parameters.AddWithValue("@Amount", value.amount);
                cmd.Parameters.AddWithValue("@DateOrder", value.orderdate);
                cmd.Parameters.AddWithValue("@DateReceived", value.datereceived);
                cmd.Parameters.AddWithValue("@Status", value.status.id);
                cmd.Parameters.AddWithValue("@Description", value.description);
                cmd.Parameters.AddWithValue("@IdSize", value.sizeclothing != null ? value.sizeclothing.id : SqlInt32.Null);
                cmd.Parameters.AddWithValue("@CreatedBy", CurrentUser.userId.id);
                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

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
        /// Update OrderArticle in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool UpdateElement(OrderArticle value)
        {
            string query = "UPDATE order_article SET iduser = @UserId, idarticle = @ArticleId, amount = @Amount, dateorder = @dateorder, datereceived = @DateReceived, status = @Status, description = @Description, idsize = @IdSize, modifieddate = @ModifiedDate, modifiedby = @ModifiedBy WHERE idorder = @IdOrder;";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@IdOrder", value.idorder);
                cmd.Parameters.AddWithValue("@UserId", value.iduser.id);
                cmd.Parameters.AddWithValue("@ArticleId", value.idarticle.id);
                cmd.Parameters.AddWithValue("@Amount", value.amount);
                cmd.Parameters.AddWithValue("@dateorder", value.orderdate);
                cmd.Parameters.AddWithValue("@DateReceived", value.datereceived);
                cmd.Parameters.AddWithValue("@Status", value.status.id);
                cmd.Parameters.AddWithValue("@Description", value.description);
                cmd.Parameters.AddWithValue("@IdSize", value.sizeclothing != null ? value.sizeclothing.id : SqlInt32.Null);
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
        /// Delete OrderArticle in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool DeleteElement(OrderArticle value)
        {
            string query = "DELETE FROM order_article WHERE idorder = @IdOrder;";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@IdOrder", value.idorder);

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
                                throw new MyErrorException("Erreur de suppression, une liaison est toujours présente, vérifier que la commande n'est plus utilisée pour les factures avant sa suppression.", GetType().Name, method, line, true);
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
