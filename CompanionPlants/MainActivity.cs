using Android.App;
using Android.Widget;
using Android.OS;

namespace CompanionPlants
{
	[Activity(Label = "Companion Plants", Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			SetContentView (Resource.Layout.Main);
		}
	}
}

