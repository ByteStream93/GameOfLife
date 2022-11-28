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
            tickTimer.Interval = TimeSpan.FromSeconds(0.1);
            tickTimer.Tick += Step;
         
        }

        Random randomRectangles = new Random();
        public const int rowsAmount = 150;
        public const int columnsAmount = 150;
        int status = 0;
        Rectangle[,] rectanglesField = new Rectangle[rowsAmount, columnsAmount];
        DispatcherTimer tickTimer = new DispatcherTimer();

        private void StartAnimation(object sender, RoutedEventArgs e)
        {
            if (tickTimer.IsEnabled)
            {
                tickTimer.Stop();
            }
            else
            {              
                tickTimer.Start();
            }

        }
        private void StartLife(object sender, EventArgs e)
        {


            for (int row = 0; row < rowsAmount; row++)
            {
                for (int column = 0; column < columnsAmount; column++)
                {
                    Rectangle rectangle = new Rectangle();
                    rectangle.Width = LifeTable.ActualWidth / rowsAmount + 1;
                    rectangle.Height = LifeTable.ActualHeight / columnsAmount + 1;
                    rectangle.Fill = (randomRectangles.Next(0,2)==1) ? Brushes.Black : Brushes.White;
                    LifeTable.Children.Add(rectangle);
                    Canvas.SetLeft(rectangle, column * rectangle.Width);
                    Canvas.SetTop(rectangle, row * rectangle.Height);
                    rectangle.MouseDown += RectangleMouseDown;

                    rectanglesField[row, column] = rectangle;
                }
            }
        }
        public void RectangleMouseDown(object sender, MouseButtonEventArgs e)
        {

            ((Rectangle)sender).Fill = (((Rectangle)sender).Fill == Brushes.Black) ? Brushes.White : Brushes.Black;
        }

        private void Step(object sender, EventArgs e)
        {
            int[,] livingNeighbours = new int[rowsAmount, columnsAmount];
            for (int row = 0; row < rowsAmount; row++)
            {
                for (int column = 0; column < columnsAmount; column++)
                {

                    int rowAbove = row - 1;
                    if (rowAbove < 0)
                    {
                        rowAbove = rowsAmount - 1;
                    }

                    int rowBelow = row + 1;
                    if (rowBelow >= rowsAmount)
                    {
                        rowBelow = 0;
                    }

                    int columnLeft = column - 1;
                    if (columnLeft < 0)
                    {
                        columnLeft = columnsAmount - 1;
                    }

                    int columnRight = column + 1;
                    if (columnRight >= columnsAmount)
                    {
                        columnRight = 0;
                    }

                    int neighbour = 0;

                    if (rectanglesField[rowAbove, columnLeft].Fill == Brushes.White)
                    {
                        neighbour++;
                    }
                    if (rectanglesField[rowAbove, column].Fill == Brushes.White)
                    {
                        neighbour++;
                    }
                    if (rectanglesField[rowAbove, columnRight].Fill == Brushes.White)
                    {
                        neighbour++;
                    }
                    if (rectanglesField[row, columnLeft].Fill == Brushes.White)
                    {
                        neighbour++;
                    }
                    if (rectanglesField[row, columnRight].Fill == Brushes.White)
                    {
                        neighbour++;
                    }
                    if (rectanglesField[rowBelow, columnLeft].Fill == Brushes.White)
                    {
                        neighbour++;
                    }
                    if (rectanglesField[rowBelow, column].Fill == Brushes.White)
                    {
                        neighbour++;
                    }
                    if (rectanglesField[rowBelow, columnRight].Fill == Brushes.White)
                    {
                        neighbour++;
                    }

                    livingNeighbours[row, column] = neighbour;
                }

            }

            for (int row = 0; row < rowsAmount; row++)
            {
                for (int column = 0; column < columnsAmount; column++)
                {

                    if (livingNeighbours[row,column] < 2 || livingNeighbours[row,column] > 3)
                    {
                        rectanglesField[row,column].Fill = Brushes.Black;
                    }
                    else if (livingNeighbours[row, column] == 3)
                    {
                        rectanglesField[row, column].Fill = Brushes.White;
                    }                 

                }
            }
        }
    }
}
    

