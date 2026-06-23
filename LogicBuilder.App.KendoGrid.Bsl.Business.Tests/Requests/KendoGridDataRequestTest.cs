using LogicBuilder.App.KendoGrid.Bsl.Business.Requests;
using LogicBuilder.Expressions.Utils.ExpansionDescriptors;
using System.Text.Json;
using Xunit;

namespace LogicBuilder.App.KendoGrid.Bsl.Business.Tests.Requests
{
    public class KendoGridDataRequestTest
    {
        private static readonly JsonSerializerOptions serializationOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        [Fact]
        public void CanSerializeToJson()
        {
            // Arrange
            var request = new KendoGridDataRequest
            {
                DataType = "Customer",
                ModelType = "CustomerModel",
                Options = new KendoGridDataSourceRequestOptions
                {
                    Page = 1,
                    PageSize = 10,
                    Sort = "name-asc",
                    Filter = "active eq true"
                },
                SelectExpandDefinition = new SelectExpandDefinitionDescriptor
                {
                    ExpandedItems = []
                }
            };

            // Act
            var json = JsonSerializer.Serialize(request);

            // Assert
            Assert.NotNull(json);
            Assert.Contains("\"DataType\":\"Customer\"", json);
            Assert.Contains("\"ModelType\":\"CustomerModel\"", json);
            Assert.Contains("\"Options\":", json);
            Assert.Contains("\"SelectExpandDefinition\":", json);
        }

        [Fact]
        public void CanDeserializeFromJson()
        {
            // Arrange
            var json = @"{
                ""DataType"": ""Product"",
                ""ModelType"": ""ProductModel"",
                ""Options"": {
                    ""Page"": 2,
                    ""PageSize"": 20,
                    ""Sort"": ""price-desc"",
                    ""Filter"": ""category eq 'Electronics'""
                },
                ""SelectExpandDefinition"": null
            }";

            // Act
            var request = JsonSerializer.Deserialize<KendoGridDataRequest>(json);

            // Assert
            Assert.NotNull(request);
            Assert.Equal("Product", request.DataType);
            Assert.Equal("ProductModel", request.ModelType);
            Assert.NotNull(request.Options);
            Assert.Equal(2, request.Options.Page);
            Assert.Equal(20, request.Options.PageSize);
            Assert.Equal("price-desc", request.Options.Sort);
            Assert.Equal("category eq 'Electronics'", request.Options.Filter);
            Assert.Null(request.SelectExpandDefinition);
        }

[Fact]
        public void CanRoundTripSerializeAndDeserialize()
        {
            // Arrange
            var original = new KendoGridDataRequest
            {
                DataType = "Order",
                ModelType = "OrderModel",
                Options = new KendoGridDataSourceRequestOptions
                {
                    Page = 3,
                    PageSize = 50,
                    Sort = "orderDate-desc",
                    Filter = "total gt 100",
                    Group = "customerId",
                    Aggregate = "sum"
                },
                SelectExpandDefinition = new SelectExpandDefinitionDescriptor
                {
                    ExpandedItems = []
                }
            };

            // Act
            var json = JsonSerializer.Serialize(original);
            var deserialized = JsonSerializer.Deserialize<KendoGridDataRequest>(json);

            // Assert
            Assert.NotNull(deserialized);
            Assert.Equal(original.DataType, deserialized.DataType);
            Assert.Equal(original.ModelType, deserialized.ModelType);
            Assert.NotNull(deserialized.Options);
            Assert.Equal(original.Options.Page, deserialized.Options.Page);
            Assert.Equal(original.Options.PageSize, deserialized.Options.PageSize);
            Assert.Equal(original.Options.Sort, deserialized.Options.Sort);
            Assert.Equal(original.Options.Filter, deserialized.Options.Filter);
            Assert.Equal(original.Options.Group, deserialized.Options.Group);
            Assert.Equal(original.Options.Aggregate, deserialized.Options.Aggregate);
        }

        [Fact]
        public void CanSerializeWithAllPropertiesNull()
        {
            // Arrange
            var request = new KendoGridDataRequest
            {
                DataType = null,
                ModelType = null,
                Options = null,
                SelectExpandDefinition = null
            };

            // Act
            var json = JsonSerializer.Serialize(request);
            var deserialized = JsonSerializer.Deserialize<KendoGridDataRequest>(json);

            // Assert
            Assert.NotNull(deserialized);
            Assert.Null(deserialized.DataType);
            Assert.Null(deserialized.ModelType);
            Assert.Null(deserialized.Options);
            Assert.Null(deserialized.SelectExpandDefinition);
        }

