/*namespace ScreenLayout.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Hosting;
    using System.Web.Http.Results;
    using AutoFixture;
    using AutoFixture.AutoMoq;
    using Moq;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Xunit;

    public class ScreenLayoutControllerTests
    {
        private ScreenLayoutControllerTests _screenLayoutController;
        private readonly Mock<IScreenLayoutService> _screenLayoutServiceMock;
        private readonly Mock<IScreenLayoutRepository> _screenLayoutRepositoryMock;
        private string _contentType;
        private string _transactionId;
        public ScreenLayoutControllerTests()
        {
            _fixture = new IFixture().Customize(new AutoMoqCustomization());
            _fixture.Register<System.Security.Cryptography.X509Certificates.X509Certificate2>(() => null);

            var data = _fixture.Create<GetDataEntity>();

            var httpRequest = new HttpRequest("", "http://localhost.com", "");
            var httpContext = new HttpContext(httpRequest, new HttpResponse(new StringWriter()));
            httpContext.Items["ModuleInfo"] = data;
            HttpContext.Current = httpContext;
            _screenLayoutServiceMock = new Mock<IScreenLayoutService>();
            _screenLayoutRepositoryMock = new Mock<IScreenLayoutRepository>();

            // TestConfig.configファイルのパスを取得
            var configFilePath = Path.Combine(Directory.GetCurrentDirectory(), "TestConfig.json");

            // TestConfig.configファイルの内容を読み込む
            var configJson = File.ReadAllText(configFilePath);
            var config = JsonConvert.DeserializeObject<JObject>(configJson);

            _contentType = config["ConnectionStrings"]["contentType"].Value<string>();
            _transactionId = config["ConnectionStrings"]["transactionId"].Value<string>();

            _screenLayoutController = new ScreenLayoutController(_screenLayoutServiceMock.Object)
            {
                Request = new System.Net.Http.HttpRequestMessage()
            };
        }

        [Fact]
        public void GetPreferences_NullContextData_ReturnUnauthorized()
        {
            // Arrange
            var httpRequest = new HttpRequest("", "http://localhost.com", "");
            var httpContext = new HttpContext(httpRequest, new HttpResponse(new StringWriter()));
            httpContext.Items["ModuleInfo"] = null;
            HttpContext.Current = httpContext;
            var screenLayoutService = new ScreenLayoutService(_screenLayoutRepositoryMock.Object);

            var screenLayoutController = new ScreenLayoutController(_contentType, _transactionId)
            {
                Request = new HttpRequestMessage()
                {
                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
                },
            };
            screenLayoutController._screenLayoutService = screenLayoutService;

            // Act
            var result = screenLayoutController.GetPreferences();

            // Assert
            Assert.NotNull(result);

            var content = Assert.IsType<ResponseMessageResult>(result);
            Assert.IsNotNull(content.Response.Content);
            Assert.Equals(HttpStatusCode.Unauthorized, content.Response.StatusCode);

            var content2 = content.Response.Content as ObjectContent<ErrorResponse>;
            var jObject = JObject.FromObject(content2.Value);
            Assert.NotNull(jObject);
            var actualErrorCode = (string)jObject["errorCode"];
            var actualMessage = (string)jObject["message"];
            Assert.Equal(ErrorConstants.codeAuthzCheck, actualErrorCode);
            Assert.Equal(ErrorConstants.messageAuthzCheck, actualMessage);
        }

        [Fact]
        public void GetPreferences_EmptyContentType_ReturnBadRequest()
        {
            // Arrange
            var screenLayoutService = new ScreenLayoutService(_screenLayoutRepositoryMock.Object);

            var screenLayoutController = new ScreenLayoutController(string.Empty, _transactionId)
            {
                Request = new HttpRequestMessage()
                {
                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
                },
            };
            screenLayoutController._screenLayoutService = screenLayoutService;

            // Act
            var result = screenLayoutController.GetPreferences();

            // Assert
            Assert.IsNotNull(result);

            var content = Assert.IsType<ResponseMessageResult>(result);
            Assert.IsNotNull(content.Response.Content);
            Assert.Equals(HttpStatusCode.BadRequest, content.Response.StatusCode);

            var content2 = content.Response.Content as ObjectContent<ErrorResponse>;
            var jObject = JObject.FromObject(content2.Value);
            Assert.NotNull(jObject);
            var actualErrorCode = (string)jObject["errorCode"];
            var actualMessage = (string)jObject["message"];
            Assert.Equal(ErrorConstants.codeBadRequest, actualErrorCode);
            Assert.Equal(ErrorConstants.messageBadRequest, actualMessage);
        }

        [Fact]
        public void GetPreferences_HaveList_ReturnsDataInit()
        {
            // Arrange
            _screenLayoutRepositoryMock.Setup(x => x.InsertScreenLayout(It.IsAny<ScreenLayoutEntity>())).Returns(true);
            var screenLayoutService = new ScreenLayoutService(_screenLayoutRepositoryMock.Object);

            var expectedScreenLayoutResultDto = new List<ScreenLayoutResultDto>
            {
                new ScreenLayoutResultDto { ScreenItem = "3", DisplayOrder = 1, IsDisplayTarget = true },
                new ScreenLayoutResultDto { ScreenItem = "4", DisplayOrder = 2, IsDisplayTarget = true },
                new ScreenLayoutResultDto { ScreenItem = "6", DisplayOrder = 3, IsDisplayTarget = true }
            };

            var screenLayoutController = new ScreenLayoutController(_contentType, _transactionId);
            screenLayoutController._screenLayoutService = screenLayoutService;

            // Act
            var result = screenLayoutController.GetPreferences();

            // Assert
            var okResult = Assert.IsType<OkNegotiatedContentResult<PreferencesJson>>(result);
            var content = okResult.Content;
            Assert.Equal(expectedScreenLayoutResultDto.Count, content.Preferences.Count);

            for (int i = 0; i < expectedScreenLayoutResultDto.Count; i++)
            {
                Assert.Equal(expectedScreenLayoutResultDto[i].ScreenItem, content.Preferences[i].ScreenItem);
                Assert.Equal(expectedScreenLayoutResultDto[i].DisplayOrder, content.Preferences[i].DisplayOrder);
                Assert.Equal(expectedScreenLayoutResultDto[i].IsDisplayTarget, content.Preferences[i].IsDisplayTarget);
            }
        }

        [Fact]
        public void GetPreferences_HaveList_ReturnsDataExsit()
        {
            // Arrange
            var expectedScreenLayoutEntity = _fixture.CreateMany<ScreenLayoutEntity>().ToList();

            var expectedScreenLayoutResultDto = expectedScreenLayoutEntity.Select(x => new ScreenLayoutResultDto
            {
                ScreenItem = x.ScreenItem.ToString(),
                DisplayOrder = x.DisplayOrder,
                IsDisplayTarget = x.IsDisplayTarget
            }).ToList();

            _screenLayoutRepositoryMock.Setup(x => x.GetListScreenLayout(It.IsAny<string>(), It.IsAny<string>())).Returns(expectedScreenLayoutEntity);

            var screenLayoutService = new ScreenLayoutService(_screenLayoutRepositoryMock.Object);

            var screenLayoutController = new ScreenLayoutController(_contentType, _transactionId);
            screenLayoutController._screenLayoutService = screenLayoutService;

            // Act
            var result = screenLayoutController.GetPreferences();

            // Assert
            var okResult = Assert.IsType<OkNegotiatedContentResult<PreferencesJson>>(result);
            var content = okResult.Content;
            Assert.NotNull(content);
            Assert.Equal(expectedScreenLayoutResultDto.Count, content.Preferences.Count);
            for (int i = 0; i < expectedScreenLayoutResultDto.Count; i++)
            {
                Assert.Equal(expectedScreenLayoutResultDto[i].ScreenItem, content.Preferences[i].ScreenItem);
                Assert.Equal(expectedScreenLayoutResultDto[i].DisplayOrder, content.Preferences[i].DisplayOrder);
                Assert.Equal(expectedScreenLayoutResultDto[i].IsDisplayTarget, content.Preferences[i].IsDisplayTarget);
            }
        }

        [Fact]
        public void GetPreferences_InsertScreenLayoutException_ReturnNull()
        {
            // Arrange
            var expectedScreenLayoutEntity = _fixture.CreateMany<ScreenLayoutEntity>().ToList();

            var expectedScreenLayoutResultDto = expectedScreenLayoutEntity.Select(x => new ScreenLayoutResultDto
            {
                ScreenItem = x.ScreenItem.ToString(),
                DisplayOrder = x.DisplayOrder,
                IsDisplayTarget = x.IsDisplayTarget
            }).ToList();

            _screenLayoutRepositoryMock.Setup(x => x.InsertScreenLayout(It.IsAny<ScreenLayoutEntity>())).Throws(new Exception("exception"));

            var screenLayoutService = new ScreenLayoutService(_screenLayoutRepositoryMock.Object);

            var screenLayoutController = new ScreenLayoutController(_contentType, _transactionId);
            screenLayoutController._screenLayoutService = screenLayoutService;

            // Act
            var result = screenLayoutController.GetPreferences();

            // Assert
            Assert.IsNull(result);
        }

        [Fact]
        public void GetPreferences_GetListScreenLayoutExceptionSql_ReturnsNull()
        {
            // Arrange
            var screenLayoutRepository = new ScreenLayoutRepository("Data Source=localhost;Initial Catalog=ContentDB;User ID=sa;Password=test");
            var screenLayoutService = new ScreenLayoutService(screenLayoutRepository);
            var screenLayoutController = new ScreenLayoutController(_contentType, _transactionId);
            screenLayoutController._screenLayoutService = screenLayoutService;

            // Act
            var result = screenLayoutController.GetPreferences();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void EditPreferences_NullContextData_ReturnUnauthorized()
        {
            // Arrange
            var httpRequest = new HttpRequest("", "http://localhost.com", "");
            var httpContext = new HttpContext(httpRequest, new HttpResponse(new StringWriter()));
            httpContext.Items["ModuleInfo"] = null;
            HttpContext.Current = httpContext;

            var listScreenLayoutRequest = _fixture.Create<ListScreenLayoutRequest>();
            var screenLayoutService = new ScreenLayoutService(_screenLayoutRepositoryMock.Object);

            var screenLayoutController = new ScreenLayoutController(_contentType, _transactionId)
            {
                Request = new HttpRequestMessage()
                {
                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
                },
            };
            screenLayoutController._screenLayoutService = screenLayoutService;

            // Act
            var result = screenLayoutController.EditPreferences(listScreenLayoutRequest);

            // Assert
            Assert.NotNull(result);

            var content = Assert.IsType<ResponseMessageResult>(result);
            Assert.NotNull(content.Response.Content);
            Assert.Equal(HttpStatusCode.Unauthorized, content.Response.StatusCode);

            var content2 = content.Response.Content as ObjectContent<ErrorResponse>;
            var jObject = JObject.FromObject(content2.Value);
            Assert.NotNull(jObject);
            var actualErrorCode = (string)jObject["errorCode"];
            var actualMessage = (string)jObject["message"];
            Assert.Equal(ErrorConstants.codeAuthzCheck, actualErrorCode);
            Assert.Equal(ErrorConstants.messageAuthzCheck, actualMessage);
        }

        [Fact]
        public void EditPreferences_EmptyContentType_ReturnBadRequest()
        {
            // Arrange
            var listScreenLayoutRequest = _fixture.Create<ListScreenLayoutRequest>();
            var screenLayoutService = new ScreenLayoutService(_screenLayoutRepositoryMock.Object);

            var screenLayoutController = new ScreenLayoutController(string.Empty, _transactionId)
            {
                Request = new HttpRequestMessage()
                {
                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
                },
            };
            screenLayoutController._screenLayoutService = screenLayoutService;

            // Act
            var result = screenLayoutController.EditPreferences(listScreenLayoutRequest);

            // Assert
            Assert.NotNull(result);

            var content = Assert.IsType<ResponseMessageResult>(result);
            Assert.NotNull(content.Response.Content);
            Assert.Equal(HttpStatusCode.BadRequest, content.Response.StatusCode);

            var content2 = content.Response.Content as ObjectContent<ErrorResponse>;
            var jObject = JObject.FromObject(content2.Value);
            Assert.NotNull(jObject);
            var actualErrorCode = (string)jObject["errorCode"];
            var actualMessage = (string)jObject["message"];
            Assert.Equal(ErrorConstants.codeBadRequest, actualErrorCode);
            Assert.Equal(ErrorConstants.messageBadRequest, actualMessage);
        }

        [Theory]
        [MemberData(nameof(ScreenLayoutResultDtoData.GetInvalidData), MemberType = typeof(ScreenLayoutResultDtoData))]
        public void EditPreferences_HaveInvalidData_ReturnsExpectedResult(List<ScreenLayoutResultDto> preferences, Type expectedType)
        {
            // Arrange
            var requestMock = new Mock<HttpRequestMessage>();
            var listScreenLayoutRequest = new ListScreenLayoutRequest
            {
                Preferences = preferences
            };

            var screenLayoutService = new ScreenLayoutService(_screenLayoutRepositoryMock.Object);

            var screenLayoutController = new ScreenLayoutController(_contentType, _transactionId)
            {
                Request = new HttpRequestMessage()
                {
                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
                },
            };
            screenLayoutController._screenLayoutService = screenLayoutService;

            // Act
            var result = screenLayoutController.EditPreferences(listScreenLayoutRequest);

            // Assert
            Assert.IsType(expectedType, result);
            var content = Assert.IsType<ResponseMessageResult>(result);
            Assert.NotNull(content.Response.Content);
            Assert.Equal(HttpStatusCode.BadRequest, content.Response.StatusCode);

            var content2 = content.Response.Content as ObjectContent<ErrorResponse>;
            var jObject = JObject.FromObject(content2.Value);
            Assert.NotNull(jObject);
            var actualErrorCode = (string)jObject["errorCode"];
            var actualMessage = (string)jObject["message"];
            Assert.Equal(ErrorConstants.codeInputError, actualErrorCode);
            Assert.Equal(ErrorConstants.messageInputError, actualMessage);
        }

        [Fact]
        public void EditPreferences_Update_ReturnSuccess()
        {
            // Arrange
            var screenItems = new[] { 3, 4, 6 };

            int currentIndex = 0;

            var expectedScreenLayoutEntity = _fixture.Build<ScreenLayoutEntity>()
                .With(e => e.ScreenItem, () => currentIndex < screenItems.Length ? screenItems[currentIndex++] : -1)
                .CreateMany(3)
                .Where(e => e.ScreenItem != -1)
                .ToList();

            currentIndex = 0;
            var newScreenLayoutEntity = _fixture.Build<ScreenLayoutEntity>()
               .With(e => e.ScreenItem, () => currentIndex < screenItems.Length ? screenItems[currentIndex++] : -1)
               .CreateMany(3)
               .Where(e => e.ScreenItem != -1)
               .ToList();

            var expectedScreenLayoutResultDto = expectedScreenLayoutEntity.Select(x => new ScreenLayoutResultDto
            {
                ScreenItem = x.ScreenItem.ToString(),
                DisplayOrder = x.DisplayOrder,
                IsDisplayTarget = x.IsDisplayTarget
            }).ToList();

            var newData = newScreenLayoutEntity.Select(x => new ScreenLayoutResultDto
            {
                ScreenItem = x.ScreenItem.ToString(),
                DisplayOrder = x.DisplayOrder,
                IsDisplayTarget = x.IsDisplayTarget
            }).ToList();

            var listScreenLayoutRequest = new ListScreenLayoutRequest
            {
                Preferences = newData
            };

            _screenLayoutRepositoryMock.Setup(x => x.UpdateScreenLayout(It.IsAny<ScreenLayoutEntity>())).Returns(true);
            _screenLayoutRepositoryMock.Setup(x => x.GetListScreenLayout(It.IsAny<string>(), It.IsAny<string>())).Returns(expectedScreenLayoutEntity);

            var screenLayoutService = new ScreenLayoutService(_screenLayoutRepositoryMock.Object);

            var screenLayoutController = new ScreenLayoutController(_contentType, _transactionId);
            screenLayoutController._screenLayoutService = screenLayoutService;

            // Act
            var result = screenLayoutController.EditPreferences(listScreenLayoutRequest);

            // Assert
            var okResult = Assert.IsType<OkNegotiatedContentResult<ResponseJson>>(result);
            var content = okResult.Content;
            Assert.NotNull(content);
            Assert.Equal("OK", content.Message);
        }

        [Fact]
        public void EditPreferences_Update_ReturnFail()
        {
            // Arrange
            var screenItems = new[] { 3, 4, 6 };

            int currentIndex = 0;

            var expectedScreenLayoutEntity = _fixture.Build<ScreenLayoutEntity>()
                .With(e => e.ScreenItem, () => currentIndex < screenItems.Length ? screenItems[currentIndex++] : -1)
                .CreateMany(3)
                .Where(e => e.ScreenItem != -1)
                .ToList();

            currentIndex = 0;
            var newScreenLayoutEntity = _fixture.Build<ScreenLayoutEntity>()
               .With(e => e.ScreenItem, () => currentIndex < screenItems.Length ? screenItems[currentIndex++] : -1)
               .CreateMany(3)
               .Where(e => e.ScreenItem != -1)
               .ToList();

            var expectedScreenLayoutResultDto = expectedScreenLayoutEntity.Select(x => new ScreenLayoutResultDto
            {
                ScreenItem = x.ScreenItem.ToString(),
                DisplayOrder = x.DisplayOrder,
                IsDisplayTarget = x.IsDisplayTarget
            }).ToList();

            var newData = newScreenLayoutEntity.Select(x => new ScreenLayoutResultDto
            {
                ScreenItem = x.ScreenItem.ToString(),
                DisplayOrder = x.DisplayOrder,
                IsDisplayTarget = x.IsDisplayTarget
            }).ToList();

            var listScreenLayoutRequest = new ListScreenLayoutRequest
            {
                Preferences = newData
            };

            _screenLayoutRepositoryMock.Setup(x => x.UpdateScreenLayout(It.IsAny<ScreenLayoutEntity>())).Returns(false);
            _screenLayoutRepositoryMock.Setup(x => x.GetListScreenLayout(It.IsAny<string>(), It.IsAny<string>())).Returns(expectedScreenLayoutEntity);

            var screenLayoutService = new ScreenLayoutService(_screenLayoutRepositoryMock.Object);

            var screenLayoutController = new ScreenLayoutController(_contentType, _transactionId)
            {
                Request = new HttpRequestMessage()
                {
                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
                }
            };
            screenLayoutController._screenLayoutService = screenLayoutService;

            // Act
            var result = screenLayoutController.EditPreferences(listScreenLayoutRequest);

            // Assert
            var content = Assert.IsType<ResponseMessageResult>(result);
            Assert.NotNull(content.Response.Content);
            Assert.Equal(HttpStatusCode.InternalServerError, content.Response.StatusCode);

            var content2 = content.Response.Content as ObjectContent<ErrorResponse>;
            var jObject = JObject.FromObject(content2.Value);
            Assert.NotNull(jObject);
            var actualErrorCode = (string)jObject["errorCode"];
            var actualMessage = (string)jObject["message"];
            Assert.Equal(ErrorConstants.codeInternalServerError, actualErrorCode);
            Assert.Equal(ErrorConstants.messageInternalServerError, actualMessage);
        }

        [Fact]
        public void EditPreferences_Catch_ReturnsInternalServerErrorWithMessage()
        {
            // Arrange
            var screenItems = new[] { 3, 4, 6 };

            int currentIndex = 0;

            var expectedScreenLayoutEntity = _fixture.Build<ScreenLayoutEntity>()
                .With(e => e.ScreenItem, () => currentIndex < screenItems.Length ? screenItems[currentIndex++] : -1)
                .CreateMany(3)
                .Where(e => e.ScreenItem != -1)
                .ToList();

            currentIndex = 0;
            var newScreenLayoutEntity = _fixture.Build<ScreenLayoutEntity>()
               .With(e => e.ScreenItem, () => currentIndex < screenItems.Length ? screenItems[currentIndex++] : -1)
               .CreateMany(3)
               .Where(e => e.ScreenItem != -1)
               .ToList();

            var expectedScreenLayoutResultDto = expectedScreenLayoutEntity.Select(x => new ScreenLayoutResultDto
            {
                ScreenItem = x.ScreenItem.ToString(),
                DisplayOrder = x.DisplayOrder,
                IsDisplayTarget = x.IsDisplayTarget
            }).ToList();

            var newData = newScreenLayoutEntity.Select(x => new ScreenLayoutResultDto
            {
                ScreenItem = x.ScreenItem.ToString(),
                DisplayOrder = x.DisplayOrder,
                IsDisplayTarget = x.IsDisplayTarget
            }).ToList();

            var listScreenLayoutRequest = new ListScreenLayoutRequest
            {
                Preferences = newData
            };

            var requestMock = new Mock<HttpRequestMessage>();

            _screenLayoutRepositoryMock.Setup(x => x.UpdateScreenLayout(It.IsAny<ScreenLayoutEntity>())).Throws(new Exception("exception"));
            var screenLayoutService = new ScreenLayoutService(_screenLayoutRepositoryMock.Object);

            var screenLayoutController = new ScreenLayoutController(_contentType, _transactionId)
            {
                Request = new HttpRequestMessage()
                {
                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
                },
            };
            screenLayoutController._screenLayoutService = screenLayoutService;

            // Act
            var actionResult = screenLayoutController.EditPreferences(listScreenLayoutRequest);

            // Assert
            Assert.NotNull(actionResult);

            var content = Assert.IsType<ResponseMessageResult>(actionResult);
            Assert.NotNull(content.Response.Content);
            Assert.Equal(HttpStatusCode.InternalServerError, content.Response.StatusCode);

            var content2 = content.Response.Content as ObjectContent<ErrorResponse>;
            var jObject = JObject.FromObject(content2.Value);
            Assert.NotNull(jObject);
            var actualErrorCode = (string)jObject["errorCode"];
            var actualMessage = (string)jObject["message"];
            Assert.Equal(ErrorConstants.codeInternalServerError, actualErrorCode);
            Assert.Equal(ErrorConstants.messageInternalServerError, actualMessage);
        }

        [Fact]
        public void EditPreferences_UpdateScreenLayoutException_ReturnNull()
        {
            // Arrange
            var screenItems = new[] { 3, 4, 6 };

            int currentIndex = 0;

            var expectedScreenLayoutEntity = _fixture.Build<ScreenLayoutEntity>()
                .With(e => e.ScreenItem, () => currentIndex < screenItems.Length ? screenItems[currentIndex++] : -1)
                .CreateMany(3)
                .Where(e => e.ScreenItem != -1)
                .ToList();

            currentIndex = 0;
            var newScreenLayoutEntity = _fixture.Build<ScreenLayoutEntity>()
               .With(e => e.ScreenItem, () => currentIndex < screenItems.Length ? screenItems[currentIndex++] : -1)
               .CreateMany(3)
               .Where(e => e.ScreenItem != -1)
               .ToList();

            var expectedScreenLayoutResultDto = expectedScreenLayoutEntity.Select(x => new ScreenLayoutResultDto
            {
                ScreenItem = x.ScreenItem.ToString(),
                DisplayOrder = x.DisplayOrder,
                IsDisplayTarget = x.IsDisplayTarget
            }).ToList();

            var newData = newScreenLayoutEntity.Select(x => new ScreenLayoutResultDto
            {
                ScreenItem = x.ScreenItem.ToString(),
                DisplayOrder = x.DisplayOrder,
                IsDisplayTarget = x.IsDisplayTarget
            }).ToList();

            var listScreenLayoutRequest = new ListScreenLayoutRequest
            {
                Preferences = newData
            };

            _screenLayoutRepositoryMock.Setup(x => x.UpdateScreenLayout(It.IsAny<ScreenLayoutEntity>())).Throws(new Exception("exception"));
            _screenLayoutRepositoryMock.Setup(x => x.GetListScreenLayout(It.IsAny<string>(), It.IsAny<string>())).Returns(expectedScreenLayoutEntity);

            var screenLayoutService = new ScreenLayoutService(_screenLayoutRepositoryMock.Object);

            var screenLayoutController = new ScreenLayoutController(_contentType, _transactionId);
            screenLayoutController._screenLayoutService = screenLayoutService;

            // Act
            var result = screenLayoutController.EditPreferences(listScreenLayoutRequest);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void EditPreferences_GetListScreenLayoutExceptionSql_ReturnNull()
        {
            // Arrange
            var screenItems = new[] { 3, 4, 6 };
            int currentIndex = 0;

            var newScreenLayoutEntity = _fixture.Build<ScreenLayoutEntity>()
               .With(e => e.ScreenItem, () => currentIndex < screenItems.Length ? screenItems[currentIndex++] : -1)
               .CreateMany(3)
               .Where(e => e.ScreenItem != -1)
               .ToList();

            var newData = newScreenLayoutEntity.Select(x => new ScreenLayoutResultDto
            {
                ScreenItem = x.ScreenItem.ToString(),
                DisplayOrder = x.DisplayOrder,
                IsDisplayTarget = x.IsDisplayTarget
            }).ToList();

            var listScreenLayoutRequest = new ListScreenLayoutRequest
            {
                Preferences = newData
            };
            var screenLayoutRepository = new ScreenLayoutRepository("Data Source=localhost;Initial Catalog=ContentDB;User ID=sa;Password=test");
            var screenLayoutService = new ScreenLayoutService(screenLayoutRepository);
            var screenLayoutController = new ScreenLayoutController(_contentType, _transactionId);
            screenLayoutController._screenLayoutService = screenLayoutService;

            // Act
            var result = screenLayoutController.EditPreferences(listScreenLayoutRequest);

            // Assert
            Assert.Null(result);
        }
    }
}*/