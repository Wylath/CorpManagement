using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorpManagement.Model
{
    class Article
    {
        public int id { get; set; }
        public string name { get; set; }
        public string refArticle { get; set; }
        public Provider idprovider { get; set; }
        public float price { get; set; }
        public int amount { get; set; }
        public int maxquantity { get; set; }
        public ArticleType idtype { get; set; }
        public string description { get; set; }
        public int credit { get; set; }

        /// <summary>
        /// Constructor with id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="refArticle"></param>
        /// <param name="idprovider"></param>
        /// <param name="price"></param>
        /// <param name="amount"></param>
        /// <param name="maxquantity"></param>
        /// <param name="idtype"></param>
        /// <param name="description"></param>
        /// <param name="credit"></param>
        public Article(int id, string name, string refArticle, Provider idprovider, float price, int amount, int maxquantity, ArticleType idtype, string description, int credit)
        {
            this.id = id;
            this.name = name;
            this.refArticle = refArticle;
            this.idprovider = idprovider;
            this.price = price;
            this.amount = amount;
            this.maxquantity = maxquantity;
            this.idtype = idtype;
            this.description = description;
            this.credit = credit;
        }

        /// <summary>
        /// Constructor for new article
        /// </summary>
        /// <param name="name"></param>
        /// <param name="refArticle"></param>
        /// <param name="idprovider"></param>
        /// <param name="price"></param>
        /// <param name="amount"></param>
        /// <param name="maxquantity"></param>
        /// <param name="idtype"></param>
        /// <param name="description"></param>
        /// <param name="credit"></param>
        public Article(string name, string refArticle, Provider idprovider, float price, int amount, int maxquantity, ArticleType idtype, string description, int credit)
        {
            this.name = name;
            this.refArticle = refArticle;
            this.idprovider = idprovider;
            this.price = price;
            this.amount = amount;
            this.maxquantity = maxquantity;
            this.idtype = idtype;
            this.description = description;
            this.credit = credit;
        }

        /// <summary>
        /// Constructor with only id
        /// </summary>
        /// <param name="id"></param>
        public Article(int id)
        {
            this.id = id;
        }
    }
}
