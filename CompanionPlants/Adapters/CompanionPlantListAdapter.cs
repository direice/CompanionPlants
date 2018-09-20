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
using CompanionPlants.Common;

namespace CompanionPlants.Adapters
{
    public class CompanionPlantListAdapter : BaseAdapter<CompanionPlant>
    {
        private List<CompanionPlant> _plants;
        private Activity _context;

        public CompanionPlantListAdapter(Activity context, List<CompanionPlant> plants) : base()
        {
            this._context = context;
            this._plants = plants;
        }

        public override CompanionPlant this[int position]
        {
            get { return _plants[position]; }
        }

        public override int Count
        {
            get { return _plants.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var plant = _plants[position];
            var imageBitMap = ImageHelper.GetImageBitmapFromURL(Common.ImageHelper.MobileImageTinyLocation + plant.PlantPicture);
            if (convertView == null)
            {
                convertView = _context.LayoutInflater.Inflate(Resource.Layout.CompanionPlantRowView, null);
            }
            convertView.FindViewById<TextView>(Resource.Id.plantNameTextView).Text = plant.Plant;
            convertView.FindViewById<ImageView>(Resource.Id.plantImageView).SetImageBitmap(imageBitMap);
            convertView.FindViewById<TextView>(Resource.Id.companionsTextView).Text = 
                convertView.Resources.GetString(Resource.String.boldCompanion) + string.Join(", ", plant.Companions);
            convertView.FindViewById<TextView>(Resource.Id.typeTextView).Text = plant.Type;
            return convertView;
        }
    }
}