using Android.Content;
using Android.Support.Design.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

namespace App5.Droid
{
    public class MyTabbedPageRenderer : TabbedPageRenderer, TabLayout.IOnTabSelectedListener
    {

        private TabbedPage tabbed;

        public MyTabbedPageRenderer(Context ctx) : base(ctx)
        {


        }
        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                tabbed = (TabbedPage)e.NewElement;
            }
            else
            {
                tabbed = (TabbedPage)e.OldElement;
            }

        }
        async void TabLayout.IOnTabSelectedListener.OnTabReselected(TabLayout.Tab tab)
        {
            await tabbed.CurrentPage.Navigation.PopToRootAsync();
        }

    }
}