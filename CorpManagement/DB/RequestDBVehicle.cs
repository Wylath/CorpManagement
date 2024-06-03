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
    class RequestDBVehicle : FactoryDB<Vehicle>
    {
        /// <summary>
        /// Converty the SqlDataReader to Vehicle
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public override Vehicle ConvertSqlDataReaderToClass(SqlDataReader dr)
        {
            RequestDBPoliceLocality RPL = new RequestDBPoliceLocality();
            RequestDBFuel RF = new RequestDBFuel();
            RequestDBStatusVehicle RSV = new RequestDBStatusVehicle();
            RequestDBTires RT = new RequestDBTires();
            RequestDBInsuranceVehicle RIV = new RequestDBInsuranceVehicle();
            Vehicle veh = null;
            int vehicleid = 0;
            string name = "";
            string numberplate = "";
            PoliceLocality idpolicelocality = null;
            DateTime saledate = DateTime.Now;
            DateTime lastcontrol = DateTime.Now;
            int kmlastcontrol = 0;
            DateTime nextcontrol = DateTime.Now;
            Tires idtires = null;
            Fuel fueltype = null;
            string vehicletype = "";
            StatusVehicle status = null;
            string description = "";
            InsuranceVehicle idinsurance = null;

            try
            {
                vehicleid = Convert.ToInt32(dr["VehicleId"]);
                if (!string.IsNullOrEmpty(dr["NameVehicle"].ToString()))
                    name = Convert.ToString(dr["NameVehicle"]);
                if (!string.IsNullOrEmpty(dr["NumberPlate"].ToString()))
                    numberplate = Convert.ToString(dr["NumberPlate"]);
                if (!string.IsNullOrEmpty(dr["SaleDate"].ToString()))
                    saledate = Convert.ToDateTime(dr["SaleDate"]);
                if (!string.IsNullOrEmpty(dr["LastControl"].ToString()))
                    lastcontrol = Convert.ToDateTime(dr["LastControl"]);
                if (!string.IsNullOrEmpty(dr["KmLastControl"].ToString()))
                    kmlastcontrol = Convert.ToInt32(dr["KmLastControl"]);
                if (!string.IsNullOrEmpty(dr["NextControl"].ToString()))
                    nextcontrol = Convert.ToDateTime(dr["NextControl"]);
                if (!string.IsNullOrEmpty(dr["VehicleType"].ToString()))
                    vehicletype = Convert.ToString(dr["VehicleType"]);
                if (!string.IsNullOrEmpty(dr["DescriptionVehicle"].ToString()))
                    description = Convert.ToString(dr["DescriptionVehicle"]);
                if (!string.IsNullOrEmpty(dr["IdTires"].ToString()))
                    idtires = RT.SelectElementById(Convert.ToInt32(dr["IdTires"]));
                if (!string.IsNullOrEmpty(dr["IdPoliceLocality"].ToString()))
                    idpolicelocality = RPL.SelectElementById(Convert.ToInt32(dr["IdPoliceLocality"]));
                if (!string.IsNullOrEmpty(dr["FuelType"].ToString()))
                    fueltype = RF.SelectElementById(Convert.ToInt32(dr["FuelType"]));
                if (!string.IsNullOrEmpty(dr["Status"].ToString()))
                    status = RSV.SelectElementById(Convert.ToInt32(dr["Status"]));
                if (!string.IsNullOrEmpty(dr["IdInsurance"].ToString()))
                    idinsurance = RIV.SelectElementById(Convert.ToInt32(dr["IdInsurance"]));

                veh = new Vehicle(vehicleid, name, numberplate, idpolicelocality, saledate, lastcontrol, kmlastcontrol, nextcontrol, idtires, fueltype, vehicletype, status, description, idinsurance);
            }
            catch (Exception ex)
            {
                ShowDebugInfo();
                throw new MyErrorException(ex.Message, GetType().Name, method, line);
            }
            return veh;
        }

        /// <summary>
        /// Return all Vehicle
        /// </summary>
        /// <returns></returns>
        public override List<Vehicle> SelectAllElement()
        {
            string query = "SELECT id As VehicleId, name As NameVehicle, numberplate As NumberPlate, idpolicelocality As IdPoliceLocality, saledate As SaleDate, lastcontrol As LastControl, kmlastconstrol As KmLastControl, nextcontrol As NextControl, idtires As IdTires, fueltype As FuelType, vehicletype As VehicleType, status As Status, description As DescriptionVehicle, idinsurance As IdInsurance FROM Vehicle;";
            List<Vehicle> allVehicle = new List<Vehicle>();

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
                            allVehicle.Add(ConvertSqlDataReaderToClass(reader));
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

            return allVehicle;
        }

        /// <summary>
        /// Return Vehicle by Id : type int
        /// </summary>
        /// <param name="VehicleId"></param>
        /// <returns></returns>
        public override Vehicle SelectElementById(int VehicleId)
        {
            string query = "SELECT id As VehicleId, name As NameVehicle, numberplate As NumberPlate, idpolicelocality As IdPoliceLocality, saledate As SaleDate, lastcontrol As LastControl, kmlastconstrol As KmLastControl, nextcontrol As NextControl, idtires As IdTires, fueltype As FuelType, vehicletype As VehicleType, status As Status, description As DescriptionVehicle, idinsurance As IdInsurance FROM Vehicle WHERE id = @VehicleId;";
            Vehicle vehicle = null;

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@VehicleId", VehicleId);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            vehicle = ConvertSqlDataReaderToClass(reader);
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

            return vehicle;
        }

        /// <summary>
        /// Insert new Vehicle in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool InsertNewElement(Vehicle value)
        {
            string query = "INSERT INTO Vehicle (name, numberplate, idpolicelocality, saledate, lastcontrol, kmlastconstrol, nextcontrol, idtires, fueltype, vehicletype, status, description, idinsurance, createddate, createdby) VALUES"
                + "(@NameVehicle, @NumberPlate, @IdPoliceLocality, @SaleDate, @LastControl, @KmLastControl, @NextControl, @IdTires, @FuelType, @VehicleType, @Status, @DescriptionVehicle, @IdInsurance, @CreatedDate, @CreatedBy);";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@NameVehicle", value.name);
                cmd.Parameters.AddWithValue("@NumberPlate", value.numberplate);
                cmd.Parameters.AddWithValue("@IdPoliceLocality", value.idpolicelocality.id);
                cmd.Parameters.AddWithValue("@SaleDate", value.saledate);
                cmd.Parameters.AddWithValue("@LastControl", value.lastcontrol);
                cmd.Parameters.AddWithValue("@KmLastControl", value.kmlastcontrol);
                cmd.Parameters.AddWithValue("@NextControl", value.nextcontrol);
                cmd.Parameters.AddWithValue("@IdTires", value.idtires != null ? value.idtires.id : SqlInt32.Null);
                cmd.Parameters.AddWithValue("@FuelType", value.fueltype.id);
                cmd.Parameters.AddWithValue("@VehicleType", value.vehicletype);
                cmd.Parameters.AddWithValue("@Status", value.status.id);
                cmd.Parameters.AddWithValue("@DescriptionVehicle", value.description);
                cmd.Parameters.AddWithValue("@IdInsurance", value.idinsurance != null ? value.idinsurance.id : SqlInt32.Null);
                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@CreatedBy", CurrentUser.userId.id);

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
                            case 2627: // Primary Key Unique violation
                                ShowDebugInfo();
                                throw new MyErrorException("Il existe déjà un véhicule avec le set de pneus choisis. Veuillez en sélectionner un autre ou en ajouter un nouveau dans l'onglet pneu.", GetType().Name, method, line, true);
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

        /// <summary>
        /// Update Vehicle in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool UpdateElement(Vehicle value)
        {
            string query = "UPDATE Vehicle SET name = @NameVehicle, numberplate = @NumberPlate, idpolicelocality = @IdPoliceLocality, saledate = @SaleDate, lastcontrol = @LastControl, kmlastconstrol = @KmLastControl, nextcontrol = @NextControl, idtires = @IdTires, fueltype = @FuelType, vehicletype = @VehicleType, status = @Status, description = @DescriptionVehicle, idinsurance = @IdInsurance, modifieddate = @ModifiedDate, modifiedby = @ModifiedBy WHERE id = @VehicleId;";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@VehicleId", value.id);
                cmd.Parameters.AddWithValue("@NameVehicle", value.name);
                cmd.Parameters.AddWithValue("@NumberPlate", value.numberplate);
                cmd.Parameters.AddWithValue("@IdPoliceLocality", value.idpolicelocality.id);
                cmd.Parameters.AddWithValue("@SaleDate", value.saledate);
                cmd.Parameters.AddWithValue("@LastControl", value.lastcontrol);
                cmd.Parameters.AddWithValue("@KmLastControl", value.kmlastcontrol);
                cmd.Parameters.AddWithValue("@NextControl", value.nextcontrol);
                cmd.Parameters.AddWithValue("@IdTires", value.idtires != null ? value.idtires.id : SqlInt32.Null);
                cmd.Parameters.AddWithValue("@FuelType", value.fueltype.id);
                cmd.Parameters.AddWithValue("@VehicleType", value.vehicletype);
                cmd.Parameters.AddWithValue("@Status", value.status.id);
                cmd.Parameters.AddWithValue("@DescriptionVehicle", value.description);
                cmd.Parameters.AddWithValue("@IdInsurance", value.idinsurance != null ? value.idinsurance.id : SqlInt32.Null);
                cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@ModifiedBy", CurrentUser.userId.id);

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
                            case 2627: // Primary Key Unique violation
                                ShowDebugInfo();
                                throw new MyErrorException("Il existe déjà un véhicule avec le set de pneus choisis. Veuillez en sélectionner un autre ou en ajouter un nouveau dans l'onglet pneu.", GetType().Name, method, line, true);
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

        /// <summary>
        /// Delete Vehicle in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool DeleteElement(Vehicle value)
        {
            string query = "DELETE FROM Vehicle WHERE id = @VehicleId;";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@VehicleId", value.id);

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
                                throw new MyErrorException("Erreur de suppression, une liaison est toujours présente dans les services, retirer le véhicule de tous les services avant sa suppression.", GetType().Name, method, line, true);
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
