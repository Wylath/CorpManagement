using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorpManagement.DB
{
    abstract class FactoryDB <T>: DBConnect
    {
        /// <summary>
        /// Converty the SqlDataReader to class
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public abstract T ConvertSqlDataReaderToClass(SqlDataReader dr);

        /// <summary>
        /// Return all elements in database
        /// </summary>
        /// <returns></returns>
        public abstract List<T> SelectAllElement();

        /// <summary>
        /// Return element by id
        /// </summary>
        /// <returns></returns>
        public abstract T SelectElementById(int ElementId);

        /// <summary>
        /// Insert new element in database
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public abstract bool InsertNewElement(T value);

        /// <summary>
        /// Update element in database
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public abstract bool UpdateElement(T value);

        /// <summary>
        /// Delete element in database
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public abstract bool DeleteElement(T value);
    }
}
