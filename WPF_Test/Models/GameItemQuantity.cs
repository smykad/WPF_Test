using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Test.Models
{
    public class GameItemQuantity
    {
        #region Properties
        public GameItem GameItem { get; set; }
        public int Quantity { get; set; }
        #endregion

        #region Constructors
        public GameItemQuantity()
        {

        }
        public GameItemQuantity(GameItem gameItem, int quantity)
        {
            GameItem = gameItem;
            Quantity = quantity;
        }
        #endregion
    }
}
