using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;
using Shouldly;
using Xunit;

namespace RoomBookingApp.Core.Tests
{
    public class RoomBookingRequestProcesorTest
    {
        private RoomBookingRequestProcesor _processor;

        public RoomBookingRequestProcesorTest()
        {
            //Arrange
            _processor  = new RoomBookingRequestProcesor();
        }
        [Fact]
        public void Should_Return_Room_Booking_Response_With_Values()
        {
            //Arrange

            var request = new RoomBookingRequest
            {
                FullName = "Test Name",
                Email = "test@request.com",
                Date = new DateTime(2022, 10, 20)
            };

            //Act

            RoomBookingResult result = _processor.BookRoom(request);

            //Assert

            result.ShouldNotBeNull();
            result.FullName.ShouldBe(request.FullName);
            result.Email.ShouldBe(request.Email);
            result.Date.ShouldBe(request.Date);
        }

        [Fact]
        public void Should_Throw_Exception_For_Null_Request()
        {
            //Act
            var exception = Should.Throw<ArgumentNullException>(() => _processor.BookRoom(null));

            //Assert
            exception.ParamName.ShouldBe("bookingRequest");
        }
    }
}
