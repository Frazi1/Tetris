using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Tetris
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow: Window
    {
        //consts
        const int COLUMNS = 10;
        const int ROWS = 18;
        int PlayerRowNumber = ROWS;

        //vars
        Game game;
        Label[,] LabelArray = new Label[ROWS+1, COLUMNS];
        Label[] PlayerArr = new Label[COLUMNS];
        //Label[,] Missiles = new Label[ROWS, COLUMNS];


        public MainWindow()
        {
            InitializeComponent();
            SetGrid();
            //SetPlayer();
            game = new Game(ROWS, COLUMNS,LabelArray);
            game.Start();
            


        }

   


        //Setting Grid and Player
        public void SetGrid()
        {
            for (int i = 0; i < ROWS+1; i++)
                for (int j = 0; j < COLUMNS; j++)
                {
                    Label lbl = new Label();
                    lbl.Background = new SolidColorBrush(Colors.White);
                    lbl.BorderBrush = new SolidColorBrush(Colors.Brown);
                    lbl.BorderThickness = new Thickness(1);
                    TetrisGrid.Children.Add(lbl);
                    Grid.SetRow(lbl, i);
                    Grid.SetColumn(lbl, j);
                    LabelArray[i, j] = lbl;
                }
        }
        public void SetPlayer()
        {
            for (int j = 0; j < COLUMNS; j++)
            {
                Label lbl = new Label();
                lbl.Background = new SolidColorBrush(Colors.Transparent);
                lbl.BorderBrush = new SolidColorBrush(Colors.Green);
                lbl.BorderThickness = new Thickness(0.2);
                TetrisGrid.Children.Add(lbl);
                Grid.SetRow(lbl, PlayerRowNumber);
                Grid.SetColumn(lbl, j);
                PlayerArr[j] = lbl;
            }
        }

        //Windows Events
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
                game.Player.MoveLeft();
            else if (e.Key == Key.Right)
                game.Player.MoveRight();

            if (e.Key == Key.Space)
                game.ShootMissile();
        }
    }

}
