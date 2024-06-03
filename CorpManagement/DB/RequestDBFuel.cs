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
    class RequestDBFuel : FactoryDB<Fuel>
    {
        /// <summary>
        /// Converty the SqlDataReader to Fuel
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public override Fuel ConvertSqlDataReaderToClass(SqlDataReader dr)
        {
            Fuel fuel = null;
            int fuelid = 0;
            string name = "";

            try
            {
                fuelid = Convert.ToInt32(dr["FuelId"]);
                if (!string.IsNullOrEmpty(dr["Name"].ToString()))
                    name = Convert.ToString(dr["Name"]);

                fuel = new Fuel(fuelid, name);
            }
            catch (Exception ex)
            {
                ShowDebugInfo();
                throw new MyErrorException(ex.Message, GetType().Name, method, line);
            }
            return fuel;
        }

        /// <summary>
        /// Return all Fuel
        /// </summary>
        /// <returns></returns>
        public override List<Fuel> SelectAllElement()
        {
            string query = "SELECT id As FuelId, name As Name FROM Fuel;";
            List<Fuel> allFuel = new List<Fuel>();

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
                            allFuel.Add(ConvertSqlDataReaderToClass(reader));
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

            return allFuel;
        }

        /// <summary>
        /// Return Fuel by Id : type int
        /// </summary>
        /// <param name="FuelId"></param>
        /// <returns></returns>
        public override Fuel SelectElementById(int FuelId)
        {
            string query = "SELECT id As FuelId, name As Name FROM Fuel WHERE id = @FuelId;";
            Fuel fuel = null;

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@FuelId", FuelId);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            fuel = ConvertSqlDataReaderToClass(reader);
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

            return fuel;
        }

        /// <summary>
        /// Insert new Fuel in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool InsertNewElement(Fuel value)
        {
            return false;
        }

        /// <summary>
        /// Update Fuel in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool UpdateElement(Fuel value)
        {
            return false;
        }

        /// <summary>
        /// Delete Fuel in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool DeleteElement(Fuel value)
        {
            return false;
        }
    }
}
