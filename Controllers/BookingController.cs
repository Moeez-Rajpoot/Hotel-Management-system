using Hotel_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace Hotel_Management_System.Controllers
{
    public class BookingController : Controller
    {
        private HotelDBModel objHotelDB;

        public BookingController()
        {
            objHotelDB = new HotelDBModel();
        }
        public ActionResult Index()
        {
            BookingViewModel objBookingViewModel = new BookingViewModel();
            objBookingViewModel.ListofRooms = (from objRoom in objHotelDB.Rooms
                                               where objRoom.BookingStatusid == 2
                                               select new SelectListItem()
                                               {
                                                   Text = objRoom.RoomNumber.ToString(),
                                                   Value = objRoom.RoomId.ToString()
                                               }).ToList();
            //objBookingViewModel.BookingFrom = DateTime.Now;
            //objBookingViewModel.BookingTo = DateTime.Now.AddDays(1);
            return View(objBookingViewModel);
        }
        [HttpPost]
        public ActionResult Index(BookingViewModel objBookingViewModel)
        {
            int numberofdays = Convert.ToInt32(objBookingViewModel.BookingDays);
            Room obj = objHotelDB.Rooms.Single(model => model.RoomId == objBookingViewModel.AssignRoomId);
            decimal RoomPrice = obj.RoomPrice;
            decimal TotalPrice = RoomPrice* numberofdays;

            RoomBooking roomBooking = new RoomBooking()
            {
                BookingDays = objBookingViewModel.BookingDays,
                AssignRoomId = objBookingViewModel.AssignRoomId,
                CustomerAddress = objBookingViewModel.CustomerAddress,
                CustomerName = objBookingViewModel.CustomerName,
                CustomerPhoneNo= objBookingViewModel.CustomerPhoneNo,
                CustomerCnic= objBookingViewModel.CustomerCnic,
                NoOfMembers = objBookingViewModel.NoOfMembers,
                TotalAmount = TotalPrice
            };
            objHotelDB.RoomBookings.Add(roomBooking);
            objHotelDB.SaveChanges();
            obj.BookingStatusid = 3;
            objHotelDB.SaveChanges();
            return Json(new {message =" Hotel Booking Successfuly Created" , success = true}, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public PartialViewResult GetAllBookingHistory()
        {
            List<BookingViewModel> listofbookingviewmodel = new List<BookingViewModel>();
            listofbookingviewmodel = (from objHotelBooking in objHotelDB.RoomBookings
                                      join objRoom in objHotelDB.Rooms on objHotelBooking.AssignRoomId equals objRoom.RoomId
                                      select new BookingViewModel()
                                      {
                                          BookingDays = objHotelBooking.BookingDays,
                                          BookingId = objHotelBooking.BookingId,
                                          CustomerAddress = objHotelBooking.CustomerAddress,
                                          CustomerName = objHotelBooking.CustomerName,
                                          CustomerPhoneNo = objHotelBooking.CustomerPhoneNo,
                                          CustomerCnic = objHotelBooking.CustomerCnic,
                                          NoOfMembers = objHotelBooking.NoOfMembers,
                                          AssignRoomId= objHotelBooking.AssignRoomId,
                                          TotalAmount = objHotelBooking.TotalAmount
                                      }).ToList();
            return PartialView("_BookingHistoryPartial", listofbookingviewmodel);
        }
    }
}