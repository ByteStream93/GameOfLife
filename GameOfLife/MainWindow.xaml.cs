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

namespace GameOfLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartLife(object sender, RoutedEventArgs e)
        {
            int rowsAmount = 47;
            int columnsAmount = 47;

            for (int row = 0; row < rowsAmount; row++)
            {
                for (int column = 0; column < columnsAmount; column++)
                {
                    Rectangle rectangle = new Rectangle();
                    rectangle.Width = LifeTable.ActualWidth / rowsAmount;
                    rectangle.Height = LifeTable.ActualHeight / columnsAmount;
                    rectangle.Fill = Brushes.Black;
                    LifeTable.Children.Add(rectangle);
                    Canvas.SetLeft(rectangle, column * rectangle.Width);
                    Canvas.SetTop(rectangle, row * rectangle.Height);
                    rectangle.MouseDown += RectangleMouseDown;//Minute 28:37 https://www.youtube.com/watch?v=yEjRK54-1EU
                }
            }
        }
        public void RectangleMouseDown(object sender, MouseButtonEventArgs e)
        {
            ((Rectangle)sender).Fill = (((Rectangle)sender).Fill == Brushes.Black) ? Brushes.White : Brushes.Black;
        }


    }

}
    

