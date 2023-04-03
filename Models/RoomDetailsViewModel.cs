using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Hotel_Management_System.Models
{
    public class RoomDetailsViewModel
    {

        public int RoomId { get; set; }

       
        public int RoomNumber { get; set; }

      
        public string RoomImage { get; set; }
       

    
        public decimal RoomPrice { get; set; }

     
        public string BookingStatus { get; set; }

   
        public string RoomType { get; set; }

 
        public int RoomCapacity { get; set; }


        public string RoomDescription { get; set; }

    }
}