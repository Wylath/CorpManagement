using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorpManagement.Model
{
    class ProfileLevel
    {
        public int id { get; set; }
        public string name { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public ProfileLevel(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        public ProfileLevel(int id)
        {
            this.id = id;
        }
    }
}
