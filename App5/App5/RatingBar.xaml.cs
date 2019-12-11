using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App5
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RatingBar : ContentView
    {
        public event EventHandler ItemTapped = delegate { };
        public static int selectedStarValue = 0;
        private static string emptyStarImage = string.Empty;
        private static string fillStarImage = string.Empty;
        private static Image star1;
        private static Image star2;
        private static Image star3;
        private static Image star4;
        private static Image star5;

        public RatingBar()
        {
            InitializeComponent();
            star1 = new Image();
            star2 = new Image();
            star3 = new Image();
            star4 = new Image();
            star5 = new Image();



            #region adding Gesture Recognizer on Star(Image Control)

            star1.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = ItemTappedCommand,
                CommandParameter = 1
            });

            star2.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = ItemTappedCommand,
                CommandParameter = 2
            });

            star3.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = ItemTappedCommand,
                CommandParameter = 3
            });

            star4.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = ItemTappedCommand,
                CommandParameter = 4
            });

            star5.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = ItemTappedCommand,
                CommandParameter = 5
            });

            #endregion

            stkRattingbar.Children.Add(star1);
            stkRattingbar.Children.Add(star2);
            stkRattingbar.Children.Add(star3);
            stkRattingbar.Children.Add(star4);
            stkRattingbar.Children.Add(star5);


            // here i added pan gesture recognizer for selecting the star 
            var panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += (s, e) =>
            {
                // Handle the pan
                var obj = e;
                double width = star1.Width;
                if (obj.TotalX > 0)
                {
                    fillStar(1);
                    Command?.Execute(1);
                }

                if (obj.TotalX > width)
                {
                    fillStar(2);
                    Command?.Execute(2);
                }

                if (obj.TotalX > (width * 2))
                {
                    fillStar(3);
                    Command?.Execute(3);

                }

                if (obj.TotalX > (width * 3))
                {
                    fillStar(4);
                    Command?.Execute(4);

                }

                if (obj.TotalX > (width * 4))
                {
                    fillStar(5);
                    Command?.Execute(5);
                }
            };
            stkRattingbar.GestureRecognizers.Add(panGesture);
        }

        // this event will fire when you click on image(star)
        private ICommand ItemTappedCommand
        {
            get
            {
                return new Command((Object obj) =>
                {
                    CommandParameter = obj;
                    Command?.Execute(CommandParameter);
                });
            }
        }

        #region  Image Height Width Property

        public static readonly BindableProperty ImageHeightProperty = BindableProperty.Create(
            propertyName: "ImageHeight",
            returnType: typeof(double),
            declaringType: typeof(RatingBar),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: ImageHeightPropertyChanged
        );

        public double ImageHeight
        {
            get { return (double) base.GetValue(ImageHeightProperty); }
            set { base.SetValue(ImageHeightProperty, value); }
        }

        private static void ImageHeightPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            // set all images height  equal
            star1.HeightRequest = (double) newValue;
            star2.HeightRequest = (double) newValue;
            star3.HeightRequest = (double) newValue;
            star4.HeightRequest = (double) newValue;
            star5.HeightRequest = (double) newValue;
        }


        //image width
        public static readonly BindableProperty ImageWidthProperty = BindableProperty.Create(
            propertyName: "ImageWidth",
            returnType: typeof(double),
            declaringType: typeof(RatingBar),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: ImageWidthPropertyChanged
        );

        public double ImageWidth
        {
            get { return (double) base.GetValue(ImageWidthProperty); }
            set { base.SetValue(ImageWidthProperty, value); }
        }

        private static void ImageWidthPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            // set all images width  equal
            star1.WidthRequest = (double) newValue;
            star2.WidthRequest = (double) newValue;
            star3.WidthRequest = (double) newValue;
            star4.WidthRequest = (double) newValue;
            star5.WidthRequest = (double) newValue;
        }

        #endregion



        #region Horizontal Vertical Allignment 

        public static readonly BindableProperty HorizontalOptionsProperty = BindableProperty.Create(
            propertyName: "HorizontalOptions",
            returnType: typeof(LayoutOptions),
            declaringType: typeof(RatingBar),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: HorizontalOptionsPropertyChanged
        );

        public LayoutOptions HorizontalOptions
        {
            get { return (LayoutOptions) base.GetValue(HorizontalOptionsProperty); }
            set { base.SetValue(HorizontalOptionsProperty, value); }
        }

        private static void HorizontalOptionsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (RatingBar) bindable;
            control.stkRattingbar.HorizontalOptions = (LayoutOptions) newValue;
        }

        //VERTICLE option set

        public static readonly BindableProperty VerticalOptionsProperty = BindableProperty.Create(
            propertyName: "VerticalOptions",
            returnType: typeof(LayoutOptions),
            declaringType: typeof(RatingBar),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: VerticalOptionsPropertyChanged
        );

        public LayoutOptions VerticalOptions
        {
            get { return (LayoutOptions) base.GetValue(VerticalOptionsProperty); }
            set { base.SetValue(VerticalOptionsProperty, value); }
        }

        private static void VerticalOptionsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (RatingBar) bindable;
            control.stkRattingbar.VerticalOptions = (LayoutOptions) newValue;
        }

        #endregion

        #region Command binding property

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            propertyName: "Command",
            returnType: typeof(ICommand),
            declaringType: typeof(RatingBar)
        );

        public ICommand Command
        {
            get { return (ICommand) base.GetValue(CommandProperty); }
            set { base.SetValue(CommandProperty, value); }
        }

        //  this property is private becuase i don't wanna access it globally
        private static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
            propertyName: "CommandParameter",
            returnType: typeof(object),
            declaringType: typeof(ImageButton),
            propertyChanged: CommandParameterPropertyChanged
        );

        private object CommandParameter
        {
            get { return base.GetValue(CommandParameterProperty); }
            set { base.SetValue(CommandParameterProperty, value); }
        }

        // on the change of command parameter replace empty star image with fillstar image
        private static void CommandParameterPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var selectedValue = (int) newValue;
            fillStar(selectedValue);

        }

        #endregion


        // this function will replace empty star with fill star
        private static void fillStar(int selectedValue)
        {
            selectedStarValue = selectedValue;
            switch (selectedValue)
            {
                case 1:
                    star1.Source = fillStarImage;
                    star2.Source = emptyStarImage;
                    star3.Source = emptyStarImage;
                    star4.Source = emptyStarImage;
                    star5.Source = emptyStarImage;
                    break;
                case 2:
                    star1.Source = fillStarImage;
                    star2.Source = fillStarImage;
                    star3.Source = emptyStarImage;
                    star4.Source = emptyStarImage;
                    star5.Source = emptyStarImage;
                    break;
                case 3:
                    star1.Source = fillStarImage;
                    star2.Source = fillStarImage;
                    star3.Source = fillStarImage;
                    star4.Source = emptyStarImage;
                    star5.Source = emptyStarImage;
                    break;
                case 4:
                    star1.Source = fillStarImage;
                    star2.Source = fillStarImage;
                    star3.Source = fillStarImage;
                    star4.Source = fillStarImage;
                    star5.Source = emptyStarImage;
                    break;
                case 5:
                    star1.Source = fillStarImage;
                    star2.Source = fillStarImage;
                    star3.Source = fillStarImage;
                    star4.Source = fillStarImage;
                    star5.Source = fillStarImage;
                    break;
            }
        }


        #region EmptyStar and fillstar property
        public static readonly BindableProperty EmptyStarImageProperty = BindableProperty.Create(
            propertyName: "EmptyStarImage",
            returnType: typeof(string),
            declaringType: typeof(RatingBar),
            defaultValue: "",
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: EmptyStarImagePropertyChanged
         );

        public string EmptyStarImage
        {
            get { return (string)base.GetValue(EmptyStarImageProperty); }
            set { base.SetValue(EmptyStarImageProperty, value); }
        }

        private static void EmptyStarImagePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (RatingBar)bindable;
            emptyStarImage = (string)newValue;
            //default set to all images as emptyStar
            star1.Source = emptyStarImage;
            star2.Source = emptyStarImage;
            star3.Source = emptyStarImage;
            star4.Source = emptyStarImage;
            star5.Source = emptyStarImage;

            // when default SelectedStarValue is assign  first and fillstariamge or emptystart image assign later then on the Property Change of
            //SelectedStar fillStartImage and emptyStart Image show emty string so to handle this  here i write this logic
            // < customcontrol:RattingBar x:Name = "customRattingBar"  ImageWidth = "25" ImageHeight = "25" SelectedStarValue = "1"    FillStarImage = "fillstar" EmptyStarImage = "emptystar" />
            if (!string.IsNullOrEmpty(fillStarImage))
            {
                fillStar(selectedStarValue);
            }
        }


        public static readonly BindableProperty FillStarImageProperty = BindableProperty.Create(
          propertyName: "FillStarImage",
          returnType: typeof(string),
          declaringType: typeof(RatingBar),
          defaultValue: "",
          defaultBindingMode: BindingMode.TwoWay,
          propertyChanged: FillStarImagePropertyChanged
       );

        public string FillStarImage
        {
            get { return (string)base.GetValue(FillStarImageProperty); }
            set { base.SetValue(FillStarImageProperty, value); }
        }

        private static void FillStarImagePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (RatingBar)bindable;
            fillStarImage = (string)newValue;

            // when default SelectedStarValue is assign  first and fillstariamge or emptystart image assign later then on the Property Change of
            //SelectedStar fillStartImage and emptyStar Image show emty string so to handle this  here i write this logic
            // < customcontrol:RattingBar x:Name = "customRattingBar"  ImageWidth = "25" ImageHeight = "25" SelectedStarValue = "1"    FillStarImage = "fillstar" EmptyStarImage = "emptystar" />
            if (!string.IsNullOrEmpty(emptyStarImage))
            {
                fillStar(selectedStarValue);
            }
        }
        #endregion


        #region this will return the selected star value and also you can set the default selected star
        public static readonly BindableProperty SelectedStarValueProperty = BindableProperty.Create(
          propertyName: "SelectedStarValue",
          returnType: typeof(int),
          declaringType: typeof(RatingBar),
          defaultValue: 0,
          defaultBindingMode: BindingMode.TwoWay,
          propertyChanged: SelectedStarValuePropertyChanged
       );

        private static void SelectedStarValuePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (RatingBar)bindable;
            selectedStarValue = (int)newValue;

            // if the SelectedStarValue is assign first and later fillStarImage & EmptyStar assign   
            if (!string.IsNullOrEmpty(fillStarImage) && !string.IsNullOrEmpty(emptyStarImage))
            {
                fillStar(selectedStarValue);
            }
        }

        public int SelectedStarValue
        {
            get { return selectedStarValue; }
            set { selectedStarValue = value; }
        }
        #endregion

    }
}
    