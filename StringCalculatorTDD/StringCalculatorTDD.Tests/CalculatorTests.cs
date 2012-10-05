using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using Xunit;

namespace StringCalculatorTDD.Tests
{
    public class CalculatorTests
    {
        [Fact]
        public void for_an_empty_string_it_will_return_0()
        {
            // Arrange
            //var _stringCalculator = new Calculator();

            //Act
            double actual = Calculator.Calculate("");
            double expected = 0;

            //Assert
            actual.Should().BeInRange(0, expected);
        }

       [Fact]
        public void for_string_2_it_should_return_2()
       {
           double actual = Calculator.Calculate("2");
           double expected = 2;

           actual.Should().BeInRange(expected, 2);
       }

        [Fact]
        public void for_string_2plus2_should_return_4()
        {
            double actual = Calculator.Calculate("2+2");
            double expected = 4;
            actual.Should().BeInRange(expected, 4);
        }
        //[Fact]
        //public void for_string5minus2_should_return3()
        //{
        //    double actual = Calculator.Calculate("5-2");
        //    double expected = 3;
        //    actual.Should().BeInRange(expected, 3);
        //}

        [Fact]
        public void Can_Build_Reverse_Polish_Notation_For_5_plus_2()
        {

            var actual = Calculator.ReversePolishNotashion("5+2");
            //var expected = new List<Value>()
            //    {
            //        new NumericValue(5),
            //        new NumericValue(2),
            //        new OperatorValue('+')

            //    };

            var actualString = ExpressionListToString(actual);
            //var expectedString = ExpressionListToString(expected);

            actualString.Should().Be("52+");
        }

        [Fact]
        public void Can_Build_Reverse_Polish_Notation_For_5_plus_2_multip_3()
        {
            var actual = Calculator.ReversePolishNotashion("5+2*3");
            var actualString = ExpressionListToString(actual);
            actualString.Should().Be("523*+");
        }
        private static string ExpressionListToString(List<Value> actual)
        {
            return string.Join("",actual.Select(s => s.ToString()).ToList());
        }
        [Fact]
        public void Calculate_string_5plus2mult3()
        {
            var actual = Calculator.Calculate("5+2*3");
            var expected=11;
            actual.Should().BeInRange(expected, 11);
        }
        [Fact]
        public void Can_Build_Reverse_Polish_Notation_For_5_plus_2_multip_3minus4()
        {
            var actual = Calculator.ReversePolishNotashion("5+2*3-4");
            var actualString = ExpressionListToString(actual);
            actualString.Should().Be("523*+4-");
        }
        [Fact]
        public void Calculate_string_5plus2mult3minus4()
        {
            var actual = Calculator.Calculate("5+2*3-4");
            var expected = 7;
            actual.Should().BeInRange(expected, 7);
        }
        [Fact]
        public void Calculate_string_8plus2mult5minus8del2()
        {
            var actual = Calculator.Calculate("8+2*5-8/2");
            var expected = 14;
            actual.Should().BeInRange(expected, 14);
        }
        [Fact]
        public void Calculate_string_8plus2mult5minus10del2()
        {
            var actual = Calculator.Calculate("8+2*5-10/2");
            var expected = 13;
            actual.Should().BeInRange(expected, 13);
        }
        [Fact]
        public void Calculate_string_820plus2mult5minus10del2()
        {
            var actual = Calculator.Calculate("820+2*5-10/2");
            var expected = 825;
            actual.Should().BeInRange(expected, 825);
        }
        [Fact]
        public void Calculate_string_8plus2mult5minus10del2vstep3()
        {
            var actual = Calculator.Calculate("8+2*5-10/2^3");
            var expected = 16.75;
            actual.Should().BeInRange(expected, 16.76);
        }
        [Fact]
        public void Calculate_string_whith_brackets()
        {
            var actual = Calculator.Calculate("2*(5+2)+3");
            var expected = 17;
            actual.Should().BeInRange(expected, 17);
        }
        [Fact]
        public void Calculate_string_whith_brackets2()
        {
            var actual = Calculator.Calculate("16/(5+3)-(14-8)");
            var expected = -4;
            actual.Should().BeInRange(expected, -4);
        }
        //[Fact]
        //public void Calculate_negative_numbers()
        //{
        //    var actual = Calculator.Calculate("5*(-3)+2");
        //    var expected = -13;
        //    actual.Should().BeInRange(expected, -13); 
        //}
    }
}
