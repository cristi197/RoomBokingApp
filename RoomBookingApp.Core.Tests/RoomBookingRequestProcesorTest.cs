using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;

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

            Assert.NotNull(result);
            Assert.Equal(request.FullName, result.FullName);
            Assert.Equal(request.Email, result.Email);
            Assert.Equal(request.Date, result.Date);

            result.ShouldNotBeNull();
            result.FullName.ShouldBe(request.FullName);
            result.Email.ShouldBe(request.Email);
            result.Date.ShouldBe(request.Date);
        }
    }
}
