/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2018 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using Dev2.Activities.Designers2.DateTimeDifference;
using System.Activities.Presentation.Model;

namespace Dev2.Activities.Designers.Tests.DateTimeDifference
{
    public class TestDateTimeDifferenceDesignerViewModel : DateTimeDifferenceDesignerViewModel
    {
        public TestDateTimeDifferenceDesignerViewModel(ModelItem modelItem)
            : base(modelItem)
        {
        }
        public string InputFormat { set { SetProperty(value); } get { return GetProperty<string>(); } }
        public string OutputType { get { return GetProperty<string>(); } set { SetProperty(value); } }
    }
    public class TestDotNetDateTimeDifferenceDesignerViewModel : Dev2.Activities.Designers2.DateTimeDifferenceStandard.DateTimeDifferenceDesignerViewModel
    {
        public TestDotNetDateTimeDifferenceDesignerViewModel(ModelItem modelItem)
            : base(modelItem)
        {
        }
        public string InputFormat { set { SetProperty(value); } get { return GetProperty<string>(); } }
        public string OutputType { get { return GetProperty<string>(); } set { SetProperty(value); } }
    }
}
