using System;
using System.Threading;
using Akka.Actor;
using Akka.Configuration;
using HomeWork.Client.Actors;
using HomeWork.Client.Messages;

namespace HomeWork.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(1000);
            Console.WriteLine("Starting client");

            var config = ConfigurationFactory.ParseString(@"
                    akka {  
                        actor {
                            provider = remote
                        }
                        remote {
                             dot-netty.tcp {
                                port = 0
                                hostname = localhost
                            }
                        }
                    }");

            var system = ActorSystem.Create("ActorSystem", config);
            system.ActorOf(Props.Create<UserBalanceActor>(), "UserBalance");

            Console.WriteLine("Client started");

            var message = new ChangeBalanceMessage(Guid.NewGuid(), 100);
            system.ActorSelection("akka://ActorSystem/user/UserBalance").Tell(message);

            Console.ReadLine();
        }
    }
}
