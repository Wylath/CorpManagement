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
    class RequestDBStatusVehicle : FactoryDB<StatusVehicle>
    {
        /// <summary>
        /// Converty the SqlDataReader to StatusVehicle
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public override StatusVehicle ConvertSqlDataReaderToClass(SqlDataReader dr)
        {
            StatusVehicle statusvehicle = null;
            int statusvehicleid = 0;
            string name = "";

            try
            {
                statusvehicleid = Convert.ToInt32(dr["StatusVehicleId"]);
                if (!string.IsNullOrEmpty(dr["Name"].ToString()))
                    name = Convert.ToString(dr["Name"]);

                statusvehicle = new StatusVehicle(statusvehicleid, name);
            }
            catch (Exception ex)
            {
                ShowDebugInfo();
                throw new MyErrorException(ex.Message, GetType().Name, method, line);
            }
            return statusvehicle;
        }

        /// <summary>
        /// Return all StatusVehicle
        /// </summary>
        /// <returns></returns>
        public override List<StatusVehicle> SelectAllElement()
        {
            string query = "SELECT id As StatusVehicleId, name As Name FROM status_vehicle;";
            List<StatusVehicle> allStatusVehicle = new List<StatusVehicle>();

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
                            allStatusVehicle.Add(ConvertSqlDataReaderToClass(reader));
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

            return allStatusVehicle;
        }

        /// <summary>
        /// Return StatusVehicle by Id : type int
        /// </summary>
        /// <param name="StatusVehicleId"></param>
        /// <returns></returns>
        public override StatusVehicle SelectElementById(int StatusVehicleId)
        {
            string query = "SELECT id As StatusVehicleId, name As Name FROM status_vehicle WHERE id = @StatusVehicleId;";
            StatusVehicle statusvehicle = null;

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@StatusVehicleId", StatusVehicleId);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            statusvehicle = ConvertSqlDataReaderToClass(reader);
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

            return statusvehicle;
        }

        /// <summary>
        /// Insert new StatusVehicle in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool InsertNewElement(StatusVehicle value)
        {
            return false;
        }

        /// <summary>
        /// Update StatusVehicle in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool UpdateElement(StatusVehicle value)
        {
            return false;
        }

        /// <summary>
        /// Delete StatusVehicle in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool DeleteElement(StatusVehicle value)
        {
            return false;
        }
    }
}
