using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE_Layer.Helper
{
    public class Pagintation<T>
    {

        public Pagintation(List<T> data, int pageIndex = 0, int pageSize = 0, int count = 0)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Data = data;
            Count = count;
        }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
        public int Count { get; set; }

        public List<T> Data { get; set; }


    }
}
