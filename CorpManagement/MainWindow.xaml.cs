using CorpManagement.Toolbox;
using MahApps.Metro.Controls;
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
using CorpManagement.ViewModel;

namespace CorpManagement
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Height = SystemParameters.WorkArea.Height;
            Width = SystemParameters.WorkArea.Width;
        }

        // Executed event handler.
        private void CloseCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        // CanExecute event handler.
        private void CanExecuteHandler(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
