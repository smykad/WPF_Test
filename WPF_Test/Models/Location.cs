using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Test.Models
{
    public class Location
    {
        #region ENUMS


        #endregion

        #region FIELDS

        private int _id; // must be a unique value for each object
        private string _name;
        private string _description;
        private bool _accessible;
        private int _requiredExperiencePoints;
        private int _modifiyExperiencePoints;
        private int _modifyHealth;
        private int _modifyLives;
        private string _message;

        #endregion


        #region PROPERTIES

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public bool Accessible
        {
            get { return _accessible; }
            set { _accessible = value; }
        }

        public int ModifiyExperiencePoints
        {
            get { return _modifiyExperiencePoints; }
            set { _modifiyExperiencePoints = value; }
        }

        public int RequiredExperiencePoints
        {
            get { return _requiredExperiencePoints; }
            set { _requiredExperiencePoints = value; }
        }

        public int ModifyHealth
        {
            get { return _modifyHealth; }
            set { _modifyHealth = value; }
        }

        public int RequiredItem { get; set; }


        public int ModifyLives
        {
            get { return _modifyLives; }
            set { _modifyLives = value; }
        }

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
        public ObservableCollection<GameItemQuantity> GameItems { get; set; }
      
        public ObservableCollection<Npc> Npcs { get; set; }
        #endregion



        #region CONSTRUCTORS

        public Location()
        {
            GameItems = new ObservableCollection<GameItemQuantity>();
        }

        #endregion


        #region METHODS

        //
        // location is open if character has enough XP
        //
        public bool IsAccessibleByExperiencePoints(int playerExperiencePoints)
        {
            return playerExperiencePoints >= _requiredExperiencePoints ? true : false;
        }

        public void UpdateLocationGameItems()
        {
            var updatedLocationGameItems = new ObservableCollection<GameItemQuantity>();

            foreach (var gameItemQuantity in GameItems)
            {
                updatedLocationGameItems.Add(gameItemQuantity);
            }

            GameItems.Clear();

            foreach (var gameItemQuantity in updatedLocationGameItems)
            {
                GameItems.Add(gameItemQuantity);
            }
        }

        public void AddGameItemQuantityToLocation(GameItemQuantity selectedGameItemQuantity)
        {
            var gameItemQuantity = GameItems.FirstOrDefault(i => i.GameItem.Id == selectedGameItemQuantity.GameItem.Id);

            if (gameItemQuantity == null)
            {
                var newGameItemQuantity = new GameItemQuantity
                {
                    GameItem = selectedGameItemQuantity.GameItem,
                    Quantity = 1
                };

                GameItems.Add(newGameItemQuantity);
            }
            else
            {
                gameItemQuantity.Quantity++;
            }

            UpdateLocationGameItems();
        }

        public void RemoveGameItemQuantityFromLocation(GameItemQuantity selectedGameItemQuantity, int quantity)
        {
            var gameItemQuantity = GameItems.FirstOrDefault(i => i.GameItem.Id == selectedGameItemQuantity.GameItem.Id);

            if (gameItemQuantity != null)
            {
                if (selectedGameItemQuantity.Quantity == quantity)
                {
                    GameItems.Remove(gameItemQuantity);
                }
            }

            UpdateLocationGameItems();
        }

        #endregion
    }
}
