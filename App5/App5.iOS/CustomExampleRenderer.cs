using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App5;
using App5.iOS;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomExampleView), typeof(CustomExampleRenderer))]
namespace App5.iOS
{
    public class CustomExampleRenderer : ViewRenderer<CustomExampleView, UIView>
    {
        UIColor checkColor;
        UIColor  boxFillColor;
        UIColor boxBorderColor;
        UIFont labelFont;
        UIColor  labelTextColor;

        bool  isEnabled;
        bool  isChecked;
        bool  showTextLabel;
        //UIColor gold = UIColor.FromRGBA(1.00f, 0.95f, 0.57f, 1.00f);
        //UIColor brown = UIColor.FromRGBA(0.79f, 0.75f, 0.18f, 1.00f);
        //UIColor lightBrown = UIColor.FromRGBA(0.69f, 0.57f, 0.23f, 1.00f);
        //UIColor darkishBlue = UIColor.FromRGBA(0.20f, 0.39f, 0.98f, 1.00f);
        //UIColor bottomColorDown = UIColor.FromRGBA(0.21f, 0.21f, 0.21f, 1.00f);

        protected override void OnElementChanged(ElementChangedEventArgs<CustomExampleView> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                //UIBezierPath boxPath = new UIBezierPath();
                var boxPath = UIBezierPath.FromRect(new CGRect(2, 2, 2, 2));
                //UIImageView uiimage = new UIImageView();
                //UIBezierPath starPath = new UIBezierPath();
                //starPath.MoveTo(new CGPoint(31, 14.5f));
                //starPath.AddLineTo(new CGPoint(26.24f, 21.45f));
                //starPath.AddLineTo(new CGPoint(18.16f, 23.83f));
                //starPath.AddLineTo(new CGPoint(23.3f, 30.5f));
                //starPath.AddLineTo(new CGPoint(23.06f, 38.92f));
                //starPath.AddLineTo(new CGPoint(31, 36.1f));
                //starPath.AddLineTo(new CGPoint(38.94f, 38.92f));
                //starPath.AddLineTo(new CGPoint(38.7f, 30.5f));
                //starPath.AddLineTo(new CGPoint(43.84f, 23.83f));
                //starPath.AddLineTo(new CGPoint(35.76f, 21.45f));
                //starPath.ClosePath();
                //gold.SetFill();
                //starPath.Fill();

                //brown.SetStroke();
                //starPath.LineWidth = 1;
                //starPath.Stroke();

            }
        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            var shapeLayer = new CAShapeLayer();
            shapeLayer.FillColor = UIColor.Yellow.CGColor;
            shapeLayer.StrokeColor = UIColor.Brown.CGColor;
            shapeLayer.StrokeColor = UIColor.Brown.CGColor;
            //shapeLayer.LineWidth = 


            //this.Layer.AddSublayer(shapeLayer);
        }

        private UIImage UIBezierPathToImage(UIBezierPath path = null)
        {
            UIGraphics.BeginImageContextWithOptions(this.Bounds.Size, false, nfloat.Parse("0.0"));
            try
            {
                this.DrawViewHierarchy(Bounds,true);
                return UIGraphics.GetImageFromCurrentImageContext();
            }
            finally
            {
                UIGraphics.EndImageContext();
            }
        }
    }
}