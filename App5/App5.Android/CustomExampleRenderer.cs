using Android.Content;
using Android.Widget;
using App5;
using App5.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomExampleView), typeof(CustomExampleRenderer))]
namespace App5.Droid
{
    public class CustomExampleRenderer : ViewRenderer<CustomExampleView, EditText>
    {
        public CustomExampleRenderer(Context ctx) : base(ctx)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<CustomExampleView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                EditText txtView = new EditText(Android.App.Application.Context)
                {
                    Text = "HELLO, Custom View"
                };
                SetNativeControl(txtView);
            }
        }
    }
}