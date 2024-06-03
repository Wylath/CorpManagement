using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorpManagement.Model
{
    class InsuranceVehicle
    {
        public int id { get; set; }
        public int insurancenumber { get; set; }
        public Provider idprovider { get; set; }
        public DateTime effectivedate { get; set; }
        public DateTime expiredate { get; set; }
        public bool active { get; set; }
        public string coverage { get; set; }
        public float price { get; set; }
        public string description { get; set; }

        /// <summary>
        /// Full constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="insurancenumber"></param>
        /// <param name="idprovider"></param>
        /// <param name="effectivedate"></param>
        /// <param name="expiredate"></param>
        /// <param name="active"></param>
        /// <param name="coverage"></param>
        /// <param name="price"></param>
        /// <param name="description"></param>
        public InsuranceVehicle(int id, int insurancenumber, Provider idprovider, DateTime effectivedate, DateTime expiredate, bool active, string coverage, float price, string description)
        {
            this.id = id;
            this.insurancenumber = insurancenumber;
            this.idprovider = idprovider;
            this.effectivedate = effectivedate;
            this.expiredate = expiredate;
            this.active = active;
            this.coverage = coverage;
            this.price = price;
            this.description = description;
        }

        /// <summary>
        /// Constructor without id
        /// </summary>
        /// <param name="insurancenumber"></param>
        /// <param name="idprovider"></param>
        /// <param name="effectivedate"></param>
        /// <param name="expiredate"></param>
        /// <param name="active"></param>
        /// <param name="coverage"></param>
        /// <param name="price"></param>
        /// <param name="description"></param>
        public InsuranceVehicle(int insurancenumber, Provider idprovider, DateTime effectivedate, DateTime expiredate, bool active, string coverage, float price, string description)
        {
            this.insurancenumber = insurancenumber;
            this.idprovider = idprovider;
            this.effectivedate = effectivedate;
            this.expiredate = expiredate;
            this.active = active;
            this.coverage = coverage;
            this.price = price;
            this.description = description;
        }
    }
}
