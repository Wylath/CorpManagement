using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorpManagement.Model
{
    class PoliceLocality
    {
        public int id { get; set; }
        public string name { get; set; }

        /// <summary>
        /// Constructor with id only
        /// </summary>
        /// <param name="id"></param>
        public PoliceLocality(int id)
        {
            this.id = id;
        }

        public PoliceLocality(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        /// <summary>
        /// Constructor for new police locality
        /// </summary>
        /// <param name="name"></param>
        public PoliceLocality(string name)
        {
            this.name = name;
        }
    }
}
