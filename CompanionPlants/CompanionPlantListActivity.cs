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
using CompanionPlants.Core.Repository;
using CompanionPlants.Core.Model;
using CompanionPlants.Adapters;
using Android.Support.V7.App;
using Android.Support.V4.View;

namespace CompanionPlants
{
    [Activity(Label = "Companion Planting Guide", MainLauncher = true, Icon = "@drawable/companionsmall", Theme = "@style/Theme.AppCompat.Light" /*Theme = "@style/CPTheme"*/)]
    public class CompanionPlantListActivity : AppCompatActivity //ActionBarActivity
    {
        //SearchView Vid: https://www.youtube.com/watch?v=4FvObC44bhM

        private List<CompanionPlant> _plants;
        private SearchView _searchView;
        private ListView _companionPlantListView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.CompanionPlantListView);

            var companionPlantsRepository = new CompanionPlantsRepository();
            _companionPlantListView = FindViewById<ListView>(Resource.Id.companionPlantListView);
            _plants = companionPlantsRepository.GetPlants();
            _companionPlantListView.Adapter = new CompanionPlantListAdapter(this, _plants);
            _companionPlantListView.FastScrollEnabled = true;
            _companionPlantListView.ItemClick += CompanionPlantRow_Click;

            //var searchButton = FindViewById<Button>(Resource.Id.searchButton);
            //searchButton.Click += SearchButton_Click;

        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            var searchText = string.Empty; //FindViewById<EditText>(Resource.Id.searchEditText).Text;
            var companionPlantListView = FindViewById<ListView>(Resource.Id.companionPlantListView);
            var companionPlantsRepository = new CompanionPlantsRepository();
            if (!string.IsNullOrEmpty(searchText))
            {
                _plants = companionPlantsRepository.GetPlants().Where(w => w.Plant.ToLower().Contains(searchText.ToLower())
                || w.Companions.Contains(searchText)).ToList<CompanionPlant>();
            }
            else
            {
                _plants = companionPlantsRepository.GetPlants();
            }
            companionPlantListView.Adapter = new CompanionPlantListAdapter(this, _plants);
        }

        private void CompanionPlantRow_Click(object sender, AdapterView.ItemClickEventArgs e)
        {
            var plant = _plants[e.Position];

            var intent = new Intent(this, typeof(CompanionPlantDetailActivity));
            intent.PutExtra("plantId", plant.PlantId);
            StartActivityForResult(intent, 100);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.main, menu);
            var item = menu.FindItem(Resource.Id.action_Search);
            var searchView = MenuItemCompat.GetActionView(item);
            var searchText = string.Empty;
            _searchView = searchView.JavaCast<SearchView>();

            //_searchView.QueryTextSubmit += (s, e) =>
            //{
            //    Toast.MakeText(this, "Search for: " + e.Query, ToastLength.Short).Show();
            //    _searchValue = e.Query;
            //    e.Handled = true;
            //};

            _searchView.QueryTextSubmit += SearchViewButton_Click;
            _searchView.QueryTextChange += QueryTextChange_Event;

            return true;
            //return base.OnCreateOptionsMenu(menu);
        }

        private void QueryTextChange_Event(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewText))
            {
                var companionPlantListView = FindViewById<ListView>(Resource.Id.companionPlantListView);
                var companionPlantsRepository = new CompanionPlantsRepository();
                _plants = companionPlantsRepository.GetPlants();
                companionPlantListView.Adapter = new CompanionPlantListAdapter(this, _plants);
            }
        }

        private void SearchViewButton_Click(object sender, SearchView.QueryTextSubmitEventArgs e)
        {
            var searchText = e.Query;
            var companionPlantListView = FindViewById<ListView>(Resource.Id.companionPlantListView);
            var companionPlantsRepository = new CompanionPlantsRepository();
            if (!string.IsNullOrEmpty(searchText))
            {
                _plants = companionPlantsRepository.GetPlants().Where(w => w.Plant.ToLower().Contains(searchText.ToLower())
                || w.Companions.Contains(searchText)).ToList<CompanionPlant>();
            }
            else
            {
                _plants = companionPlantsRepository.GetPlants();
            }
            companionPlantListView.Adapter = new CompanionPlantListAdapter(this, _plants);
            e.Handled = true;
        }

    }
}