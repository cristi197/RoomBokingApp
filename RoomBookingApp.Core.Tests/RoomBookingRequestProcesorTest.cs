using Moq;
using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Domain;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;
using Shouldly;
using Xunit;

namespace RoomBookingApp.Core.Tests
{
    public class RoomBooking_requestProcesorTest
    {
        private RoomBookingRequestProcesor _processor;
        private RoomBookingRequest _request;
        private Mock<IRoomBookingService> _roomBookingServiceMock;

        public RoomBooking_requestProcesorTest()
        {
            //Arrange
            _request = new RoomBookingRequest
            {
                FullName = "Test Name",
                Email = "test@_request.com",
                Date = new DateTime(2022, 10, 20)
            };

            _roomBookingServiceMock = new Mock<IRoomBookingService>();
            _processor = new RoomBookingRequestProcesor(_roomBookingServiceMock.Object);

        }
        [Fact]
        public void Should_Return_Room_Booking_Response_With_Values()
        {
            //Act

            RoomBookingResult result = _processor.BookRoom(_request);

            //Assert

            result.ShouldNotBeNull();
            result.FullName.ShouldBe(_request.FullName);
            result.Email.ShouldBe(_request.Email);
            result.Date.ShouldBe(_request.Date);
        }

        [Fact]
        public void Should_Throw_Exception_For_Null__request()
        {
            //Act
            var exception = Should.Throw<ArgumentNullException>(() => _processor.BookRoom(null));

            //Assert
            exception.ParamName.ShouldBe("bookingRequest");
        }

        [Fact]
        public void Should_Save_Room_Booking_Request()
        {
            RoomBooking savedBooking = null;
            _roomBookingServiceMock.Setup(q => q.Save(It.IsAny<RoomBooking>()))
                .Callback<RoomBooking>(booking =>
                {
                    savedBooking = booking;
                });

            _processor.BookRoom(_request);

            _roomBookingServiceMock.Verify(q=>q.Save(It.IsAny<RoomBooking>()), Times.Once);

            savedBooking.ShouldNotBeNull();
            savedBooking.FullName.ShouldBe(_request.FullName);
            savedBooking.Email.ShouldBe(_request.Email);
            savedBooking.Date.ShouldBe(_request.Date);
        }
    }
}
