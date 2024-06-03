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
    class RequestDBProvider : FactoryDB<Provider>
    {
        /// <summary>
        /// Converty the SqlDataReader to Provider
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public override Provider ConvertSqlDataReaderToClass(SqlDataReader dr)
        {
            RequestDBProviderType RPT = new RequestDBProviderType();
            Provider provider = null;
            int providerid = 0;
            string name = "";
            int numberphone = 0;
            string mail = "";
            string street = "";
            string housenumber = "";
            string postalcode = "";
            string town = "";
            string description = "";
            ProviderType idtype = null;

            try
            {
                providerid = Convert.ToInt32(dr["ProviderId"]);
                if (!string.IsNullOrEmpty(dr["Name"].ToString()))
                    name = Convert.ToString(dr["Name"]);
                if (!string.IsNullOrEmpty(dr["NumberPhone"].ToString()))
                    numberphone = Convert.ToInt32(dr["NumberPhone"]);
                if (!string.IsNullOrEmpty(dr["Mail"].ToString()))
                    mail = Convert.ToString(dr["Mail"]);
                if (!string.IsNullOrEmpty(dr["Street"].ToString()))
                    street = Convert.ToString(dr["Street"]);
                if (!string.IsNullOrEmpty(dr["HouseNumber"].ToString()))
                    housenumber = Convert.ToString(dr["HouseNumber"]);
                if (!string.IsNullOrEmpty(dr["PostalCode"].ToString()))
                    postalcode = Convert.ToString(dr["PostalCode"]);
                if (!string.IsNullOrEmpty(dr["Town"].ToString()))
                    town = Convert.ToString(dr["Town"]);
                if (!string.IsNullOrEmpty(dr["Description"].ToString()))
                    description = Convert.ToString(dr["Description"]);
                if (!string.IsNullOrEmpty(dr["IdType"].ToString()))
                    idtype = RPT.SelectElementById(Convert.ToInt32(dr["IdType"]));

                provider = new Provider(providerid, name, numberphone, mail, street, housenumber, postalcode, town, description, idtype);
            }
            catch (Exception ex)
            {
                ShowDebugInfo();
                throw new MyErrorException(ex.Message, GetType().Name, method, line);
            }
            return provider;
        }

        /// <summary>
        /// Return all Provider
        /// </summary>
        /// <returns></returns>
        public override List<Provider> SelectAllElement()
        {
            string query = "SELECT id As ProviderId, name As Name, phone As NumberPhone, mail As Mail, street As Street, housenumber As HouseNumber, postalcode As PostalCode, town As Town, description As Description, idtype As IdType FROM Provider;";
            List<Provider> allProvider = new List<Provider>();

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
                            allProvider.Add(ConvertSqlDataReaderToClass(reader));
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

            return allProvider;
        }

        /// <summary>
        /// Return all Insurance (idtype 1)
        /// </summary>
        /// <returns></returns>
        public List<Provider> SelectAllInsurance()
        {
            string query = "SELECT id As ProviderId, name As Name, phone As NumberPhone, mail As Mail, street As Street, housenumber As HouseNumber, postalcode As PostalCode, town As Town, description As Description, idtype As IdType FROM Provider WHERE idtype = 1;";
            List<Provider> allProvider = new List<Provider>();

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
                            allProvider.Add(ConvertSqlDataReaderToClass(reader));
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

            return allProvider;
        }

        /// <summary>
        /// Return Provider by Id : type int
        /// </summary>
        /// <param name="ProviderId"></param>
        /// <returns></returns>
        public override Provider SelectElementById(int ProviderId)
        {
            string query = "SELECT id As ProviderId, name As Name, phone As NumberPhone, mail As Mail, street As Street, housenumber As HouseNumber, postalcode As PostalCode, town As Town, description As Description, idtype As IdType FROM Provider WHERE id = @ProviderId;";
            Provider provider = null;

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@ProviderId", ProviderId);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            provider = ConvertSqlDataReaderToClass(reader);
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

            return provider;
        }

        /// <summary>
        /// Insert new Provider in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool InsertNewElement(Provider value)
        {
            string query = "INSERT INTO Provider (name, phone, mail, street, housenumber, postalcode, town, description, idtype, createddate, createdby) VALUES"
                + "(@Name, @NumberPhone, @Mail, @Street, @HouseNumber, @PostalCode, @Town, @Description, @IdType, @CreatedDate, @CreatedBy);";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@Name", value.name);
                cmd.Parameters.AddWithValue("@NumberPhone", value.phone);
                cmd.Parameters.AddWithValue("@Mail", value.mail);
                cmd.Parameters.AddWithValue("@Street", value.street);
                cmd.Parameters.AddWithValue("@HouseNumber", value.housenumber);
                cmd.Parameters.AddWithValue("@PostalCode", value.postalcode);
                cmd.Parameters.AddWithValue("@Town", value.town);
                cmd.Parameters.AddWithValue("@Description", value.description);
                cmd.Parameters.AddWithValue("@IdType", value.idtype.id);
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
        /// Update Provider in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool UpdateElement(Provider value)
        {
            string query = "UPDATE Provider SET name = @Name, phone = @NumberPhone, mail = @Mail, street = @Street, housenumber = @HouseNumber, postalcode = @PostalCode, town = @Town, description = @Description, idtype = @IdType, modifieddate = @ModifiedDate, modifiedby = @ModifiedBy WHERE id = @ProviderId;";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@ProviderId", value.id);
                cmd.Parameters.AddWithValue("@Name", value.name);
                cmd.Parameters.AddWithValue("@NumberPhone", value.phone);
                cmd.Parameters.AddWithValue("@Mail", value.mail);
                cmd.Parameters.AddWithValue("@Street", value.street);
                cmd.Parameters.AddWithValue("@HouseNumber", value.housenumber);
                cmd.Parameters.AddWithValue("@PostalCode", value.postalcode);
                cmd.Parameters.AddWithValue("@Town", value.town);
                cmd.Parameters.AddWithValue("@Description", value.description);
                cmd.Parameters.AddWithValue("@IdType", value.idtype.id);
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
        /// Delete Provider in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool DeleteElement(Provider value)
        {
            string query = "DELETE FROM Provider WHERE id = @ProviderId;";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@ProviderId", value.id);

                try
                {
                    DeleteInsuranceVehicle(value); // Delete the insurance vehicle in same time
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
        /// Delete the insurance vehicle
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool DeleteInsuranceVehicle(Provider value)
        {
            string query = "DELETE FROM insurance_vehicle WHERE idprovider = @ProviderId;";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@ProviderId", value.id);

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
                                throw new MyErrorException("Erreur de suppression, une liaison est toujours présente, vérifier que l'entreprise n'est plus utilisée pour les assurances, articles, services avant sa suppression.", GetType().Name, method, line, true);
                            case 2601: // Primary key violation
                            default:
                                ShowDebugInfo();
                                throw new MyErrorException(ex.Message, GetType().Name, method, line);
                        }
                    }
                }

                //CloseConnection();
                return true;
            }

            return false;
        }
    }
}
