using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Hotel_Management_System.Models
{
    public class BookingViewModel
    {
        public int BookingId { get; set; }
        [Display(Name ="Customer Name")]
        public string CustomerName { get; set; }
        [Display(Name = "Customer Address")]
        public string CustomerAddress { get; set; }
        [Display(Name = "Customer Phone No")]
        public string CustomerPhoneNo { get; set; }
        [Display(Name = "Customer CNIC No")]
        public string CustomerCnic { get; set; }
        [Display(Name = "Booking Days")]
        public string BookingDays { get; set; }
       
        [Display(Name = "Assign Room")]
        public int AssignRoomId { get; set; }
        [Display(Name = "No of Memebers")]
        public int NoOfMembers { get; set; }
        public decimal TotalAmount { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> ListofRooms { get; set; }    
    }
}