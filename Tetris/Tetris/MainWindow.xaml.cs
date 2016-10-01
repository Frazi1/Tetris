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
        Drawer drawer;

        Label[,] LabelArray = new Label[ROWS+1, COLUMNS];
        Label[] PlayerArr = new Label[COLUMNS];


        public MainWindow()
        {
            InitializeComponent();
            SetGrid();
            game = new Game(ROWS, COLUMNS);
            drawer = new Drawer(game, LabelArray);
            

            game.BlockMoved += Game_BlockMoved;
            game.BlockDestroyed += Game_BlockDestroyed;
            game.ScoreUpdated += Game_ScoreUpdated;
            game.MissileCollided += Game_MissileCollided;
            game.Player.PlayerMoved += Player_PlayerMoved;
            game.BlockSpawned += Game_BlockSpawned;
            game.PlayerShoot += Game_PlayerShoot;
            game.GameOver += Game_GameOver;
    

            game.Start();
        }

        private void Game_PlayerShoot()
        {
            drawer.PaintTetris();
        }

        private void Game_GameOver()
        {
            MessageBox.Show("GAME OVER \r\n YOUR SCORE: " + game.Score);
        }


        private void Game_BlockSpawned()
        {
            drawer.PaintTetris();
        }

        private void Player_PlayerMoved()
        {
            drawer.PaintTetris();
        }

        private bool Game_MissileCollided()
        {
            drawer.PaintTetris();
            return true;
        }

        private void Game_ScoreUpdated()
        {
            Score.Content = game.Score;
        }

        private void Game_BlockDestroyed()
        {
            drawer.PaintTetris();
        }

        private void Game_BlockMoved()
        {
            drawer.PaintTetris();
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
            for (int j = 0; j < COLUMNS; j++)
            {
                LabelArray[PlayerRowNumber, j].BorderBrush = new SolidColorBrush(Colors.Blue);

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
            if (e.Key == Key.S)
                game.NewBlock();
        }
    }

}
