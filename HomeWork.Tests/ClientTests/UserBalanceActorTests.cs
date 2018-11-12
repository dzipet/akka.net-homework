using System;
using System.Collections.Generic;
using Akka.Actor;
using Akka.TestKit;
using Akka.TestKit.TestActors;
using Akka.TestKit.Xunit2;
using HomeWork.Client.Actors;
using HomeWork.Client.Messages;
using HomeWork.Client.Utilities;
using HomeWork.Common.Messages;
using Moq;
using Xunit;

namespace HomeWork.Tests.ClientTests
{
    public class UserBalanceActorTests : TestKit
    {
        [Fact]
        public void HandleBalanceReportOutputsCorrectInformation()
        {
            //prepare
            var consoleMock = new Mock<IConsoleWriter>();
            var output = string.Empty;
            consoleMock.Setup(x => x.WriteLine(It.IsAny<string>())).Callback((string x) => output = x);

            var actor = ActorOfAsTestActorRef<UserBalanceActor>(Props.Create(() => new UserBalanceActor(consoleMock.Object, null)));
            var message = new BalanceReport(100, new List<double>{50, 50}, Guid.NewGuid());
            
            //execute
            actor.Tell(message);

            //assert
            consoleMock.Verify(x => x.WriteLine(It.IsAny<string>()), Times.Once);
            Assert.Equal($"user {message.UserId} has balance '{message.CurrentBalance}' with last 5 operations: '{string.Join(", ", message.LastOperations)}'", output);
        }

        [Fact]
        public void RequestChangeBalanceOutputsBalanceChangeRequest()
        {
            //prepare
            var consoleMock = new Mock<IConsoleWriter>();
            var output = string.Empty;
            consoleMock.Setup(x => x.WriteLine(It.IsAny<string>())).Callback((string x) => output = x);

            var actor = ActorOfAsTestActorRef<UserBalanceActor>(Props.Create(() => new UserBalanceActor(consoleMock.Object, null)));
            var message = new ChangeBalanceMessage(Guid.NewGuid(), 100);

            //execute
            actor.Tell(message);

            //assert
            consoleMock.Verify(x => x.WriteLine(It.IsAny<string>()), Times.Once);
            Assert.Equal($"User '{message.UserId}' requested change for amount '{message.ChangedAmount}'", output);
        }

        [Fact]
        public void RequestChangeBalanceSendsCorrectMessage()
        {
            //prepare
            var consoleMock = new Mock<IConsoleWriter>();
            var mockRemoteActor = CreateTestProbe();
            var actor = ActorOfAsTestActorRef<UserBalanceActor>(Props.Create(() => new UserBalanceActor(consoleMock.Object, mockRemoteActor)));
            var message = new ChangeBalanceMessage(Guid.NewGuid(), 100);
            
            //execute
            actor.Tell(message);

            //assert
            var sentMessage = mockRemoteActor.ExpectMsg<ChangeBalance>();
            Assert.Equal(message.UserId, sentMessage.UserId);
            Assert.Equal(message.ChangedAmount, sentMessage.ChangedAmount);
        }
    }
}
