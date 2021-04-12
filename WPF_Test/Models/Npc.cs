using System.Collections.ObjectModel;
using System.Linq;

namespace WPF_Test.Models
{
    public abstract class Npc : Character
    {
        public string Description { get; set; }

        //public int XpToGain { get; set; }
        public ObservableCollection<GameItemQuantity> GameItems { get; set; }
        public string Information => InformationText();

        

        public Npc()
        {
            GameItems = new ObservableCollection<GameItemQuantity>();
        }

        public Npc(int id, string name, RaceType race, string description)
            : base(name, race, id)
        {
            Id = id;
            Name = name;
            Race = race;
            Description = description;
            GameItems = new ObservableCollection<GameItemQuantity>();
        }

        protected abstract string InformationText();

        public void RemoveGameItemQuantityFromInventory(GameItemQuantity selectedGameItemQuantity)
        {
            var gameItemQuantity = GameItems.FirstOrDefault(i => i.GameItem.Id == selectedGameItemQuantity.GameItem.Id);

            if (gameItemQuantity != null)
            {
                if (selectedGameItemQuantity.Quantity == 1)
                {
                    GameItems.Remove(gameItemQuantity);
                }
                else
                {
                    gameItemQuantity.Quantity--;
                }
            }

            UpdateMerchantGameItems();
        }

        public void UpdateMerchantGameItems()
        {
            var updatedMerchantGameItems = new ObservableCollection<GameItemQuantity>();

            foreach (var gameItemQuantity in GameItems)
            {
                updatedMerchantGameItems.Add(gameItemQuantity);
            }

            GameItems.Clear();

            foreach (var gameItemQuantity in updatedMerchantGameItems)
            {
                GameItems.Add(gameItemQuantity);
            }
        }
    }
}
