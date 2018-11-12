using System;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Configuration;
using HomeWork.Client.Actors;
using HomeWork.Client.Messages;
using HomeWork.Client.Utilities;

namespace HomeWork.Client
{
    class Program
    {
        private static ActorSystem _system;

        static async Task Main(string[] args)
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
            var userAccountsCoordinator = await _system.ActorSelection("akka.tcp://ActorSystem@localhost:8091/user/UserAccountsCoordinator").ResolveOne(TimeSpan.FromSeconds(1));
            _system.ActorOf(Props.Create<UserBalanceActor>(new ConsoleWriter(), userAccountsCoordinator), "UserBalance");
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
