using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SinavOtomasyonuProjesi.Infrastructure
{
    public class PageData<T> : IEnumerable<T>
    {
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int TotalPages { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }

        public int NextPage
        {
            get
            {
                if (!HasNextPage)
                {
                    throw new NotImplementedException();
                }

                return Page + 1;
            }
        }

        public int PreviousPage
        {
            get
            {
                if (!HasPreviousPage)
                {
                    throw new NotImplementedException();
                }
                return Page - 1;
            }
        }

        public PageData(IEnumerable<T> currentItems, int totalCount, int page, int perPage)
        {
            _currentItems = currentItems;
            TotalCount = totalCount;
            PerPage = perPage;
            Page = page;

            TotalPages = (int)Math.Ceiling((float)TotalCount / perPage);

            HasNextPage = Page < TotalPages;
            HasPreviousPage = Page > 1;

        }

        public IEnumerable<T> _currentItems { get; set; }

        public IEnumerator<T> GetEnumerator()
        {
            return _currentItems.GetEnumerator();

        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}