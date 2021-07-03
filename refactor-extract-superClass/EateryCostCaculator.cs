using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace refactor_extract_superClass
{
    public class EateryCostCaculator
    {
        private string Name { get; set; }
        private int MonthlyRent { get; set; }

        private List<double> MonthlyFlourImportCost { get; set; }
        private List<double> MonthlyVegetableImportCost { get; set; }
        private List<double> MonthlyPorkImportCost { get; set; }

        public List<Employee> Employees { get; set; }

        private int[] MonthlyGasCost { get; set; }

        private int[] MonthlyElectricityCost { get; set; }


        public List<CostRecord> CreateAnnualyCostList()
        {
            var result = new List<CostRecord>();
            result.Add(CreateEmployeeCostList());
            result.Add(CreateFoodingredientsCostList());
            result.Add(CreateItemDepreciationCostList());
            result.Add(CreateElectricityCost());
            return result;
        }

        
        public CostRecord CreateEmployeeCostList()
        {
            var costContents = new Dictionary<string, double>();
            var anuualSalary = GetAnnualSalaryCost();
            var annualLaborInsurance = GetAnnalLaborInsurance();

            costContents.Add(anuualSalary.Key, anuualSalary.Value);
            costContents.Add(annualLaborInsurance.Key, annualLaborInsurance.Value);
            return new CostRecord()
            {
                Detail = new CostDetail()
                {
                    Title = $"Store Name：{Name}",
                    Description = $"Employee cost",
                    Memo = string.Empty
                },
                CostContents = costContents
            };
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

        public CostRecord CreateItemDepreciationCostList()
        {
            var itemDepreciations = new Dictionary<string, double>();
            itemDepreciations.Add("lamp update cost", 4000);
            itemDepreciations.Add("decoration update cost", 5000);
            itemDepreciations.Add("kitchenware update cost", 10000);

            return new CostRecord()
            {
                Detail = new CostDetail()
                {
                    Title = $"kitchenware and utility cost",
                    Description = string.Empty,
                    Memo = string.Empty
                },
                CostContents = itemDepreciations
            };
        }

        public CostRecord CreateElectricityCost()
        {
            var electricityCosts = new Dictionary<string, double>();
            electricityCosts.Add("gas cost",450);
            electricityCosts.Add("general Electricity", 10500);
            return new CostRecord()
            {
                Detail = new CostDetail()
                {
                    Title = $"Electricity Cost",
                    Description = "include the cost of aircondition and refrigerator",
                    Memo = string.Empty
                },
                CostContents = electricityCosts
            };
        }

        private KeyValuePair<string,double> GetAnnualSalaryCost()
        {
            var totalSalary = Employees.Select(o => o.MonthSalary).Sum() * 12;
            return new KeyValuePair<string, double>("AnnualSalary cost", totalSalary);
        }

        private KeyValuePair<string,double> GetAnnalLaborInsurance()
        {
            double totalLaborInsurance = 0;
            var salaries = Employees.Select(o => o.MonthSalary).ToList();
            Employees.ForEach(o =>
                totalLaborInsurance += o.MonthSalary * o.YearOfService * 0.00775 + 3000
            );
            return new KeyValuePair<string, double>("Annual LaborInsurance Cost", totalLaborInsurance);
        }
    }
}
