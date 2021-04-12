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
using System.Windows.Shapes;

namespace WPF_Test.PresentationLayer
{
    /// <summary>
    /// Interaction logic for QuestStatusView.xaml
    /// </summary>
    public partial class QuestStatusView : Window
    {
        private QuestStatusViewModel _questStatusViewModel;
        public QuestStatusView(QuestStatusViewModel questStatusViewModel)
        {
            _questStatusViewModel = questStatusViewModel;
            DataContext = questStatusViewModel;

            IntializeWindowTheme();
            InitializeComponent();
        }

        private void IntializeWindowTheme()
        {
            this.Title = "Killer Rabbit Productions";
        }

        private void QuestStatusWindowClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
