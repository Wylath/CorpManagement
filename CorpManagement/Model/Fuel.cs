using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorpManagement.Model
{
    class Fuel
    {
        public int id { get; set; }
        public string name { get; set; }

        /// <summary>
        /// Constructor with id only
        /// </summary>
        /// <param name="id"></param>
        public Fuel(int id)
        {
            this.id = id;
        }

        public Fuel(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}
