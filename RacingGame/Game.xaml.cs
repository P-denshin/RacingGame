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

namespace RacingGame {
    /// <summary>
    /// Game.xaml の相互作用ロジック
    /// </summary>
    public partial class Game : Page {
        MainWindow win;
        Simulator sm;
        List<Rectangle> enemies;
        DispatcherTimer dt;
        int flameRate;
        int counter;
        int score;

        public Game(MainWindow win) {
            InitializeComponent();
            this.win = win;
            flameRate = 1000;
            enemies = new List<Rectangle>();
            counter = 0;
            score = 0;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e) {
            win.PreviewKeyDown += Page_PreviewKeyDown;
            sm = new Simulator();
            dt = new DispatcherTimer();
            dt.Interval = new TimeSpan(0, 0, 0, 0, flameRate);
            dt.Tick += EnemiesUpDate;
            dt.Start();
        }

        private void EnemiesUpDate(object sender, EventArgs e) {
            bool isEnd = false;
            if (!sm.EnemiesUpdate()) {
                isEnd = true;
            }
            
            foreach (var i in enemies) {
                mainGrid.Children.Remove(i);
            }

            enemies.Clear();

            foreach (var i in sm.EnemiesPoint) {
                int h;

                if (i.hp == HoraizontalPoint.Left) h = 10;
                else if (i.hp == HoraizontalPoint.Center) h = 121;
                else h = 230;

                int v = 67 + i.vp * 105;

                enemies.Add(new Rectangle() { 
                    Margin = new Thickness(h, v, 0, 0), 
                    Fill = new SolidColorBrush(Colors.Black),
                    Height = 100,
                    Width = 80,
                    VerticalAlignment = System.Windows.VerticalAlignment.Top,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left
                });           
            }

            foreach (var i in enemies) {
                mainGrid.Children.Add(i);
            }

            if (isEnd) {
                Ending();
            }

            score++;
            scorel.Content = "スコアは" + score;
            if (++counter >= 25) {
                if(flameRate >= 400)
                    flameRate -= 100;
                counter = 0;
                dt.Interval = new TimeSpan(0, 0, 0, 0, flameRate);
                scorel.Content = scorel.Content + "\nレベルアップ！";
            }
        }

        private void Page_PreviewKeyDown(object sender, KeyEventArgs e) {
            //自機移動処理
            bool isEnd = true;
            if (e.Key == Key.Left){
                isEnd = sm.PlayerUpdate(MovingDirection.Left);
            } else if (e.Key == Key.Right) {
                isEnd = sm.PlayerUpdate(MovingDirection.Right);
            } 

            //CheetDebug
            if (e.Key == Key.S) {
                dt.Start();
            } else if (e.Key == Key.T) {
                dt.Stop();
            }

            //描写移動
            if (sm.PlayerCarPoint == HoraizontalPoint.Center) {
                myCar.Margin = new Thickness(121, 487, 0, 0);
            } else if (sm.PlayerCarPoint == HoraizontalPoint.Left) {
                myCar.Margin = new Thickness(10, 487, 0, 0);
            } else {
                myCar.Margin = new Thickness(230, 487, 0, 0);
            }

            if (!isEnd) {
                Ending();
            }
        }

        private void Ending() {
            dt.Stop();
            MessageBox.Show("GAME OVER");
            win.EndingGame.Invoke(this, new EventArgs());
        }
    }
}
