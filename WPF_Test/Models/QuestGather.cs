using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Test.Models
{
    class QuestGather: Quest
    {
        #region PROPERTIES

        /// <summary>
        /// **********************************************************
        ///         Get/Set: Required Items
        /// **********************************************************
        /// </summary>
        public List<GameItemQuantity> RequiredGameItemQuantities { get; set; }

        #endregion

        #region METHODS

        /// <summary>
        /// **********************************************************
        ///             Game Items not yet Obtained
        /// **********************************************************
        /// </summary>
        /// <param name="inventory">items</param>
        /// <returns>Returns the items still needed for the Quest</returns>
        public List<GameItemQuantity> GameItemQuantitiesNotCompleted(List<GameItemQuantity> inventory)
        {
            var gameItemQuantitiesToComplete = new List<GameItemQuantity>();

            foreach (var questGameItem in RequiredGameItemQuantities)
            {
                var inventoryItemMatch =
                    inventory.FirstOrDefault(x => x.GameItem.Id == questGameItem.GameItem.Id);
                if (inventoryItemMatch == null)
                {
                    gameItemQuantitiesToComplete.Add(questGameItem);
                }
                else
                {
                    if (inventoryItemMatch.Quantity < questGameItem.Quantity)
                    {
                        gameItemQuantitiesToComplete.Add(questGameItem);
                    }
                }
            }

            return gameItemQuantitiesToComplete;
        }

        #endregion
    }
}
