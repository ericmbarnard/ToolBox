using System;

namespace Architecture.Queries
{
    public abstract class Query<TResult> : Query
    {
        /// <summary>
        /// The Result of the Query
        /// </summary>
        public TResult Result { get; protected set; }
    }

    public abstract class Query
    {
        private int? _page;
        private int? _pageSize;

        /// <summary>
        /// The Column to Sort the Query by
        /// </summary>
        public virtual string SortBy { get; set; }

        /// <summary>
        /// The Sort Direction of the <see cref="SortBy"/> Column
        /// </summary>
        public virtual string SortDirection { get; set; }

        /// <summary>
        /// For Paging purposes, the number of records to skip past
        /// </summary>
        public int? Skip { get; set; }

        /// <summary>
        /// For Paging purposes, the number of records to return in the result
        /// </summary>
        public int? Take { get; set; }

        /// <summary>
        /// The Current Page of Data to Get (Page "1" is the First Page)
        /// </summary>
        public virtual int? Page
        {
            get
            {
                if (_page == null)
                {
                    CalculatePaging();
                }
                return _page;
            }
            set
            {
                _page = value;
            }
        }

        /// <summary>
        /// The Size of the Paged Data to Return
        /// </summary>
        public virtual int? PageSize
        {
            get
            {
                if (_pageSize == null)
                {
                    CalculatePaging();
                }
                return _pageSize;
            }
            set
            {
                _pageSize = value;
            }
        }

        /// <summary>
        /// Executes the Query
        /// </summary>
        public abstract void Execute();


        protected virtual void CalculatePaging()
        {
            int skip = Skip ?? 0;
            int take = Take ?? 250;

            take = (take == 0 ? 250 : take);

            int pageSize = take;

            double divPage = skip / pageSize;
            int page = Convert.ToInt32(Math.Ceiling(divPage));

            page = (page < 1 ? 1 : page);

            // assign them
            _page = page;
            _pageSize = pageSize;
        }
    }
}
