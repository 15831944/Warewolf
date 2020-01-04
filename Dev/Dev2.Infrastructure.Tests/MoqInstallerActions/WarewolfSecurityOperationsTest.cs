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
using System.Runtime.InteropServices;
using Dev2.Services.Security.MoqInstallerActions;
using NUnit.Framework;
using Warewolf.UnitTestAttributes;

namespace Dev2.Infrastructure.Tests.MoqInstallerActions
{
    [TestFixture]
    public class WarewolfSecurityOperationsTest
    {
        
        [Test]
        [Author("Travis Frisinger")]
        [Category("WarewolfSecurityOperations_AddWarewolfGroup")]
        public void WarewolfSecurityOperations_AddWarewolfGroup_ExpectGroupAdded()
        {
            var grpOps = MoqInstallerActionFactory.CreateSecurityOperationsObject();
            grpOps.DeleteWarewolfGroup();
            grpOps.AddWarewolfGroup();
            var result = grpOps.DoesWarewolfGroupExist();

            Assert.IsTrue(result);
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("WarewolfSecurityOperations_DoesWarewolfGroupExist")]
        public void WarewolfSecurityOperationsDoesWarewolfGroupExistWhenGroupDoesNotExistExpectFalse()
        {
            //------------Setup for test--------------------------
            var warewolfGroupOps = MoqInstallerActionFactory.CreateSecurityOperationsObject();
            warewolfGroupOps.DeleteWarewolfGroup();

            //------------Execute Test---------------------------
            var result = warewolfGroupOps.DoesWarewolfGroupExist();

            //------------Assert Results-------------------------
            Assert.IsFalse(result);
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("WarewolfSecurityOperations_DoesWarewolfGroupExist")]
        public void WarewolfSecurityOperations_DoesWarewolfGroupExist_WhenGroupDoesExist_ExpectTrue()
        {
            //------------Setup for test--------------------------
            var warewolfGroupOps = MoqInstallerActionFactory.CreateSecurityOperationsObject();
            warewolfGroupOps.DeleteWarewolfGroup();
            warewolfGroupOps.AddWarewolfGroup();

            //------------Execute Test---------------------------
            var result = warewolfGroupOps.DoesWarewolfGroupExist();

            //------------Assert Results-------------------------
            Assert.IsTrue(result);
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("WarewolfSecurityOperations_DeleteGroup")]
        public void WarewolfSecurityOperations_DeleteGroupWorks_WhenGroupExist_ExpectGroupDeleted()
        {
            //------------Setup for test--------------------------
            var warewolfGroupOps = MoqInstallerActionFactory.CreateSecurityOperationsObject();
            warewolfGroupOps.DeleteWarewolfGroup();
            warewolfGroupOps.AddWarewolfGroup();

            //------------Execute Test---------------------------
            warewolfGroupOps.DeleteWarewolfGroup();

            //------------Assert Results-------------------------

            // Will throw exception on failure ;)
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("WarewolfSecurityOperations_AddWarewolfGroupToAdministrators")]
        public void WarewolfSecurityOperations_AddWarewolfGroupToAdministrators_WhenNotAMember_ExpectNotAdded()
        {
            //------------Setup for test--------------------------
            var warewolfGroupOps = MoqInstallerActionFactory.CreateSecurityOperationsObject();

            // Delete warewolf if already a member...
            warewolfGroupOps.DeleteWarewolfGroup();
            warewolfGroupOps.AddWarewolfGroup();

            //------------Execute Test---------------------------
            var result = warewolfGroupOps.IsAdminMemberOfWarewolf();

            //------------Assert Results-------------------------
            Assert.IsFalse(result);
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("WarewolfSecurityOperations_AddWarewolfGroupToAdministrators")]
        public void WarewolfSecurityOperations_AddWarewolfGroupToAdministrators_WhenNotAlreadyMember_ExpectAdministratorsMemberOfWarewolf()
        {
            //------------Setup for test--------------------------
            var warewolfGroupOps = MoqInstallerActionFactory.CreateSecurityOperationsObject();

            // Delete warewolf if already a member...
            warewolfGroupOps.DeleteWarewolfGroup();
            warewolfGroupOps.AddWarewolfGroup();

            //------------Execute Test---------------------------
            try
            {
                warewolfGroupOps.AddAdministratorsGroupToWarewolf();
            }
            catch (COMException e)
            {
                //'The Server service is not started.' error is expected in containers. See: https://github.com/moby/moby/issues/26409#issuecomment-304978309
                if (e.Message != "The Server service is not started.\r\n")
                {
                    throw e;
                }
            }
            var result = warewolfGroupOps.IsAdminMemberOfWarewolf();

            //------------Assert Results-------------------------
            Assert.IsTrue(result);
        }
        
