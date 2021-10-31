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
    }
}
