using System;
using System.Collections.Generic;

namespace TravelPlaner;

public class TripInfo
{
    public string FullName { get; set; }
    public string Country { get; set; }
    public List<string> Attractions { get; set; } = new();
    public string Transport { get; set; }
    public List<string> Cities { get; set; } = new();
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string ImagePath { get; set; }

    public double EstimateCost()
    {
        double baseCost = 1000;
        double attractionCost = Attractions.Count * 100;
        double transportMultiplier = Transport switch
        {
            "Samolot" => 1.5,
            "Samochód" => 1.2,
            "Pociąg" => 1.1,
            "Statek" => 2.0,
            _ => 1.0
        };

        double duration = (EndDate - StartDate).TotalDays;
        return baseCost + (attractionCost + (Cities.Count * 150)) * transportMultiplier + duration * 50;
    }
}