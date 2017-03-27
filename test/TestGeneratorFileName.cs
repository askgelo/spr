using src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace test
{
    class CustomTimeProvider : TimeProvider
    {
        public override DateTime Now { get { return new DateTime(2016, 10, 27); } }
    }

    public class TestGeneratorFileName
    {
        [Theory]
        [InlineData(GeneratorFileName.MaxCountByDate - 1, GeneratorFileName.MaxCountByDate - 2)]
        [InlineData(0, GeneratorFileName.MaxCountByDate - 1)]
        public void TestGenerateName(int newNumber, int current)
        {
            var generator = new GeneratorFileName();
            TimeProvider.Current = new CustomTimeProvider();
            for (int i = 0; i < current; i++) generator.GenerateNextName();

            generator.GenerateNextName();

            string expected = string.Format("{0}{1}{2}", GeneratorFileName.BaseName, "20161027", newNumber);
            Assert.Equal(expected, generator.CurrentName);
        }
    }
}
