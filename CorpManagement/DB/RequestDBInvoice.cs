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
    class RequestDBInvoice : FactoryDB<Invoice>
    {
        /// <summary>
        /// Converty the SqlDataReader to Invoice
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public override Invoice ConvertSqlDataReaderToClass(SqlDataReader dr)
        {
            RequestDBInsuranceVehicle RIV = new RequestDBInsuranceVehicle();
            RequestDBInvoiceType RIT = new RequestDBInvoiceType();
            RequestDBOrderArticle ROA = new RequestDBOrderArticle();
            RequestDBServicing RS = new RequestDBServicing();
            Invoice invoice = null;
            int paymentid = 0;
            InsuranceVehicle idinsurance = null;
            OrderArticle idorderarticle = null;
            Servicing idservicing = null;
            float price = 0.0f;
            DateTime dateinvoice = DateTime.Now;
            string description = "";
            DateTime datepaid = DateTime.Now;
            InvoiceType idtype = null;

            try
            {
                paymentid = Convert.ToInt32(dr["InvoiceId"]);
                if (!string.IsNullOrEmpty(dr["Price"].ToString()))
                    float.TryParse(Convert.ToString(dr["Price"]), out price);
                if (!string.IsNullOrEmpty(dr["DateInvoice"].ToString()))
                    dateinvoice = Convert.ToDateTime(dr["DateInvoice"]);
                if (!string.IsNullOrEmpty(dr["Description"].ToString()))
                    description = Convert.ToString(dr["Description"]);
                if (!string.IsNullOrEmpty(dr["DatePaid"].ToString()))
                    datepaid = Convert.ToDateTime(dr["DatePaid"]);
                if (!string.IsNullOrEmpty(dr["IdType"].ToString()))
                    idtype = RIT.SelectElementById(Convert.ToInt32(dr["IdType"]));
                if (!string.IsNullOrEmpty(dr["IdInsurance"].ToString()))
                    idinsurance = RIV.SelectElementById(Convert.ToInt32(dr["IdInsurance"]));
                if (!string.IsNullOrEmpty(dr["IdOrderArticle"].ToString()))
                    idorderarticle = ROA.SelectElementById(Convert.ToInt32(dr["IdOrderArticle"]));
                if (!string.IsNullOrEmpty(dr["IdServicing"].ToString()))
                    idservicing = RS.SelectElementById(Convert.ToInt32(dr["IdServicing"]));

                if (!string.IsNullOrEmpty(dr["DatePaid"].ToString()))
                    invoice = new Invoice(paymentid, idinsurance, idorderarticle, idservicing, price, dateinvoice, description, datepaid, idtype);
                else invoice = new Invoice(paymentid, idinsurance, idorderarticle, idservicing, price, dateinvoice, description, idtype);
            }
            catch (Exception ex)
            {
                ShowDebugInfo();
                throw new MyErrorException(ex.Message, GetType().Name, method, line);
            }
            return invoice;
        }

        /// <summary>
        /// Return all Invoice
        /// </summary>
        /// <returns></returns>
        public override List<Invoice> SelectAllElement()
        {
            string query = "SELECT id As InvoiceId, idinsurance As IdInsurance, idorderarticle As IdOrderArticle, idservicing As IdServicing, price As Price, dateinvoice As DateInvoice, description As Description, datepaid As DatePaid, idtype As IdType FROM invoice;";
            List<Invoice> allInsuranceCoverage = new List<Invoice>();

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
                            allInsuranceCoverage.Add(ConvertSqlDataReaderToClass(reader));
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

            return allInsuranceCoverage;
        }

        /// <summary>
        /// Retrun the max value price for the insurance id
        /// </summary>
        /// <param name="IdInsurance"></param>
        /// <returns></returns>
        public float SelectMaxPriceInvoiceByInsurance(InsuranceVehicle IdInsurance)
        {
            string query = "SELECT MAX(price) AS Price FROM invoice WHERE idinsurance = @IdInsurance GROUP BY idinsurance;";
            float maxvalueprice = 0;

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@IdInsurance", IdInsurance.id);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            float.TryParse(Convert.ToString(reader["Price"]), out maxvalueprice);
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

            return maxvalueprice;
        }

        /// <summary>
        /// Return Invoice by InvoiceId : type int
        /// </summary>
        /// <param name="InvoiceId"></param>
        /// <returns></returns>
        public override Invoice SelectElementById(int InvoiceId)
        {
            string query = "SELECT id As InvoiceId, idinsurance As IdInsurance, idorderarticle As IdOrderArticle, idservicing As IdServicing, price As Price, dateinvoice As DateInvoice, description As Description, datepaid As DatePaid, idtype As IdType FROM invoice WHERE id = @InvoiceId;";
            Invoice insurancecoverage = null;

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@InvoiceId", InvoiceId);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            insurancecoverage = ConvertSqlDataReaderToClass(reader);
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

            return insurancecoverage;
        }

        /// <summary>
        /// Insert new Invoice in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool InsertNewElement(Invoice value)
        {
            string query = "INSERT INTO invoice (idinsurance, idorderarticle, idservicing, price, dateinvoice, description, datepaid, idtype, createddate, createdby) VALUES"
                + "(@IdInsurance, @IdOrderArticle, @IdServicing, @Price, @DateInvoice, @Description, @DatePaid, @IdType, @CreatedDate, @CreatedBy);";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@IdInsurance", value.idinsurance != null ? value.idinsurance.id : SqlInt32.Null);
                cmd.Parameters.AddWithValue("@IdOrderArticle", value.idorderarticle != null ? value.idorderarticle.idorder : SqlInt32.Null);
                cmd.Parameters.AddWithValue("@IdServicing", value.idservicing != null ? value.idservicing.id : SqlInt32.Null);
                cmd.Parameters.AddWithValue("@Price", value.price);
                cmd.Parameters.AddWithValue("@DateInvoice", value.dateinvoice);
                cmd.Parameters.AddWithValue("@Description", value.description);
                cmd.Parameters.AddWithValue("@DatePaid", value.datepaid.Year > 1 ? value.datepaid : SqlDateTime.Null);
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
        /// Update Invoice in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool UpdateElement(Invoice value)
        {
            string query = "UPDATE invoice SET idinsurance = @IdInsurance, idorderarticle = @IdOrderArticle, idservicing = @IdServicing, price = @Price, dateinvoice = @DateInvoice, description = @Description, datepaid = @DatePaid, idtype = @IdType, modifieddate = @ModifiedDate, modifiedby = @ModifiedBy WHERE id = @InvoiceId;";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@InvoiceId", value.id);
                cmd.Parameters.AddWithValue("@IdInsurance", value.idinsurance != null ? value.idinsurance.id : SqlInt32.Null);
                cmd.Parameters.AddWithValue("@IdOrderArticle", value.idorderarticle != null ? value.idorderarticle.idorder : SqlInt32.Null);
                cmd.Parameters.AddWithValue("@IdServicing", value.idservicing != null ? value.idservicing.id : SqlInt32.Null);
                cmd.Parameters.AddWithValue("@Price", value.price);
                cmd.Parameters.AddWithValue("@DateInvoice", value.dateinvoice);
                cmd.Parameters.AddWithValue("@Description", value.description);
                cmd.Parameters.AddWithValue("@DatePaid", value.datepaid.Year > 1 ? value.datepaid : SqlDateTime.Null);
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
        /// Delete Invoice in db
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool DeleteElement(Invoice value)
        {
            string query = "DELETE FROM invoice WHERE id = @InvoiceId;";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@InvoiceId", value.id);

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
