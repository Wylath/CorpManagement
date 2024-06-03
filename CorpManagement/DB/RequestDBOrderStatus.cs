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
    class RequestDBOrderStatus : FactoryDB<OrderStatus>
    {
        /// <summary>
        /// Converty the SqlDataReader to OrderStatus
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public override OrderStatus ConvertSqlDataReaderToClass(SqlDataReader dr)
        {
            OrderStatus orderstatus = null;
            int OrderStatusid = 0;
            string name = "";

            try
            {
                OrderStatusid = Convert.ToInt32(dr["OrderStatusId"]);
                if (!string.IsNullOrEmpty(dr["Name"].ToString()))
                    name = Convert.ToString(dr["Name"]);

                orderstatus = new OrderStatus(OrderStatusid, name);
            }
            catch (Exception ex)
            {
                ShowDebugInfo();
                throw new MyErrorException(ex.Message, GetType().Name, method, line);
            }
            return orderstatus;
        }

        /// <summary>
        /// Return all OrderStatus
        /// </summary>
        /// <returns></returns>
        public override List<OrderStatus> SelectAllElement()
        {
            string query = "SELECT id As OrderStatusId, name As Name FROM order_status_type;";
            List<OrderStatus> allorderstatys = new List<OrderStatus>();

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
                            allorderstatys.Add(ConvertSqlDataReaderToClass(reader));
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

            return allorderstatys;
        }

        /// <summary>
        /// Return OrderStatus by Id : type int
        /// </summary>
        /// <param name="OrderStatusId"></param>
        /// <returns></returns>
        public override OrderStatus SelectElementById(int OrderStatusId)
        {
            string query = "SELECT id As OrderStatusId, name As Name FROM order_status_type WHERE id = @OrderStatusId;";
            OrderStatus orderstatus = null;

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@OrderStatusId", OrderStatusId);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            orderstatus = ConvertSqlDataReaderToClass(reader);
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

            return orderstatus;
        }

        /// <summary>
        /// Insert new OrderStatus in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool InsertNewElement(OrderStatus value)
        {
            return false;
        }

        /// <summary>
        /// Update OrderStatus in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool UpdateElement(OrderStatus value)
        {
            return false;
        }

        /// <summary>
        /// Delete OrderStatus in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool DeleteElement(OrderStatus value)
        {
            return false;
        }
    }
}
