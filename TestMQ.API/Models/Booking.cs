﻿using System.ComponentModel.DataAnnotations;

namespace TestMQ.API.Models;

public class Booking
{
    [Key]
    public Guid Id { get; set; }
    
    public string PassengerName { get; set; }
    
    public string PasportNumber { get; set; }
    
    public string From { get; set; }
    
    public string To { get; set; }
    
    public int Status { get; set; }
}
