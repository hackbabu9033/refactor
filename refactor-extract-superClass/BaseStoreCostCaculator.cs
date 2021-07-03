using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace refactor_extract_superClass
{
    public abstract class BaseStoreCostCaculator
    {
        protected string Name { get; set; }

        protected List<Employee> Employees { get; set; }

        public virtual List<CostRecord> CreateAnnualyCostList()
        {
            var result = new List<CostRecord>();
            result.Add(CreateEmployeeCostList());
            result.Add(CreateItemDepreciationCostList());
            result.Add(CreateElectricityCost());
            return result;
        }

        public virtual CostRecord CreateEmployeeCostList()
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

        public virtual CostRecord CreateItemDepreciationCostList()
        {
            var itemDepreciations = new Dictionary<string, double>();
            itemDepreciations.Add("lamp update cost", 4000);
            itemDepreciations.Add("decoration update cost", 5000);

            return new CostRecord()
            {
                Detail = new CostDetail()
                {
                    Title = $"Item depreciation cost",
                    Description = string.Empty,
                    Memo = string.Empty
                },
                CostContents = itemDepreciations
            };
        }

        public virtual CostRecord CreateElectricityCost()
        {
            var electricityCosts = new Dictionary<string, double>();
            electricityCosts.Add("general Electricity", 10500);
            return new CostRecord()
            {
                Detail = new CostDetail()
                {
                    Title = $"Electricity Cost",
                    Description = string.Empty,
                    Memo = string.Empty
                },
                CostContents = electricityCosts
            };
        }

        private KeyValuePair<string, double> GetAnnualSalaryCost()
        {
            var totalSalary = Employees.Select(o => o.MonthSalary).Sum() * 12;
            return new KeyValuePair<string, double>("AnnualSalary cost", totalSalary);
        }

        private KeyValuePair<string, double> GetAnnalLaborInsurance()
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
