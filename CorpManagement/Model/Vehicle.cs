using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorpManagement.Model
{
    class Vehicle
    {
        public int id { get; set; }
        public string name { get; set; }
        public string numberplate { get; set; }
        public PoliceLocality idpolicelocality { get; set; }
        public DateTime saledate { get; set; }
        public DateTime lastcontrol { get; set; }
        public int kmlastcontrol { get; set; }
        public DateTime nextcontrol { get; set; }
        public Tires idtires { get; set; }
        public Fuel fueltype { get; set; }
        public string vehicletype { get; set; }
        public StatusVehicle status { get; set; }
        public string description { get; set; }
        public InsuranceVehicle idinsurance { get; set; }

        /// <summary>
        /// Constructor with only id
        /// </summary>
        /// <param name="id"></param>
        public Vehicle(int id)
        {
            this.id = id;
        }

        /// <summary>
        /// Constructor with only id and name
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public Vehicle(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        /// <summary>
        /// Constructor for vehicle in db
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="numberplate"></param>
        /// <param name="idpolicelocality"></param>
        /// <param name="saledate"></param>
        /// <param name="lastcontrol"></param>
        /// <param name="kmlastcontrol"></param>
        /// <param name="nextcontrol"></param>
        /// <param name="setnumber"></param>
        /// <param name="fueltype"></param>
        /// <param name="vehicletype"></param>
        /// <param name="status"></param>
        /// <param name="description"></param>
        public Vehicle(int id, string name, string numberplate, PoliceLocality idpolicelocality, DateTime saledate, DateTime lastcontrol, int kmlastcontrol, DateTime nextcontrol, Tires idtires, Fuel fueltype, string vehicletype, StatusVehicle status, string description, InsuranceVehicle idinsurance)
        {
            this.id = id;
            this.name = name;
            this.numberplate = numberplate;
            this.idpolicelocality = idpolicelocality;
            this.saledate = saledate;
            this.lastcontrol = lastcontrol;
            this.kmlastcontrol = kmlastcontrol;
            this.nextcontrol = nextcontrol;
            this.idtires = idtires;
            this.fueltype = fueltype;
            this.vehicletype = vehicletype;
            this.status = status;
            this.description = description;
            this.idinsurance = idinsurance;
        }

        /// <summary>
        /// Constructor for new entry
        /// </summary>
        /// <param name="name"></param>
        /// <param name="numberplate"></param>
        /// <param name="idpolicelocality"></param>
        /// <param name="saledate"></param>
        /// <param name="lastcontrol"></param>
        /// <param name="kmlastcontrol"></param>
        /// <param name="nextcontrol"></param>
        /// <param name="setnumber"></param>
        /// <param name="fueltype"></param>
        /// <param name="vehicletype"></param>
        /// <param name="status"></param>
        /// <param name="description"></param>
        public Vehicle(string name, string numberplate, PoliceLocality idpolicelocality, DateTime saledate, DateTime lastcontrol, int kmlastcontrol, DateTime nextcontrol, Tires idtires, Fuel fueltype, string vehicletype, StatusVehicle status, string description, InsuranceVehicle idinsurance)
        {
            this.name = name;
            this.numberplate = numberplate;
            this.idpolicelocality = idpolicelocality;
            this.saledate = saledate;
            this.lastcontrol = lastcontrol;
            this.kmlastcontrol = kmlastcontrol;
            this.nextcontrol = nextcontrol;
            this.idtires = idtires;
            this.fueltype = fueltype;
            this.vehicletype = vehicletype;
            this.status = status;
            this.description = description;
            this.idinsurance = idinsurance;
        }
    }
}
