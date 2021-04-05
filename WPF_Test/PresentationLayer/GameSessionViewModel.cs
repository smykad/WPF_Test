using System;
using WPF_Test.Models;
using System.Windows.Threading;
using System.Media;
using System.Windows;

namespace WPF_Test.PresentationLayer
{
    public class GameSessionViewModel : ObservableObject
    {
        #region FIELDS

        private string _gameTimeDisplay;

        private DateTime _gameStartTime;
        private TimeSpan _gameTime;

        private string _currentMessage;

        private GameItemQuantity _currentGameItem;
        private Npc _currentNpc;

        
        private Player _player;
        private Map _gameMap;

        private Location _currentLocation;
        private Location _northLocation, _eastLocation, _southLocation, _westLocation;
        private string _currentLocationInformation;

        private Random random = new Random();

        #endregion

        #region PROPERTIES

        public string MissionTimeDisplay
        {
            get { return _gameTimeDisplay; }
            set
            {
                _gameTimeDisplay = value;
                OnPropertyChanged(nameof(MissionTimeDisplay));
            }
        }
        public string CurrentMessage
        {
            get { return _currentMessage; }
            set 
            { 
                _currentMessage = value;
                OnPropertyChanged(nameof(CurrentMessage));
            }
        }
        public string MessageDisplay
        {
            get { return _currentLocation.Message; }
        }
        public string CurrentLocationInformation
        {
            get { return _currentLocationInformation; }
            set
            {
                _currentLocationInformation = value;
                OnPropertyChanged(nameof(CurrentLocationInformation));
            }
        }
        public GameItemQuantity CurrentGameItem
        {
            get { return _currentGameItem; }
            set
            {
                _currentGameItem = value;
                OnPropertyChanged(nameof(CurrentGameItem));
            }
        }
        public Npc CurrentNpc
        {
            get => _currentNpc;
            set
            {
                _currentNpc = value;
                OnPropertyChanged(nameof(CurrentNpc));
            }
        }
        public Player Player
        {
            get { return _player; }
            set { _player = value; }
        }
        public Map GameMap
        {
            get { return _gameMap; }
            set { _gameMap = value; }
        }


        #region Locations
        public Location CurrentLocation
        {
            get { return _currentLocation; }
            set
            {
                _currentLocation = value;
                OnPropertyChanged(nameof(CurrentLocation));
            }
        }
        public Location NorthLocation
        {
            get { return _northLocation; }
            set
            {
                _northLocation = value;
                OnPropertyChanged(nameof(NorthLocation));
                OnPropertyChanged(nameof(HasNorthLocation));
            }
        }

        public Location EastLocation
        {
            get { return _eastLocation; }
            set
            {
                _eastLocation = value;
                OnPropertyChanged(nameof(EastLocation));
                OnPropertyChanged(nameof(HasEastLocation));
            }
        }

        public Location SouthLocation
        {
            get { return _southLocation; }
            set
            {
                _southLocation = value;
                OnPropertyChanged(nameof(SouthLocation));
                OnPropertyChanged(nameof(HasSouthLocation));
            }
        }
        public Location WestLocation
        {
            get { return _westLocation; }
            set
            {
                _westLocation = value;
                OnPropertyChanged(nameof(WestLocation));
                OnPropertyChanged(nameof(HasWestLocation));
            }
        }
        public bool HasNorthLocation { get { return NorthLocation != null; } }
        public bool HasEastLocation { get { return EastLocation != null; } }
        public bool HasSouthLocation { get { return SouthLocation != null; } }
        public bool HasWestLocation { get { return WestLocation != null; } }

        #endregion 

        #endregion

        #region CONSTRUCTORS

        public GameSessionViewModel()
        {

        }

        public GameSessionViewModel(
            Player player,
            Map gameMap,
            GameMapCoordinates currentLocationCoordinates)
        {
            _player = player;

            _gameMap = gameMap;
            _gameMap.CurrentLocationCoordinates = currentLocationCoordinates;
            _currentLocation = _gameMap.CurrentLocation;
            InitializeView();
            BackgroundMusic();
            GameTimer();
        }

        #endregion

        #region METHODS

        #region Music
        /// <summary>
        /// **********************************************************
        ///                     MUSIC PLAYER METHOD
        /// **********************************************************
        /// </summary>
        private static void BackgroundMusic()
        {
            var backgroundMusic = new SoundPlayer("PresentationLayer/Resources/background.wav");
            backgroundMusic.Load();
            backgroundMusic.PlayLooping();
        }
        #endregion

