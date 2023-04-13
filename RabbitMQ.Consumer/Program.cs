using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;


// Bağlantı Oluşturma

ConnectionFactory factory = new ();
factory.Uri = new Uri("");

// Bağlantı aktifleştirme ve kanal açma

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

// Queue oluşturma

channel.QueueDeclare(queue: "example-queue", exclusive: false); // Consumer'da da kuyruk publisherdaki ile bire bir aynı yapılandırmada tanımlanmalıdır.


// Queue dan mesaj okuma.

// Channel üzerinden bir event operasyonu için

EventingBasicConsumer consumer = new (channel);

channel.BasicConsume(queue: "example-queue", autoAck: true, consumer);

while (true)
{

    consumer.Received += (sender, e) =>
    {
        // Kuyruktan gelen mesajın işlendiği yer.

        // e.Body : Kuyruktaki mesajın verisini bütünsel olarak getirecektir.

        // e.Body.Span veya e.Body.ToArray() : Kuyruktaki mesajın byte mesajını verecektir.

        Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));

    };
    Console.ReadLine();
}


