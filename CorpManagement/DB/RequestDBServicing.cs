using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorpManagement.Model;
using CorpManagement.ToolBox;
using CorpManagement.ViewModel;

namespace CorpManagement.DB
{
    class RequestDBServicing : FactoryDB<Servicing>
    {
        /// <summary>
        /// Converty the SqlDataReader to Servicing
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public override Servicing ConvertSqlDataReaderToClass(SqlDataReader dr)
        {
            RequestDBVehicle RV = new RequestDBVehicle();
            RequestDBProvider RP = new RequestDBProvider();
            RequestDBTypeServicing RTS = new RequestDBTypeServicing();
            Servicing servicing = null;
            int servicingid = 0;
            Vehicle veh = null;
            DateTime dateservicing = DateTime.Now;
            float price = 0.0f;
            Provider idprovider = null;
            string description = "";
            TypeServicing idtypeservicing = null;
            int km = 0;

            try
            {
                servicingid = Convert.ToInt32(dr["ServicingId"]);
                if (!string.IsNullOrEmpty(dr["DateServicing"].ToString()))
                    dateservicing = Convert.ToDateTime(dr["DateServicing"]);
                if (!string.IsNullOrEmpty(dr["PriceServicing"].ToString()))
                    float.TryParse(Convert.ToString(dr["PriceServicing"]), out price);
                if (!string.IsNullOrEmpty(dr["DescriptionServicing"].ToString()))
                    description = Convert.ToString(dr["DescriptionServicing"]);
                if (!string.IsNullOrEmpty(dr["VehicleId"].ToString()))
                    veh = RV.SelectElementById(Convert.ToInt32(dr["VehicleId"]));
                if (!string.IsNullOrEmpty(dr["ProviderId"].ToString()))
                    idprovider = RP.SelectElementById(Convert.ToInt32(dr["ProviderId"]));
                if (!string.IsNullOrEmpty(dr["IdTypeServicing"].ToString()))
                    idtypeservicing = RTS.SelectElementById(Convert.ToInt32(dr["IdTypeServicing"]));
                if (!string.IsNullOrEmpty(dr["Km"].ToString()))
                    km = Convert.ToInt32(dr["Km"]);

                servicing = new Servicing(servicingid, veh, dateservicing, price, idprovider, description, idtypeservicing, km);

            }
            catch (Exception ex)
            {
                ShowDebugInfo();
                throw new MyErrorException(ex.Message, GetType().Name, method, line);
            }
            return servicing;
        }

        /// <summary>
        /// Retrun all intervention
        /// </summary>
        /// <returns></returns>
        public override List<Servicing> SelectAllElement()
        {
            string query = "SELECT id As ServicingId, idvehicle As VehicleId, dateservicing As DateServicing, price As PriceServicing, idprovider As ProviderId, description As DescriptionServicing, idtypeservicing As IdTypeServicing, km As Km FROM Servicing;";
            List<Servicing> allServicing = new List<Servicing>();

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
                            allServicing.Add(ConvertSqlDataReaderToClass(reader));
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

            return allServicing;
        }

        /// <summary>
        /// Retrun all servicing by vehicle
        /// </summary>
        /// <param name="IdVehicle"></param>
        /// <returns></returns>
        public List<Servicing> SelectAllServicingByVehicle(int IdVehicle)
        {
            string query = "SELECT id As ServicingId, idvehicle As VehicleId, dateservicing As DateServicing, price As PriceServicing, idprovider As ProviderId, description As DescriptionServicing, idtypeservicing As IdTypeServicing, km As Km FROM Servicing WHERE idvehicle = @IdVehicle;";
            List<Servicing> allServicing = new List<Servicing>();

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@IdVehicle", IdVehicle);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            allServicing.Add(ConvertSqlDataReaderToClass(reader));
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

            return allServicing;
        }

        /// <summary>
        /// Retrun the max value km for the vehicle id
        /// </summary>
        /// <param name="IdVehicle"></param>
        /// <returns></returns>
        public int SelectMaxKmServicingByVehicle(Vehicle IdVehicle)
        {
            string query = "SELECT idvehicle As VehicleId, max(km) As Km FROM Servicing WHERE idvehicle = @IdVehicle group by idvehicle;";
            int maxvaluekm = 0;

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@IdVehicle", IdVehicle.id);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            maxvaluekm = Convert.ToInt32(reader["Km"]);
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

            return maxvaluekm;
        }

        /// <summary>
        /// Return Servicing by Id
        /// </summary>
        /// <param name="ServicingId"></param>
        /// <returns></returns>
        public override Servicing SelectElementById(int ServicingId)
        {
            string query = "SELECT id As ServicingId, idvehicle As VehicleId, dateservicing As DateServicing, price As PriceServicing, idprovider As ProviderId, description As DescriptionServicing, idtypeservicing As IdTypeServicing, km As Km FROM Servicing WHERE id = @ServicingId;";
            Servicing servicing = null;

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@ServicingId", ServicingId);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            servicing = ConvertSqlDataReaderToClass(reader);
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

            return servicing;
        }

        /// <summary>
        /// Insert new Servicing in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool InsertNewElement(Servicing value)
        {
            string query = "INSERT INTO Servicing (idvehicle, dateservicing, price, idprovider, description, idtypeservicing, km, createddate, createdby) VALUES"
                + "(@VehicleId, @DateServicing, @Price, @ProviderId, @Description, @IdTypeServicing, @Km, @CreatedDate, @CreatedBy);";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@VehicleId", value.idvehicle.id);
                cmd.Parameters.AddWithValue("@DateServicing", value.dateservicing);
                cmd.Parameters.AddWithValue("@Price", value.price);
                cmd.Parameters.AddWithValue("@ProviderId", value.idprovider.id);
                cmd.Parameters.AddWithValue("@Description", value.description);
                cmd.Parameters.AddWithValue("@IdTypeServicing", value.idtypeservicing.id);
                cmd.Parameters.AddWithValue("@Km", value.km);
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
        /// Update Servicing in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool UpdateElement(Servicing value)
        {
            string query = "UPDATE Servicing SET idvehicle = @VehicleId, dateservicing = @DateServicing, price = @Price, idprovider = @ProviderId, description = @Description, idtypeservicing = @IdTypeServicing, km = @Km, modifieddate = @ModifiedDate, modifiedby = @ModifiedBy WHERE id = @ServicingId;";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@ServicingId", value.id);
                cmd.Parameters.AddWithValue("@VehicleId", value.idvehicle.id);
                cmd.Parameters.AddWithValue("@DateServicing", value.dateservicing);
                cmd.Parameters.AddWithValue("@Price", value.price);
                cmd.Parameters.AddWithValue("@ProviderId", value.idprovider.id);
                cmd.Parameters.AddWithValue("@Description", value.description);
                cmd.Parameters.AddWithValue("@IdTypeServicing", value.idtypeservicing.id);
                cmd.Parameters.AddWithValue("@Km", value.km);
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
        /// Delete Servicing in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool DeleteElement(Servicing value)
        {
            string query = "DELETE FROM Servicing WHERE id = @ServicingId;";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@ServicingId", value.id);

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
