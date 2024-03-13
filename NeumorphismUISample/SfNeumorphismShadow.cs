using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeumorphismUISample
{
    public class SfNeumorphismDrawer : SfShadowDrawer
    {
        /// <summary>
        /// 
        /// </summary>
        public SfNeumorphismDrawer()
        {
            lightShadowColor = Colors.White;
            Blur = 10;
            Opacity = 0.2f;
            Offset = new SizeF(6, 6);
#if IOS||MACCATALYST
            Offset = new SizeF(3, 3);
            LightOffSet = new SizeF(-3, -3);
#endif
            ShadowColor = Colors.Black;
        }

        //Todo: need to fix offset value for ios

        private SizeF lightOffSet = new SizeF(-6, -6);
        /// <summary>
        /// 
        /// </summary>
        public SizeF LightOffSet
        {
            get { return lightOffSet; }
            set { lightOffSet = value; }
        }

        private float lightOpacity = 0.5f;
        /// <summary>
        /// 
        /// </summary>
        public float LightOpacity
        {
            get { return lightOpacity; }
            set { lightOpacity = value; }
        }

        private Color lightShadowColor;
        /// <summary>
        /// 
        /// </summary>
        public Color LightShadowColor
        {
            get { return lightShadowColor; }
            set { lightShadowColor = value; }
        }

        private bool isPressedState = false;
        /// <summary>
        /// 
        /// </summary>
        public bool IsPressedState
        {
            get { return isPressedState; }
            set
            {
                isPressedState = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="dirtyRect"></param>
        protected override void DrawShadow(ICanvas canvas, RectF dirtyRect)
        {


            if (IsPressedState)
            {
                double radius = CornerRadius > dirtyRect.Width / 2 ? dirtyRect.Width / 2 : CornerRadius;
                if (dirtyRect.Width / 3 < radius)
                {
                    ApplyShadow(canvas, new RectF(dirtyRect.Left, dirtyRect.Top, -dirtyRect.Width, dirtyRect.Height), Offset, ShadowColor, Opacity, radius);
                    ApplyShadow(canvas, new RectF(dirtyRect.Right, dirtyRect.Top, dirtyRect.Width, dirtyRect.Height), LightOffSet, lightShadowColor, LightOpacity, radius);
                }

                else
                {
                    ApplyShadow(canvas, new RectF(dirtyRect.Left, dirtyRect.Top, -dirtyRect.Width, dirtyRect.Height), Offset, ShadowColor, Opacity, radius);
                    ApplyShadow(canvas, new RectF(dirtyRect.Left - 10, dirtyRect.Top, dirtyRect.Width + 10, -dirtyRect.Height), Offset, ShadowColor, Opacity, radius);
                    ApplyShadow(canvas, new RectF(dirtyRect.Right, dirtyRect.Top, dirtyRect.Width, dirtyRect.Height), LightOffSet, lightShadowColor, LightOpacity, radius);
                    ApplyShadow(canvas, new RectF(dirtyRect.Left, dirtyRect.Bottom, dirtyRect.Width, dirtyRect.Height), LightOffSet, lightShadowColor, LightOpacity, radius);
                }
            }
            else
            {

                var paddingRect = new RectF() { Left = dirtyRect.Left + Padding, Top = dirtyRect.Top + Padding, Right = dirtyRect.Right - Padding, Bottom = dirtyRect.Bottom - Padding };
                double radius = CornerRadius > paddingRect.Width / 2 ? paddingRect.Width / 2 : CornerRadius;
                ApplyShadow(canvas, paddingRect, Offset, ShadowColor, Opacity, radius);
                ApplyShadow(canvas, paddingRect, LightOffSet, lightShadowColor, LightOpacity, radius);
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class SfShadowDrawer : IDrawable
    {
        private float padding = 10;
        /// <summary>
        /// 
        /// </summary>
        public float Padding
        {
            get { return padding; }
            set { padding = value; }
        }

        //Todo: need to fix offset value for ios

        private SizeF offset = new SizeF(10, 10);
        /// <summary>
        /// 
        /// </summary>
        public SizeF Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        private float blur = 4;
        /// <summary>
        /// 
        /// </summary>
        public float Blur
        {
            get { return blur; }
            set { blur = value; }
        }

        private Color shadowColor = Colors.Black;
        /// <summary>
        /// 
        /// </summary>
        public Color ShadowColor
        {
            get { return shadowColor; }
            set { shadowColor = value; }
        }

        private float opacity = 0.5f;
        /// <summary>
        /// 
        /// </summary>
        public float Opacity
        {
            get { return opacity; }
            set { opacity = value; }
        }

        private Color background = Colors.GhostWhite;
        /// <summary>
        /// 
        /// </summary>
        public Color BackgroundColor
        {
            get { return background; }
            set { background = value; }
        }

        private double cornerRadius = 5;
        /// <summary>
        /// 
        /// </summary>
        public double CornerRadius
        {
            get { return cornerRadius; }
            set { cornerRadius = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="dirtyRect"></param>
        protected virtual void DrawShadow(ICanvas canvas, RectF dirtyRect)
        {
            var paddingRect = new RectF() { Left = dirtyRect.Left + padding, Top = dirtyRect.Top + padding, Right = dirtyRect.Right - padding, Bottom = dirtyRect.Bottom - padding };
            ApplyShadow(canvas, paddingRect, Offset, ShadowColor, Opacity, CornerRadius);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="dirtyRect"></param>
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            double radius = CornerRadius > dirtyRect.Width / 2 ? dirtyRect.Width / 2 : CornerRadius;

            canvas.SaveState();
            canvas.ClipPath(GetDrawPath(dirtyRect, (float)radius));
            DrawShadow(canvas, dirtyRect);
            canvas.RestoreState();
        }

        internal void ApplyShadow(ICanvas canvas, RectF dirtyRect, SizeF offset, Color shadowColor, float opacity, double cornerRadius)
        {
            canvas.SaveState();
            canvas.FillColor = BackgroundColor;
            canvas.SetShadow(offset, Blur, shadowColor.WithAlpha(opacity));
            canvas.FillRoundedRectangle(dirtyRect, cornerRadius);
            canvas.RestoreState();
        }

        PathF GetDrawPath(RectF roundedRect, float roundCornerRadius)
        {
            PointCollection points = new PointCollection();
            var drawRadius = roundCornerRadius * (1 - 0.552228474);
            var rect = roundedRect;
            var x = rect.X;
            var y = rect.Y;
            var width = rect.Width;
            var height = rect.Height;
            points.Add(new Point(x + roundCornerRadius, y));
            points.Add(new Point(x + width - roundCornerRadius, y));
            points.Add(new Point(x + width - drawRadius, y)); //c1
            points.Add(new Point(x + width, y + drawRadius)); //c1
            points.Add(new Point(x + width, y + roundCornerRadius));
            points.Add(new Point(x + width, y + height - roundCornerRadius));
            points.Add(new Point(x + width, y + height - drawRadius));//c2
            points.Add(new Point(x + width - drawRadius, y + height));//c2
            points.Add(new Point(x + width - roundCornerRadius, y + height));
            points.Add(new Point(x + roundCornerRadius, y + height));
            points.Add(new Point(x + drawRadius, y + height));//c3
            points.Add(new Point(x, y + height - drawRadius));//c3
            points.Add(new Point(x, y + height - roundCornerRadius));
            points.Add(new Point(x, y + roundCornerRadius));
            points.Add(new Point(x, y + drawRadius));//c4
            points.Add(new Point(x + drawRadius, y));//c4
            points.Add(new Point(x + roundCornerRadius, y));
            var path = new PathF();

            if (points != null)
            {
                path.MoveTo(points[0]);
                path.LineTo(points[1]);
                path.CurveTo(points[2], points[3], points[4]);
                path.LineTo(points[5]);
                path.CurveTo(points[6], points[7], points[8]);
                path.LineTo(points[9]);
                path.CurveTo(points[10], points[11], points[12]);
                path.LineTo(points[13]);
                path.CurveTo(points[14], points[15], points[16]);
            }

            path.Close();
            return path;
        }
    }
}
