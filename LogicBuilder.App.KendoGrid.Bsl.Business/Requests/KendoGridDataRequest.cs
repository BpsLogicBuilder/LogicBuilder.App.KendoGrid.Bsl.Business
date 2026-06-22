using LogicBuilder.Expressions.Utils.ExpansionDescriptors;

namespace LogicBuilder.App.KendoGrid.Bsl.Business.Requests
{
    public class KendoGridDataRequest
    {
        public string? DataType { get; set; }
        public string? ModelType { get; set; }
        public KendoGridDataSourceRequestOptions? Options { get; set; }
        public SelectExpandDefinitionDescriptor? SelectExpandDefinition { get; set; }
    }
}
