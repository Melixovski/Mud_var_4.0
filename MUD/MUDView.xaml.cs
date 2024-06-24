using System.Windows;

namespace MUD
{

    public partial class MUDView : Window
    {
        private ViewModel viewModel;
        public MUDView()
        {
            InitializeComponent();
            viewModel = new ViewModel();
            DataContext = viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SelectedFile = (sender as FrameworkElement)?.DataContext as ViewModel.Files;
            string message = null;
            Autodesk.Revit.UI.Result result = viewModel.Execute(ViewModel.commandData, ref message, null);
        }
    
    }
}
