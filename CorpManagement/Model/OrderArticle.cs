using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorpManagement.Model
{
    class OrderArticle
    {
        public int idorder { get; set; }
        public User iduser { get; set; }
        public Article idarticle { get; set; }
        public int amount { get; set; }
        public DateTime orderdate { get; set; }
        public DateTime datereceived { get; set; }
        public OrderStatus status { get; set; }
        public string description { get; set; }
        public SizeClothing sizeclothing { get; set; }

        /// <summary>
        /// Constructor with id
        /// </summary>
        /// <param name="idorder"></param>
        /// <param name="iduser"></param>
        /// <param name="idarticle"></param>
        /// <param name="amount"></param>
        /// <param name="orderdate"></param>
        /// <param name="datereceived"></param>
        /// <param name="status"></param>
        public OrderArticle(int idorder, User iduser, Article idarticle, int amount, DateTime orderdate, DateTime datereceived, OrderStatus status, string description)
        {
            this.idorder = idorder;
            this.iduser = iduser;
            this.idarticle = idarticle;
            this.amount = amount;
            this.orderdate = orderdate;
            this.datereceived = datereceived;
            this.status = status;
            this.description = description;
        }

        public OrderArticle(int idorder, User iduser, Article idarticle, int amount, DateTime orderdate, DateTime datereceived, OrderStatus status, string description, SizeClothing sizeclothing) : this(idorder, iduser, idarticle, amount, orderdate, datereceived, status, description)
        {
            this.sizeclothing = sizeclothing;
        }

        /// <summary>
        /// Constructor without id for new entry
        /// </summary>
        /// <param name="iduser"></param>
        /// <param name="idarticle"></param>
        /// <param name="amount"></param>
        /// <param name="orderdate"></param>
        /// <param name="datereceived"></param>
        public OrderArticle(User iduser, Article idarticle, int amount, DateTime orderdate, DateTime datereceived, OrderStatus status, string description)
        {
            this.iduser = iduser;
            this.idarticle = idarticle;
            this.amount = amount;
            this.orderdate = orderdate;
            this.datereceived = datereceived;
            this.status = status;
            this.description = description;
        }

        public OrderArticle(User iduser, Article idarticle, int amount, DateTime orderdate, DateTime datereceived, OrderStatus status, string description, SizeClothing sizeclothing) : this(iduser, idarticle, amount, orderdate, datereceived, status, description)
        {
            this.sizeclothing = sizeclothing;
        }
    }
}
