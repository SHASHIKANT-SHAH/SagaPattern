using SagaPattern.Models;

namespace SagaPattern.Services
{
    public class BookingService
    {
        private readonly List<BookingSaga> _sagas = new List<BookingSaga>();

        // Simulate booking flight
        public bool BookFlight(BookingSaga saga)
        {
            if (saga.IsCancelled)
            {
                return false;
            }

            saga.Steps.Add("FlightBooked");

            // Simulate failure for demo purposes
            var number = new Random().Next(2);
            if (number == 0)
            {
                Console.WriteLine("Flight booking failed");
                return false;
            }

            return true;
        }

        // Simulate booking hotel
        public bool BookHotel(BookingSaga saga)
        {
            if (saga.IsCancelled)
            {
                return false;
            }

            saga.Steps.Add("HotelBooked");

            // Simulate failure for demo purposes
            var number = new Random().Next(2);
            if (number == 0)
            {
                Console.WriteLine("Hotel booking failed");
                return false;
            }

            return true;
        }

        // Compensation for flight booking
        public void CompensateFlight(BookingSaga saga)
        {
            if (saga.Steps.Contains("FlightBooked"))
            {
                Console.WriteLine("Flight booking compensated");
                saga.Steps.Remove("FlightBooked");
            }
        }

        // Compensation for hotel booking
        public void CompensateHotel(BookingSaga saga)
        {
            if (saga.Steps.Contains("HotelBooked"))
            {
                Console.WriteLine("Hotel booking compensated");
                saga.Steps.Remove("HotelBooked");
            }
        }
    }

}
