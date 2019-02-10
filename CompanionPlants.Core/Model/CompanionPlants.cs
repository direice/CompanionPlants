using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanionPlants.Core.Model
{
	public class CompanionPlants
	{
		public List<CompanionPlant> Plants { get; set; }
	}

	public class CompanionPlant
	{
		public string PlantId { get; set; }
		public string Plant { get; set; }
		public List<string> Companions { get; set; }
		public List<string> Incompatibles { get; set; }
		public string Benefits { get; set; }
		public string Type { get; set; }
		public string Rating { get; set; }
		public string PlantPicture { get; set; }
		public string ScientificName { get; set; }
		public string PrevPlant { get; set; }
		public string PrevPlantId { get; set; }
		public string NextPlant { get; set; }
		public string NextPlantId { get; set; }
	}

	public class PlantDetails
	{
		public CompanionPlant Plant { get; set; }
		public List<CompanionPlant> Plants { get; set; }
	}
}
