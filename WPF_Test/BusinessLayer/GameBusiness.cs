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
            InstantiateAndShowView();
        }
        private void SetupPlayer()
        {
            if (_newPlayer)
            {
                _playerSetupView = new PlayerSetupView(_player);
                _playerSetupView.ShowDialog();
                _player.ExperiencePoints = 0;
                _player.Health = 100;
                _player.Lives = 3;
                _player.SkillLevel = 5;
                _player.Inventory = new ObservableCollection<GameItemQuantity>()
                {
                    new GameItemQuantity(GameData.GameItemByID(101), 1),
                    new GameItemQuantity(GameData.GameItemByID(102), 1),
                    new GameItemQuantity(GameData.GameItemByID(103), 1)
                };
                _player.Quests = new ObservableCollection<Quest>()
                {
                    GameData.QuestById(1),
                    GameData.QuestById(2),
                    GameData.QuestById(3),
                    GameData.QuestById(4)
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
            
            _gameSessionViewModel = new GameSessionViewModel(
                _player,
                GameData.GameMap(),
                GameData.InitialGameMapLocation()
                );
            GameSessionView gameSessionView = new GameSessionView(_gameSessionViewModel);

            gameSessionView.DataContext = _gameSessionViewModel;

            gameSessionView.Show();
                        
            _playerSetupView.Close();
        }
    }
}
