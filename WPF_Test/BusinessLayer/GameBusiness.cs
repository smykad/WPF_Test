using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Test.PresentationLayer;
using WPF_Test.DataLayer;
using WPF_Test.Models;
using System.Collections.ObjectModel;

namespace WPF_Test.BusinessLayer
{
    public class GameBusiness
    {
        bool _newPlayer = true;

        GameSessionViewModel _gameSessionViewModel;
        Player _player = new Player();
        PlayerSetupView _playerSetupView = null;

        public GameBusiness()
        {
            SetupPlayer();
            //InitializeDataSet();
            InstantiateAndShowView();
        }
        private void SetupPlayer()
        {
            if (_newPlayer)
            {
                _playerSetupView = new PlayerSetupView(_player);
                _playerSetupView.ShowDialog();

                //
                // setup up game based player properties
                //
                _player.ExperiencePoints = 0;
                _player.Health = 100;
                _player.Lives = 3;
                _player.SkillLevel = 5;
                _player.Inventory = new ObservableCollection<GameItemQuantity>()
                {
                    new GameItemQuantity(GameData.GameItemByID(101), 1),
                    new GameItemQuantity(GameData.GameItemByID(401), 25)
                };
            }
            else
            {
                InitializeDataSet();
            }
        }
        private void InitializeDataSet()
        {
            _player = GameData.PlayerData();
        }
        private void InstantiateAndShowView()
        {
            //
            // instantiate the view model and initialize the data set
            //
            _gameSessionViewModel = new GameSessionViewModel(
                _player,
                GameData.GameMap(),
                GameData.InitialGameMapLocation()
                );
            GameSessionView gameSessionView = new GameSessionView(_gameSessionViewModel);

            gameSessionView.DataContext = _gameSessionViewModel;

            gameSessionView.Show();

            //
            // dialog window is initially hidden to mitigate issue with
            // main window closing after dialog window closes
            _playerSetupView.Close();
        }
    }
}
