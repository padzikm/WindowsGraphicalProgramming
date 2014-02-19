using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace zadaniewin3
{
    class RoundButton : Button
    {        
        public bool DomyslnyKsztalt { get; set; }
        public bool Odwrotnie { get; set; }

        public RoundButton()
        {                                                                    
            DomyslnyKsztalt = true;
            Odwrotnie = true;
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            if (DomyslnyKsztalt)
            {
                GraphicsPath buttonPath = new GraphicsPath();
                Rectangle newRectangle = this.ClientRectangle;
                newRectangle.Inflate(-1, -1);
                GraphicsPath GrPath = new GraphicsPath();
                GrPath.AddEllipse(newRectangle);
                this.Region = new Region(GrPath);
            }
            else if (Odwrotnie)
            {
                int x, y;
                GraphicsPath GrPath = new GraphicsPath();
                GraphicsPath GrPathExclude1 = new GraphicsPath();
                GraphicsPath GrPath2 = new GraphicsPath();
                GraphicsPath GrPathExclude2 = new GraphicsPath();
                GraphicsPath GrPath3 = new GraphicsPath();
                GraphicsPath GrPathExclude3 = new GraphicsPath();
                Rectangle newRectangle = this.ClientRectangle;
                newRectangle.Inflate(-1, -1);                        
                Rectangle rect1 = new Rectangle();
                Rectangle rect2 = new Rectangle();
                Rectangle rect3 = new Rectangle();
                Rectangle rect4 = new Rectangle();

                GrPath.AddEllipse(newRectangle);

                newRectangle.Inflate(-2, -2);

                GrPathExclude1.AddArc(newRectangle, 90, 180);

                rect1.X = newRectangle.X + (newRectangle.Width / 4);
                rect1.Y = newRectangle.Y;
                rect1.Width = newRectangle.Width / 2;
                rect1.Height = newRectangle.Height / 2;
                GrPath2.AddEllipse(rect1);

                rect2.X = newRectangle.X + (newRectangle.Width / 4);
                rect2.Y = newRectangle.Y + (newRectangle.Height / 2);
                rect2.Width = newRectangle.Width / 2;
                rect2.Height = newRectangle.Height / 2;
                GrPathExclude2.AddEllipse(rect2);

                x = newRectangle.Width / 2;
                y = 3 * newRectangle.Height / 4;
                //rect4.X = rect2.X + (rect2.Width / 3);
                //rect4.Y = rect2.Y + (rect2.Height / 3);
                rect4.Width = rect2.Width / 4;
                rect4.Height = rect2.Height / 4;
                rect4.X = x - rect4.Width / 2;
                rect4.Y = y - rect4.Height / 2;
                GrPath3.AddEllipse(rect4);

                x = newRectangle.Width / 2;
                y = newRectangle.Height / 4;
                //rect3.X = rect1.X + (rect1.Width / 3);
                //rect3.Y = rect1.Y + (rect1.Height / 3);
                rect3.Width = rect1.Width / 4;
                rect3.Height = rect1.Height / 4;
                rect3.X = x - rect3.Width / 2;
                rect3.Y = y - rect3.Height / 2;
                GrPathExclude3.AddEllipse(rect3);

                Region region = new Region(GrPath);
                region.Exclude(GrPathExclude1);
                region.Union(GrPath2);
                region.Exclude(GrPathExclude2);
                region.Exclude(GrPathExclude3);
                region.Union(GrPath3);
                this.Region = region;
            }
            else
            {
                int x, y;
                GraphicsPath GrPath = new GraphicsPath();
                GraphicsPath GrPathExclude1 = new GraphicsPath();
                GraphicsPath GrPath2 = new GraphicsPath();
                GraphicsPath GrPathExclude2 = new GraphicsPath();
                GraphicsPath GrPath3 = new GraphicsPath();
                GraphicsPath GrPathExclude3 = new GraphicsPath();
                Rectangle newRectangle = this.ClientRectangle;
                newRectangle.Inflate(-1, -1);                        
                Rectangle rect1 = new Rectangle();
                Rectangle rect2 = new Rectangle();
                Rectangle rect3 = new Rectangle();
                Rectangle rect4 = new Rectangle();

                GrPath.AddEllipse(newRectangle);

                newRectangle.Inflate(-2, -2);

                GrPathExclude1.AddArc(newRectangle, 90, -180);

                rect1.X = newRectangle.X + (newRectangle.Width / 4);
                rect1.Y = newRectangle.Y;
                rect1.Width = newRectangle.Width / 2;
                rect1.Height = newRectangle.Height / 2;
                GrPath2.AddEllipse(rect1);

                rect2.X = newRectangle.X + (newRectangle.Width / 4);
                rect2.Y = newRectangle.Y + (newRectangle.Height / 2);
                rect2.Width = newRectangle.Width / 2;
                rect2.Height = newRectangle.Height / 2;
                GrPathExclude2.AddEllipse(rect2);

                x = newRectangle.Width / 2;
                y = 3 * newRectangle.Height / 4;
                //rect4.X = rect2.X + (rect2.Width / 3);
                //rect4.Y = rect2.Y + (rect2.Height / 3);
                rect4.Width = rect2.Width / 4;
                rect4.Height = rect2.Height / 4;
                rect4.X = x - rect4.Width / 2;
                rect4.Y = y - rect4.Height / 2;
                GrPath3.AddEllipse(rect4);

                x = newRectangle.Width / 2;
                y = newRectangle.Height / 4;
                //rect3.X = rect1.X + (rect1.Width / 3);
                //rect3.Y = rect1.Y + (rect1.Height / 3);
                rect3.Width = rect1.Width / 4;
                rect3.Height = rect1.Height / 4;
                rect3.X = x - rect3.Width / 2;
                rect3.Y = y - rect3.Height / 2;
                GrPathExclude3.AddEllipse(rect3);

                Region region = new Region(GrPath);
                region.Exclude(GrPathExclude1);
                region.Union(GrPath2);
                region.Exclude(GrPathExclude2);
                region.Exclude(GrPathExclude3);
                region.Union(GrPath3);
                this.Region = region;
            }
            base.OnPaint(e);
        }
    }
}
