using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorpManagement.Model
{
    class SizeClothing
    {
        public int id { get; set; }
        public string size { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="idarticle"></param>
        /// <param name="size"></param>
        public SizeClothing(int id, string size)
        {
            this.id = id;
            this.size = size;
        }
    }
}
