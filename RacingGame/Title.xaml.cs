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
    /// Title.xaml の相互作用ロジック
    /// </summary>
    public partial class Title : Page {
        MainWindow win;
        Selector sl;

        public Title(MainWindow win) {
            InitializeComponent();
            this.win = win;
            sl = Selector.Start;
        }

        private void Page_PreviewKeyDown(object sender, KeyEventArgs e) {
            //カーソル移動処理
            if (e.Key == Key.Down || e.Key == Key.Up) {
                if (sl == Selector.Start) {
                    sl = Selector.Exit;
                    cursor.Margin = new Thickness(105.0, 457.0, 0.0, 0.0);
                } else {
                    sl = Selector.Start;
                    cursor.Margin = new Thickness(105.0, 415.0, 0.0, 0.0);
                }
            }

            //決定処理
            if (e.Key == Key.Enter || e.Key == Key.Z) {
                if (sl == Selector.Exit) {
                    win.Close();
                } else {
                    win.StartingGame.Invoke(this, new EventArgs());
                    win.PreviewKeyDown -= Page_PreviewKeyDown;
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e) {
            win.PreviewKeyDown += Page_PreviewKeyDown;
        }
    }
}
