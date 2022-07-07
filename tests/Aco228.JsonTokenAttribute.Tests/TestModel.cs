using Newtonsoft.Json;

namespace Aco228.JsonTokenAttribute.Tests;

public class TestModel
{
    
    [JsonToken.JsonToken("status.id", Required = false)]
    public string? ThisOneDoesNotExists { get; set; }
    
    [JsonToken.JsonToken("key")]
    public string Key { get; set; }
    
    [JsonToken.JsonToken("fields.project.id")]
    public string ProjectId { get; set; }
    
    [JsonToken.JsonToken("fields.issuetype.id")]
    public string IssueTypeId { get; set; }
    
    [JsonToken.JsonToken("fields.status.id")]
    public int StatusId { get; set; }
    
    [JsonToken.JsonToken("fields.components[1].name")]
    public string ArrayValue { get; set; }
    
    [JsonToken.JsonToken("fields.customfield_10018")]
    public InlineModel InlineModel { get; set; }
}

public class InlineModel
{
    [JsonProperty("hasEpicLinkFieldDependency")]
    public bool hasEpicLinkFieldDependency { get; set; }
    
    [JsonProperty("showField")]
    public bool showField { get; set; }
}