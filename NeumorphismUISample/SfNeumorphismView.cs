﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeumorphismUISample
{
    public class SfNeumorphismView : ContentView
    {
        private readonly Grid grid;

        private readonly GraphicsView graphicsView;
        /// <summary>
        /// 
        /// </summary>
        public SfNeumorphismView()
        {
            Drawable = new SfNeumorphismDrawer();

            grid = new Grid();
            grid.Margin = new Thickness(0);

            graphicsView = new GraphicsView();
            graphicsView.Margin = new Thickness(0);
            graphicsView.BackgroundColor = Colors.Transparent;
            graphicsView.SetBinding(GraphicsView.DrawableProperty, new Binding() { Path = nameof(Drawable), Source = this });

            grid.Children.Add(graphicsView);
            base.Content = grid;
        }

        /// <summary>
        /// 
        /// </summary>
        public SfNeumorphismDrawer Drawable
        {
            get { return (SfNeumorphismDrawer)GetValue(DrawableProperty); }
            set { SetValue(DrawableProperty, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public static readonly BindableProperty DrawableProperty =
            BindableProperty.Create(nameof(Drawable), typeof(SfNeumorphismDrawer), typeof(SfNeumorphismView), defaultValue: null, propertyChanged: OnDrawablePropertyChanged);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        protected static void OnDrawablePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        public new View Content
        {
            get { return (View)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public static readonly new BindableProperty ContentProperty =
            BindableProperty.Create(nameof(Content), typeof(View), typeof(SfNeumorphismView), defaultValue: null, propertyChanged: OnContentPropertyChanged);
        /// <summary>
        /// 
        /// </summary>
        public void Invalidate()
        {
            graphicsView.Invalidate();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        protected static void OnContentPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            SfNeumorphismView view = (SfNeumorphismView)bindable;
            if (newValue is View newView)
            {
                if (!view.grid.Children.Contains(newView))
                {
                    view.grid.Children.Add(newView);
                }
            }

            if (oldValue is View oldView)
            {
                if (view.grid.Children.Contains(oldView))
                {
                    view.grid.Children.Remove(oldView);
                }
            }
        }
    }
}
