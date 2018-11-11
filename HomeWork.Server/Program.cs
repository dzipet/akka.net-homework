using System;
using Akka.Actor;
using Akka.Configuration;
using HomeWork.Server.Actors;

namespace HomeWork.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting server");
            var config = ConfigurationFactory.ParseString(@"
                    akka {  
                        actor {
                            provider = remote
                        }
                        remote {
                             dot-netty.tcp {
                                port = 8091
                                hostname = localhost
                            }
                        }
                    }");

            var system = ActorSystem.Create("ActorSystem", config);
            var actor = system.ActorOf(Props.Create<UserAccountsCoordinatorActor>(), "UserAccountsCoordinator");

            Console.WriteLine("Server started");

            Console.ReadLine();
        }
    }
} //       akka://ActorSystem/user/UserAccountsCoordinator
