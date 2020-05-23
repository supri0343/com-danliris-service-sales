using Com.Danliris.Service.Sales.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Sales.Test.Helpers

{
    public class EmailTest
    {
        [Fact]
        public void IsValid_Return_True()
        {
            var result = Email.IsValid("elfatih@gmail.com");
            Assert.True(result);
        }

        [Fact]
        public void IsValid_Return_False()
        {
            var result = Email.IsValid("elfatih@gmail");
            Assert.False(result);
        }
    }
}
