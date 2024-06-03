using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorpManagement.Model
{
    class Invoice
    {
        public int id { get; set; }
        public InsuranceVehicle idinsurance { get; set; }
        public OrderArticle idorderarticle { get; set; }
        public Servicing idservicing { get; set; }
        public float price { get; set; }
        public DateTime dateinvoice { get; set; }
        public string description { get; set; }
        public DateTime datepaid { get; set; }
        public InvoiceType idtype { get; set; }

        /// <summary>
        /// Constructor with id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idinsurance"></param>
        /// <param name="idorderarticle"></param>
        /// <param name="idservicing"></param>
        /// <param name="price"></param>
        /// <param name="dateinvoice"></param>
        /// <param name="description"></param>
        /// <param name="datepaid"></param>
        /// <param name="idtype"></param>
        public Invoice(int id, InsuranceVehicle idinsurance, OrderArticle idorderarticle, Servicing idservicing, float price, DateTime dateinvoice, string description, DateTime datepaid, InvoiceType idtype)
        {
            this.id = id;
            this.idinsurance = idinsurance;
            this.idorderarticle = idorderarticle;
            this.idservicing = idservicing;
            this.price = price;
            this.dateinvoice = dateinvoice;
            this.description = description;
            this.datepaid = datepaid;
            this.idtype = idtype;
        }

        /// <summary>
        /// Constructor with id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idinsurance"></param>
        /// <param name="idorderarticle"></param>
        /// <param name="idservicing"></param>
        /// <param name="price"></param>
        /// <param name="dateinvoice"></param>
        /// <param name="description"></param>
        /// <param name="idtype"></param> without datepaid
        public Invoice(int id, InsuranceVehicle idinsurance, OrderArticle idorderarticle, Servicing idservicing, float price, DateTime dateinvoice, string description, InvoiceType idtype)
        {
            this.id = id;
            this.idinsurance = idinsurance;
            this.idorderarticle = idorderarticle;
            this.idservicing = idservicing;
            this.price = price;
            this.dateinvoice = dateinvoice;
            this.description = description;
            this.idtype = idtype;
        }

        /// <summary>
        /// Constructor for new Invoice Insurance
        /// </summary>
        /// <param name="idinsurance"></param>
        /// <param name="idorderarticle"></param>
        /// <param name="idservicing"></param>
        /// <param name="price"></param>
        /// <param name="dateinvoice"></param>
        /// <param name="description"></param>
        /// <param name="datepaid"></param>
        /// <param name="idtype"></param>
        public Invoice(InsuranceVehicle idinsurance, OrderArticle idorderarticle, Servicing idservicing, float price, DateTime dateinvoice, string description, DateTime datepaid, InvoiceType idtype)
        {
            this.idinsurance = idinsurance;
            this.idorderarticle = idorderarticle;
            this.idservicing = idservicing;
            this.price = price;
            this.dateinvoice = dateinvoice;
            this.description = description;
            this.datepaid = datepaid;
            this.idtype = idtype;
        }

        /// <summary>
        /// Constructor for new Invoice Insurance without datepaid
        /// </summary>
        /// <param name="idinsurance"></param>
        /// <param name="idorderarticle"></param>
        /// <param name="idservicing"></param>
        /// <param name="price"></param>
        /// <param name="dateinvoice"></param>
        /// <param name="description"></param>
        /// <param name="idtype"></param>
        public Invoice(InsuranceVehicle idinsurance, OrderArticle idorderarticle, Servicing idservicing, float price, DateTime dateinvoice, string description, InvoiceType idtype)
        {
            this.idinsurance = idinsurance;
            this.idorderarticle = idorderarticle;
            this.idservicing = idservicing;
            this.price = price;
            this.dateinvoice = dateinvoice;
            this.description = description;
            this.idtype = idtype;
        }
    }
}
