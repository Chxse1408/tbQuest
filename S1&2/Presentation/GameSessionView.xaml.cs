using System.Windows;

namespace tbQuest.Presentation
{
    /// <summary>
    /// Interaction logic for GameSessionView.xaml
    /// </summary>
    public partial class GameSessionView : Window
    {
        private GameSessionViewModel _gameSessionViewModel;

        public GameSessionView(GameSessionViewModel gameSessionViewModel)
        {
            _gameSessionViewModel = gameSessionViewModel;

            InitializeWindowTheme();

            InitializeComponent();
        }

        private void InitializeWindowTheme()
        {
            this.Title = "ChaseKelly Productions";
        }

        private void ForwardButton_Click(object sender, RoutedEventArgs e)
        {
            _gameSessionViewModel.MoveForward();
        }

        private void BackwardButton_Click(object sender, RoutedEventArgs e)
        {
            _gameSessionViewModel.MoveBackward();
        }
    }
}