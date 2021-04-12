using System.Collections.Generic;
using System.Linq;

namespace WPF_Test.Models
{
    class QuestTravel: Quest
    {
        #region Properties

        /// <summary>
        /// **********************************************************
        ///         Get/Set: Required Location
        /// **********************************************************
        /// </summary>
        public List<Location> RequiredLocations { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Determines locations not yet visited
        /// </summary>
        /// <param name="locationsTraveled">Locations Traveled</param>
        /// <returns>Returns locations that still need to be visited</returns>
        public List<Location> LocationsNotCompleted(List<Location> locationsTraveled)
        {
            return RequiredLocations.Where(requiredLocation => locationsTraveled.All(x => x.Id != requiredLocation.Id)).ToList();
        }

        #endregion
    }
}
