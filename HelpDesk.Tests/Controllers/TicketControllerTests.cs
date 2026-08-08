using HelpDesk.Api.Controllers;
using HelpDesk.Api.Models;
using HelpDesk.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace HelpDesk.Tests.Controllers
{
    public class TicketControllerTests
    {
        private readonly Mock<ITicketRepository> _mockRepo;
        private readonly TicketController _controller;

        public TicketControllerTests()
        {
            _mockRepo = new Mock<ITicketRepository>();
            _controller = new TicketController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAllTickets_ReturnsOkResult_WhenTicketsExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetAllTicketsAsync()).ReturnsAsync(new List<Ticket> { new Ticket() });

            // Act
            var result = await _controller.GetAllTickets();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetTicketById_ReturnsOkResult_WhenTicketExists()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetTicketByIdAsync(1)).ReturnsAsync(new Ticket { Id = 1 });

            // Act
            var result = await _controller.GetTicketById(1);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetTicketById_ReturnsNotFound_WhenTicketDoesNotExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetTicketByIdAsync(1)).ReturnsAsync((Ticket)null);

            // Act
            var result = await _controller.GetTicketById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreateTicket_ReturnsOkResult_WhenTicketIsCreatedSuccessfully()
        {
            // Arrange
            var ticket = new Ticket { Title = "Test" };
            _mockRepo.Setup(repo => repo.CreateTicketAsync(ticket)).ReturnsAsync(1);

            // Act
            var result = await _controller.CreateTicket(ticket);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CreateTicket_ReturnsBadRequest_WhenTicketIsNull()
        {
            // Act
            var result = await _controller.CreateTicket(null);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task GetTicketsByStatus_ReturnsOkResult_WhenMatchingTicketExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetTicketsByStatusAsync("Open")).ReturnsAsync(new List<Ticket> { new Ticket { Status = "Open" } });

            // Act
            var result = await _controller.GetTicketsByStatus("Open");

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
