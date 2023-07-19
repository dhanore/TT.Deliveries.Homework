namespace TT.Deliveries.Tests.Controllers
{
    using Moq;
    using NUnit.Framework;
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using TT.Deliveries.Application.Common;
    using TT.Deliveries.Application.Features.DeliveryFeatures;
    using TT.Deliveries.Domain.Common;
    using TT.Deliveries.Web.Api.Controllers;

    [TestFixture]
    public class DeliveryControllerTests
    {
        Mock<IDeliveryServices> serviceMock;
        Mock<IQueueClient<ScheduleMessage>> queueClientMock;
        private DeliveriesController sut;

        [SetUp]
        public void Setup()
        {
            serviceMock = new Mock<IDeliveryServices>();
            queueClientMock = new Mock<IQueueClient<ScheduleMessage>>();
            sut = new DeliveriesController(serviceMock.Object, queueClientMock.Object);
        }

        [Test]
        public async Task GetById_Should_Return_404_If_Delivery_Doesnt_Exist()
        {
            serviceMock.Setup(o => o.getDeliveryById(It.IsAny<string>())).ReturnsAsync(new GetDeliveryResponse());
            var result = await sut.GetOne(It.IsAny<string>());
            var code = (HttpStatusCode)result
                  .GetType()
                  .GetProperty("StatusCode")
                  .GetValue(result, null);

            Assert.AreEqual(code, HttpStatusCode.NotFound);

            serviceMock.Verify(a => a.getDeliveryById(It.IsAny<string>()), Times.Once);

        }

        [Test]
        public async Task GetById_Should_Return_Delivery_Details()
        {
            serviceMock.Setup(o => o.getDeliveryById(It.IsAny<string>())).ReturnsAsync(new GetDeliveryResponse()
            {
                Id = "1",
                AccessWindow = new Data.Dto.AccessWindow
                {
                    StartTime = DateTime.UtcNow,
                    EndTime = DateTime.UtcNow.AddDays(1)
                },
                State = Data.Dto.DeliveryState.Created,
                Order = new Data.Dto.Order
                {
                    OrderName = "Order",
                    Sender = "sender"
                },
                Recipient = new Data.Dto.Recipient
                {
                    Address = "Addr1",
                    Email = "email@test.com",
                    Name = "name",
                    PhoneNumber = "12344"
                }
            });

            var result = await sut.GetOne("1");
            var code = (HttpStatusCode)result
                  .GetType()
                  .GetProperty("StatusCode")
                  .GetValue(result, null);

            Assert.AreEqual(code, HttpStatusCode.OK);

            serviceMock.Verify(a => a.getDeliveryById("1"), Times.Once);
        }
    }
}
