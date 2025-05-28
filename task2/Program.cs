// Базовый класс бронирования
public abstract class Reservation
{
    public string ReservationID { get; set; }
    public string CustomerName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public abstract decimal CalculatePrice();

    public virtual void DisplayDetails()
    {
        Console.WriteLine($"Reservation ID: {ReservationID}");
        Console.WriteLine($"Customer: {CustomerName}");
        Console.WriteLine($"Period: {StartDate:d} - {EndDate:d}");
        Console.WriteLine($"Total price: {CalculatePrice():C}");
    }
}

// Производные классы
public class HotelReservation : Reservation
{
    public string RoomType { get; set; }
    public string MealPlan { get; set; }

    public override decimal CalculatePrice()
    {
        int days = (EndDate - StartDate).Days;
        decimal basePrice = days * 100; // $100 per night base price

        // Adjust for room type
        decimal roomMultiplier = RoomType switch
        {
            "Standard" => 1.0m,
            "Deluxe" => 1.5m,
            "Suite" => 2.0m,
            _ => 1.0m
        };

        // Adjust for meal plan
        decimal mealPrice = MealPlan switch
        {
            "None" => 0,
            "Breakfast" => days * 15,
            "Full Board" => days * 40,
            _ => 0
        };

        return basePrice * roomMultiplier + mealPrice;
    }

    public override void DisplayDetails()
    {
        base.DisplayDetails();
        Console.WriteLine($"Room Type: {RoomType}");
        Console.WriteLine($"Meal Plan: {MealPlan}");
    }
}

public class FlightReservation : Reservation
{
    public string DepartureAirport { get; set; }
    public string ArrivalAirport { get; set; }

    public override decimal CalculatePrice()
    {
        // Simple pricing logic - could be much more complex
        decimal basePrice = 200;
        decimal distanceMultiplier = Math.Abs(ArrivalAirport.GetHashCode() - DepartureAirport.GetHashCode()) % 300 / 100m;
        return basePrice * (1 + distanceMultiplier);
    }

    public override void DisplayDetails()
    {
        base.DisplayDetails();
        Console.WriteLine($"Flight: {DepartureAirport} → {ArrivalAirport}");
    }
}

public class CarRentalReservation : Reservation
{
    public string CarType { get; set; }
    public bool InsuranceIncluded { get; set; }

    public override decimal CalculatePrice()
    {
        int days = (EndDate - StartDate).Days;
        decimal dailyRate = CarType switch
        {
            "Economy" => 30,
            "Compact" => 40,
            "SUV" => 70,
            "Luxury" => 120,
            _ => 50
        };

        decimal insuranceCost = InsuranceIncluded ? days * 15 : 0;

        return days * dailyRate + insuranceCost;
    }

    public override void DisplayDetails()
    {
        base.DisplayDetails();
        Console.WriteLine($"Car Type: {CarType}");
        Console.WriteLine($"Insurance: {(InsuranceIncluded ? "Included" : "Not included")}");
    }
}

// Система бронирования
public class BookingSystem
{
    private List<Reservation> _reservations = new List<Reservation>();
    private int _nextId = 1;

    public Reservation CreateReservation(string reservationType)
    {
        Reservation reservation = reservationType switch
        {
            "Hotel" => new HotelReservation(),
            "Flight" => new FlightReservation(),
            "Car" => new CarRentalReservation(),
            _ => throw new ArgumentException("Invalid reservation type")
        };

        reservation.ReservationID = $"RES-{_nextId++}";
        _reservations.Add(reservation);
        return reservation;
    }

    public bool CancelReservation(string reservationId)
    {
        var reservation = _reservations.FirstOrDefault(r => r.ReservationID == reservationId);
        if (reservation != null)
        {
            _reservations.Remove(reservation);
            return true;
        }
        return false;
    }

    public decimal GetTotalBookingValue()
    {
        return _reservations.Sum(r => r.CalculatePrice());
    }

    public void DisplayAllReservations()
    {
        foreach (var reservation in _reservations)
        {
            reservation.DisplayDetails();
            Console.WriteLine();
        }
    }
}