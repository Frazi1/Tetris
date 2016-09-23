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
        int PlayerRowNumber = ROWS + 1;

        //vars
        Game game;
        Label[,] LabelArray = new Label[ROWS, COLUMNS];
        Label[] PlayerArr = new Label[COLUMNS];
        //Label[,] Missiles = new Label[ROWS, COLUMNS];


        public MainWindow()
        {
            InitializeComponent();
            SetGrid();
            game = new Game(ROWS, COLUMNS);
            game.Start();


            SetPlayer();
            DrawPlayer(PlayerArr, game.Player);


            game.BlockMoved += Game_BlockMoved;
            game.BlockMoving += Game_BlockMoving;
            game.Player.PlayerMoving += Player_PlayerMoving;
            game.Player.PlayerMoved += Player_PlayerMoved;
            game.MissileMoving += Game_MissileMoving;
            game.MissileMoved += Game_MissileMoved;
            game.PlayerShoot += Game_PlayerShoot;
            game.BlockDestroying += Game_BlockRemoving;
            game.MissileDestroyed += Game_MissileDestroyed;
            game.MissileDestroying += Game_MissileDestroying;
            game.MissileCollided += Game_MissileCollided;
            

        }

        //Game events handling
        private void Game_BlockMoving()
        {
            RemoveCurrentBlock();
        }
        private void Game_BlockMoved()
        {
            PaintBlocks(LabelArray, game.TetrisGrid);
        }
        private void Player_PlayerMoving()
        {
            RemovePlayer();
        }
        private void Player_PlayerMoved()
        {
            DrawPlayer(PlayerArr, game.Player);
        }
        private void Game_MissileMoved()
        {
            PaintMissiles(LabelArray, game.Missiles);
        }
        private void Game_MissileMoving()
        {
            RemoveMissiles();
        }
        private void Game_PlayerShoot()
        {
            PaintMissiles(LabelArray, game.Missiles);
        }
        private void Game_BlockRemoving(Block b)
        {
            foreach (Part p in b.Parts)
            {
                int x = p.PosX;
                int y = p.PosY;

                LabelArray[y, x].Background = new SolidColorBrush(Colors.White);
            }
        }
        private void Game_MissileDestroying()
        {
        }
        private void Game_MissileDestroyed()
        {
        }
        private bool Game_MissileCollided(Missile m, Part p)
        {
            int x = p.PosX;
            int y = p.PosY;
            LabelArray[y, x].Background = new SolidColorBrush(Colors.White);
            PaintBlocks(LabelArray,game.TetrisGrid);
            return true;
        }


        //Drawing Tetris
        private static void PaintBlocks(Label[,] lblArr, List<Block> grid)
        {
            foreach (var block in grid)
                PaintParts(lblArr, block.Parts);
        }
        private static void PaintParts(Label[,] lblArr, List<Part> parts)
        {
            foreach (Part p in parts)
            {
                int x = p.PosX;
                int y = p.PosY;

                lblArr[y, x].Background = new SolidColorBrush(Colors.Black);
            }
        }
        private void RemoveCurrentBlock()
        {
            foreach (Block b in game.Blocks)
            {
                foreach (Part p in b.Parts)
                {
                    int x = p.PosX;
                    int y = p.PosY;

                    LabelArray[y, x].Background = new SolidColorBrush(Colors.White);
                }
            }
        }
        private void RemoveMissiles()
        {
            foreach (Missile m in game.Missiles)
            {
                int x = m.PosX;
                int y = m.PosY;

                LabelArray[y, x].Background = new SolidColorBrush(Colors.White);
            }
        }
        private void PaintMissiles(Label[,] lblArr, List<Missile> missiles)
        {
            foreach(Missile m in missiles)
            {
                int x = m.PosX;
                int y = m.PosY;
                lblArr[y, x].Background = new SolidColorBrush(m.Color);
            }
        }

        //Drawing Player
        private static void DrawPlayer(Label[] lblArr, Player player)
        {
            foreach (Part p in player.Parts)
            {
                int x = p.PosX;

                lblArr[x].Background = new SolidColorBrush(player.Color);
            }
        }
        private void RemovePlayer()
        {
            foreach (Part p in game.Player.Parts)
            {
                int x = p.PosX;

                PlayerArr[x].Background = new SolidColorBrush(Colors.White);
                PlayerArr[x].BorderBrush = new SolidColorBrush(Colors.Green);
                PlayerArr[x].BorderThickness = new Thickness(0.2);
            }
        }

        //Setting Grid and Player
        public void SetGrid()
        {
            for (int i = 0; i < ROWS; i++)
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
