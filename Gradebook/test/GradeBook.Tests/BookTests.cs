using AppCore.Data;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using TennisApp.Controllers;
using TennisApp.ViewModels;
using Xunit;

namespace GradeBook.Tests
{
    public class BookTests
    {
        [Fact]
        public void BookCalculatesAnAverageGrade()
        {
            //arrange
            var book = new InMemoryBook("");
            book.AddGrade(89.1);
            book.AddGrade(90.5);
            book.AddGrade(77.5);

            //act
            var result = book.GetStatistics();

            //assert
            Assert.Equal(85.7, result.Average, 1);
            Assert.Equal(90.5, result.High);
            Assert.Equal(77.5, result.Low);
            Assert.Equal('B', result.Letter);
        }

        [Fact]
        public void ReturnsExpectedViewModel_WhenWeatherIsSun()
        {
            var mockWeatherForecaster = new Mock<IWeatherForecaster>();
            mockWeatherForecaster.Setup(w => w.GetCurrentWeather()).Returns(new WeatherResult
            {
                WeatherCondition = WeatherCondition.Sun
            });

            var sut = new HomeController(mockWeatherForecaster.Object);

            var result = sut.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<HomeViewModel>(viewResult.ViewData.Model);
            Assert.Contains("It's sunny right now.", model.WeatherDescription);
        }

    }
}
