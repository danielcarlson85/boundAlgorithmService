using Microsoft.Graph;
using NSubstitute;

namespace Bound.AlgorithmService.Tests.AttributeHelper
{
    public class AttributeHelpersFixture
    {
        public User User { get; set; }

        public AttributeHelpersFixture()
        {
            User = Substitute.For<User>();
        }
    }
}
