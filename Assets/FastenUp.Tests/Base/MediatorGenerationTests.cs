using FastenUp.Runtime.Base;
using FastenUp.Runtime.Bindables;
using NSubstitute;
using NUnit.Framework;

namespace FastenUp.Tests.Base
{
    [TestFixture]
    public class MediatorGenerationTests
    {
        [Test]
        public void Test()
        {
            //Arrange
            var bindable = Substitute.For<IBindable<string>>();
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
        private BindPoint<string> Text { get; } = new();

        public TestMediator(string text) => 
            Text.Value = text;

        public void SetText(string text) => 
            Text.Value = text;
    }
}