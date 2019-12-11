using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using App5.Annotations;
using Xamarin.Forms;

namespace App5
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand rattingBarCommand { get; set; }
        public ICommand clickCommand { get; set; }
        public MainPageViewModel()
        {
            rattingBarCommand = new Command(onItemTapped); // this will execute on tap of image (star)
            clickCommand = new Command(onClicked); // this will execute on the click of Click me button.
        }

        void onClicked(object obj)
        {
            RatingBar b = (RatingBar)obj;
            App.Current.MainPage.DisplayAlert("Selected Value is", b.SelectedStarValue.ToString(), "OK");
        }


        private string _selectedStar;


        public string SelectedStar
        {
            get { return _selectedStar; }
            set { _selectedStar = value; OnPropertyChanged(); }
        }



        private void onItemTapped(object obj)
        {
            var obje = obj;
            SelectedStar = "Selected Star is " + obj;
        }
    }
}
