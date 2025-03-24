using ECommerce.Shared.Middlewares;
using ECommerce.Shared.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Moq;

namespace ECommerce.UnitTests.Shared;

public class GlobalExceptionHandlerTests
{
    private readonly Mock<ILoggerService> _mockLogger;
    private readonly RequestDelegate _next;
    private readonly GlobalExceptionHandler _middleware;

    public GlobalExceptionHandlerTests()
    {
        _mockLogger = new Mock<ILoggerService>();
        _next = Mock.Of<RequestDelegate>();
        _middleware = new GlobalExceptionHandler(_next, _mockLogger.Object);
    }

    [Fact]
    public async Task InvokeAsync_Should_Call_Next()
    {
        // Arrange
        var context = new DefaultHttpContext();
        var nextMock = new Mock<RequestDelegate>();
        var middleware = new GlobalExceptionHandler(nextMock.Object, _mockLogger.Object);

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        nextMock.Verify(n => n(It.IsAny<HttpContext>()), Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_Should_Handle_Exception_And_Return_500()
    {
        // Arrange
        var context = new DefaultHttpContext();
        var nextMock = new Mock<RequestDelegate>();
        nextMock.Setup(n => n(It.IsAny<HttpContext>())).ThrowsAsync(new Exception("Test Exception"));
        var middleware = new GlobalExceptionHandler(nextMock.Object, _mockLogger.Object);

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        _mockLogger.Verify(l => l.LogError(It.IsAny<string>(), It.IsAny<Exception>()), Times.Once);
        Assert.Equal(StatusCodes.Status500InternalServerError, context.Response.StatusCode);
    }

    [Fact]
    public async Task InvokeAsync_Should_Handle_Exception_And_Log_Error()
    {
        // Arrange
        var logMessage = "An unhandled exception has occurred.";
        var exception = new Exception("Test Exception");
        var context = new DefaultHttpContext();
        var nextMock = new Mock<RequestDelegate>();
        nextMock.Setup(n => n(It.IsAny<HttpContext>())).ThrowsAsync(exception);
        var middleware = new GlobalExceptionHandler(nextMock.Object, _mockLogger.Object);

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        _mockLogger.Verify(l => l.LogError(logMessage, exception), Times.Once);
        Assert.Equal(StatusCodes.Status500InternalServerError, context.Response.StatusCode);
    }

    [Theory]
    [InlineData(StatusCodes.Status429TooManyRequests)]
    [InlineData(StatusCodes.Status401Unauthorized)]
    [InlineData(StatusCodes.Status403Forbidden)]
    public async Task InvokeAsync_Should_Modify_Response_For_Specific_StatusCodes(int statusCode)
    {
        // Arrange
        var context = new DefaultHttpContext();
        context.Response.StatusCode = statusCode;
        var middleware = new GlobalExceptionHandler(_next, _mockLogger.Object);

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        Assert.Equal(statusCode, context.Response.StatusCode);
    }

    [Theory]
    [InlineData(typeof(TaskCanceledException), StatusCodes.Status408RequestTimeout)]
    [InlineData(typeof(TimeoutException), StatusCodes.Status408RequestTimeout)]
    public async Task InvokeAsync_Should_Handle_Timeout_Exceptions(Type exceptionType, int expectedStatusCode)
    {
        // Arrange
        var context = new DefaultHttpContext();
        var exception = (Exception)Activator.CreateInstance(exceptionType)!;
        var nextMock = new Mock<RequestDelegate>();
        nextMock.Setup(n => n(It.IsAny<HttpContext>())).ThrowsAsync(exception);
        var middleware = new GlobalExceptionHandler(nextMock.Object, _mockLogger.Object);

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        Assert.Equal(expectedStatusCode, context.Response.StatusCode);
    }
}
