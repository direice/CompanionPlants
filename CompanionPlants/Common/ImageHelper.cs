using System.Net;
using Android.Graphics;

namespace CompanionPlants.Common
{
	public class ImageHelper
	{
        public static string ImageLocation { get { return "http://www.zenfulneps.com/images/plants/"; } }
        public static string MobileImageLocation { get { return "http://www.zenfulneps.com/images/plants/Mobile_Images/"; } }
        public static string MobileImageTinyLocation { get { return "http://www.zenfulneps.com/images/plants/Mobile_Images/Tiny/"; } }
        
        public static Bitmap GetImageBitmapFromURL(string url)
		{
			Bitmap imageBitmap = null;
			using (var webClient = new WebClient())
			{
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
			}
			return imageBitmap;
		}
	}
}