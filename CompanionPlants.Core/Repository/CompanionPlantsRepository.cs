using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using CompanionPlants.Core.Model;

namespace CompanionPlants.Core.Repository
{
	public class CompanionPlantsRepository
	{
		public List<CompanionPlant> GetPlants()
		{
			var items = new List<CompanionPlant>();
			XDocument doc = XDocument.Load("http://zenfulneps.com/Data/CompanionPlants.xml");
			foreach (var plant in doc.Descendants("Plants"))
			{
				var item = new CompanionPlant();
				item.PlantId = (string)plant.Element("PlantID");
				item.Plant = (string)plant.Element("Plant");
				item.Companions = new List<string>();
                item.Companions = ((string)plant.Element("Companions"))
                    .Replace(System.Environment.NewLine, string.Empty).Replace("         ", "  ").Trim().Split(',').ToList();
                if (item.Companions.Count == 1 && string.IsNullOrEmpty(item.Companions[0]))
                {
                    item.Companions[0] = "None";
                }
                if (item.Companions.Count == 0)
                {
                    item.Companions.Add("None");
                }
                item.Incompatibles = new List<string>();
				if (!string.IsNullOrEmpty((string) plant.Element("Incompatible")))
				{
					item.Incompatibles = ((string)plant.Element("Incompatible"))
                        .Replace(System.Environment.NewLine, string.Empty).Replace("         ", "  ").Trim().Split(',').ToList();
                }
                if (item.Incompatibles.Count == 1 && string.IsNullOrEmpty(item.Incompatibles[0]))
                {
                    item.Incompatibles[0] = "None";
                }
                if (item.Incompatibles.Count == 0)
                {
                    item.Incompatibles.Add("None");
                }
                item.Benefits = string.Empty;
				if (!string.IsNullOrEmpty((string)plant.Element("Benefits")))
				{
                    item.Benefits = ((string)plant.Element("Benefits")).Trim()
                        .Replace(System.Environment.NewLine, string.Empty).Replace("         ", "  ");
				}
				item.Type = ((string)plant.Element("Type")).Trim();
				item.PlantPicture = ((string)plant.Element("PlantPic")).Trim();
				if (!string.IsNullOrEmpty((string)plant.Element("Stars")))
				{
					item.Rating = ((string)plant.Element("Stars")).Trim();
				}
				item.ScientificName = ((string)plant.Element("ScientificName")).Trim();
				items.Add(item);
			}
			return items.OrderBy(o => o.Plant).ToList();
		}

		public PlantDetails GetPlant(string id)
		{
            //var details = new PlantDetails();
            //details.P lant = GetPlants().FirstOrDefault(f => f.PlantId == id);
            //return details;

            List<CompanionPlant> plants = GetPlants();
            var plant = plants.FirstOrDefault(f => f.PlantId == id);
            var plantIndex = plants.FindIndex(f => f.PlantId == id);
            var prevIndex = plantIndex - 1;
            var nextIndex = plantIndex + 1;
            if (prevIndex >= 0)
            {
                plant.PrevPlant = plants[prevIndex].Plant;
                plant.PrevPlantId = plants[prevIndex].PlantId;
            }
            if (nextIndex < plants.Count)
            {
                plant.NextPlant = plants[nextIndex].Plant;
                plant.NextPlantId = plants[nextIndex].PlantId;
            }

            return new PlantDetails { Plant = plant };
        }
	}
}
