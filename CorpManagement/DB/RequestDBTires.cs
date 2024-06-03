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
    class RequestDBTires : FactoryDB<Tires>
    {
        /// <summary>
        /// Converty the SqlDataReader to Tires
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public override Tires ConvertSqlDataReaderToClass(SqlDataReader dr)
        {
            RequestDBStatusTires RST = new RequestDBStatusTires();
            Tires tires = null;
            int tireid = 0;
            string name = "";
            string description = "";
            int setnumber = 0;
            string Dim1 = "";
            string Dim2 = "";
            string Dim3 = "";
            StatusTire state = null;

            try
            {
                tireid = Convert.ToInt32(dr["TireId"]);
                if (!string.IsNullOrEmpty(dr["Name"].ToString()))
                    name = Convert.ToString(dr["Name"]);
                if (!string.IsNullOrEmpty(dr["Description"].ToString()))
                    description = Convert.ToString(dr["Description"]);
                if (!string.IsNullOrEmpty(dr["SetNumber"].ToString()))
                    setnumber = Convert.ToInt32(dr["SetNumber"]);
                if (!string.IsNullOrEmpty(dr["Dim1"].ToString()))
                    Dim1 = Convert.ToString(dr["Dim1"]);
                if (!string.IsNullOrEmpty(dr["Dim2"].ToString()))
                    Dim2 = Convert.ToString(dr["Dim2"]);
                if (!string.IsNullOrEmpty(dr["Dim3"].ToString()))
                    Dim3 = Convert.ToString(dr["Dim3"]);
                if (!string.IsNullOrEmpty(dr["IdState"].ToString()))
                    state = RST.SelectElementById(Convert.ToInt32(dr["IdState"]));

                tires = new Tires(tireid, name, state, description, setnumber, Dim1, Dim2, Dim3);
            }
            catch (Exception ex)
            {
                ShowDebugInfo();
                throw new MyErrorException(ex.Message, GetType().Name, method, line);
            }
            return tires;
        }

        /// <summary>
        /// Return all Tires
        /// </summary>
        /// <returns></returns>
        public override List<Tires> SelectAllElement()
        {
            string query = "SELECT id As TireId, name As Name, state As IdState, description As Description, setnumber As SetNumber, dim1 As Dim1, dim2 As Dim2, dim3 As Dim3 FROM Tires;";
            List<Tires> allTires = new List<Tires>();

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
                            allTires.Add(ConvertSqlDataReaderToClass(reader));
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

            return allTires;
        }

        /// <summary>
        /// Return Tires by Id : type int
        /// </summary>
        /// <param name="TireId"></param>
        /// <returns></returns>
        public override Tires SelectElementById(int TireId)
        {
            string query = "SELECT id As TireId, name As Name, state As IdState, description As Description, setnumber As SetNumber, dim1 As Dim1, dim2 As Dim2, dim3 As Dim3 FROM Tires WHERE id = @TireId;";
            Tires tires = null;

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@TireId", TireId);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            tires = ConvertSqlDataReaderToClass(reader);
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

            return tires;
        }

        /// <summary>
        /// Insert new Tires in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool InsertNewElement(Tires value)
        {
            string query = "INSERT INTO Tires (name, state, description, setnumber, dim1, dim2, dim3, createddate, createdby) VALUES"
                + "(@Name, @IdState, @Description, @SetNumber, @Dim1, @Dim2, @Dim3, @CreatedDate, @CreatedBy);";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@Name", value.name);
                cmd.Parameters.AddWithValue("@IdState", value.state.id);
                cmd.Parameters.AddWithValue("@Description", value.description);
                cmd.Parameters.AddWithValue("@SetNumber", value.setnumber);
                cmd.Parameters.AddWithValue("@Dim1", value.Dim1);
                cmd.Parameters.AddWithValue("@Dim2", value.Dim2);
                cmd.Parameters.AddWithValue("@Dim3", value.Dim3);
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
        /// Update Tires in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool UpdateElement(Tires value)
        {
            string query = "UPDATE Tires SET name = @Name, state = @IdState, description = @Description, setnumber = @SetNumber, dim1 = @Dim1, dim2 = @Dim2, dim3 = @Dim3, modifieddate = @ModifiedDate, modifiedby = @ModifiedBy WHERE id = @TireId;";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@TireId", value.id);
                cmd.Parameters.AddWithValue("@Name", value.name);
                cmd.Parameters.AddWithValue("@IdState", value.state.id);
                cmd.Parameters.AddWithValue("@Description", value.description);
                cmd.Parameters.AddWithValue("@SetNumber", value.setnumber);
                cmd.Parameters.AddWithValue("@Dim1", value.Dim1);
                cmd.Parameters.AddWithValue("@Dim2", value.Dim2);
                cmd.Parameters.AddWithValue("@Dim3", value.Dim3);
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
        /// Delete Tires in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool DeleteElement(Tires value)
        {
            string query = "DELETE FROM Tires WHERE id = @TireId;";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@TireId", value.id);

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
                                throw new MyErrorException("Erreur de suppression, une liaison est toujours présente dans les véhicules, retirer le set de pneu à tous les véhicules avant sa suppression.", GetType().Name, method, line, true);
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
