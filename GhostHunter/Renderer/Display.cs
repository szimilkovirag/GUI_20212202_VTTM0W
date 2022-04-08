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
        Size size;
        public void SetUpModel(IGameModel model)
        {
            this.model = model;
        }
        public void Resize(Size area)
        {
            this.size = area;
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
            if(model!=null && size.Width > 50 && size.Height > 50)
            {
                double rectWidth = size.Width / model.GameMatrix.GetLength(1);
                double rectHeight = size.Height / model.GameMatrix.GetLength(0);

                drawingContext.DrawRectangle(StarterBrush, null, new Rect(0, 0, size.Width, size.Height));

                foreach (var item in model.Arrows)
                {
                    //drawingContext.PushTransform(new RotateTransform(model.an));
                    drawingContext.DrawEllipse(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "arrow.png"), UriKind.RelativeOrAbsolute))), null, new Point(item.Center.X, item.Center.Y), 10, 5);
                }

                for (int i = 0; i < model.GameMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < model.GameMatrix.GetLength(1); j++)
                    {
                        switch (model.GameMatrix[i, j])
                        {
                            case MapItem.flower:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "flower.png"), UriKind.RelativeOrAbsolute)))
                                    ,null, new Rect(j * rectWidth, i * rectHeight, 35, 35));
                                break;
                            case MapItem.grass:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "grass_mushroom_tree.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 120, 90));
                                break;
                            case MapItem.player1:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "attacker_character.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 60, 60));
                                break;
                            case MapItem.player2:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "archer_character.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 60, 60));
                                break;
                            case MapItem.tree2:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "tree2.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 100, 150));
                                break;
                            case MapItem.trees:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "trees.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 200, 180));
                                break;
                            case MapItem.woods:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "woods.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 45, 50));
                                break;
                            case MapItem.winter:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "winter.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 45, 50));
                                break;
                            case MapItem.desert:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "desert.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 45, 50));
                                break;
                            case MapItem.rocks:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "grass_rocks.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 110, 65));
                                break;
                            case MapItem.mushroom:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "rocks_mushrooms.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 120, 90));
                                break;
                            case MapItem.tree1:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "tree1.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 35, 35));
                                break;
                            case MapItem.enemy:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "archer_enemy.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 45, 60));
                                break;
                            case MapItem.enemy2:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "attacker_enemy.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 45, 60));
                                break;
                            case MapItem.boss:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "boss3.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 150, 119));
                                break;
                            case MapItem.ground:
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
