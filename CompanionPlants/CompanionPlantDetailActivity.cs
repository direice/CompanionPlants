using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CompanionPlants.Core.Model;
using CompanionPlants.Core.Repository;

namespace CompanionPlants
{
	[Activity(Label = "Companion Plant Detail", Icon = "@drawable/companionsmall", Theme = "@style/CPTheme")]
	public class CompanionPlantDetailActivity : Activity
	{
        private ImageView _plantImageView;
        private TextView _plantNameTextView;
        private TextView _plantscientificNameTextView;
        private TextView _companionsTextView;
        private TextView _incompatiblesTextView;
        private TextView _benefitsTextView;
        private TextView _typeTextView;
        private Button _prevButton;
        private Button _nextButton;
        private Button _homeButton;

        private PlantDetails _plant;

        protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.CompanionPlantDetailView);

            var plantRepo = new CompanionPlantsRepository();

            var plantId = Intent.Extras.GetString("plantId");
            _plant = plantRepo.GetPlant(plantId);

            FindViews();
            BindData(_plant.Plant);
            HandleEvents();
        }

        private void HandleEvents()
        {
            _nextButton.Click += NextButton_Click;
            _prevButton.Click += PrevButton_Click;
            _homeButton.Click += HomeButton_Click;
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(CompanionPlantDetailActivity));
            intent.PutExtra("plantId", _plant.Plant.NextPlantId);
            StartActivityForResult(intent, 100);
        }

        private void PrevButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(CompanionPlantDetailActivity));
            intent.PutExtra("plantId", _plant.Plant.PrevPlantId);
            StartActivityForResult(intent, 100);
        }

        private void HomeButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(CompanionPlantListActivity));
            StartActivityForResult(intent, 200);
        }

        private void FindViews()
        {
            _plantImageView = FindViewById<ImageView>(Resource.Id.plantImageView);
            _plantNameTextView = FindViewById<TextView>(Resource.Id.plantNameTextView);
            _plantscientificNameTextView = FindViewById<TextView>(Resource.Id.plantscientificNameTextView);
            _companionsTextView = FindViewById<TextView>(Resource.Id.companionsTextView);
            _incompatiblesTextView = FindViewById<TextView>(Resource.Id.incompatiblesTextView);
            _benefitsTextView = FindViewById<TextView>(Resource.Id.benefitsTextView);
            _typeTextView = FindViewById<TextView>(Resource.Id.typeTextView);
            _prevButton = FindViewById<Button>(Resource.Id.previousButton);
            _nextButton = FindViewById<Button>(Resource.Id.nextButton);
            _homeButton = FindViewById<Button>(Resource.Id.homeButton);
        }

        private void BindData(CompanionPlant plant)
        {
            var imageBitmap = Common.ImageHelper.GetImageBitmapFromURL(Common.ImageHelper.ImageLocation + plant.PlantPicture);
            _plantImageView.SetImageBitmap(imageBitmap);
            _plantNameTextView.Text = plant.Plant;
            _plantscientificNameTextView.Text = plant.ScientificName;
            _companionsTextView.Text = string.Join(",", plant.Companions);
            _incompatiblesTextView.Text = string.Join(",", plant.Incompatibles);
            _benefitsTextView.Text = string.Join(",", plant.Benefits);
            _typeTextView.Text = string.Join(",", plant.Type);
            if (string.IsNullOrEmpty(plant.PrevPlant))
            {
                _prevButton.Visibility = ViewStates.Invisible;
            }
            if (string.IsNullOrEmpty(plant.NextPlant))
            {
                _nextButton.Visibility = ViewStates.Invisible;
            }
        }

    }
}