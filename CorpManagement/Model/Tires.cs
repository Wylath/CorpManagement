using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorpManagement.Model
{
    class Tires
    {
        public int id { get; set; }
        public string name { get; set; }
        public StatusTire state { get; set; }
        public string description { get; set; }
        public int setnumber { get; set; }
        public string Dim1 { get; set; }
        public string Dim2 { get; set; }
        public string Dim3 { get; set; }

        /// <summary>
        /// Constructor with only id
        /// </summary>
        /// <param name="id"></param>
        public Tires(int id)
        {
            this.id = id;
        }

        /// <summary>
        /// Constructor with id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="state"></param>
        /// <param name="description"></param>
        /// <param name="setnumber"></param>
        /// <param name="dim1"></param>
        /// <param name="dim2"></param>
        /// <param name="dim3"></param>
        public Tires(int id, string name, StatusTire state, string description, int setnumber, string dim1, string dim2, string dim3)
        {
            this.id = id;
            this.name = name;
            this.state = state;
            this.description = description;
            this.setnumber = setnumber;
            Dim1 = dim1;
            Dim2 = dim2;
            Dim3 = dim3;
        }

        /// <summary>
        /// Constructor for new tire
        /// </summary>
        /// <param name="name"></param>
        /// <param name="state"></param>
        /// <param name="description"></param>
        /// <param name="setnumber"></param>
        /// <param name="dim1"></param>
        /// <param name="dim2"></param>
        /// <param name="dim3"></param>
        public Tires(string name, StatusTire state, string description, int setnumber, string dim1, string dim2, string dim3)
        {
            this.name = name;
            this.state = state;
            this.description = description;
            this.setnumber = setnumber;
            Dim1 = dim1;
            Dim2 = dim2;
            Dim3 = dim3;
        }
    }
}
