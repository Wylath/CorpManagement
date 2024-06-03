using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorpManagement.Model
{
    class Provider
    {
        public int id { get; set; }
        public string name { get; set; }
        public int phone { get; set; }
        public string mail { get; set; }
        public string street { get; set; }
        public string housenumber { get; set; }
        public string postalcode { get; set; }
        public string town { get; set; }
        public string description { get; set; }
        public ProviderType idtype { get; set; }

        /// <summary>
        /// Constructor with only id
        /// </summary>
        /// <param name="id"></param>
        public Provider(int id)
        {
            this.id = id;
        }

        /// <summary>
        /// Constructor with id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="mail"></param>
        /// <param name="street"></param>
        /// <param name="housenumber"></param>
        /// <param name="postalcode"></param>
        /// <param name="town"></param>
        /// <param name="description"></param>
        /// <param name="idtype"></param>
        public Provider(int id, string name, int phone, string mail, string street, string housenumber, string postalcode, string town, string description, ProviderType idtype)
        {
            this.id = id;
            this.name = name;
            this.phone = phone;
            this.mail = mail;
            this.street = street;
            this.housenumber = housenumber;
            this.postalcode = postalcode;
            this.town = town;
            this.description = description;
            this.idtype = idtype;
        }

        /// <summary>
        /// Constructor for new provider
        /// </summary>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="mail"></param>
        /// <param name="street"></param>
        /// <param name="housenumber"></param>
        /// <param name="postalcode"></param>
        /// <param name="town"></param>
        /// <param name="description"></param>
        /// <param name="idtype"></param>
        public Provider(string name, int phone, string mail, string street, string housenumber, string postalcode, string town, string description, ProviderType idtype)
        {
            this.name = name;
            this.phone = phone;
            this.mail = mail;
            this.street = street;
            this.housenumber = housenumber;
            this.postalcode = postalcode;
            this.town = town;
            this.description = description;
            this.idtype = idtype;
        }

        /// <summary>
        /// Constructor with only id and name
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public Provider(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="idtype"></param>
        public Provider(int id, string name, string description, ProviderType idtype) : this(id, name)
        {
            this.description = description;
            this.idtype = idtype;
        }
    }
}
