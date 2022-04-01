using GhostHunter.Logic;
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
        IGameModel model;
        public void SetUpModel(IGameModel model)
        {
            this.model = model;
        }

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

            if(model!=null && area.Width > 0 && area.Height > 0)
            {
                double rectWidth = ActualWidth / model.GameMatrix.GetLength(1);
                double rectHeight = ActualHeight / model.GameMatrix.GetLength(0);

                for (int i = 0; i < model.GameMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < model.GameMatrix.GetLength(1); j++)
                    {
                        switch (model.GameMatrix[i, j])
                        {
                            case GhostHunterLogic.MapItem.flower:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "flower.png"), UriKind.RelativeOrAbsolute)))
                                    , new Pen(Brushes.Black, 0), new Rect(i * rectWidth, j * rectHeight, 25, 30));
                                break;
                            case GhostHunterLogic.MapItem.grass:
                                break;
                            case GhostHunterLogic.MapItem.player:
                                break;
                            case GhostHunterLogic.MapItem.tree2:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "tree2.png"), UriKind.RelativeOrAbsolute)))
                                    , new Pen(Brushes.Black, 0), new Rect(i * rectWidth, j * rectHeight, 100, 150));
                                break;
                            case GhostHunterLogic.MapItem.trees:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "trees.png"), UriKind.RelativeOrAbsolute)))
                                    , new Pen(Brushes.Black, 0), new Rect(i * rectWidth, j * rectHeight, 200, 180));
                                break;
                            case GhostHunterLogic.MapItem.signboard:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "signboard.png"), UriKind.RelativeOrAbsolute)))
                                    , new Pen(Brushes.Black, 0), new Rect(i * rectWidth, j * rectHeight, 45, 50));
                                break;
                            case GhostHunterLogic.MapItem.ground:
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
    }
}
