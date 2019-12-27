using NUnit.Framework;
using Warewolf.Test.Agent;

namespace Dev2.Activities.Specs
{
    [TestFixture]
    public static class BuildConfig
    {
        [OneTimeSetUp]
        public static void Apply(TestContext context) => TestLauncher.EnableDocker = Job_Definitions.GetEnableDockerValue();
    }
}
