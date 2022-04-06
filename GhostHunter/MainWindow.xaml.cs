﻿using GhostHunter.Controller;
using GhostHunter.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace GhostHunter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GhostHunterLogic logic;
        GameController controller;
        DispatcherTimer dt;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            logic = new GhostHunterLogic();
            display.SetupSizes(new Size(grid.ActualWidth,grid.ActualHeight));
            display.SetUpModel(logic);
            controller = new GameController(logic);
            display.InvalidateVisual();
            dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromMilliseconds(200);
            dt.Tick += (sender, eargs) =>
            {
                foreach (var item in logic.Enemies)
                {
                    item.MoveEnemy();
                    display.InvalidateVisual();
                }
            };
            dt.Start();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            display.SetupSizes(new Size(grid.ActualWidth, grid.ActualHeight));
            display.InvalidateVisual();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            controller.KeyPressed(e.Key);
            display.InvalidateVisual();
        }
    }
}
