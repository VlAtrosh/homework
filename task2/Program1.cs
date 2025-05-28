var bookingSystem = new BookingSystem();

// Создание бронирования отеля
var hotelReservation = (HotelReservation)bookingSystem.CreateReservation("Hotel");
hotelReservation.CustomerName = "John Doe";
hotelReservation.StartDate = DateTime.Today.AddDays(7);
hotelReservation.EndDate = DateTime.Today.AddDays(14);
hotelReservation.RoomType = "Deluxe";
hotelReservation.MealPlan = "Breakfast";

// Вывод информации о бронировании
hotelReservation.DisplayDetails();

// Получение общей стоимости всех бронирований
Console.WriteLine($"Total booking value: {bookingSystem.GetTotalBookingValue():C}");