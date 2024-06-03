using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorpManagement.Model
{
    class StatusVehicle
    {
        public int id { get; set; }
        public string name { get; set; }

        /// <summary>
        /// Constructor with id only
        /// </summary>
        /// <param name="id"></param>
        public StatusVehicle(int id)
        {
            this.id = id;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public StatusVehicle(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}
