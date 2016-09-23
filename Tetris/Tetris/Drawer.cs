using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tetris
{
    public class Drawer
    {
        Game _Game { get; set; }
        Label[,] _Labels { get; set; }

        public Drawer(Game game, Label[,] labels)
        {
            _Game = game;
            _Labels = labels;
        }

        public void PaintTetris()
        {
            ClearLabels();
            PaintPlayer(_Game.Player);
            PaintMissiles(_Game.Missiles);
            PaintBlocks(_Game.Blocks);

        }


        private void PaintParts(List<Part> parts)
        {
            foreach (Part p in parts)
            {
                int x = p.PosX;
                int y = p.PosY;

                _Labels[y, x].Background = new SolidColorBrush(Colors.Black);
            }
        }
        private void PaintMissiles(List<Missile> missiles)
        {
            foreach (Missile m in missiles)
            {
                int x = m.PosX;
                int y = m.PosY;
                _Labels[y, x].Background = new SolidColorBrush(m.Color);
            }
        }
        private void PaintBlocks(List<Block> grid)
        {
            foreach (Block block in grid)
                PaintParts(block.Parts);
        }
        private void PaintPlayer(Player player)
        {
            foreach (Part p in player.Parts)
            {
                int x = p.PosX;

                _Labels[_Game.Player.PosY-1,x].Background = new SolidColorBrush(player.Color);
            }
        }
        private void ClearLabels()
        {
            foreach (Label l in _Labels)
            {
                if (l != null)
                    l.Background = new SolidColorBrush(Colors.Transparent);
            }
        }
    }
}
