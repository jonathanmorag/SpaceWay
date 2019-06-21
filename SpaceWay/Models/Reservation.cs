﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SpaceWay.Models
{
    public class Reservation
    {
        public int ReservationID { get; set; }

        public int PassengerID { get; set; }
        public Passenger Passenger { get; set; }
        public DateTime OrderDate { get; set; }

        //[ForeignKey("Flight")]
        public int OutboundID { get; set; }
        public Flight Outbound { get; set; }

        //[ForeignKey("Flight")]
        public int InboundID { get; set; }
        public Flight Inbound { get; set; }

        public int NumOfTickets { get; set; }
        public double TotalPrice { get; set; }

    }
}