using LogicBuilder.App.KendoGrid.Bsl.Business.Requests;
using System.Text.Json;
using Xunit;

namespace LogicBuilder.App.KendoGrid.Bsl.Business.Tests.Requests
{
    public class KendoGridDataSourceRequestOptionsTest
    {
        private static readonly JsonSerializerOptions serializationOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        [Fact]
        public void CanSerializeToJson()
        {
            // Arrange
            var options = new KendoGridDataSourceRequestOptions
            {
                Aggregate = "sum",
                Filter = "name eq \'test\'",
                Group = "category",
                Page = 1,
                Sort = "name-asc",
                PageSize = 10
            };

            // Act
            var json = JsonSerializer.Serialize(options);

            // Assert
            Assert.NotNull(json);
            Assert.Contains("\"Aggregate\":\"sum\"", json);
            Assert.Contains("\"Filter\":\"name eq \\u0027test\\u0027\"", json);
            Assert.Contains("\"Group\":\"category\"", json);
            Assert.Contains("\"Page\":1", json);
            Assert.Contains("\"Sort\":\"name-asc\"", json);
            Assert.Contains("\"PageSize\":10", json);
        }

        [Fact]
        public void CanDeserializeFromJson()
        {
            // Arrange
            var json = @"{
                ""Aggregate"": ""sum"",
                ""Filter"": ""name eq 'test'"",
                ""Group"": ""category"",
                ""Page"": 2,
                ""Sort"": ""name-desc"",
                ""PageSize"": 20
            }";

            // Act
            var options = JsonSerializer.Deserialize<KendoGridDataSourceRequestOptions>(json);

            // Assert
            Assert.NotNull(options);
            Assert.Equal("sum", options.Aggregate);
            Assert.Equal("name eq 'test'", options.Filter);
            Assert.Equal("category", options.Group);
            Assert.Equal(2, options.Page);
            Assert.Equal("name-desc", options.Sort);
            Assert.Equal(20, options.PageSize);
        }

        [Fact]
        public void CanRoundTripSerializeAndDeserialize()
        {
            // Arrange
            var original = new KendoGridDataSourceRequestOptions
            {
                Aggregate = "count",
                Filter = "age gt 18",
                Group = "department",
                Page = 3,
                Sort = "age-asc",
                PageSize = 50
            };

            // Act
            var json = JsonSerializer.Serialize(original);
            var deserialized = JsonSerializer.Deserialize<KendoGridDataSourceRequestOptions>(json);

            // Assert
            Assert.NotNull(deserialized);
            Assert.Equal(original.Aggregate, deserialized.Aggregate);
            Assert.Equal(original.Filter, deserialized.Filter);
            Assert.Equal(original.Group, deserialized.Group);
            Assert.Equal(original.Page, deserialized.Page);
            Assert.Equal(original.Sort, deserialized.Sort);
            Assert.Equal(original.PageSize, deserialized.PageSize);
        }

        [Fact]
        public void CanSerializeWithNullablePropertiesNull()
        {
            // Arrange
            var options = new KendoGridDataSourceRequestOptions
            {
                Aggregate = null,
                Filter = null,
                Group = null,
                Page = 1,
                Sort = null,
                PageSize = 10
            };

            // Act
            var json = JsonSerializer.Serialize(options);
            var deserialized = JsonSerializer.Deserialize<KendoGridDataSourceRequestOptions>(json);

            // Assert
            Assert.NotNull(deserialized);
            Assert.Null(deserialized.Aggregate);
            Assert.Null(deserialized.Filter);
            Assert.Null(deserialized.Group);
            Assert.Equal(1, deserialized.Page);
            Assert.Null(deserialized.Sort);
            Assert.Equal(10, deserialized.PageSize);
        }

        [Fact]
        public void CanDeserializeWithMissingOptionalProperties()
        {
            // Arrange
            var json = @"{
                ""Page"": 1,
                ""PageSize"": 25
            }";

            // Act
            var options = JsonSerializer.Deserialize<KendoGridDataSourceRequestOptions>(json);

            // Assert
            Assert.NotNull(options);
            Assert.Null(options.Aggregate);
            Assert.Null(options.Filter);
            Assert.Null(options.Group);
            Assert.Equal(1, options.Page);
            Assert.Null(options.Sort);
            Assert.Equal(25, options.PageSize);
        }

        [Fact]
        public void DeserializedObjectHasDefaultValuesForIntProperties()
        {
            // Arrange
            var json = @"{}";

            // Act
            var options = JsonSerializer.Deserialize<KendoGridDataSourceRequestOptions>(json);

            // Assert
            Assert.NotNull(options);
            Assert.Equal(0, options.Page);
            Assert.Equal(0, options.PageSize);
        }

        [Fact]
        public void CanSerializeWithSpecialCharactersInStrings()
        {
            // Arrange
            var options = new KendoGridDataSourceRequestOptions
            {
                Filter = "name eq \"O'Brien\" and age gt 30",
                Sort = "name-asc,age-desc",
                Group = "department/team",
                Page = 1,
                PageSize = 10
            };

            // Act
            var json = JsonSerializer.Serialize(options);
            var deserialized = JsonSerializer.Deserialize<KendoGridDataSourceRequestOptions>(json);

            // Assert
            Assert.NotNull(deserialized);
            Assert.Equal(options.Filter, deserialized.Filter);
            Assert.Equal(options.Sort, deserialized.Sort);
            Assert.Equal(options.Group, deserialized.Group);
        }

        [Fact]
        public void CanDeserializeWithCaseInsensitivePropertyNames()
        {
            // Arrange
            var json = @"{
                ""aggregate"": ""sum"",
                ""filter"": ""test"",
                ""group"": ""category"",
                ""page"": 5,
                ""sort"": ""name"",
                ""pageSize"": 15
            }";

            // Act
            var result = JsonSerializer.Deserialize<KendoGridDataSourceRequestOptions>(json, serializationOptions);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("sum", result.Aggregate);
            Assert.Equal("test", result.Filter);
            Assert.Equal("category", result.Group);
            Assert.Equal(5, result.Page);
            Assert.Equal("name", result.Sort);
            Assert.Equal(15, result.PageSize);
        }
    }
}
