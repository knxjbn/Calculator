using System.Windows;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Start(object sender, StartupEventArgs e)
        {
            Views.CalculatorView view = new Views.CalculatorView();
            view.DataContext = new ViewModels.CalculatorViewModel();
            view.Show();
        }
    }
}
