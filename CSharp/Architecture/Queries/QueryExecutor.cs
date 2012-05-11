using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Architecture.Queries
{
    public class QueryExecutor
    {
        public T Query<T>(Query<T> qry)
        {
            if (qry == null)
                throw new ArgumentNullException("qry");

            qry.Execute();

            return qry.Result;
        }
    }
}
