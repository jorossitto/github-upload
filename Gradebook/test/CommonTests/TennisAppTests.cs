using Application.Data;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TennisApp.Controllers;
using TennisApp.ViewModels;
using Xunit;

namespace CommonTests.Tests
{
    public class TennisAppTests
    {
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
