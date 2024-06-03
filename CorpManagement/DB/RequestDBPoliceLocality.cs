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
    class RequestDBPoliceLocality : FactoryDB<PoliceLocality>
    {
        /// <summary>
        /// Converty the SqlDataReader to PoliceLocality
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public override PoliceLocality ConvertSqlDataReaderToClass(SqlDataReader dr)
        {
            PoliceLocality policelocality = null;
            int providerid = 0;
            string name = "";

            try
            {
                providerid = Convert.ToInt32(dr["PoliceLocalityId"]);
                if (!string.IsNullOrEmpty(dr["Name"].ToString()))
                    name = Convert.ToString(dr["Name"]);

                policelocality = new PoliceLocality(providerid, name);
            }
            catch (Exception ex)
            {
                ShowDebugInfo();
                throw new MyErrorException(ex.Message, GetType().Name, method, line);
            }
            return policelocality;
        }

        /// <summary>
        /// Return all PoliceLocality
        /// </summary>
        /// <returns></returns>
        public override List<PoliceLocality> SelectAllElement()
        {
            string query = "SELECT id As PoliceLocalityId, name As Name FROM police_locality;";
            List<PoliceLocality> allPoliceLocality = new List<PoliceLocality>();

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
                            allPoliceLocality.Add(ConvertSqlDataReaderToClass(reader));
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

            return allPoliceLocality;
        }

        /// <summary>
        /// Return PoliceLocality by Id : type int
        /// </summary>
        /// <param name="PoliceLocalityId"></param>
        /// <returns></returns>
        public override PoliceLocality SelectElementById(int PoliceLocalityId)
        {
            string query = "SELECT id As PoliceLocalityId, name As Name FROM police_locality WHERE id = @PoliceLocalityId;";
            PoliceLocality policelocality = null;

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@PoliceLocalityId", PoliceLocalityId);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            policelocality = ConvertSqlDataReaderToClass(reader);
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

            return policelocality;
        }

        /// <summary>
        /// Insert new PoliceLocality in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool InsertNewElement(PoliceLocality value)
        {
            string query = "INSERT INTO police_locality (name, createddate, createdby) VALUES"
                + "(@Name, @CreatedDate, @CreatedBy);";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@Name", value.name);
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
        /// Update PoliceLocality in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool UpdateElement(PoliceLocality value)
        {
            string query = "UPDATE police_locality SET name = @Name, modifieddate = @ModifiedDate, modifiedby = @ModifiedBy WHERE id = @PoliceLocalityId;";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@PoliceLocalityId", value.id);
                cmd.Parameters.AddWithValue("@Name", value.name);
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
        /// Delete PoliceLocality in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool DeleteElement(PoliceLocality value)
        {
            string query = "DELETE FROM police_locality WHERE id = @PoliceLocalityId;";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@PoliceLocalityId", value.id);

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
                                throw new MyErrorException("Erreur de suppression, une liaison est toujours présente, vérifier que la localité n'est plus utilisée pour les véhicules avant sa suppression.", GetType().Name, method, line, true);
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
