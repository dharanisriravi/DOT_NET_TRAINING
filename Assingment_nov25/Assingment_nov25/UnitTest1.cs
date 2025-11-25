using Moq;
using NUnit.Framework;
namespace Assingment_nov25
{
    public class UnitTest1
    {
        [Test]
        public void Test1()
        {
            var m = new Mock<ICalculator>();
            m.Setup(c => c.Add(2, 3)).Returns(5);

            int result = m.Object.Add(2, 3);

            Assert.That(result, Is.EqualTo(5));
            m.Verify(c => c.Add(2, 3), Times.Once);
        }
    }
}
