using Confluent.Kafka;
using Newtonsoft.Json;

var config = new ProducerConfig{BootstrapServers = "localhost:9092"};

using var producer=new ProducerBuilder<Null, string>(config).Build();

try
{
	string? product;
	while ((product = Console.ReadLine()) != null)
	{
		var resp = await producer.ProduceAsync("order-topic",
			new Message<Null, string> { Value = JsonConvert.SerializeObject(new Order(product, 4)) }
			);
		Console.WriteLine(resp.Value);
	}
}
catch (ProduceException<Null,string> exe)
{
	Console.WriteLine(exe.Message);
}
public record Order(string product, int quantity);
