using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Hotel_Management_System.RoomViewModel
{
    public class RoomViewModelClass
    {
        public int RoomId { get; set; }

        [Display(Name = "Room No.")]
       
        public int RoomNumber { get; set; }

        [Display(Name = "Room Image.")]
       
        public string RoomImage { get; set; }
        public HttpPostedFileBase Image { get; set; }

        [Display(Name = "Room Price.")]
       
        public decimal RoomPrice { get; set; }

        [Display(Name = "Room Status.")]
       
        public int BookingStatusid { get; set; }

        [Display(Name = "Room Type.")]
       
        public int RoomTypeid { get; set; }

        [Display(Name = "Room Capacity.")]
        
        public int RoomCapacity { get; set; }

        [Display(Name = "Room Description.")]
     
        public string RoomDescription { get; set; }



        public List<SelectListItem> ListofBookingStatus { get; set; }
        public List<SelectListItem> ListofRoomType { get; set; }

    }
}