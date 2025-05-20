using Xunit;
using PerkembanganApi.Controllers;
using SharedModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
namespace LaporanPerkembanganTests
{


    public class PerkembanganControllerTests
    {
        [Fact]
        public void GetById_ReturnsCorrectSiswa()
        {
            // Arrange
            var controller = new PerkembanganController();

            // Act
            var result = controller.GetById("1");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var data = Assert.IsType<ReportData>(okResult.Value);
            Assert.Equal("1", data.Id);
        }

        [Fact]
        public void GetById_ReturnsNotFound_WhenIdNotExist()
        {
            // Arrange
            var controller = new PerkembanganController();

            // Act
            var result = controller.GetById("999");

            // Assert
            var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);
        }
    }
}