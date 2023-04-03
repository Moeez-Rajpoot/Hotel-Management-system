using Hotel_Management_System.Models;
using Hotel_Management_System.RoomViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.WebPages.Html;

namespace Hotel_Management_System.Controllers
{
    public class RoomController : Controller
    {

        private HotelDBModel objhotelDBModel;
        public  RoomController(){
            objhotelDBModel = new HotelDBModel();

        }

        // GET: Room
        public ActionResult Index()
        {
            RoomViewModelClass roomViewModel = new RoomViewModelClass();
            roomViewModel.ListofBookingStatus = (from obj in objhotelDBModel.BookingStatus
                                                      select new System.Web.Mvc.SelectListItem() 
                                                      {
                                                      Text = obj.BookingStatus,
                                                      Value = obj.BookingStatusid.ToString()
                                           
                                                      }
                                                      ).ToList();

            roomViewModel.ListofRoomType = (from obj in objhotelDBModel.Roomtypes
                                                      select new System.Web.Mvc.SelectListItem()
                                                      {
                                                          Text = obj.RoomTypeName,
                                                          Value = obj.RoomTypeid.ToString()

                                                      }
                                          ).ToList();


            return View(roomViewModel);
        }
        [HttpPost]
        public ActionResult Index(RoomViewModelClass obj)
        {

            string message = string.Empty;
            string ImageUniqueName = string.Empty;
            string ActualImageName = string.Empty;
            if (obj.RoomId == 0)
            {
                 ImageUniqueName = Guid.NewGuid().ToString();
                 ActualImageName = ImageUniqueName + Path.GetExtension(obj.Image.FileName);
                obj.Image.SaveAs(Server.MapPath("~/RoomImages/" + ActualImageName));
                Room objRoom = new Room()
                {
                    RoomNumber = obj.RoomNumber,
                    RoomPrice = obj.RoomPrice,
                    RoomCapacity = obj.RoomCapacity,
                    RoomDescription = obj.RoomDescription,
                    RoomTypeid = obj.RoomTypeid,
                    BookingStatusid = obj.BookingStatusid,
                    IsActive = true,
                    RoomImage = ActualImageName


                };
                objhotelDBModel.Rooms.Add(objRoom);
                message = "Added";
            }
            else
            {
                Room objroom = objhotelDBModel.Rooms.Single(model => model.RoomId == obj.RoomId);
                if (obj.Image != null)
                {
                     ImageUniqueName = Guid.NewGuid().ToString();
                     ActualImageName = ImageUniqueName + Path.GetExtension(obj.Image.FileName);

                    obj.Image.SaveAs(Server.MapPath("~/RoomImages/" + ActualImageName));
                    objroom.RoomImage = ActualImageName;


                }




                objroom.RoomNumber = obj.RoomNumber;
                objroom.RoomPrice = obj.RoomPrice;
                objroom.RoomCapacity = obj.RoomCapacity;
                objroom.RoomDescription = obj.RoomDescription;
                objroom.RoomTypeid = obj.RoomTypeid;
                objroom.BookingStatusid = obj.BookingStatusid;
                objroom.IsActive = true;
                message = "Updated"; 
            }
           
            objhotelDBModel.SaveChanges();
            return Json(new { message = "Room SuccessFully "+ message, success = true }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetAllRooms()
        {
            IEnumerable<RoomDetailsViewModel> listofroomdetailsviewmodel =
                (from objRoom in objhotelDBModel.Rooms
                 join objBooking in objhotelDBModel.BookingStatus on objRoom.BookingStatusid equals objBooking.BookingStatusid
                 join objRoomType in objhotelDBModel.Roomtypes on objRoom.RoomTypeid equals objRoomType.RoomTypeid
                 where objRoom.IsActive== true
                 select new RoomDetailsViewModel()
                 {
                     RoomNumber = objRoom.RoomNumber,
                     RoomPrice = objRoom.RoomPrice, 
                     RoomCapacity = objRoom.RoomCapacity,   
                     RoomDescription = objRoom.RoomDescription,
                     BookingStatus = objBooking.BookingStatus,
                     RoomType= objRoomType.RoomTypeName,
                     RoomImage = objRoom.RoomImage,
                     RoomId= objRoom.RoomId
                 }).ToList();
            return PartialView("_RoomDetailsPartial", listofroomdetailsviewmodel );
        }
        [HttpGet]
        public JsonResult EditRoomDetails(int roomId)
        {
            var result = objhotelDBModel.Rooms.Single(model => model.RoomId == roomId);
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DeleteRoomDetails(int roomId)
        {
            Room objRoom = objhotelDBModel.Rooms.Single(model => model.RoomId==roomId);
            objRoom.IsActive= false;
            objhotelDBModel.SaveChanges();
            return Json(new { message = "Successfull Deactivated", success = true }, JsonRequestBehavior.AllowGet);

        }
    }
}