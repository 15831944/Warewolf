/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2019 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using NUnit.Framework;

namespace Dev2.Core.Tests.Diagnostics
{
    [TestFixture]
    [SetUpFixture]
    public class FileHelperTests
    {
        static string NewPath;
        static string OldPath;
        static TestContext Context;

        [OneTimeSetUp]
        public static void ClassInit(TestContext testContext)
        {
            Context = testContext;
            NewPath = Context.TestDirectory + @"\Warewolf\";
            OldPath = Context.TestDirectory + @"\Dev2\";
        }

        #region Migrate Temp Data

        #endregion

        #region Create Directory from String


        #endregion
    }
}
