using System;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using App5.Annotations;
using Plugin.Media.Abstractions;
using Stormlion.ImageCropper;
using Xamarin.Forms;

namespace App5
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        public int _selectedValue;

        public int SelectedValue
        {
            get { return _selectedValue; }
            set { _selectedValue = value; OnPropertyChanged(); }
        }
        public ICommand rattingBarCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainPage()
        {
            rattingBarCommand = new Xamarin.Forms.Command(ratingChanged);
            InitializeComponent();
            BindingContext = this;
            //BindingContext = new MainPageViewModel();
        }

        private void ratingChanged(object obj)
        {
            var opt = SelectedValue;
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
           // var opt2 = customRattingBar.SelectedStarValue;
           // var opt3 = SelectedValue;

            new ImageCropper()
            {
                
                AspectRatioX = 1,
                AspectRatioY = 1,
                CropShape = ImageCropper.CropShapeType.Rectangle,
                Success = (imageFile) =>
                {
                    var base64String = Convert.ToBase64String(File.ReadAllBytes(imageFile));
                    //using (Image image = Image.FromFile(Path))
                    //{
                    //    using (MemoryStream m = new MemoryStream())
                    //    {
                    //        image.Save(m, image.RawFormat);
                    //        byte[] imageBytes = m.ToArray();

                    //        // Convert byte[] to Base64 String
                    //        string base64String = Convert.ToBase64String(imageBytes);
                    //        return base64String;
                    //    }
                    //}
                    var opt = new MediaFile(imageFile,null,null);
                    //Device.BeginInvokeOnMainThread(() =>
                    //{
                    //    imageView.Source = ImageSource.FromFile(imageFile);
                    //    var opts = imageView.Source;
                    //});
                }
            }.Show(this);
            //new ImageCropper()
            //{
            //    PageTitle = "Test Title",
            //    AspectRatioX = 1,
            //    AspectRatioY = 1,
            //    CropShape = ImageCropper.CropShapeType.Rectangle,
            //    SelectSourceTitle = "Select source",
            //    TakePhotoTitle = "Take Photo",
            //    PhotoLibraryTitle = "Photo Library",
            //    CancelButtonTitle = "Cancel",
            //    Success = (imageFile) =>
            //    {
            //        Device.BeginInvokeOnMainThread(() =>
            //        {
            //            imageView.Source = ImageSource.FromFile(imageFile);
            //        });
            //    }
            //}.Show(this);
        }

        private async void Button_OnClicked2(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                if (button.StyleId.Equals("Register"))
                {
                    var opt = Application.Current.Properties["token"];
                    RegId.Text = await DependencyService.Get<ICrop>().SendRegistrationToken();
                }
                else
                {
                   var opt = await new HttpClient().PostAsync(
                        "http://fixlynotificationdemo.azurewebsites.net/Home/DeleteRegistration?id=" +
                        Application.Current.Properties["REGID"], null);
                }
            }
        }
    }
}
