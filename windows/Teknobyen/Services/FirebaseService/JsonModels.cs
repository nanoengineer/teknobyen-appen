﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknobyen.Models;

namespace Teknobyen.Services.FirebaseService
{
    class ReservationRootModel
    {
        Dictionary<string, ReservationJsonModel> reservations { get; set; }
    }

    public class ReservationJsonModel
    {
        public ReservationJsonModel() { }
        public ReservationJsonModel(ProjectorReservationModel model)
        {
            userID = model.userId;
            name = model.name;
            roomNumber = model.roomNumber.ToString();
            comment = model.comment;
            date = model.date.ToString(App.DATEFORMAT);
            startTime = model.startTime.ToString(App.TIMEFORMAT);
            var usCulture = new CultureInfo("en-US");
            duration = (model.endTime.Subtract(model.startTime)).TotalHours.ToString(usCulture.NumberFormat);
        }

        public string userID { get; set; }
        public string comment { get; set; }
        public string name { get; set; }
        public string roomNumber { get; set; }
        public string date { get; set; }
        public string startTime { get; set; }
        public string duration { get; set; }
    }

    public class WashDayJsonModel
    {
        public WashDayJsonModel() { }

        public WashDayJsonModel(WashDayModel model)
        {
            this.date = model.Date.ToString("dd.MM.yyyy");
            this.assignment = model.Assignment;
            this.roomNumber = model.RoomNumber;
        }

        public string date { get; set; }
        public int assignment { get; set; }
        public int roomNumber { get; set; }
    }

}