        #region Timer
        /// <summary>
        /// **********************************************************
        ///             GAME TIMER
        /// **********************************************************
        /// </summary>
        public void GameTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += OnGameTimerTick;
            timer.Start();
        }
        #endregion

        #region Movement Methods 
        /// <summary>
        /// **********************************************************
        ///                     AVAILABLE TRAVEL POINTS
        /// **********************************************************
        /// </summary>
        private void UpdateAvailableTravelPoints()
        {
            //
            // reset travel location information
            //
            NorthLocation = null;
            EastLocation = null;
            SouthLocation = null;
            WestLocation = null;

            //
            // north location exists
            //
            if (_gameMap.NorthLocation() != null)
            {
                Location nextNorthLocation = _gameMap.NorthLocation();

                //
                // location generally accessible or player has required conditions
                // 
                if (nextNorthLocation.Accessible == true )
                {
                    NorthLocation = nextNorthLocation;
                }
            }

            //
            // east location exists
            //
            if (_gameMap.EastLocation() != null)
            {
                Location nextEastLocation = _gameMap.EastLocation();

                //
                // location generally accessible or player has required conditions
                //
                if (nextEastLocation.Accessible == true )
                {
                    EastLocation = nextEastLocation;
                }
            }

            //
            // south location exists
            //
            if (_gameMap.SouthLocation() != null)
            {
                Location nextSouthLocation = _gameMap.SouthLocation();

                //
                // location generally accessible or player has required conditions
                //
                if (nextSouthLocation.Accessible == true )
                {
                    SouthLocation = nextSouthLocation;
                }
            }

            //
            // west location exists
            //
            if (_gameMap.WestLocation() != null)
            {
                Location nextWestLocation = _gameMap.WestLocation();

                //
                // location generally accessible or player has required conditions
                //
                if (nextWestLocation.Accessible == true )
                {
                    WestLocation = nextWestLocation;
                }
            }
        }

        /// <summary>
        /// **********************************************************
        ///                     ON PLAYER MOVE
        /// **********************************************************
        /// </summary>
        private void OnPlayerMove()
        {
            //
            // update player stats when in new location
            //
            if (!_player.HasVisited(_currentLocation))
            {
                //
                // add location to list of visited locations
                //
                _player.LocationsVisited.Add(_currentLocation);

                // 
                // update experience points
                //
                _player.ExperiencePoints += _currentLocation.ModifiyExperiencePoints;

                //
                // update health
                //
                _player.Health += _currentLocation.ModifyHealth;

                //
                // update lives
                //
                if (_currentLocation.ModifyLives != 0) _player.Lives += _currentLocation.ModifyLives;

                //
                // display a new message if available
                //
                OnPropertyChanged(nameof(MessageDisplay));
            }
            OnPropertyChanged(nameof(MessageDisplay));
        }
        #region MOVEMENT
        /// <summary>
        /// **********************************************************
        ///                     MOVE NORTH
        /// **********************************************************
        /// </summary>
        public void MoveNorth()
        {
            if (HasNorthLocation)
            {
                _gameMap.MoveNorth();
                CurrentLocation = _gameMap.CurrentLocation;
                UpdateAvailableTravelPoints();
                OnPlayerMove();
            }
        }

        /// <summary>
        /// **********************************************************
        ///                     MOVE EAST
        /// **********************************************************
        /// </summary>
        public void MoveEast()
        {
            if (HasEastLocation)
            {
                _gameMap.MoveEast();
                CurrentLocation = _gameMap.CurrentLocation;
                UpdateAvailableTravelPoints();
                OnPlayerMove();
            }
        }

        /// <summary>
        /// **********************************************************
        ///                     MOVE SOUTH
        /// **********************************************************
        /// </summary>
        public void MoveSouth()
        {
            if (HasSouthLocation)
            {
                _gameMap.MoveSouth();
                CurrentLocation = _gameMap.CurrentLocation;
                UpdateAvailableTravelPoints();
                OnPlayerMove();
            }
        }

        /// <summary>
        /// **********************************************************
        ///                     MOVE WEST
        /// **********************************************************
        /// </summary>
        public void MoveWest()
        {
            if (HasWestLocation)
            {
                _gameMap.MoveWest();
                CurrentLocation = _gameMap.CurrentLocation;
                UpdateAvailableTravelPoints();
                OnPlayerMove();
            }
        }
        #endregion

