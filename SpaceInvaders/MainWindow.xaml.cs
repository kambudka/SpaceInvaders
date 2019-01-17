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
       public static int shipCounter = 0;//liczba statków na mapie
        public static int mapCount = 1;//liczba rozpoczętych rund
        public static int points = 0;//wynik gracza
    }
    public class ThreadShipMissileControl
    {
        // Informacje używane w zadaniu
        iEnemyShip enemyShip;
        Canvas map;
        // Wspolrzedne enemyShip:
        int x, y;
        bool dead=false;
        bool endLine;
        int missileCounter;
        bool isMovingRigth;
        PlayerShip player;
        // Konstruktor pobiera potrzebne dane
        public ThreadShipMissileControl(iEnemyShip enemyShip, int xx, int yy,Canvas map, PlayerShip player)
        {
            this.player = player;
            missileCounter = 1;
            this.map = map;
            x=xx;
            y=yy;
            this.enemyShip = enemyShip;
            
            this.isMovingRigth = true;
        }

        // Wątek wykonuje zadanie
        public void ThreadProc()
        {

            while (true)
            {

                Application.Current.Dispatcher.Invoke((Action)(() =>
                {
                    enemyShip.MoveTo(x, y);
                }));
                if (x < 10)//sprawdza czy statek doleciał do końca lini
                {
                    endLine = true;
                }
                if (x > 700)//sprawdza czy statek doleciał do końca lini
                {
                    endLine = true;
                }
                if (endLine == true && isMovingRigth == true)//jeśłi statek doleciał do końca lini sprawdza kierunek w którym leciał i zmienia go
                {
                    isMovingRigth = false;
                    endLine = false;
                }
                if (endLine == true && isMovingRigth == false)
                {
                    isMovingRigth = true;
                    endLine = false;
                }
                if (isMovingRigth == true)//ustalenie następnej pozycji statku
                {
                    x += enemyShip.GetSpeed();
                }
                if (isMovingRigth == false)//ustalenie następnej pozycji statku
                {
                    x -= enemyShip.GetSpeed();
                }


                Application.Current.Dispatcher.Invoke((Action)(() =>
                {
                   
                    for(int i = 0;i< Globals.playerMissiles.Count();i++)  
                        //Sprawdzenie czy przeciwnik zostal trafiony rakietą przez gracza
                        if (Globals.playerMissiles[i].y > y-50 && Globals.playerMissiles[i].y < y + 50 &&
                        Globals.playerMissiles[i].x > x-50 && Globals.playerMissiles[i].x < x + 50) // Wystąpienie kolizji
                        {
                            //Sprawdzenie ile zycia posiada przeciwnik
                            if(enemyShip.GetLifes() > 0) 
                            {
                                int gundmg = player.gundmg;
                                enemyShip.RemoveLife(gundmg);  // Usuniecie ilości życia równej obrażeniom gracza
                            }
                            else // Jezeli przeciwnik straci cale życie jest niszczony
                            {

                                enemyShip.GetImage().Source = null;
                                Globals.points++;

                                MainWindow.main.Dispatcher.Invoke(new Action(delegate ()
                                {
                                    MainWindow.main.ScorePoints = Globals.points.ToString();
                                }));
                               // MainWindow.main.LifePoints = Globals.points.ToString();
                                dead = true;
                                Globals.shipCounter--;
                                
                            }
                            Globals.playerMissiles[i].dynamicImage.Source = null;
                            Globals.playerMissiles.RemoveAt(i);
                            if (dead == true)
                                break;
                        }
                }));

                if (dead == true)
                    break;
                
                Thread.Sleep(100);
                missileCounter++;
                if (missileCounter % 50 == 0) // Pociski enemyShip
                {
                    Missile miss = new Missile(map,enemyShip.GetGunDmg());
                    FlyingMissile missile = new FlyingMissile(miss, this.x, this.y, player);
                    Thread t = new Thread(new ThreadStart(missile.ThreadProc));
                    t.Start();
                }
                
            }
        }
    }
    //Game master prowadzi gre
    public class GameMaster
    {
        ShipFactory factory = new ConcreteShipFactory();
        Random random = new Random();

        PlayerShip player;
        Canvas mapa;
        int shipLottery;
        int boosterCounter;
        public GameMaster(Canvas mapa, PlayerShip player)
        {
            this.player = player;
            this.mapa = mapa;
        }

        public void RunGame()
        {

            while (true)
            {

                if (Globals.shipCounter == 0)
                {
                    Globals.mapCount++;
                    if (Globals.mapCount > 2)
                    {
                        Application.Current.Dispatcher.Invoke((Action)(() =>
                        {
                            mapa.Visibility = Visibility.Hidden;
                        }));
                    }
                    for (int i = 0; i < Globals.mapCount; i++)
                    {
                        shipLottery = random.Next(1, 6);//wybierz losowy statek do stworzenia
                        Application.Current.Dispatcher.Invoke((Action)(() =>
                        {

                            if (shipLottery == 1)
                            {
                                iEnemyShip enemy = factory.GetShip(mapa, "Cruiser");

                                ThreadShipMissileControl tws = new ThreadShipMissileControl(enemy, random.Next(20, 400), random.Next(20, 400), mapa, player);
                                Thread t = new Thread(new ThreadStart(tws.ThreadProc));
                                t.Start();
                            }
                            if (shipLottery == 2)
                            {
                                iEnemyShip enemy2 = new UpgradeSpeed(factory.GetShip(mapa, "Destroyer"));

                                ThreadShipMissileControl tws2 = new ThreadShipMissileControl(enemy2, random.Next(20, 400), random.Next(20, 400), mapa, player);
                                Thread t2 = new Thread(new ThreadStart(tws2.ThreadProc));
                                t2.Start();
                            }
                            if (shipLottery == 3)
                            {
                                iEnemyShip enemy = new UpgradeLife(factory.GetShip(mapa, "Cruiser"));

                                ThreadShipMissileControl tws = new ThreadShipMissileControl(enemy, random.Next(20, 400), random.Next(20, 400), mapa, player);
                                Thread t = new Thread(new ThreadStart(tws.ThreadProc));
                                t.Start();
                            }
                            if (shipLottery == 4)
                            {
                                iEnemyShip enemy = new UpgradeGun(factory.GetShip(mapa, "Cruiser"));

                                ThreadShipMissileControl tws = new ThreadShipMissileControl(enemy, random.Next(20, 400), random.Next(20, 400), mapa, player);
                                Thread t = new Thread(new ThreadStart(tws.ThreadProc));
                                t.Start();
                            }
                            if (shipLottery == 5)
                            {
                                iEnemyShip enemy = new UpgradeGun(factory.GetShip(mapa, "Destroyer"));

                                ThreadShipMissileControl tws = new ThreadShipMissileControl(enemy, random.Next(20, 400), random.Next(20, 400), mapa, player);
                                Thread t = new Thread(new ThreadStart(tws.ThreadProc));
                                t.Start();
                            }
                            if (shipLottery == 6)
                            {
                                iEnemyShip enemy = new UpgradeLife(factory.GetShip(mapa, "Cruiser"));

                                ThreadShipMissileControl tws = new ThreadShipMissileControl(enemy, random.Next(20, 400), random.Next(20, 400), mapa, player);
                                Thread t = new Thread(new ThreadStart(tws.ThreadProc));
                                t.Start();
                            }
                        }));
                        Thread.Sleep(200);
                        Globals.shipCounter++;
                    }

                    Application.Current.Dispatcher.Invoke((Action)(() =>
                    {
                        mapa.Visibility = Visibility.Visible;
                    }));


                }
                boosterCounter++;//tworzenie wzmocnień co określoną ilość czasu
                if (boosterCounter % 100 == 0)
                {

                    Booster boost = new Booster(mapa, "gun");
                    FlyingBooster flyingBooster = new FlyingBooster(boost, player);
                    Thread t = new Thread(new ThreadStart(flyingBooster.ThreadProc));
                    t.Start();
                }
                if (boosterCounter % 150 == 0)
                {

                    Booster boost = new Booster(mapa, "armor");
                    FlyingBooster flyingBooster = new FlyingBooster(boost, player);
                    Thread t = new Thread(new ThreadStart(flyingBooster.ThreadProc));
                    t.Start();
                }
                if (boosterCounter % 200 == 0)
                {

                    Booster boost = new Booster(mapa, "life");
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
        TextBlock lifecounter;
        TextBlock pointscounter;
        internal static MainWindow main;
        Dictionary<string, int> highscores;


        public MainWindow()
        {
            playerlife = 2;
            InitializeComponent();
            Globals.playerMissiles = new List<PlayerMissile>();
            PlayerShip statekgracza = PlayerShip.Instance();
            Designs designs = new Designs();
            highscores = new Dictionary<string, int>();


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

            #region Adding Scores
            highscores.Add("Kamil", 20);
            highscores.Add("Piotr", 30);
            highscores.Add("Rafał", 40);
            #endregion


            #region Initialize Window
            main = this;
            LifePoints = "5";
            mapa = new Canvas();
            lifecounter = LifeCounter;
            pointscounter = PointCounter;
            mapa.Width = 800;
            mapa.Height = 600;
            mapa.Background = new SolidColorBrush(Colors.White);
            mapa.Focusable = true;
            Grid.SetRow(mapa, 0);
            Grid.SetRow(mapa, 0);
            Grid.SetColumnSpan(mapa, 6);
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
            Environment.Exit(0);
        }


        internal string LifePoints
        {
            get { return LifeCounter.Text; }
            set { Dispatcher.Invoke(new Action(() => { LifeCounter.Text = value; })); }
        }

        internal string ScorePoints
        {
            get { return PointCounter.Text; }
            set { Dispatcher.Invoke(new Action(() => { PointCounter.Text = value; })); }
        }

        public void SetLifeLabel(int life)
        {
            LifeCounter.Text = life.ToString();
        }
        public void SetpointLabel(int points)
        {
            PointCounter.Text = points.ToString();
        }


        public void AddToScoreBoard(string name, int score)
        {
            highscores.Add(name, score);
        }


        public void ShowScoreboard()
        {


            var ordered = highscores.OrderBy(x => x.Value);
            //highscores = ordered.ToDictionary(string, int);
            AddToScoreBoard("Twoj wynik", Globals.points);
            Root.Visibility = Visibility.Hidden;
            mapa.Visibility = Visibility.Hidden;
            int ilosc = highscores.Count;
            RowDefinition newrow;
            ColumnDefinition c1 = new ColumnDefinition();
            c1.Width = GridLength.Auto;
            ColumnDefinition c2 = new ColumnDefinition();
            c2.Width = GridLength.Auto;
            ScoreBoard.ColumnDefinitions.Add(c1);
            ScoreBoard.ColumnDefinitions.Add(c2);
            newrow = new RowDefinition();
            newrow.Height = GridLength.Auto;
            ScoreBoard.RowDefinitions.Add(newrow);
            Label tabelawynikow = new Label();
            Grid.SetRow(tabelawynikow, 0);
            tabelawynikow.Content = "Tabela wyników";
            Grid.SetColumnSpan(tabelawynikow, 2);
            ScoreBoard.Children.Add(tabelawynikow);
            ScoreBoard.Width = 300;
            for (int i = 0; i < ilosc; i++)
            {
                Label nowygracz = new Label();
                Label nowywynik = new Label();
                newrow = new RowDefinition();
                newrow.Height = GridLength.Auto;
                ScoreBoard.RowDefinitions.Add(newrow);
                nowygracz.Content = highscores.ElementAt(i).Key;
                nowywynik.Content = highscores.ElementAt(i).Value;
                Grid.SetRow(nowygracz, i + 1);
                Grid.SetColumn(nowygracz, 0);
                Grid.SetRow(nowywynik, i + 1);
                Grid.SetColumn(nowywynik, 1);
                ScoreBoard.Children.Add(nowywynik);
                ScoreBoard.Children.Add(nowygracz);
            }

            newrow = new RowDefinition();
            newrow.Height = GridLength.Auto;
            ScoreBoard.RowDefinitions.Add(newrow);
            Label wyjscie = new Label();
            Grid.SetRow(wyjscie, ilosc + 1);
            wyjscie.Content = "Aby zakończyć nacisnij ESC";
            Grid.SetColumnSpan(wyjscie, 2);
            ScoreBoard.Children.Add(wyjscie);
            //Root.Visibility = Visibility.Visible;
            //mapa.Visibility = Visibility.Visible;
        }

    }
}