        [Fact]
        public void CanDeserializeWithMissingProperties()
        {
            // Arrange
            var json = @"{
                ""DataType"": ""Employee""
            }";

            // Act
            var request = JsonSerializer.Deserialize<KendoGridDataRequest>(json);

            // Assert
            Assert.NotNull(request);
            Assert.Equal("Employee", request.DataType);
            Assert.Null(request.ModelType);
            Assert.Null(request.Options);
            Assert.Null(request.SelectExpandDefinition);
        }

        [Fact]
        public void CanDeserializeEmptyJson()
        {
            // Arrange
            var json = @"{}";

            // Act
            var request = JsonSerializer.Deserialize<KendoGridDataRequest>(json);

            // Assert
            Assert.NotNull(request);
            Assert.Null(request.DataType);
            Assert.Null(request.ModelType);
            Assert.Null(request.Options);
            Assert.Null(request.SelectExpandDefinition);
        }

        [Fact]
        public void CanSerializeWithOnlyDataTypeAndModelType()
        {
            // Arrange
            var request = new KendoGridDataRequest
            {
                DataType = "Invoice",
                ModelType = "InvoiceModel"
            };

            // Act
            var json = JsonSerializer.Serialize(request);
            var deserialized = JsonSerializer.Deserialize<KendoGridDataRequest>(json);

            // Assert
            Assert.NotNull(deserialized);
            Assert.Equal("Invoice", deserialized.DataType);
            Assert.Equal("InvoiceModel", deserialized.ModelType);
            Assert.Null(deserialized.Options);
            Assert.Null(deserialized.SelectExpandDefinition);
        }

        [Fact]
        public void CanDeserializeWithCaseInsensitivePropertyNames()
        {
            // Arrange
            var json = @"{
                ""dataType"": ""Customer"",
                ""modelType"": ""CustomerModel"",
                ""options"": {
                    ""page"": 1,
                    ""pageSize"": 10
                }
            }";

            // Act
            var request = JsonSerializer.Deserialize<KendoGridDataRequest>(json, serializationOptions);

            // Assert
            Assert.NotNull(request);
            Assert.Equal("Customer", request.DataType);
            Assert.Equal("CustomerModel", request.ModelType);
            Assert.NotNull(request.Options);
            Assert.Equal(1, request.Options.Page);
            Assert.Equal(10, request.Options.PageSize);
        }

        [Fact]
        public void CanSerializeNestedOptionsWithSpecialCharacters()
        {
            // Arrange
            var request = new KendoGridDataRequest
            {
                DataType = "Person",
                ModelType = "PersonModel",
                Options = new KendoGridDataSourceRequestOptions
                {
                    Filter = "name eq \"O'Reilly\" and age gt 21",
                    Sort = "lastName-asc,firstName-asc",
                    Page = 1,
                    PageSize = 25
                }
            };

            // Act
            var json = JsonSerializer.Serialize(request);
            var deserialized = JsonSerializer.Deserialize<KendoGridDataRequest>(json);

            // Assert
            Assert.NotNull(deserialized);
            Assert.NotNull(deserialized.Options);
            Assert.Equal(request.Options.Filter, deserialized.Options.Filter);
            Assert.Equal(request.Options.Sort, deserialized.Options.Sort);
        }

        [Fact]
        public void CanSerializeWithComplexSelectExpandDefinition()
        {
            // Arrange
            var request = new KendoGridDataRequest
            {
                DataType = "Order",
                ModelType = "OrderModel",
                SelectExpandDefinition = new SelectExpandDefinitionDescriptor
                {
                    ExpandedItems =
                    [
                        new SelectExpandItemDescriptor("Customer"),
                        new SelectExpandItemDescriptor("OrderItems")
                    ]
                }
            };

            // Act
            var json = JsonSerializer.Serialize(request);
            var deserialized = JsonSerializer.Deserialize<KendoGridDataRequest>(json);

            // Assert
            Assert.NotNull(deserialized);
            Assert.NotNull(deserialized.SelectExpandDefinition);
            Assert.NotNull(deserialized.SelectExpandDefinition.ExpandedItems);
            Assert.Equal(2, deserialized.SelectExpandDefinition.ExpandedItems.Count);
        }
    }
}
