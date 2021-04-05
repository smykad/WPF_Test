using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Test.Models;
using System.Windows.Threading;
using System.Media;
using static System.Net.Mime.MediaTypeNames;
using System.Windows;

namespace WPF_Test.PresentationLayer
{
    public class GameSessionViewModel : ObservableObject
    {
        #region ENUMS

        #endregion

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
                if (nextNorthLocation.Accessible == true || PlayerCanAccessLocation(nextNorthLocation))
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
                if (nextEastLocation.Accessible == true || PlayerCanAccessLocation(nextEastLocation))
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
                if (nextSouthLocation.Accessible == true || PlayerCanAccessLocation(nextSouthLocation))
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
                if (nextWestLocation.Accessible == true || PlayerCanAccessLocation(nextWestLocation))
                {
                    WestLocation = nextWestLocation;
                }
            }
        }

        /// <summary>
        /// **********************************************************
        ///                     LOCATION ACCESSABILITY
        /// **********************************************************
        /// </summary>
        /// <param name="nextLocation">location to check accessibility</param>
        /// <returns>accessibility</returns>
        private bool PlayerCanAccessLocation(Location nextLocation)
        {
            //
            // check access by experience points
            //
            if (nextLocation.IsAccessibleByExperiencePoints(_player.ExperiencePoints))
            {
                return true;
            }
            else
            {
                return false;
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
                    default:
                        break;
                }
            }
            catch (NullReferenceException)
            {
                CurrentMessage = "Sorry, there is no use for that!";
            }

        }

        private void ProcessPotionUse(Potion potion)
        {
            CurrentMessage = potion.UseMessage;
            Player.ExperiencePoints += potion.XpGain;
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
