using FastenUp.Runtime.Bindables;
using FastenUp.Runtime.Binders;
using FastenUp.Runtime.Mediators;
using FastenUp.Runtime.Utils;
using NSubstitute;
using NUnit.Framework;

namespace FastenUp.Tests.Mediators
{
    [TestFixture]
    [TestOf(typeof(IInternalMediator))]
    public class MediatorGenerationTests
    {
        [Test]
        public void Test()
        {
            //Arrange
            var bindable = Substitute.For<IBinder<string>>();
            bindable.Name.Returns("Text");
            var mediator = new TestMediator("Test");
            //Act & Assert
            mediator.Bind(bindable);
            bindable.Received(1).SetValue("Test");
            mediator.SetText("Test2");
            bindable.Received(1).SetValue("Test2");
            mediator.Unbind(bindable);
            mediator.SetText("Test3");
            bindable.DidNotReceive().SetValue("Test3");
        }
    }
    
    internal sealed partial class TestMediator : IMediator
    {
        private Bindable<string> Text { get; } = new();

        public TestMediator(string text) => 
            Text.Value = text;

        public void SetText(string text) => 
            Text.Value = text;
        
    }

    //TODO: Remove after source generator update
    internal partial class TestMediator : IInternalMediator
    {
        /// <inheritdoc />
        public void Bind(IBinder binder)
        {
            BindUtilities.TryBind(Text, nameof(Text), binder);
        }

        /// <inheritdoc />
        public void Unbind(IBinder binder)
        {
            BindUtilities.TryUnbind(Text, nameof(Text), binder);
        }
    }
}