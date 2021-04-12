using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Test.Models
{
    class QuestEngage : Quest
    {
        #region PROPERTIES

        /// <summary>
        /// **********************************************************
        ///             Get/Set: Required Npcs
        /// **********************************************************
        /// </summary>
        public List<Npc> RequiredNpcs { get; set; }

        #endregion

        #region METHODS
        /// <summary>
        /// **********************************************************
        ///             List of NPC's not yet engaged
        /// **********************************************************
        /// </summary>
        /// <param name="npcsEngaged"></param>
        /// <returns></returns>
        public List<Npc> NpcsNotEngaged(List<Npc> npcsEngaged)
        {
            return RequiredNpcs.Where(requiredNpc => npcsEngaged.All(x => x.Id != requiredNpc.Id)).ToList();
        }

        #endregion
    }
}
