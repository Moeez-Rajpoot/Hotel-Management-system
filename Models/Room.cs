//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hotel_Management_System.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Room
    {
        public int RoomId { get; set; }
        public int RoomNumber { get; set; }
        public string RoomImage { get; set; }
        public decimal RoomPrice { get; set; }
        public int BookingStatusid { get; set; }
        public int RoomTypeid { get; set; }
        public int RoomCapacity { get; set; }
        public string RoomDescription { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}