        #endregion

        #region GAME TIME METHODS

        /// <summary>
        /// **********************************************************
        ///                     RUNNING TIME
        /// **********************************************************
        /// </summary>
        /// <returns></returns>
        private TimeSpan GameTime()
        {
            return DateTime.Now - _gameStartTime;
        }

        /// <summary>
        /// **********************************************************
        ///                 GAME TIMER EVENT HANDLER
        ///             1) UPDATE PLAYED TIME ON WINDOW
        /// **********************************************************
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnGameTimerTick(object sender, EventArgs e)
        {
            _gameTime = DateTime.Now - _gameStartTime;
            MissionTimeDisplay = "Time Played " + _gameTime.ToString(@"hh\:mm\:ss");
        }

        #endregion

        #region INITIALIZATION
        /// <summary>
        /// **********************************************************
        ///             INITIAL SETUP OF THE GAME SESSION VIEW
        /// **********************************************************
        /// </summary>
        private void InitializeView()
        {
            _gameStartTime = DateTime.Now;
            UpdateAvailableTravelPoints();
            _currentLocationInformation = CurrentLocation.Description;
            Player.UpdateInventory();
            Player.CalculateWealth();
        }

        public void QuestWindow()
        {
            var newVm = new QuestStatusViewModel
            {

            };
            var questStatus = new QuestStatusView(newVm);
            questStatus.Show();

        }
        #endregion

        #region INVENTORY
        public void AddItemToInventory()
        {
            if (CurrentGameItem == null || !_currentLocation.GameItems.Contains(CurrentGameItem)) return;
            var selectedGameItemQuantity = CurrentGameItem;

            _currentLocation.RemoveGameItemQuantityFromLocation(selectedGameItemQuantity, selectedGameItemQuantity.Quantity);
            Player.AddGameItemQuantityToInventory(selectedGameItemQuantity, selectedGameItemQuantity.Quantity);

            OnPlayerPickUp(selectedGameItemQuantity);
        }
        public void RemoveItemFromInventory()
        {
            if (CurrentGameItem == null) return;
            var selectedGameItemQuantity = CurrentGameItem;

            _currentLocation.AddGameItemQuantityToLocation(selectedGameItemQuantity);
            Player.RemoveGameItemQuantityFromInventory(selectedGameItemQuantity);

            OnPlayerPutDown(selectedGameItemQuantity);
        }

        private void OnPlayerPickUp(GameItemQuantity gameItemQuantity)
        {
            Player.Wealth += gameItemQuantity.GameItem.Value;
        }
        private void OnPlayerPutDown(GameItemQuantity gameItemQuantity)
        {
            Player.Wealth -= gameItemQuantity.GameItem.Value;
        }

        public void OnUseGameItem()
        {
            try
            {
                switch (CurrentGameItem.GameItem)
                {
                    case Potion potion:
                        ProcessPotionUse(potion);
                        break;
                    case Armor armor:
                        ProcessArmor(armor);
                        break;
                    case Relic relic:
                        ProcessRelicUse(relic);
                        break;
                    default:
                        break;
                }
            }
            catch (NullReferenceException)
            {
                CurrentMessage = "Sorry, there is no use for that!";
            }

        }
        private void ProcessRelicUse(Relic relic)
        {
            string message;

            switch (relic.UseAction)
            {
                case Relic.UseActionType.OPENLOCATION:
                    message = _gameMap.OpenLocationsByRelic(relic.Id);
                    CurrentMessage = relic.UseMessage;
                    UpdateAvailableTravelPoints();
                    break;
                default:
                    break;
            }
        }

        private void ProcessPotionUse(Potion potion)
        {
            CurrentMessage = potion.UseMessage;
            Player.ExperiencePoints += potion.XpGain;
            Player.Health += potion.HealthChange;
            Player.RemoveGameItemQuantityFromInventory(CurrentGameItem);
            Player.Wealth -= potion.Value;
        }
        private void ProcessArmor(Armor armor)
        {
            CurrentMessage = armor.UseMessage;
            Player.ExperiencePoints += armor.XpGain;
            Player.RemoveGameItemQuantityFromInventory(CurrentGameItem);
            Player.Wealth -= armor.Value;
        }
        #endregion

