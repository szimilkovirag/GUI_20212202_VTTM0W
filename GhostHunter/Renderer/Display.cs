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
        Size area;
        public void SetUpModel(IGameModel model)
        {
            this.model = model;
        }
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
            if(model!=null && area.Width > 0 && area.Height > 0)
            {
                drawingContext.DrawRectangle(StarterBrush, null, new Rect(0, 0, area.Width, area.Height));

                double rectWidth = ActualWidth / model.GameMatrix.GetLength(0);
                double rectHeight = ActualHeight / model.GameMatrix.GetLength(0);

                for (int i = 0; i < model.GameMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < model.GameMatrix.GetLength(1); j++)
                    {
                        switch (model.GameMatrix[i, j])
                        {
                            case GhostHunterLogic.MapItem.flower:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "flower.png"), UriKind.RelativeOrAbsolute)))
                                    ,null, new Rect(j * rectWidth, i * rectHeight, 35, 35));
                                break;
                            case GhostHunterLogic.MapItem.grass:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "grass_mushroom_tree.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 120, 90));
                                break;
                            case GhostHunterLogic.MapItem.player:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "cat.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 40, 40));
                                break;
                            case GhostHunterLogic.MapItem.tree2:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "tree2.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 100, 150));
                                break;
                            case GhostHunterLogic.MapItem.trees:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "trees.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 200, 180));
                                break;
                            case GhostHunterLogic.MapItem.woods:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "woods.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 45, 50));
                                break;
                            case GhostHunterLogic.MapItem.winter:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "winter.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 45, 50));
                                break;
                            case GhostHunterLogic.MapItem.desert:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "desert.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 45, 50));
                                break;
                            case GhostHunterLogic.MapItem.rocks:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "grass_rocks.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 110, 65));
                                break;
                            case GhostHunterLogic.MapItem.mushroom:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "rocks_mushrooms.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 120, 90));
                                break;
                            case GhostHunterLogic.MapItem.tree1:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "tree1.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 35, 35));
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
