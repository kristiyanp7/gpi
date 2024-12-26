using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Draw
{
    [Serializable]
    public class HeartShape : Shape
    {
        #region Constructor

        public HeartShape(RectangleF rect) : base(rect)
        {
        }

        public HeartShape(HeartShape heart) : base(heart)
        {
        }

        #endregion

        public override bool Contains(PointF point)
        {   
            float x = (point.X - Location.X) / Width;
            float y = (point.Y - Location.Y) / Height;

           
            float yShifted = y - 0.3f;
            float xSquared = x * x;

            bool heartShape = xSquared + yShifted * yShifted <= 0.16f || (xSquared * xSquared) + yShifted * yShifted <= 0.01f;

            bool rectangle = x >= -0.5f && x <= 0.5f && y >= -1 && y <= 0.1f;

            return heartShape || rectangle;
        }

        public override void DrawSelf(Graphics grfx)
        {
            base.DrawSelf(grfx);
            base.RotateShape(grfx);

            
            float controlPointOffsetX = Width * 0.4f;
            float controlPointOffsetY = Height * 0.4f;
            PointF bottomCenter = new PointF(Location.X + Width / 2, Location.Y + Height / 2 + controlPointOffsetY);
            PointF topLeft = new PointF(Location.X + Width / 2 - controlPointOffsetX, Location.Y + controlPointOffsetY);
            PointF topRight = new PointF(Location.X + Width / 2 + controlPointOffsetX, Location.Y + controlPointOffsetY);

          
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(Location.X + Width / 2 - controlPointOffsetX, Location.Y + controlPointOffsetY / 2, controlPointOffsetX , controlPointOffsetY, -180, 180);
            path.AddArc(Location.X + Width / 2, Location.Y + controlPointOffsetY / 2, controlPointOffsetX, controlPointOffsetY , -180, 180);
            path.AddLine(topRight, bottomCenter);
            path.AddLine(bottomCenter, topLeft);
            path.CloseFigure();

          
            grfx.FillPath(new SolidBrush(Color.FromArgb(Transparency, FillColor)), path);           
            grfx.DrawPath(new Pen(Color.FromArgb(Transparency, BorderColor), BorderSize), path);
        }
    }
}
