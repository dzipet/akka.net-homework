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
        private static ActorSystem _system;

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

            _system = ActorSystem.Create("ActorSystem", config);
            _system.ActorOf(Props.Create<UserBalanceActor>(), "UserBalance");
            Console.WriteLine("Client started");

            // presentation

            var user1 = Guid.NewGuid();
            var user2 = Guid.NewGuid();

            SendBalanceChangeMessage(user1, 100);
            SendBalanceChangeMessage(user2, -50);
            SendBalanceChangeMessage(user1, 15);
            SendBalanceChangeMessage(user2, 200);
            SendBalanceChangeMessage(user1, -58);
            SendBalanceChangeMessage(user2, 40);
            SendBalanceChangeMessage(user2, -20);
            SendBalanceChangeMessage(user2, 66);
            SendBalanceChangeMessage(user2, -40);
            SendBalanceChangeMessage(user2, 15);


            

            Console.ReadLine();
        }

        private static void SendBalanceChangeMessage(Guid userId, double changedAmount)
        {
            var message = new ChangeBalanceMessage(userId, changedAmount);
            _system.ActorSelection("akka://ActorSystem/user/UserBalance").Tell(message);
        }
    }
}
