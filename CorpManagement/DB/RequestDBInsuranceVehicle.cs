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
    class RequestDBInsuranceVehicle : FactoryDB<InsuranceVehicle>
    {
        /// <summary>
        /// Converty the SqlDataReader to InsuranceVehicle
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public override InsuranceVehicle ConvertSqlDataReaderToClass(SqlDataReader dr)
        {
            RequestDBProvider RP = new RequestDBProvider();
            InsuranceVehicle insurance = null;
            int id = 0;
            int number = 0;
            Provider idprovider = null;
            DateTime effective = DateTime.Now;
            DateTime expire = DateTime.Now;
            bool active = false;
            string coverage = "";
            float price = 0.00f;
            string description = "";

            try
            {
                id = Convert.ToInt32(dr["Id"]);
                if (!string.IsNullOrEmpty(dr["InsuranceNumber"].ToString()))
                    number = Convert.ToInt32(dr["InsuranceNumber"]);
                if (!string.IsNullOrEmpty(dr["EffectiveDate"].ToString()))
                    effective = Convert.ToDateTime(dr["EffectiveDate"]);
                if (!string.IsNullOrEmpty(dr["ExpireDate"].ToString()))
                    expire = Convert.ToDateTime(dr["ExpireDate"]);
                if (!string.IsNullOrEmpty(dr["Active"].ToString()))
                    active = Convert.ToBoolean(dr["Active"]);
                if (!string.IsNullOrEmpty(dr["Coverage"].ToString()))
                    coverage = Convert.ToString(dr["Coverage"]);
                if (!string.IsNullOrEmpty(dr["Price"].ToString()))
                    float.TryParse(Convert.ToString(dr["Price"]), out price);
                if (!string.IsNullOrEmpty(dr["Description"].ToString()))
                    description = Convert.ToString(dr["Description"]);
                if (!string.IsNullOrEmpty(dr["IdProvider"].ToString()))
                    idprovider = RP.SelectElementById(Convert.ToInt32(dr["IdProvider"]));

                insurance = new InsuranceVehicle(id, number, idprovider, effective, expire, active, coverage, price, description);
            }
            catch (Exception ex)
            {
                ShowDebugInfo();
                throw new MyErrorException(ex.Message, GetType().Name, method, line);
            }
            return insurance;
        }

        /// <summary>
        /// Return all InsuranceVehicle
        /// </summary>
        /// <returns></returns>
        public override List<InsuranceVehicle> SelectAllElement()
        {
            string query = "SELECT id As Id, idprovider As IdProvider, insurancenumber As InsuranceNumber, effectivedate As EffectiveDate, expiredate As ExpireDate, active As Active, coverage As Coverage, price As Price, description As Description FROM insurance_vehicle;";
            List<InsuranceVehicle> allInsuranceVehicle = new List<InsuranceVehicle>();

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
                            allInsuranceVehicle.Add(ConvertSqlDataReaderToClass(reader));
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

            return allInsuranceVehicle;
        }

        /// <summary>
        /// Return InsuranceVehicle by Id : type int
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public override InsuranceVehicle SelectElementById(int IdInsurance)
        {
            string query = "SELECT id As Id, idprovider As IdProvider, insurancenumber As InsuranceNumber, effectivedate As EffectiveDate, expiredate As ExpireDate, active As Active, coverage As Coverage, price As Price, description As Description FROM insurance_vehicle WHERE id = @IdInsurance;";
            InsuranceVehicle insurancevehicle = null;

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@IdInsurance", IdInsurance);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            insurancevehicle = ConvertSqlDataReaderToClass(reader);
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

            return insurancevehicle;
        }

        /// <summary>
        /// Insert new InsuranceVehicle in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool InsertNewElement(InsuranceVehicle value)
        {
            string query = "INSERT INTO insurance_vehicle (idprovider, insurancenumber, effectivedate, expiredate, active, coverage, price, description, createddate, createdby) VALUES"
                + "(@IdProvider, @InsuranceNumber, @EffectiveDate, @ExpireDate, @Active, @Coverage, @Price, @Description, @CreatedDate, @CreatedBy);";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@IdProvider", value.idprovider.id);
                cmd.Parameters.AddWithValue("@InsuranceNumber", value.insurancenumber);
                cmd.Parameters.AddWithValue("@EffectiveDate", value.effectivedate);
                cmd.Parameters.AddWithValue("@ExpireDate", value.expiredate);
                cmd.Parameters.AddWithValue("@Active", value.active);
                cmd.Parameters.AddWithValue("@Coverage", value.coverage);
                cmd.Parameters.AddWithValue("@Price", value.price);
                cmd.Parameters.AddWithValue("@Description", value.description);
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
        /// Update InsuranceVehicle in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool UpdateElement(InsuranceVehicle value)
        {
            string query = "UPDATE insurance_vehicle SET idprovider = @IdProvider, insurancenumber = @InsuranceNumber, effectivedate = @EffectiveDate, expiredate = @ExpireDate, active = @Active, coverage = @Coverage, price = @Price, description = @Description, modifieddate = @ModifiedDate, modifiedby = @ModifiedBy WHERE id = @IdInsurance;";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@IdInsurance", value.id);
                cmd.Parameters.AddWithValue("@IdProvider", value.idprovider.id);
                cmd.Parameters.AddWithValue("@InsuranceNumber", value.insurancenumber);
                cmd.Parameters.AddWithValue("@EffectiveDate", value.effectivedate);
                cmd.Parameters.AddWithValue("@ExpireDate", value.expiredate);
                cmd.Parameters.AddWithValue("@Active", value.active);
                cmd.Parameters.AddWithValue("@Coverage", value.coverage);
                cmd.Parameters.AddWithValue("@Price", value.price);
                cmd.Parameters.AddWithValue("@Description", value.description);
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
        /// Delete InsuranceVehicle in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool DeleteElement(InsuranceVehicle value)
        {
            string query = "DELETE FROM insurance_vehicle WHERE id = @IdInsurance;";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@IdInsurance", value.id);

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
                                throw new MyErrorException("Erreur de suppression, une liaison est toujours présente, vérifier que l'assurance n'est plus utilisée pour les véhicules et les factures avant sa suppression.", GetType().Name, method, line, true);
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
