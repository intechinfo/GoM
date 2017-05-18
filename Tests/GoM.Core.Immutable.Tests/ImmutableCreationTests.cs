using System;
using Xunit;
using System.Collections.Immutable;
using FluentAssertions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GoM.Core.Mutable.Tests;
using GoM.Core.Mutable;

namespace GoM.Core.Immutable.Tests
{
    public class CreationTests
    {
        [Fact]
        public void Convert_mutable_GoMContext_to_immutable()
        {
            var create = new MutableCreationTests();
            var ImmutableGoMContext = GoMContext.Create(create.CreateTestGoMContext());
            (ImmutableGoMContext is Immutable.GoMContext).Should().BeTrue();         
        }

        public void test()
        {

        }
    }
}
