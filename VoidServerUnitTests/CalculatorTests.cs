using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoidServer.UnitTests
{
    [TestFixture]
    public class CalculatorTests
    {
        [Test]
        public void AdditionIsCorrect()
        {
            //arrange
            VoidServerLibrary.Util.Calculator calc = new VoidServerLibrary.Util.Calculator();
            var answer = calc.Calculate(VoidServerLibrary.Util.Operation.Addition, 1, 2);
            Assert.AreEqual(answer, 3);
        }
        [Test]
        public void AdditionOfNegativesIsCorrect()
        {
            //arrange
            VoidServerLibrary.Util.Calculator calc = new VoidServerLibrary.Util.Calculator();
            var answer = calc.Calculate(VoidServerLibrary.Util.Operation.Addition, 1, -12);
            Assert.AreEqual(answer, -11);
        }

        [Test]
        public void DivisionThrowsArgumentExceptionOnZero()
        {
            VoidServerLibrary.Util.Calculator calc = new VoidServerLibrary.Util.Calculator();            
            var ex = Assert.Throws<ArgumentException>(() => calc.Calculate(VoidServerLibrary.Util.Operation.Division, 0, -12));
            StringAssert.StartsWith("argument cannot be zero", ex.Message);

        }

        [Test]
        public void DivisionIsCorrect()
        {
            VoidServerLibrary.Util.Calculator calc = new VoidServerLibrary.Util.Calculator();
            var answer = calc.Calculate(VoidServerLibrary.Util.Operation.Division, 6, 2);
            Assert.AreEqual(answer, 3);
        }
        [Test]
        public void MultiplicationsAreCorrect()
        {
            VoidServerLibrary.Util.Calculator calc = new VoidServerLibrary.Util.Calculator();
            //multiple test cases
            Assert.Multiple(() =>
            {
                Assert.That(calc.Calculate(VoidServerLibrary.Util.Operation.Multiplication, 6, 2), Is.EqualTo(12));
                Assert.That(calc.Calculate(VoidServerLibrary.Util.Operation.Multiplication, 0, 2), Is.EqualTo(0));
                Assert.That(calc.Calculate(VoidServerLibrary.Util.Operation.Multiplication, -2, 2), Is.EqualTo(-4));
            });
        }
        [Test]
        public void SubtractionIsCorrect()
        {
            VoidServerLibrary.Util.Calculator calc = new VoidServerLibrary.Util.Calculator();
            //multiple test cases
            Assert.Multiple(() =>
            {
                Assert.That(calc.Calculate(VoidServerLibrary.Util.Operation.Subtraction, 3, 2), Is.EqualTo(1));
                Assert.That(calc.Calculate(VoidServerLibrary.Util.Operation.Subtraction, -3, 2), Is.EqualTo(-5));
                Assert.That(calc.Calculate(VoidServerLibrary.Util.Operation.Subtraction, 0, 2), Is.EqualTo(-2));
            });
        }
    }
}
