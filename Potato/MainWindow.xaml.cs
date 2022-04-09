using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Potato
{
    public partial class MainWindow : Window
    {
        private Pickle _pickle1 = new Pickle();
        private Pickle _pickle2 = new Pickle();
        private bool _war = true;

        public MainWindow()
        {
            InitializeComponent();
            pickle1View.DataContext = _pickle1;
            pickle2View.DataContext = _pickle2;
            _pickle1.OnDied += DiedPickle;
            _pickle2.OnDied += DiedPickle;
        }

        public void DiedPickle(string name)
        {
            this.Dispatcher.Invoke(() => { 
            resultWar.Text = name + " проиграл!";
            _war = false;
            });
        }
        public void Penetration(string nameAtackPickle, string nameLoxPickle, int damage)
        {
            this.Dispatcher.Invoke(() =>
            {
                report.Items.Add($"{nameAtackPickle} пробил {nameLoxPickle} на {damage} хэпэшек");
                report.ScrollIntoView(report.Items[report.Items.Count - 1]);
            });
        }

        public void NotPenetration(string nameAtackPickle, string nameGeniusPickle)
        {
            this.Dispatcher.Invoke(() =>
            {
                report.Items.Add($"{nameAtackPickle} закис, {nameGeniusPickle} радуется");
                report.ScrollIntoView(report.Items[report.Items.Count - 1]);
            });
        }

        private async void War_Click(object sender, RoutedEventArgs e)
        {
            report.Items.Clear();
            await Task.Run(Battle);
        }

        private void Battle()
        {
            _war = true;
            while (_war)
            {
                (bool, int) attack = _pickle1.Attack(_pickle2);
                if (attack.Item1) Penetration(_pickle1.Name, _pickle2.Name, attack.Item2);
                else NotPenetration(_pickle1.Name, _pickle2.Name);

                Thread.Sleep(700);
                if (!_war) break;

                attack = _pickle2.Attack(_pickle1);
                if (attack.Item1) Penetration(_pickle2.Name, _pickle1.Name, attack.Item2);
                else NotPenetration(_pickle2.Name, _pickle1.Name);

                Thread.Sleep(700);
            }
            
        }


    }
}
