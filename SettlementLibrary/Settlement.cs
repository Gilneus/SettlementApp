//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SettlementLibrary
{
    using System;
    using System.Collections.Generic;
    
    public partial class Settlement
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string TimeperiodAbsolute { get; set; }
        public Nullable<int> TimeperiodRelative { get; set; }
        public Nullable<int> NumberBuildings { get; set; }
        public Nullable<int> ActivityYears { get; set; }
    }
}
