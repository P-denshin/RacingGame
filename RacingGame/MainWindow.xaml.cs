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

namespace RacingGame {
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window {
        Page title;
        Page game;

        public MainWindow() {
            InitializeComponent();
            StartingGame += Starting_Game;
            EndingGame += Ending_Game;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            title = new Title(this);
            mainFrame.Content = title;
        }

        public EventHandler StartingGame;
        public EventHandler EndingGame;

        private void Starting_Game(object sender, EventArgs e) {
            game = new Game(this);
            mainFrame.Content = game;
        }

        private void Ending_Game(object sender, EventArgs e) {
            title = new Title(this);
            mainFrame.Content = title;
        }
    }
}
