﻿using System;
using Dev2.DynamicServices;
using Dev2.Runtime.Hosting;
using NUnit.Framework;


namespace Dev2.Tests.Runtime.Hosting
{
    [TestFixture]
    [SetUpFixture]
    [Category("Runtime Hosting")]
    public class ServiceActionRepoTests
    {
        [Test]
        [Author("Hagashen Naidu")]
        [Category("ServiceActionRepo_AddToCache")]
        public void ServiceActionRepo_AddToCache_WhenNotExisting_ShouldAdd()
        {
            //------------Setup for test--------------------------
            var id = Guid.NewGuid();
            var ds = new DynamicService { DisplayName = "Ds 1" };
            //------------Execute Test---------------------------
            ServiceActionRepo.Instance.AddToCache(id, ds);
            //------------Assert Results-------------------------
            var readDs = ServiceActionRepo.Instance.ReadCache(id);
            NUnit.Framework.Assert.IsNotNull(readDs);
            NUnit.Framework.Assert.AreEqual("Ds 1",readDs.DisplayName);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("ServiceActionRepo_AddToCache")]
        public void ServiceActionRepo_AddToCache_WhenIdExists_ShouldReplace()
        {
            //------------Setup for test--------------------------
            var id = Guid.NewGuid();
            var ds = new DynamicService { DisplayName = "Ds 1" };
            var ds2 = new DynamicService { DisplayName = "Ds 2" };
            //------------Execute Test---------------------------
            ServiceActionRepo.Instance.AddToCache(id, ds);
            ServiceActionRepo.Instance.AddToCache(id, ds2);
            //------------Assert Results-------------------------
            var readDs = ServiceActionRepo.Instance.ReadCache(id);
            NUnit.Framework.Assert.IsNotNull(readDs);
            NUnit.Framework.Assert.AreEqual("Ds 2",readDs.DisplayName);
        }


        [Test]
        [Author("Hagashen Naidu")]
        [Category("ServiceActionRepo_AddToCache")]
        public void ServiceActionRepo_ReadFromCache_WhenNotExisting_ShouldReturnNull()
        {
            //------------Setup for test--------------------------
            var id = Guid.NewGuid();
            var ds = new DynamicService { DisplayName = "Ds 1" };
            ServiceActionRepo.Instance.AddToCache(id, ds);
            //------------Execute Test---------------------------
            var readDs = ServiceActionRepo.Instance.ReadCache(Guid.NewGuid());
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsNull(readDs);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("ServiceActionRepo_AddToCache")]
        public void ServiceActionRepo_ReadFromCache_WhenExisting_ShouldReturnDynamicService()
        {
            //------------Setup for test--------------------------
            var id = Guid.NewGuid();
            var ds = new DynamicService { DisplayName = "Ds 1" };
            ServiceActionRepo.Instance.AddToCache(id, ds);
            //------------Execute Test---------------------------
            var readDs = ServiceActionRepo.Instance.ReadCache(id);
            //------------Assert Results-------------------------
            NUnit.Framework.Assert.IsNotNull(readDs);
            NUnit.Framework.Assert.AreEqual("Ds 1", readDs.DisplayName);
        }

        [Test]
        [Author("Hagashen Naidu")]
        [Category("ServiceActionRepo_RemoveFromCache")]
        public void ServiceActionRepo_RemoveFromCache_WhenIdExists_ShouldRemove()
        {
            //------------Setup for test--------------------------
            var id = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var ds = new DynamicService { DisplayName = "Ds 1" };
            var ds2 = new DynamicService { DisplayName = "Ds 2" };
            ServiceActionRepo.Instance.AddToCache(id, ds);
            ServiceActionRepo.Instance.AddToCache(id2, ds2);
            //------------Execute Test---------------------------
            ServiceActionRepo.Instance.RemoveFromCache(id);
            //------------Assert Results-------------------------
            var readDs = ServiceActionRepo.Instance.ReadCache(id);
            NUnit.Framework.Assert.IsNull(readDs);
            var readDs2 = ServiceActionRepo.Instance.ReadCache(id2);
            NUnit.Framework.Assert.IsNull(readDs);
            NUnit.Framework.Assert.AreEqual("Ds 2", readDs2.DisplayName);
        }
    }
}
