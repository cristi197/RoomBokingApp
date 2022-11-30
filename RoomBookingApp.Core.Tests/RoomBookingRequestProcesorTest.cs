using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;
using Shouldly;
using Xunit;

namespace RoomBookingApp.Core.Tests
{
    public class RoomBookingRequestProcesorTest
    {
        [Fact]
        public void ShouldReturn_RoomBookingResponse_WithValues()
        {
            //Arrange

            var request = new RoomBookingRequest
            {
                FullName = "Test Name",
                Email = "test@request.com",
                Date = new DateTime(2022, 10, 20)
            };

            var processor = new RoomBookingRequestProcesor();

            //Act

            RoomBookingResult result = processor.BookRoom(request);

            //Assert

            result.ShouldNotBeNull();
            result.FullName.ShouldBe(request.FullName);
            result.Email.ShouldBe(request.Email);
            result.Date.ShouldBe(request.Date);
        }
    }
}
