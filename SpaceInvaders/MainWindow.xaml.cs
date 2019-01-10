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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SpaceInvaders
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        PlayerCommands commands;
        iCommand moveUp;
        iCommand moveDown;
        iCommand moveLeft;
        iCommand moveRight;
        Canvas mapa;

        public MainWindow()
        {
            InitializeComponent();
            PlayerShip nowystatek = new PlayerShip();
            
            moveUp = new MoveUp(nowystatek);
            moveDown = new MoveDown(nowystatek);
            moveLeft = new MoveLeft(nowystatek);
            moveRight = new MoveRight(nowystatek);
            commands = new PlayerCommands();
            
            mapa = new Canvas();
            mapa.Width = 800;
            mapa.Height = 450;
            mapa.Background = new SolidColorBrush(Colors.LightCyan);
            mapa.Focusable = true;
            Root.Children.Add(mapa);
            nowystatek.CreateShipDynamically(mapa);

        }

        private void OnTextInputKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    commands.DoCommand(moveUp);
                break;
                case Key.Down:
                    commands.DoCommand(moveDown);
                    break;
                case Key.Left:
                    commands.DoCommand(moveLeft);
                    break;
                case Key.Right:
                    commands.DoCommand(moveRight);
                    break;

            }

        }


    }
}
