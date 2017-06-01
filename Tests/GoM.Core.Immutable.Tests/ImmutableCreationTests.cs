using Xunit;
using FluentAssertions;
using GoM.Core.Mutable.Tests;
using GoM.Core.Mutable;
using GoM.Core.Immutable;

namespace GoM.Core.Immutable.Tests
{
    public class CreationTests
    {
        [Fact]
        public void Convert_mutable_GoMContext_to_immutable_to_immutable_to_mutable_to_mutable_and_transform_using_ToImmutable_and_ToImmutable()
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

            //Testing some ToImmutable() and ToMutable() extension methods
            (MutableGoMContext2.ToImmutable() is Immutable.GoMContext).Should().BeTrue();
            (MutableGoMContext2.Repositories[0].ToImmutable() is Immutable.BasicGitRepository).Should().BeTrue();
            (ImmutableGoMContext2.ToMutable() is Mutable.GoMContext).Should().BeTrue();
            (ImmutableGoMContext2.Repositories[0].Details.Branches[0].ToMutable() is Mutable.BasicGitBranch).Should().BeTrue();

            //Idempotent
            (ImmutableGoMContext2.ToImmutable() is Immutable.GoMContext).Should().BeTrue();
            (MutableGoMContext2.ToMutable() is Mutable.GoMContext).Should().BeTrue();

        }
    }
}
