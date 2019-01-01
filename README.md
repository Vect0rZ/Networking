# Networking
Resilient TCP data transfer.

# What is this repository all about?
This is a class library providing interface for receiving packet based data, instead of continuous stream, with minimum additional application load and based on a simple header and data tagging.

# Examples

## Server
```csharp
using System;
using Networking;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 1234);
            server.Start();
            
            while (true)
            {
                // Wait for a client
                TcpClient client = server.AcceptTcpClient();
                
                // Create client stream
                TcpStream stream = new TcpStream(client.GetStream());
                
                // Reply
                stream.Send("Echo!");
            }
        }
    }
}
```

## Client
```csharp
using System;
using Networking;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient();
            client.Connect(IPAddress.Parse("127.0.0.1"), 1234);
            
            // Create a new TcpStream from Networking CL
            TcpStream stream = new TcpStream(client.GetStream());
            
            while (true)
            {
                // Blocks the main thread while queueing stream data, until the whole message is received
                string data = _stream.Receive();
                
                // Message
                Console.WriteLine(data);
            }
        }
    }
}

```
