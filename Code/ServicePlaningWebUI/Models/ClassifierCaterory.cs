using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using ServicePlaningWebUI.Objects;

namespace ServicePlaningWebUI.Models
{
    public class ClassifierCaterory:DbModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Descr { get; set; }
        public int Complexity { get; set; }

        private void FillSelf(ClassifierCaterory model)
        {
            Id = model.Id;
            Name = model.Name;
            Number = model.Number;
            Descr = model.Descr;
            Complexity = model.Complexity;
        }

        public static IEnumerable<ClassifierCaterory> GetLowerList()
        {
            Uri uri = new Uri(String.Format("{0}/Classifier/GetCategoryLowerList", OdataServiceUri));
            string jsonString = GetJson(uri);
            var model = JsonConvert.DeserializeObject<IEnumerable<ClassifierCaterory>>(jsonString);
            return model;
        }
    }
}