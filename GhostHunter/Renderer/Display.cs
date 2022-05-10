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

        public Brush FirstBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Maps", "MAP_DESERT_BACKGROUND.png"), UriKind.RelativeOrAbsolute)));
            }
        }

        public Brush SecondBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Maps", "MAP_WINTER_BACKGROUND.png"), UriKind.RelativeOrAbsolute)));
            }
        }


        public Brush ThirdBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Maps", "MAP3_FOREST_BACKGROUND.png"), UriKind.RelativeOrAbsolute)));
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if(model!=null && size.Width > 50 && size.Height > 50)
            {
                double rectWidth = size.Width / model.GameMatrix.GetLength(1);
                double rectHeight = size.Height / model.GameMatrix.GetLength(0);
                if (model.Current.Contains("L00"))
                    drawingContext.DrawRectangle(StarterBrush, null, new Rect(0, 0, size.Width, size.Height));
                else if (model.Current.Contains("L01"))
                    drawingContext.DrawRectangle(FirstBrush, null, new Rect(0, 0, size.Width, size.Height));
                else if (model.Current.Contains("L02"))
                    drawingContext.DrawRectangle(SecondBrush, null, new Rect(0, 0, size.Width, size.Height));
                else if (model.Current.Contains("L03"))
                    drawingContext.DrawRectangle(ThirdBrush, null, new Rect(0, 0, size.Width, size.Height));

                //drawingContext.DrawRectangle(StarterBrush, null, new Rect(0, 0, size.Width, size.Height));

                foreach (var item in model.Arrows)
                {
                    drawingContext.PushTransform(new RotateTransform(item.Angle,item.Center.X, item.Center.Y));
                    drawingContext.DrawEllipse(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "arrow.png"), UriKind.RelativeOrAbsolute))), null, new Point(item.Center.X, item.Center.Y), 10, 5);
                    drawingContext.Pop();
                }

                foreach (var item in model.Enemy_Arrows)
                {
                    drawingContext.PushTransform(new RotateTransform(item.Angle, item.Center.X, item.Center.Y));
                    drawingContext.DrawEllipse(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "bullet.PNG"), UriKind.RelativeOrAbsolute))), null, new Point(item.Center.X, item.Center.Y), 10, 5);
                    drawingContext.Pop();
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
                                drawingContext.PushTransform(new ScaleTransform(model.Player.Scale, 1, model.Player.J * rectWidth + 30, model.Player.I * rectHeight + 30));
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "attacker_character.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 60, 60));
                                drawingContext.Pop();
                                break;
                            case MapItem.player2:
                                drawingContext.PushTransform(new ScaleTransform(model.Player.Scale, 1, model.Player.J * rectWidth + 30, model.Player.I * rectHeight + 30));
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "archer_character.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 60, 60));
                                drawingContext.Pop();
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
                            case MapItem.enemy2:
                                foreach (var item in model.Enemies)
                                {
                                    if(item is ArcherEnemy)
                                    {
                                        drawingContext.PushTransform(new ScaleTransform(item.Scale, 1, item.Enemy_j * rectWidth + 22, item.Enemy_i * rectHeight + 30));
                                        drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "archer_enemy.png"), UriKind.RelativeOrAbsolute)))
                                            , null, new Rect(j * rectWidth, i * rectHeight, 45, 60));
                                        drawingContext.Pop();
                                    }
                                }
                                break;
                            case MapItem.enemy:
                                foreach (var item in model.Enemies)
                                {
                                    if(item is AttackerEnemy)
                                    {
                                        drawingContext.PushTransform(new ScaleTransform(item.Scale, 1, item.Enemy_j * rectWidth + 22, item.Enemy_i * rectHeight + 30));
                                        drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "attacker_enemy.png"), UriKind.RelativeOrAbsolute)))
                                            , null, new Rect(j * rectWidth, i * rectHeight, 45, 60));
                                        drawingContext.Pop();
                                    }
                                }
                                break;
                            case MapItem.boss:
                                foreach (var item in model.Enemies)
                                {
                                    if (item is BossEnemy)
                                    {
                                        drawingContext.PushTransform(new ScaleTransform(item.Scale, 1, item.Enemy_j * rectWidth + 75, item.Enemy_i * rectHeight + 60));
                                        drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "boss3.png"), UriKind.RelativeOrAbsolute)))
                                            , null, new Rect(j * rectWidth, i * rectHeight, 150, 120));
                                        drawingContext.Pop();
                                    }
                                }
                                break;
                            case MapItem.ground:
                                break;
                            case MapItem.wintertop:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "MAP_WINTER_TOP.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 60, 70));
                                break;
                            case MapItem.winterbush:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "MAP_WINTER_BUSH.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 60, 70));
                                break;
                            case MapItem.winteriglo:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "MAP_WINTER_IGLO.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 35, 35));
                                break;
                            case MapItem.wintertree:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "MAP_WINTER-ELEMENT_0000s_0000_Réteg-5.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 100, 200));
                                break;

                            case MapItem.desertlake:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "MAP2_DESERT_LAKE.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 35, 35));
                                break;
                            case MapItem.desertcactus:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "MAP2_DESERT_CACTUS.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 100, 200));
                                break;
                            case MapItem.desertrock:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "MAP2_DESERT_ROCK.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 60, 70));
                                break;

                            case MapItem.woodsdeadtree:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "MAP3_FOREST_0001_TREE.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 100, 200));
                                break;
                            case MapItem.woodstree:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "MAP3_FOREST_0002_TREE2.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 60, 70));
                                break;
                            case MapItem.woodsnest:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "MAP3_FOREST_NEST.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 100, 200));
                                break;
                            case MapItem.woods1:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "WOODS1.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 60, 70));
                                break;
                            case MapItem.woods2:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "WOODS2.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 60, 70));
                                break;
                            case MapItem.woods3:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "WOODS3.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 60, 70));
                                break;
                            case MapItem.woods4:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "WOODS4.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 60, 70));
                                break;
                            case MapItem.snow1:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "SNOW1.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 35, 35));
                                break;
                            case MapItem.snow2:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "SNOW2.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 35, 35));
                                break;
                            case MapItem.snow3:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "SNOW3.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 60, 70));
                                break;
                            case MapItem.desert1:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "DESERT1.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 50, 50));
                                break;
                            case MapItem.desert2:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "DESERT2.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 35, 35));

                                break;
                            case MapItem.desert3:
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Elements", "DESERT3.png"), UriKind.RelativeOrAbsolute)))
                                    , null, new Rect(j * rectWidth, i * rectHeight, 50, 50));
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
