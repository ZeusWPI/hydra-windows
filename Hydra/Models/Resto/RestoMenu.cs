using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.Models.Resto {
    /// <summary>
    /// A RestoMenu contains information on a specific kind of food in the resto's.
    /// Subclasses can for example keep track of the daily menus and the sandwiches.
    /// In a later stage, we can maybe add resto-specific items (the default snacks,
    /// special treatment for Sint-Jansvest, or even Het Pand).
    /// </summary>
    public interface RestoMenu {
    }
}
