using System;
using System.Collections.Generic;
using System.Text;

namespace refactor_extract_superClass
{
    public class CostRecord
    {
        public CostDetail Detail { get; set; }
        public IDictionary<string, double> CostContents { get; set; }
    }

    public class CostDetail
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public string Memo { get; set; }
    }
}
