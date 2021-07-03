using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace refactor_extract_superClass
{
    public class EateryCostCaculator : BaseStoreCostCaculator
    {
        private List<double> MonthlyFlourImportCost { get; set; }
        private List<double> MonthlyVegetableImportCost { get; set; }
        private List<double> MonthlyPorkImportCost { get; set; }

        public override List<CostRecord> CreateAnnualyCostList()
        {
            var result = base.CreateAnnualyCostList();
            result.Add(CreateFoodingredientsCostList());
            return result;
        }

        public CostRecord CreateFoodingredientsCostList()
        {
            var materialImportResult = new Dictionary<string, double>();
            var flourCost = MonthlyFlourImportCost.Sum();
            var porkCost = MonthlyPorkImportCost.Sum();
            var vegetable = MonthlyVegetableImportCost.Sum();

            materialImportResult.Add("Flour import cost", flourCost);
            materialImportResult.Add("Pork import cost", porkCost);
            materialImportResult.Add("Vegetable import cost", vegetable);

            return new CostRecord()
            {
                Detail = new CostDetail()
                {
                    Title = $"food ingredient import cost",
                    Description = string.Empty,
                    Memo = string.Empty
                },
                CostContents = materialImportResult
            };
                
        }

        public override CostRecord CreateItemDepreciationCostList()
        {
            var baseItemDepreciations = base.CreateItemDepreciationCostList();
            baseItemDepreciations.CostContents.Add("kitchenware update cost", 10000);

            return baseItemDepreciations;
        }

        public override CostRecord CreateElectricityCost()
        {
            var baseElectricity = base.CreateElectricityCost();
            baseElectricity.CostContents.Add("gas cost",450);
            baseElectricity.Detail.Description = "include the cost of aircondition and refrigerato";
            return baseElectricity;
        }
    }
}
