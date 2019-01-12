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
            Designs designs = new Designs();


            #region Adding Textures
            Uri uri = new Uri(@"C:\Users\Zalgo\Source\Repos\SpaceInvaders\SpaceInvaders\Cruiser.PNG");
            designs.addDesign("Cruiser", uri);
            uri = new Uri(@"C:\Users\Zalgo\Source\Repos\SpaceInvaders\SpaceInvaders\Destroyer.PNG");
            designs.addDesign("Destroyer", uri);
            uri = new Uri(@"C:\Users\Zalgo\Source\Repos\SpaceInvaders\SpaceInvaders\PlayerShip.PNG");
            designs.addDesign("PlayerShip", uri);
            #endregion



            #region Adding Commands
            moveUp = new MoveUp(nowystatek);
            moveDown = new MoveDown(nowystatek);
            moveLeft = new MoveLeft(nowystatek);
            moveRight = new MoveRight(nowystatek);
            commands = new PlayerCommands();
            #endregion


            mapa = new Canvas();
            mapa.Width = 800;
            mapa.Height = 600;
            mapa.Background = new SolidColorBrush(Colors.White);
            mapa.Focusable = true;
            Root.Children.Add(mapa);
            nowystatek.CreateShipDynamically(mapa);
            Cruiser enemy = new Cruiser(mapa);
            Cruiser enemy2 = new Cruiser(mapa);
            Destroyer enemy3 = new Destroyer(mapa);

            enemy.MoveTo(50, 50);
            enemy3.MoveTo(200, 200);


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
