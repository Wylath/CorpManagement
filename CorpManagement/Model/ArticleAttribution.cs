using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorpManagement.Model
{
    class ArticleAttribution
    {
        public int id { get; set; }
        public User iduser { get; set; }
        public Article idarticle { get; set; }
        public string serialnumber { get; set; }
        public string specialnumber { get; set; }
        public string description { get; set; }
        public StateArticleAttribution state { get; set; }

        /// <summary>
        /// Constructor with id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="iduser"></param>
        /// <param name="idarticle"></param>
        /// <param name="serialnumber"></param>
        /// <param name="specialnumber"></param>
        /// <param name="description"></param>
        /// <param name="state"></param>
        public ArticleAttribution(int id, User iduser, Article idarticle, string serialnumber, string specialnumber, string description, StateArticleAttribution state)
        {
            this.id = id;
            this.iduser = iduser;
            this.idarticle = idarticle;
            this.serialnumber = serialnumber;
            this.specialnumber = specialnumber;
            this.description = description;
            this.state = state;
        }

        /// <summary>
        /// Constructor without id
        /// </summary>
        /// <param name="iduser"></param>
        /// <param name="idarticle"></param>
        /// <param name="serialnumber"></param>
        /// <param name="specialnumber"></param>
        /// <param name="description"></param>
        /// <param name="state"></param>
        public ArticleAttribution(User iduser, Article idarticle, string serialnumber, string specialnumber, string description, StateArticleAttribution state)
        {
            this.iduser = iduser;
            this.idarticle = idarticle;
            this.serialnumber = serialnumber;
            this.specialnumber = specialnumber;
            this.description = description;
            this.state = state;
        }
    }
}