        [Test]
        [Author("Travis Frisinger")]
        [Category("WarewolfSecurityOperations_DeleteGroup")]
        public void WarewolfSecurityOperations_IsUserInGroup_WhenUserNotPresent_ExpectFalse()
        {
            //------------Setup for test--------------------------
            var warewolfGroupOps = MoqInstallerActionFactory.CreateSecurityOperationsObject();

            //------------Execute Test---------------------------
            var result = warewolfGroupOps.IsUserInGroup("Dev2\\MyNewUser");

            //------------Assert Results-------------------------
            Assert.IsFalse(result);
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("WarewolfSecurityOperations_DeleteGroup")]
        public void WarewolfSecurityOperations_AddLocalUserToWarewolfGroup_WhenUserNotPresent_ExpectUserAdded()
        {
            //------------Setup for test--------------------------
            var warewolfGroupOps = MoqInstallerActionFactory.CreateSecurityOperationsObject();
            warewolfGroupOps.DeleteWarewolfGroup();
            warewolfGroupOps.AddWarewolfGroup();
            var myPc = Environment.MachineName;

            var userStr = warewolfGroupOps.FormatUserForInsert("Guest", myPc);

            //------------Execute Test---------------------------
            warewolfGroupOps.AddUserToWarewolf(userStr);
            var result = warewolfGroupOps.IsUserInGroup("Guest");

            //------------Assert Results-------------------------
            Assert.IsTrue(result);
        }


        [Test]
        [Author("Travis Frisinger")]
        [Category("WarewolfSecurityOperations_DeleteGroup")]
        public void WarewolfSecurityOperations_FormatUserForInsert_WhenLocalUser_ExpectUserFormated()
        {
            //------------Setup for test--------------------------
            var warewolfGroupOps = MoqInstallerActionFactory.CreateSecurityOperationsObject();
            
            //------------Execute Test---------------------------
            var result = warewolfGroupOps.FormatUserForInsert("Guest", "MyPC");

            //------------Assert Results-------------------------
            StringAssert.Contains("WinNT://MyPC/Guest", result);
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("WarewolfSecurityOperations_DeleteGroup")]
        public void WarewolfSecurityOperations_FormatUserForInsert_WhenDomainUser_ExpectUserFormated()
        {
            //------------Setup for test--------------------------
            var warewolfGroupOps = MoqInstallerActionFactory.CreateSecurityOperationsObject();

            // Environment.MachineName
            //------------Execute Test---------------------------
            var result = warewolfGroupOps.FormatUserForInsert("Dev2\\DummyUser", "MyPC");

            //------------Assert Results-------------------------
            StringAssert.Contains("WinNT://Dev2/DummyUser", result);
        }

        #region Exception Test

        [Test]
        [Author("Travis Frisinger")]
        [Category("WarewolfSecurityOperations_AddUserToWarewolf")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WarewolfSecurityOperations_AddUserToWarewolfGroup_WhenUserNull_ExpectException()
        {
            //------------Setup for test--------------------------
            var warewolfGroupOps = MoqInstallerActionFactory.CreateSecurityOperationsObject();

            //------------Execute Test---------------------------
            warewolfGroupOps.AddUserToWarewolf(null);
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("WarewolfSecurityOperations_FormatUserForInsert")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WarewolfSecurityOperations_FormatUserForInsert_WhenNullUser_ExpectException()
        {
            //------------Setup for test--------------------------
            var warewolfGroupOps = MoqInstallerActionFactory.CreateSecurityOperationsObject();

            //------------Execute Test---------------------------
            warewolfGroupOps.FormatUserForInsert(null, null);
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("WarewolfSecurityOperations_FormatUserForInsert")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WarewolfSecurityOperations_FormatUserForInsert_WhenNullMachineName_ExpectException()
        {
            //------------Setup for test--------------------------
            var warewolfGroupOps = MoqInstallerActionFactory.CreateSecurityOperationsObject();

            //------------Execute Test---------------------------
            warewolfGroupOps.FormatUserForInsert("testUser", null);
        }

        [Test]
        [Author("Travis Frisinger")]
        [Category("WarewolfSecurityOperations_DeleteGroup")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WarewolfSecurityOperations_IsUserInGroup_WhenUserNotPresent_ExpectException()
        {
            //------------Setup for test--------------------------
            var warewolfGroupOps = MoqInstallerActionFactory.CreateSecurityOperationsObject();

            //------------Execute Test---------------------------
            warewolfGroupOps.IsUserInGroup(null);
        }

        #endregion


    }
}
