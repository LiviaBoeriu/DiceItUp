//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DiceItUp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Match
    {
        public int match_id { get; set; }
        public Nullable<int> first_player_id { get; set; }
        public Nullable<int> second_player_id { get; set; }
        public string match_state { get; set; }
        public string first_player_state { get; set; }
        public string second_player_state { get; set; }
    
        public virtual PlayerLogin FirstPlayer { get; set; }
        public virtual PlayerLogin SecondPlayer { get; set; }
    }
}
