using Gremlin.Net.Driver;
using Gremlin.Net.Structure.IO.GraphSON;
using Newtonsoft.Json;
using Route.API;
using System.Text.Json;
using System.Text.Json.Nodes;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var gremlinServer = new GremlinServer(GremlinSettings.hostname,port:GremlinSettings.port, enableSsl:true,username: "/dbs/" + GremlinSettings.database + "/colls/" + GremlinSettings.collection, password: GremlinSettings.authKey);

app.MapGet("/{location:alpha:length(3):required}", async (string location) =>
{
    var response = new
    {
        Destination = location,
        Routes = new List<List<string>>()
    };
    using var client = new GremlinClient(gremlinServer, new GraphSON2MessageSerializer());

    var dbResponse =  JsonConvert.SerializeObject(await client.SubmitAsync<dynamic>($"g.V().hasLabel('USA').repeat(out().simplePath()).until(hasLabel('{location}')).limit(3).path().by('label')"));

    var res = JsonConvert.DeserializeObject<List<Dictionary<dynamic,dynamic>>>(dbResponse);
    foreach(var item in res)
    {
        item.TryGetValue("objects", out var result);
        response.Routes.Add(result.ToObject<List<string>>());
    }

    return response;
});

app.Run();

