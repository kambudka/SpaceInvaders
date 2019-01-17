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
using System.Diagnostics;
using System.Threading;
using System.Windows.Threading;

namespace SpaceInvaders
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    /// 
    public static class Globals
    {
       public static List<PlayerMissile> playerMissiles;
        public static int shipCounter = 0; 
        
    }
    public class ThreadWithState
    {
        // State information used in the task.
        iEnemyShip enemyShip;
        Canvas map;
        int x;
        int y;
        bool dead=false;
        bool endLine;
        int missileCounter;
        bool isMovingRigth;
        PlayerShip player;
        // The constructor obtains the state information.
        public ThreadWithState(iEnemyShip enemyShip, int xx, int yy,Canvas map, PlayerShip player)
        {
            this.player = player;
            missileCounter = 1;
            this.map = map;
            x=xx;
            y=yy;
            this.enemyShip = enemyShip;
            
            this.isMovingRigth = true;
        }

        // The thread procedure performs the task, such as formatting
        // and printing a document.
        public void ThreadProc()
        {

            while (true)
            {

                Application.Current.Dispatcher.Invoke((Action)(() =>
                {
                    enemyShip.MoveTo(x, y);
                }));
                if (x < 10)
                {
                    endLine = true;
                }
                if (x > 600)
                {
                    endLine = true;
                }
                if (endLine == true && isMovingRigth == true)
                {
                    isMovingRigth = false;
                    endLine = false;
                }
                if (endLine == true && isMovingRigth == false)
                {
                    isMovingRigth = true;
                    endLine = false;
                }
                if (isMovingRigth == true)
                {
                    x += enemyShip.GetSpeed();
                }
                if (isMovingRigth == false)
                {
                    x -= enemyShip.GetSpeed();
                }


                Application.Current.Dispatcher.Invoke((Action)(() =>
                {
                   
                    for(int i = 0;i< Globals.playerMissiles.Count();i++)
                    if (Globals.playerMissiles[i].y > y-50 && Globals.playerMissiles[i].y < y + 50 && Globals.playerMissiles[i].x > x-50 && Globals.playerMissiles[i].x < x + 50)
                    {

                            Globals.playerMissiles[i].dynamicImage.Source = null;
                            enemyShip.GetImage().Source = null;
                            Globals.playerMissiles.RemoveAt(i);
                            dead = true;
                            Globals.shipCounter--;
                            break;
                        }
                }));

                if (dead == true)
                    break;
                
                Thread.Sleep(100);
                missileCounter++;
                if (missileCounter % 20 == 0)
                {
                    Missile miss = new Missile(map);
                    FlyingMissile missile = new FlyingMissile(miss, this.x, this.y);
                    Thread t = new Thread(new ThreadStart(missile.ThreadProc));
                    t.Start();
                }
                
            }
        }
    }

    public class GameMaster
    {
        ShipFactory factory = new ConcreteShipFactory();
        Random random = new Random();
        int mapCount;
        PlayerShip player;
        Canvas mapa;
        int boosterCounter;
        public GameMaster(Canvas mapa,PlayerShip player)
        {
            this.player = player;
            this.mapa = mapa;
        }

        public void RunGame()
        {
            mapCount = 1;
            while(true)
            {
                
                if (Globals.shipCounter == 0)
                {
                    mapCount++;
                    
                        for (int i = 0; i < mapCount; i++)
                    {
                        Application.Current.Dispatcher.Invoke((Action)(() =>
                        {
                            iEnemyShip enemy = factory.GetShip(mapa, "Cruiser");
                           
                        ThreadWithState tws = new ThreadWithState(enemy, random.Next(1, 400), random.Next(1, 400), mapa, player);
                        Thread t = new Thread(new ThreadStart(tws.ThreadProc));
                        t.Start();
                            iEnemyShip enemy2 = new UpgradeSpeed(factory.GetShip(mapa, "Destroyer"));
                           
                            ThreadWithState tws2 = new ThreadWithState(enemy2, random.Next(1, 400), random.Next(1, 400), mapa, player);
                            Thread t2 = new Thread(new ThreadStart(tws2.ThreadProc));
                            t2.Start();
                        }));
                        Thread.Sleep(200);
                        Globals.shipCounter++;
                    }
                    
                    
                    
                    
                }
                boosterCounter++;
                if (boosterCounter % 50 == 0)
                {
                    Booster boost = new Booster(mapa, "gun");
                    FlyingBooster flyingBooster = new FlyingBooster(boost, player);
                    Thread t = new Thread(new ThreadStart(flyingBooster.ThreadProc));
                    t.Start();
                }
                Thread.Sleep(200);

            }
        }


    }

    public partial class MainWindow : Window
    {
        public int playerlife { get; set; }
        PlayerCommands commands;
        iCommand moveLeft;
        iCommand moveRight;
        iCommand shoot;
        iCommand exit;
        Canvas mapa;

        

        public MainWindow()
        {
            playerlife = 2;
            InitializeComponent();
            Globals.playerMissiles = new List<PlayerMissile>();
            PlayerShip statekgracza = PlayerShip.Instance();
            Designs designs = new Designs();


            #region Adding Textures
            Uri uri = new Uri(@"/Cruiser.PNG", UriKind.Relative);
            designs.addDesign("Cruiser", uri);
            uri = new Uri(@"/Destroyer.PNG", UriKind.Relative);
            designs.addDesign("Destroyer", uri);
            uri = new Uri(@"/PlayerShip.PNG", UriKind.Relative);
            designs.addDesign("PlayerShip", uri);
            uri = new Uri(@"/bullet.PNG", UriKind.Relative);
            designs.addDesign("Missile1", uri);
            uri = new Uri(@"/bullet.PNG", UriKind.Relative);
            designs.addDesign("Missile2", uri);
            uri = new Uri(@"/Booster.PNG", UriKind.Relative);
            designs.addDesign("Booster", uri);
            #endregion



            #region Adding Commands
            moveLeft = new MoveLeft(statekgracza);
            moveRight = new MoveRight(statekgracza);
            shoot = new Shoot(statekgracza);
            exit = new Exit(this);
            commands = new PlayerCommands();
            #endregion


            #region Initialize Window
            mapa = new Canvas();
            mapa.Width = 800;
            mapa.Height = 600;
            mapa.Background = new SolidColorBrush(Colors.White);
            mapa.Focusable = true;
            Grid.SetRow(mapa, 0);
            Grid.SetRow(mapa, 0);
            Root.Children.Add(mapa);
            statekgracza.CreateShipDynamically(mapa);
            GameMaster gameMaster = new GameMaster(mapa, statekgracza);
            Thread t = new Thread(new ThreadStart(gameMaster.RunGame));
            t.Start();
            #endregion

        }

        private void OnTextInputKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    commands.DoCommand(moveLeft);
                    break;
                case Key.Right:
                    commands.DoCommand(moveRight);
                    break;
                case Key.Space:
                    commands.DoCommand(shoot);
                    break;
                case Key.Escape:
                    commands.DoCommand(exit);
                    break;

            }

        }

        public void Exit()
        {
            System.Windows.Application.Current.Shutdown();
        }


    }
}
