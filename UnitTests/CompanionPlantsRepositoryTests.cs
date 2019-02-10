using System;
using System.Collections.Generic;
using CompanionPlants.Core.Model;
using CompanionPlants.Core.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
	[TestClass]
	public class CompanionPlantsRepositoryTests
	{
		private CompanionPlantsRepository _companionPlantsRepository;
		private List<CompanionPlant> _plants;

		[TestInitialize]
		public void Initialize()
		{
			_companionPlantsRepository = new CompanionPlantsRepository();
			_plants = _companionPlantsRepository.GetPlants();
		}

		[TestMethod]
		public void TestMethod1()
		{
			Assert.IsTrue(_plants.Count > 1);
		}
	}
}
