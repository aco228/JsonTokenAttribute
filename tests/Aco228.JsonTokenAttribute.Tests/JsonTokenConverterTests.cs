using Aco228.JsonToken;

namespace Aco228.JsonTokenAttribute.Tests;

public class JsonTokenConverterTests
{

    [Fact]
    public void Should_Parse_Json()
    {
        var data = File.ReadAllText("./random.json");
        var model = JsonTokenConverter.Convert<TestModel>(data);
        
        Assert.NotNull(model);
        Assert.Equal("III2-2558", model!.Key);
        Assert.Equal("10038", model.ProjectId);
        Assert.Equal("10004", model.IssueTypeId);
        Assert.Equal(10102, model.StatusId);
        Assert.Equal("Aleksandar", model.ArrayValue);
        
        Assert.False(model.InlineModel.hasEpicLinkFieldDependency);
        Assert.True(model.InlineModel.showField);
        Assert.Null(model.ThisOneDoesNotExists);
    }
    
    
}