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
    class RequestDBGradePoint : FactoryDB<GradePoint>
    {
        /// <summary>
        /// Converty the SqlDataReader to GradePoint
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public override GradePoint ConvertSqlDataReaderToClass(SqlDataReader dr)
        {
            GradePoint gradepoint = null;
            int id = 0;
            string name = "";
            int totalpoint = 0;

            try
            {
                id = Convert.ToInt32(dr["GradePointId"]);
                if (!string.IsNullOrEmpty(dr["Name"].ToString()))
                    name = Convert.ToString(dr["Name"]);
                if (!string.IsNullOrEmpty(dr["TotalPoint"].ToString()))
                    totalpoint = Convert.ToInt32(dr["TotalPoint"]);

                gradepoint = new GradePoint(id, name, totalpoint);
            }
            catch (Exception ex)
            {
                ShowDebugInfo();
                throw new MyErrorException(ex.Message, GetType().Name, method, line);
            }
            return gradepoint;
        }

        /// <summary>
        /// Return all GradePoint
        /// </summary>
        /// <returns></returns>
        public override List<GradePoint> SelectAllElement()
        {
            string query = "SELECT id As GradePointId, name As Name, totalpoint As TotalPoint FROM gradepoint;";
            List<GradePoint> allGradePoint = new List<GradePoint>();

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
                            allGradePoint.Add(ConvertSqlDataReaderToClass(reader));
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

            return allGradePoint;
        }

        /// <summary>
        /// Return GradePoint by Id : type int
        /// </summary>
        /// <param name="GradePointId"></param>
        /// <returns></returns>
        public override GradePoint SelectElementById(int GradePointId)
        {
            string query = "SELECT id As GradePointId, name As Name, totalpoint As TotalPoint FROM gradepoint WHERE id = @GradePointId;";
            GradePoint gradepoint = null;

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@GradePointId", GradePointId);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            gradepoint = ConvertSqlDataReaderToClass(reader);
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

            return gradepoint;
        }

        /// <summary>
        /// Insert new GradePoint in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool InsertNewElement(GradePoint value)
        {
            return false;
        }

        /// <summary>
        /// Update GradePoint in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool UpdateElement(GradePoint value)
        {
            return false;
        }

        /// <summary>
        /// Delete GradePoint in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool DeleteElement(GradePoint value)
        {
            return false;
        }
    }
}
