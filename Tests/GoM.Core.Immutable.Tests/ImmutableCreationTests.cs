using Xunit;
using FluentAssertions;
using GoM.Core.Mutable.Tests;
using GoM.Core.Mutable;

namespace GoM.Core.Immutable.Tests
{
    public class CreationTests
    {
        [Fact]
        public void Convert_mutable_GoMContext_to_immutable_to_immutable_to_mutable_to_mutable()
        {
            //Mutable to immutable
            var MutableGoMContext1 = new MutableCreationTests();
            var ImmutableGoMContext1 = Immutable.GoMContext.Create(MutableGoMContext1.CreateTestGoMContext());

            (ImmutableGoMContext1 is Immutable.GoMContext).Should().BeTrue();

            //Immutable to Immutable
            var ImmutableGoMContext2 = GoMContext.Create(ImmutableGoMContext1);

            (ImmutableGoMContext2 is Immutable.GoMContext).Should().BeTrue();
            
            //Immutable to Mutable
            var MutableGoMContext2 = new Mutable.GoMContext(ImmutableGoMContext2);

            (MutableGoMContext2 is Mutable.GoMContext).Should().BeTrue();

            //Mutable to Mutable
            (new Mutable.GoMContext(MutableGoMContext2) is Mutable.GoMContext).Should().BeTrue();
        }
    }
}
