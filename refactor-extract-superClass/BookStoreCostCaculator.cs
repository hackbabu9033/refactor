using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace refactor_extract_superClass
{
    public class BookStoreCostCaculator : BaseStoreCostCaculator
    {
        public override List<CostRecord> CreateAnnualyCostList()
        {
            var result = base.CreateAnnualyCostList();
            result.Add(CreateBookImportAndroyaltyCostList());
            return result;
        }

        public CostRecord CreateBookImportAndroyaltyCostList()
        {
            var result = new Dictionary<string, double>();

            result.Add("schoolbook import cost", 10000);
            result.Add("novel import cost", 20000);
            result.Add("book royalty cost", 50000);

            return new CostRecord()
            {
                Detail = new CostDetail()
                {
                    Title = $"book import and royalty cost",
                    Description = string.Empty,
                    Memo = string.Empty
                },
                CostContents = result
            };

        }

        public override CostRecord CreateItemDepreciationCostList()
        {
            var itemDepreciations = new Dictionary<string, double>();
            var baseItemDepreciationCost = base.CreateItemDepreciationCostList();
            itemDepreciations.Add("lamp update cost", 4000);
            itemDepreciations.Add("decoration update cost", 5000);
            baseItemDepreciationCost.CostContents.Add("warehouse maintance cost", 4000);
            baseItemDepreciationCost.Detail.Title = $"store and wareHouse cost";
            return baseItemDepreciationCost;
        }
    }
}
