using NUnit.Framework;
using Warewolf.Test.Agent;

namespace Dev2.Activities.Specs
{
    [TestFixture]
    [SetUpFixture]
    public static class BuildConfig
    {
        [OneTimeSetUp]
        public static void Apply() => TestLauncher.EnableDocker = Job_Definitions.GetEnableDockerValue();
    }
}