        #region NPC METHODS
        public void OnPlayerTalkTo()
        {
            if (CurrentNpc != null && CurrentNpc is ISpeak)
            {
                ISpeak speakingNpc = CurrentNpc as ISpeak;
                CurrentMessage = speakingNpc.Speak();
            }
        }
        #endregion

        #region BATTLE METHODS

        /// <summary>
        /// handle the attack event in the view.
        /// </summary>
        public void OnPlayerAttack()
        {
            _player.BattleMode = BattleModeName.ATTACK;
            Battle();
        }

        /// <summary>
        /// handle the defend event in the view.
        /// </summary>
        public void OnPlayerDefend()
        {
            _player.BattleMode = BattleModeName.DEFEND;
            Battle();
        }

        /// <summary>
        /// handle the retreat event in the view.
        /// </summary>
        public void OnPlayerRetreat()
        {
            _player.BattleMode = BattleModeName.RETREAT;
            Battle();
        }

        private void Battle()
        {

            if(_currentNpc is IBattle)
            {
                IBattle battleNpc = _currentNpc as IBattle;
                int playerHitPoints = 0;
                int battleNpcHitPoints = 0;
                string battleInformation = "";

                playerHitPoints = CalculatePlayerHitPoints();
                battleNpcHitPoints = CalculateNpcHitPoints(battleNpc);

                battleInformation +=
                    $"Player: {_player.BattleMode}     Hit Points: {playerHitPoints}" + Environment.NewLine +
                    $"NPC: {battleNpc.BattleMode}     Hit Points: {battleNpcHitPoints}" + Environment.NewLine;
                if (playerHitPoints >= battleNpcHitPoints)
                {
                    battleInformation += $"You have slain {_currentNpc.Name}.";
                    _currentLocation.Npcs.Remove(_currentNpc);
                }
                else
                {
                    battleInformation += $"You have been slain by {_currentNpc.Name}.";
                    _player.Lives--;
                }

                CurrentMessage = battleInformation;
                _player.Health = playerHitPoints;
                if (_player.Lives <= 0) OnPlayerDies("You have been slain and have no lives left.");

            }
            else
            {
                CurrentMessage = "The current NPC is not an enemy, you may not engage in combat";
            }
        }

        private int CalculatePlayerHitPoints()
        {
            int playerHitPoints = 0;

            switch (_player.BattleMode)
            {
                case BattleModeName.ATTACK:
                    playerHitPoints = _player.Attack();
                    break;
                case BattleModeName.DEFEND:
                    playerHitPoints = _player.Defend();
                    break;
                case BattleModeName.RETREAT:
                    playerHitPoints = _player.Retreat();
                    break;
            }

            return playerHitPoints;
        }

        private int CalculateNpcHitPoints(IBattle battleNpc)
        {
            int battleNpcHitPoints = 0;

            switch (NpcBattleResponse())
            {
                case BattleModeName.ATTACK:
                    battleNpcHitPoints = battleNpc.Attack();
                    break;
                case BattleModeName.DEFEND:
                    battleNpcHitPoints = battleNpc.Defend();
                    break;
                case BattleModeName.RETREAT:
                    battleNpcHitPoints = battleNpc.Retreat();
                    break;
            }

            return battleNpcHitPoints;
        }

        private BattleModeName NpcBattleResponse()
        {
            BattleModeName npcBattleResponse = BattleModeName.RETREAT;

            switch (DieRoll(3))
            {
                case 1:
                    npcBattleResponse = BattleModeName.ATTACK;
                    break;
                case 2:
                    npcBattleResponse = BattleModeName.DEFEND;
                    break;
                case 3:
                    npcBattleResponse = BattleModeName.RETREAT;
                    break;
            }
            return npcBattleResponse;
        }
        #endregion

        #region FILE MENU ACTIONS
        public void QuitApplication()
        {
            Environment.Exit(0);
        }

        public void ResetPlayer()
        {
            InitializeView();
        }
        #endregion

        #region PLAYER WIN/LOSE
        private void OnPlayerDies(string message)
        {
            var messagetext = message +
                              "\n\nWould you like to play again?";

            var titleText = "Death";
            const MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxResult result = MessageBox.Show(messagetext, titleText, button);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    ResetPlayer();
                    break;
                case MessageBoxResult.No:
                    QuitApplication();
                    break;
            }
        }
        #endregion

        #region HELPER METHODS
        private int DieRoll(int sides)
        {
            return random.Next(1, sides + 1);
        }
        #endregion

        #endregion

        #region EVENTS

        #endregion
    }
}
