using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace WPF_Test.Models
{
    public class Player : Character
    {
        #region ENUMS

        public enum JobTitleName { Warrior, Archer, Wizard }

        #endregion

        private const int DEFENDER_DAMAGE_ADJUSTMENT = 5;
        private const int MAXIMUM_RETREAT_DAMAGE = 10;

        #region FIELDS

        private int _lives;
        private int _health;
        private int _experiencePoints;
        private int _wealth;
        private int _skillLevel;
        private BattleModeName _battleMode;
        private JobTitleName _jobTitle;
        private List<Location> _locationsVisited;

        public ObservableCollection<GameItemQuantity> Inventory { get; set; }
        public ObservableCollection<GameItemQuantity> Potions { get; set; }
        public ObservableCollection<GameItemQuantity> Armor { get; set; }

        public ObservableCollection<GameItemQuantity> Relics { get; set; }



        #endregion

        #region PROPERTIES

        public int Lives
        {
            get { return _lives; }
            set
            {
                _lives = value;
                OnPropertyChanged(nameof(Lives));
            }
        }

        public JobTitleName JobTitle
        {
            get { return _jobTitle; }
            set
            {
                _jobTitle = value;
                OnPropertyChanged(nameof(JobTitle));
            }
        }

        public int Health
        {
            get { return _health; }
            set
            {
                _health = value;

                if (_health > 100)
                {
                    _health = 100;
                }
                else if (_health <= 0)
                {
                    _health = 100;
                    _lives--;
                }

                OnPropertyChanged(nameof(Health));
            }
        }

        public int ExperiencePoints
        {
            get { return _experiencePoints; }
            set
            {
                _experiencePoints = value;
                OnPropertyChanged(nameof(ExperiencePoints));
            }
        }
        public int SkillLevel
        {
            get { return _skillLevel; }
            set { _skillLevel = value; }
        }

        public int Wealth
        {
            get => _wealth;
            set
            {
                _wealth = value;
                OnPropertyChanged(nameof(Wealth));
            }
        }

        public List<Location> LocationsVisited
        {
            get { return _locationsVisited; }
            set { _locationsVisited = value; }
        }

        #endregion

        #region CONSTRUCTORS

        public Player()
        {
            _locationsVisited = new List<Location>();
            Potions = new ObservableCollection<GameItemQuantity>();
            Armor = new ObservableCollection<GameItemQuantity>();
            Inventory = new ObservableCollection<GameItemQuantity>();
            Relics = new ObservableCollection<GameItemQuantity>();
        }

        #endregion

        #region METHODS
        /// <summary>
        /// **********************************************************
        ///             HAS VISITED
        /// **********************************************************
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public bool HasVisited(Location location)
        {
            return _locationsVisited.Contains(location);
        }

        /// <summary>
        /// override the default greeting in the Character class to include the job title
        /// set the proper article based on the job title
        /// </summary>
        /// <returns>default greeting</returns>
        public override string DefaultGreeting()
        {
            string article = "a";

            List<string> vowels = new List<string>() { "A", "E", "I", "O", "U" };

            if (vowels.Contains(_jobTitle.ToString().Substring(0, 1))) ;
            {
                article = "an";
            }

            return $"Hello, my name is {_name} and I am {article} {_jobTitle} for the Aion Project.";
        }

        public void CalculateWealth()
        {
            Wealth = Inventory.Sum(i => i.GameItem.Value * i.Quantity);
        }

        #region INVENTORY METHODS

        /// <summary>
        /// **********************************************************
        ///                 UPDATE INVENTORY
        /// **********************************************************
        /// </summary>
        public void UpdateInventory()
        {
            Potions.Clear();
            Armor.Clear();
            Relics.Clear();

            foreach (var gameItemQuantity in Inventory)
            {
                if (gameItemQuantity.GameItem is Potion) Potions.Add(gameItemQuantity);
                if (gameItemQuantity.GameItem is Armor) Armor.Add(gameItemQuantity);
                if (gameItemQuantity.GameItem is Relic) Relics.Add(gameItemQuantity);
            }
        }
        /// <summary>
        /// **********************************************************
        ///                 ADD GAME ITEM
        /// **********************************************************
        /// </summary>
        /// <param name="selectedGameItemQuantity"></param>
        /// <param name="quantity"></param>
        public void AddGameItemQuantityToInventory(GameItemQuantity selectedGameItemQuantity, int quantity)
        {
            var gameItemQuantity = Inventory.FirstOrDefault(i => i.GameItem.Id == selectedGameItemQuantity.GameItem.Id);

            if (gameItemQuantity == null)
            {
                var newGameItemQuantity = new GameItemQuantity
                {
                    GameItem = selectedGameItemQuantity.GameItem,
                    Quantity = quantity
                };

                Inventory.Add(newGameItemQuantity);
            }
            else
            {
                gameItemQuantity.Quantity += quantity;
            }

            UpdateInventory();
        }

        /// <summary>
        /// **********************************************************
        ///             REMOVE GAME ITEM
        /// **********************************************************
        /// </summary>
        /// <param name="selectedGameItemQuantity"></param>
        public void RemoveGameItemQuantityFromInventory(GameItemQuantity selectedGameItemQuantity)
        {
            var gameItemQuantity = Inventory.FirstOrDefault(i => i.GameItem.Id == selectedGameItemQuantity.GameItem.Id);

            if (gameItemQuantity != null)
            {
                if (selectedGameItemQuantity.Quantity == 1)
                {
                    Inventory.Remove(gameItemQuantity);
                }
                else
                {
                    gameItemQuantity.Quantity--;
                }
            }

            UpdateInventory();
        }
        #endregion
        

        #region BattleMethods

        public int Attack()
        {
            int hitPoints = random.Next(5, 25) * _skillLevel;

            if (hitPoints <= 100)
            {
                return hitPoints;
            }
            else
            {
                return 100;
            }
        }
        public int Defend()
        {
            int hitPoints = (random.Next(5, 25) * _skillLevel) - DEFENDER_DAMAGE_ADJUSTMENT;

            if (hitPoints >= 0 && hitPoints <= 100)
            {
                return hitPoints;
            }
            else if (hitPoints > 100)
            {
                return 100;
            }
            else
            {
                return 0;
            }
        }
        public int Retreat()
        {
            int hitPoints = _skillLevel * MAXIMUM_RETREAT_DAMAGE;

            if (hitPoints <= 100)
            {
                return hitPoints;
            }
            else
            {
                return 100;
            }
        }

        #endregion

        #endregion

        #region EVENTS



        #endregion
    }
}
