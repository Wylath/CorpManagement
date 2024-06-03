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
    class RequestDBUser : FactoryDB<User>
    {
        /// <summary>
        /// Converty the SqlDataReader to User
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public override User ConvertSqlDataReaderToClass(SqlDataReader dr)
        {
            RequestDBProfileLevel RPL = new RequestDBProfileLevel();
            RequestDBGradePoint RGP = new RequestDBGradePoint();
            User user = null;
            int userid = 0;
            string lastname = "";
            string firstname = "";
            int matricule = 0;
            ProfileLevel idprofilelevel = null;
            int pointarticle = 0;
            GradePoint gradepoint = null;
            bool status = false;
            DateTime lastupdatepoint = DateTime.Now;

            try
            {
                userid = Convert.ToInt32(dr["UserId"]);
                if (!string.IsNullOrEmpty(dr["LastName"].ToString()))
                    lastname = Convert.ToString(dr["LastName"]);
                if (!string.IsNullOrEmpty(dr["FirstName"].ToString()))
                    firstname = Convert.ToString(dr["FirstName"]);
                if (!string.IsNullOrEmpty(dr["Matricule"].ToString()))
                    matricule = Convert.ToInt32(dr["Matricule"]);
                if (!string.IsNullOrEmpty(dr["PointArticle"].ToString()))
                    pointarticle = Convert.ToInt32(dr["PointArticle"]);
                if (!string.IsNullOrEmpty(dr["Status"].ToString()))
                    status = Convert.ToBoolean(dr["Status"]);
                if (!string.IsNullOrEmpty(dr["LastUpdatePoint"].ToString()))
                    lastupdatepoint = Convert.ToDateTime(dr["LastUpdatePoint"]);
                if (!string.IsNullOrEmpty(dr["IdProfileLevel"].ToString()))
                    idprofilelevel = RPL.SelectElementById(Convert.ToInt32(dr["IdProfileLevel"]));
                if (!string.IsNullOrEmpty(dr["GradePoint"].ToString()))
                    gradepoint = RGP.SelectElementById(Convert.ToInt32(dr["GradePoint"]));

                user = new User(userid, lastname, firstname, matricule, idprofilelevel, pointarticle, gradepoint, status, lastupdatepoint);
            }
            catch (Exception ex)
            {
                ShowDebugInfo();
                throw new MyErrorException(ex.Message, GetType().Name, method, line);
            }
            return user;
        }

        /// <summary>
        /// Return all User
        /// </summary>
        /// <returns></returns>
        public override List<User> SelectAllElement()
        {
            string query = "SELECT id As UserId, lastname As LastName, firstname As FirstName, matricule As Matricule, pointarticle As PointArticle, idprofilelevel As IdProfileLevel, gradepoint As GradePoint, status As Status, lastupdatepoint As LastUpdatePoint FROM [User];";
            List<User> allUser = new List<User>();

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
                            allUser.Add(ConvertSqlDataReaderToClass(reader));
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

            return allUser;
        }

        /// <summary>
        /// Return User by Id : type int
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public override User SelectElementById(int UserId)
        {
            string query = "SELECT id As UserId, lastname As LastName, firstname As FirstName, matricule As Matricule, pointarticle As PointArticle, idprofilelevel As IdProfileLevel, gradepoint As GradePoint, status As Status, lastupdatepoint As LastUpdatePoint FROM [User] WHERE id = @UserId;";
            User user = null;

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@UserId", UserId);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            user = ConvertSqlDataReaderToClass(reader);
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

            return user;
        }

        /// <summary>
        /// Get user by matricule
        /// </summary>
        /// <param name="matricule"></param>
        /// <returns></returns>
        public User SelectElementByMatricule(int Matricule)
        {
            string query = "SELECT id As UserId, lastname As LastName, firstname As FirstName, matricule As Matricule, pointarticle As PointArticle, idprofilelevel As IdProfileLevel, gradepoint As GradePoint, status As Status, lastupdatepoint As LastUpdatePoint FROM [User] WHERE matricule = @Matricule;";
            User user = null;

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@Matricule", Matricule);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            user = ConvertSqlDataReaderToClass(reader);
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

            return user;
        }

        /// <summary>
        /// Insert new User in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool InsertNewElement(User value)
        {
            string query = "INSERT INTO [User] (lastname, firstname, matricule, pointarticle, idprofilelevel, gradepoint, status, lastupdatepoint, createddate, createdby) VALUES"
                + "(@LastName, @FirstName, @Matricule, @PointArticle, @IdProfileLevel, @GradePoint, @Status, @LastUpdatePoint, @CreatedDate, @CreatedBy);";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@LastName", value.lastname);
                cmd.Parameters.AddWithValue("@FirstName", value.firstname);
                cmd.Parameters.AddWithValue("@Matricule", value.matricule);
                cmd.Parameters.AddWithValue("@PointArticle", value.pointarticle);
                cmd.Parameters.AddWithValue("@IdProfileLevel", value.idprofilelevel.id);
                cmd.Parameters.AddWithValue("@GradePoint", value.gradepoint.id);
                cmd.Parameters.AddWithValue("@Status", value.status);
                cmd.Parameters.AddWithValue("@LastUpdatePoint", value.lastupdatepoint);
                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@CreatedBy", (CurrentUser.userId != null) ? CurrentUser.userId.id : SqlInt32.Null);

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
        /// Update User in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool UpdateElement(User value)
        {
            string query = "UPDATE [User] SET lastname = @LastName, firstname = @FirstName, matricule = @Matricule, pointarticle = @PointArticle, idprofilelevel = @IdProfileLevel, gradepoint = @GradePoint, status = @Status, lastupdatepoint = @LastUpdatePoint, modifieddate = @ModifiedDate, modifiedby = @ModifiedBy WHERE id = @UserId;";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@UserId", value.id);
                cmd.Parameters.AddWithValue("@LastName", value.lastname);
                cmd.Parameters.AddWithValue("@FirstName", value.firstname);
                cmd.Parameters.AddWithValue("@Matricule", value.matricule);
                cmd.Parameters.AddWithValue("@PointArticle", value.pointarticle);
                cmd.Parameters.AddWithValue("@IdProfileLevel", value.idprofilelevel.id);
                cmd.Parameters.AddWithValue("@GradePoint", value.gradepoint.id);
                cmd.Parameters.AddWithValue("@Status", value.status);
                cmd.Parameters.AddWithValue("@LastUpdatePoint", value.lastupdatepoint);
                cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@ModifiedBy", (CurrentUser.userId != null) ? CurrentUser.userId.id : SqlInt32.Null);

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
        /// Delete User in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool DeleteElement(User value)
        {
            string query = "DELETE FROM [User] WHERE id = @UserId;";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@UserId", value.id);

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
