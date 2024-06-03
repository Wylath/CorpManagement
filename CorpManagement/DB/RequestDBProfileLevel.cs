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
    class RequestDBProfileLevel : FactoryDB<ProfileLevel>
    {
        /// <summary>
        /// Converty the SqlDataReader to ProfileLevel
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public override ProfileLevel ConvertSqlDataReaderToClass(SqlDataReader dr)
        {
            ProfileLevel profilelevel = null;
            int profilelevelid = 0;
            string name = "";

            try
            {
                profilelevelid = Convert.ToInt32(dr["ProfileLevelId"]);
                if (!string.IsNullOrEmpty(dr["Name"].ToString()))
                    name = Convert.ToString(dr["Name"]);

                profilelevel = new ProfileLevel(profilelevelid, name);
            }
            catch (Exception ex)
            {
                ShowDebugInfo();
                throw new MyErrorException(ex.Message, GetType().Name, method, line);
            }
            return profilelevel;
        }

        /// <summary>
        /// Return all ProfileLevel
        /// </summary>
        /// <returns></returns>
        public override List<ProfileLevel> SelectAllElement()
        {
            string query = "SELECT id As ProfileLevelId, name As Name FROM profilelevel;";
            List<ProfileLevel> allProfileLevel = new List<ProfileLevel>();

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
                            allProfileLevel.Add(ConvertSqlDataReaderToClass(reader));
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

            return allProfileLevel;
        }

        /// <summary>
        /// Return ProfileLevel by Id : type int
        /// </summary>
        /// <param name="ProfileLevelId"></param>
        /// <returns></returns>
        public override ProfileLevel SelectElementById(int ProfileLevelId)
        {
            string query = "SELECT id As ProfileLevelId, name As Name FROM profilelevel WHERE id = @ProfileLevelId;";
            ProfileLevel profilelevel = null;

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@ProfileLevelId", ProfileLevelId);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            profilelevel = ConvertSqlDataReaderToClass(reader);
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

            return profilelevel;
        }

        /// <summary>
        /// Insert new ProfileLevel in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool InsertNewElement(ProfileLevel value)
        {
            return false;
        }

        /// <summary>
        /// Update ProfileLevel in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool UpdateElement(ProfileLevel value)
        {
            return false;
        }

        /// <summary>
        /// Delete ProfileLevel in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool DeleteElement(ProfileLevel value)
        {
            return false;
        }
    }
}
