using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using POD.Droid.Renderers;
using POD.Forms.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(FloatingButton), typeof(FloatingButtonRenderer))]
namespace POD.Droid.Renderers
{
    public class FloatingButtonRenderer : ViewRenderer<FloatingButton, FrameLayout>
    {
        private const int MARGIN_DIPS = 16;
        private const int FAB_HEIGHT_NORMAL = 56;
        private const int FAB_HEIGHT_MINI = 40;
        private const int FAB_FRAME_HEIGHT_WITH_PADDING = (MARGIN_DIPS * 2) + FAB_HEIGHT_NORMAL;
        private const int FAB_FRAME_WIDTH_WITH_PADDING = (MARGIN_DIPS * 2) + FAB_HEIGHT_NORMAL;
        private const int FAB_MINI_FRAME_HEIGHT_WITH_PADDING = (MARGIN_DIPS * 2) + FAB_HEIGHT_MINI;
        private const int FAB_MINI_FRAME_WIDTH_WITH_PADDING = (MARGIN_DIPS * 2) + FAB_HEIGHT_MINI;
        private readonly Context _context;
        private readonly FloatingActionButton _button;

        public FloatingButtonRenderer()
        {
            _context = Xamarin.Forms.Forms.Context;

            var d = _context.Resources.DisplayMetrics.Density;
            var margin = (int)(MARGIN_DIPS * d); // margin in pixels

            _button = new FloatingActionButton(_context);
            var lp = new FrameLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent)
            {
                Gravity = GravityFlags.CenterVertical | GravityFlags.CenterHorizontal,
                LeftMargin = margin,
                TopMargin = margin,
                BottomMargin = margin,
                RightMargin = margin
            };
            _button.LayoutParameters = lp;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<FloatingButton> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || this.Element == null)
                return;

            if (e.OldElement != null)
                e.OldElement.PropertyChanged -= HandlePropertyChanged;

            if (this.Element != null)
            {
                //UpdateContent ();
                this.Element.PropertyChanged += HandlePropertyChanged;
            }

            Element.Show = Show;
            Element.Hide = Hide;

            SetImage(Element.ImageName);
            //SetSize(Element.Size);
            SetColorNormal(Element.ColorNormal);
            SetColorRipple(Element.ColorRipple);

            _button.Click += OnButtonClicked;

            var frame = new FrameLayout(_context);
            frame.RemoveAllViews();
            frame.AddView(_button);

            SetNativeControl(frame);
        }

        public void Show()
        {
            _button.Show();
        }

        public void Hide()
        {
            _button.Hide();
        }

        private void HandlePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Content")
            {
                Tracker.UpdateLayout();
            }
            else if (e.PropertyName == FloatingButton.ColorNormalProperty.PropertyName)
            {
                SetColorNormal(Element.ColorNormal);
            }
            else if (e.PropertyName == FloatingButton.ColorRippleProperty.PropertyName)
            {
                SetColorRipple(Element.ColorRipple);
            }
            else if (e.PropertyName == FloatingButton.ImageNameProperty.PropertyName)
            {
                SetImage(Element.ImageName);
            }
            else if (e.PropertyName == FloatingButton.SizeProperty.PropertyName)
            {
            //    SetSize(Element.Size);
            }
        }

        private void SetColorNormal(Color colorNormal)
        {
            _button.BackgroundTintList = ColorStateList.ValueOf(colorNormal.ToAndroid());
        }

        private void SetColorRipple(Color colorRipple)
        {
            _button.SetRippleColor(colorRipple.ToAndroid().ToArgb());
        }

        private void SetImage(string imageName)
        {
            if (!string.IsNullOrWhiteSpace(imageName))
            {
                try
                {
                    var drawableNameWithoutExtension = Path.GetFileNameWithoutExtension(imageName);
                    var resources = _context.Resources;
                    var imageResourceName = resources.GetIdentifier(drawableNameWithoutExtension, "drawable", _context.PackageName);
                    _button.SetImageBitmap(Android.Graphics.BitmapFactory.DecodeResource(_context.Resources, imageResourceName));
                }
                catch (Exception ex)
                {
                    throw new FileNotFoundException("There was no Android Drawable by that name.", ex);
                }
            }
        }

        //void SetSize(FloatingButtonSize size)
        //{
        //    if (size == FloatingButtonSize.Mini)
        //    {
        //        _button.Size = FabSize.Mini;
        //        Element.WidthRequest = FAB_MINI_FRAME_WIDTH_WITH_PADDING;
        //        Element.HeightRequest = FAB_MINI_FRAME_HEIGHT_WITH_PADDING;
        //    }
        //    else
        //    {
        //        _button.Size = FabSize.Normal;
        //        Element.WidthRequest = FAB_FRAME_WIDTH_WITH_PADDING;
        //        Element.HeightRequest = FAB_FRAME_HEIGHT_WITH_PADDING;
        //    }
        //}

        private void OnButtonClicked(object sender, EventArgs e)
        {
            var clicked = Element.Clicked;
            if (Element != null)
            {
                clicked?.Invoke(sender, e);
            }
        }
    }
}