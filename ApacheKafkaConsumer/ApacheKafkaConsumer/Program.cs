using Confluent.Kafka;
using Newtonsoft.Json;

var config = new ConsumerConfig {
    BootstrapServers = "localhost:9092" ,
    GroupId="order-consumer-group",
    AutoOffsetReset=AutoOffsetReset.Earliest
};

using var consumer=new ConsumerBuilder<Null,string>(config).Build();
consumer.Subscribe("order-topic");
var token=new CancellationTokenSource();
try
{
    while (true)
    {
        var resp=consumer.Consume(token.Token);
        if(resp.Message!=null)
        {
            Console.WriteLine(resp.Message.Value);
           // var record = JsonConvert.DeserializeObject(resp.Message.Value);
        }
    }
}
catch (ConsumeException ex)
{
  Console.WriteLine(ex.Message);
}


public record Order(string product, int quantity);
