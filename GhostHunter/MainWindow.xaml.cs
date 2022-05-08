using GhostHunter.Controller;
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
        //DispatcherTimer dtarrow;
        public MainWindow()
        {
            InitializeComponent();
            logic = new GhostHunterLogic();
            display.SetUpModel(logic);
            controller = new GameController(logic);

            //dt = new DispatcherTimer();
            //dt.Interval = TimeSpan.FromMilliseconds(100);
            //dt.Tick += Dt_Tick;
            //dt.Start();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromMilliseconds(200);
            dt.Tick += Dt_Tick;
            dt.Start();

            //dtarrow = new DispatcherTimer();
            //dtarrow.Interval = TimeSpan.FromMilliseconds(70);
            //dtarrow.Tick += Dtarrow_Tick;
            //dtarrow.Start();

            display.Resize(new Size(grid.ActualWidth, grid.ActualHeight));
            logic.GameOver += Logic_GameOver;
            logic.Resize(new Size(grid.ActualWidth, grid.ActualHeight));
            display.InvalidateVisual();
        }

        private void Logic_GameOver(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Game over!");
            if (result == MessageBoxResult.OK)
            {
                this.Close();
            }
        }

        //private void Dtarrow_Tick(object sender, EventArgs e)
        //{
        //    logic.MoveArrow();
        //    display.InvalidateVisual();
        //}

        private void Dt_Tick(object sender, EventArgs e)
        {
            logic.MoveItems();
            display.InvalidateVisual();
            //logic.Dead();
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            display.Resize(new Size(grid.ActualWidth, grid.ActualHeight));
            logic.Resize(new Size(grid.ActualWidth, grid.ActualHeight));
            display.InvalidateVisual();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            controller.KeyPressed(e.Key);
            display.InvalidateVisual();
        }
    }
}
