using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleProject.Models.Enums;

namespace ConsoleProject.Models
{
    partial class Pharmacy
    {
        public override string ToString()
        {
            return $"{Name} | ID: {Id}";
        }

        public void AddDrugs(Drug drug)
        {
            drugs.Add(drug);
        }
        public void AddDrugType(DrugType type)
        {
            drugTypes.Add(type);
        }
        public IEnumerable<Drug> InfoDrug(string name)
        {
            var searchResult = drugs.FindAll(drug => drug.Name.ToLower().Contains(name.Trim().ToLower()));
            if (searchResult.Count==0)
            {
                yield break;
            }
            foreach (Drug drug in searchResult)
            {
                yield return drug;
            }
        }
        public IEnumerable<Drug> ShowDrugItems()
        {
            if (drugs.Count==0)
            {
                yield break;
            }

            foreach (Drug drug in drugs)
            {
                yield return drug;
            }
        }
        public IEnumerable<DrugType> ShowDrugTypes()
        {
            if (drugTypes.Count == 0)
            {
                yield break;
            }

            foreach (DrugType drugType in drugTypes)
            {
                yield return drugType;
            }
        }
        public bool IsDrugExist(string name)
        {
            return drugs.Any<Drug>(x => x.Name.ToLower() == name.Trim().ToLower());
        }
        public bool UpdateDrugCount(string name,Drug drug)
        {
            Drug resultDrug=drugs.Find(x => x.Name.ToLower() == name.Trim().ToLower());
            if (resultDrug==null)
            {
                return false;
            }
            if (drug.Price!=0)
            {
                resultDrug.Price = drug.Price;
                return true;
            }
            resultDrug.Count += drug.Count;
            return true;
        }
        public Drug FindDrug(Predicate<Drug> predicate)
        {
            return drugs.Find(predicate);
        }
        public int DrugsCount()
        {
            return drugs.Count;
        }
        public int DrugTypesCount()
        {
            return drugTypes.Count;
        }
        public bool IsDrugTypeExist(string typeName)
        {
            return drugTypes.Any<DrugType>(x => x.TypeName.ToLower() == typeName.Trim().ToLower());
        }
        public DrugType GetDrugType(Predicate<DrugType> predicate)
        {
            return drugTypes.Find(predicate);
        }
    }
}
