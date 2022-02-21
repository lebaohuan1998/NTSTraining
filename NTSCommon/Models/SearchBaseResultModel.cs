using System;
using System.Collections.Generic;
using System.Text;

namespace NTSCommon.Models
{
   public class SearchBaseResultModel<T>
    {
        public int TotalItems { get; set; }
        public List<T> DataResults { get; set; }
    }
}
