using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GhostHunter.Renderer
{
    internal class Display:FrameworkElement
    {
        Size area;
        public void SetupSizes(Size area)
        {
            this.area = area;
            this.InvalidateVisual();
        }

        public Brush StarterBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Maps","starter_background.png"),UriKind.RelativeOrAbsolute)));
            }
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if(area.Width > 0 && area.Height > 0)
            {
                drawingContext.DrawRectangle(StarterBrush, null, new Rect(0, 0, area.Width, area.Height));
            }
        }
    }
}
