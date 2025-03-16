using Microsoft.AspNetCore.Mvc;
using SagaPattern.Models;
using SagaPattern.Services;

namespace SagaPattern.Controllers
{
    public class BookingController : Controller
    {
        private readonly BookingService _bookingService;
        private static List<BookingSaga> _sagas = new List<BookingSaga>();

        public BookingController(BookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public IActionResult StartBooking()
        {
            var saga = new BookingSaga();
            _sagas.Add(saga);

            // Start the saga by booking flight and hotel
            if (!_bookingService.BookFlight(saga))
            {
                _bookingService.CompensateFlight(saga);
                return View("BookingFailed", saga);
            }

            if (!_bookingService.BookHotel(saga))
            {
                _bookingService.CompensateFlight(saga);
                _bookingService.CompensateHotel(saga);
                return View("BookingFailed", saga);
            }

            saga.IsCompleted = true;
            return View("BookingSuccess", saga);
        }

        public IActionResult CancelBooking(int sagaId)
        {
            var saga = _sagas.FirstOrDefault(x => x.Id == sagaId);
            if (saga != null)
            {
                _bookingService.CompensateFlight(saga);
                _bookingService.CompensateHotel(saga);
                saga.IsCancelled = true;
                return View("BookingCancelled", saga);
            }

            return NotFound();
        }
    }

}
