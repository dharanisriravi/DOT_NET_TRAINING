using Moq;
using NUnit.Framework;

namespace Assignment_6
{
    public class UnitTest1
    {
        // QUESTION - 1
        [Test]
        public void Test1()
        {
            var m = new Mock<ICalculator>();
            m.Setup(c => c.Add(2, 3)).Returns(5);

            int result = m.Object.Add(2, 3);

            Assert.That(result, Is.EqualTo(5));
            m.Verify(c => c.Add(2, 3), Times.Once);
        }

        // QUESTION - 2
        [Test]
        public void Test2()
        {
            var m = new Mock<ICustomerRepository>();
            var customer = new Customer { Id = 1, Name = "john" };
            m.Setup(r => r.GetCustomerById(1)).Returns(customer);
            var service = new CustomerService(m.Object);

            string result = service.GetCustomerName(1);
            Assert.That(result, Is.EqualTo("john"));
        }

        // QUESTION - 3
        [Test]
        public void Test3()
        {
            var m = new Mock<ICustomerRepository>();
            m.Setup(r => r.GetCustomerById(-1)).Throws(new ArgumentException());

            var service = new CustomerService(m.Object);

            Assert.Throws<ArgumentException>(() => service.GetCustomerName(-1));
        }

        // QUESTION - 4
        [Test]
        public void Test4()
        {
            var m = new Mock<IParser>();
            m.Setup(m => m.TryParse("123", out It.Ref<string>.IsAny))
                .Callback((string input, out string output) =>
                {
                    output = "123";
                })
                .Returns(true);
            string result;

            bool success = m.Object.TryParse("123", out result);

            Assert.That(success, Is.True);
            Assert.That(result, Is.EqualTo("123"));

            m.Verify(m => m.TryParse("123", out It.Ref<string>.IsAny), Times.Once);
        }

        // QUESTION - 5
        [Test]
        public void Test5()
        {
            var m = new Mock<ILogger>();
            var processor = new ILogger.Processor(m.Object);
            processor.Process();
            m.Verify(l => l.Log(It.IsAny<string>()), Times.Exactly(3));
        }

        // QUESTION - 6
        [Test]
        public void Test6()
        {
            var m = new Mock<IConfig>();
            m.SetupGet(c => c.Environment).Returns("Production");

            var checker = new EnvironmentChecker(m.Object);
            bool result = checker.IsProduction();

            Assert.That(result, Is.True);
            m.VerifyGet(c => c.Environment, Times.Once);
        }

        // QUESTION - 7
        [Test]
        public void Test7()
        {
            var m = new Mock<INotifier>();
            string Message = null;
            m.Setup(n => n.Notify(It.IsAny<string>())).Callback<string>(msg => Message = msg);

            m.Object.Notify("Hello World");

            Assert.That(Message, Is.EqualTo("Hello World"));
        }

        // QUESTION - 8
        [Test]
        public void Test8()
        {
            var m = new Mock<DataReader>();
            m.SetupSequence(r => r.ReadLine())
                      .Returns("First")
                      .Returns("Second")
                      .Returns("Third");

            var collector = new Line(m.Object);

            List<string> result = collector.ReadThreeLines();

            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result[0], Is.EqualTo("First"));
            Assert.That(result[1], Is.EqualTo("Second"));
            Assert.That(result[2], Is.EqualTo("Third"));
        }
    }
}

