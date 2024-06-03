using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorpManagement.Model
{
    class Servicing
    {
        public int id { get; set; }
        public Vehicle idvehicle { get; set; }
        public DateTime dateservicing { get; set; }
        public float price { get; set; }
        public Provider idprovider { get; set; }
        public string description { get; set; }
        public TypeServicing idtypeservicing { get; set; }
        public int km { get; set; }

        /// <summary>
        /// Constructor with id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idvehicle"></param>
        /// <param name="dateservicing"></param>
        /// <param name="price"></param>
        /// <param name="idprovider"></param>
        /// <param name="description"></param>
        /// <param name="idtypeservicing"></param>
        public Servicing(int id, Vehicle idvehicle, DateTime dateservicing, float price, Provider idprovider, string description, TypeServicing idtypeservicing)
        {
            this.id = id;
            this.idvehicle = idvehicle;
            this.dateservicing = dateservicing;
            this.price = price;
            this.idprovider = idprovider;
            this.description = description;
            this.idtypeservicing = idtypeservicing;
        }

        public Servicing(int id, Vehicle idvehicle, DateTime dateservicing, float price, Provider idprovider, string description, TypeServicing idtypeservicing, int km) : this(id, idvehicle, dateservicing, price, idprovider, description, idtypeservicing)
        {
            this.km = km;
        }

        /// <summary>
        /// Constructor for new servicing
        /// </summary>
        /// <param name="idvehicle"></param>
        /// <param name="dateservicing"></param>
        /// <param name="price"></param>
        /// <param name="idprovider"></param>
        /// <param name="description"></param>
        /// <param name="idtypeservicing"></param>
        public Servicing(Vehicle idvehicle, DateTime dateservicing, float price, Provider idprovider, string description, TypeServicing idtypeservicing)
        {
            this.idvehicle = idvehicle;
            this.dateservicing = dateservicing;
            this.price = price;
            this.idprovider = idprovider;
            this.description = description;
            this.idtypeservicing = idtypeservicing;
        }

        public Servicing(Vehicle idvehicle, DateTime dateservicing, float price, Provider idprovider, string description, TypeServicing idtypeservicing, int km) : this(idvehicle, dateservicing, price, idprovider, description, idtypeservicing)
        {
            this.km = km;
        }

        /// <summary>
        /// Construcotr for new servicing in db
        /// </summary>
        /// <param name="idvehicle"></param>
        /// <param name="dateservicing"></param>
        /// <param name="price"></param>
        /// <param name="idprovider"></param>
        /// <param name="description"></param>
        public Servicing(Vehicle idvehicle, DateTime dateservicing, float price, Provider idprovider, string description)
        {
            this.idvehicle = idvehicle;
            this.dateservicing = dateservicing;
            this.price = price;
            this.idprovider = idprovider;
            this.description = description;
        }

        /// <summary>
        /// Constructor for servicing in db
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idvehicle"></param>
        /// <param name="dateservicing"></param>
        /// <param name="price"></param>
        /// <param name="idprovider"></param>
        /// <param name="description"></param>
        public Servicing(int id, Vehicle idvehicle, DateTime dateservicing, float price, Provider idprovider, string description)
        {
            this.id = id;
            this.idvehicle = idvehicle;
            this.dateservicing = dateservicing;
            this.price = price;
            this.idprovider = idprovider;
            this.description = description;
        }
    }
}
