/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2019 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using System;
using Dev2.Common.Interfaces.Scheduler.Interfaces;
using NUnit.Framework;

namespace Dev2.Scheduler.Test
{
    [TestFixture]
    [SetUpFixture]
    public class EventInfoTest
    {
        [Test]
        [Author("Leon Rajindrapersadh")]
        [Category("TaskSheduler_EventInfo_Test")]
        public void TaskSheduler_EventInfo_ShouldconstructCorrectly()
        {

            var ie = new EventInfo(new DateTime(2000, 1, 1), TimeSpan.MaxValue, new DateTime(2001, 1, 1),ScheduleRunStatus.Error, 
                                         "12345");
            Assert.AreEqual(new DateTime(2000, 1, 1), ie.StartDate);
            Assert.AreEqual(new DateTime(2001, 1, 1), ie.EndDate);
            Assert.AreEqual(TimeSpan.MaxValue, ie.Duration);
            Assert.AreEqual(ScheduleRunStatus.Error, ie.Success);
            Assert.AreEqual("12345", ie.EventId);


        }
    }
}
