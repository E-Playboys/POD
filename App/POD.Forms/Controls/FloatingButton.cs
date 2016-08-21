using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace POD.Forms.Controls
{
    /// <summary>
    /// Used only in Android, to present the floating button in the list view.
    /// </summary>
    public class FloatingButton : View
    {
        public static readonly BindableProperty ImageNameProperty = BindableProperty.Create<FloatingButton, string>(p => p.ImageName, string.Empty);
        public string ImageName
        {
            get { return (string)GetValue(ImageNameProperty); }
            set { SetValue(ImageNameProperty, value); }
        }

        public static readonly BindableProperty ColorNormalProperty = BindableProperty.Create<FloatingButton, Color>(p => p.ColorNormal, Color.White);
        public Color ColorNormal
        {
            get { return (Color)GetValue(ColorNormalProperty); }
            set { SetValue(ColorNormalProperty, value); }
        }

        public static readonly BindableProperty ColorRippleProperty = BindableProperty.Create<FloatingButton, Color>(p => p.ColorRipple, Color.White);
        public Color ColorRipple
        {
            get { return (Color)GetValue(ColorRippleProperty); }
            set { SetValue(ColorRippleProperty, value); }
        }

        public static readonly BindableProperty SizeProperty = BindableProperty.Create<FloatingButton, FloatingButtonSize>(p => p.Size, FloatingButtonSize.Normal);
        public FloatingButtonSize Size
        {
            get { return (FloatingButtonSize)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }

        public Action Show { get; set; }

        public Action Hide { get; set; }

        public Action<object, EventArgs> Clicked { get; set; }
    }

    public enum FloatingButtonSize
    {
        Normal,
        Mini
    }
}
