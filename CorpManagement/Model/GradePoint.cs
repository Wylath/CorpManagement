using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorpManagement.Model
{
    class GradePoint
    {
        public int id { get; set; }
        public string name { get; set; }
        public int totalpoint { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="totalpoint"></param>
        public GradePoint(int id, string name, int totalpoint)
        {
            this.id = id;
            this.name = name;
            this.totalpoint = totalpoint;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        public GradePoint(int id)
        {
            this.id = id;
        }
    }
}
