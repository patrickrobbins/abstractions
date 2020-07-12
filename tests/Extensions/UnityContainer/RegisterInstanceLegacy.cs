﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace Extensions.Tests
{
    public partial class UnityContainerTests
    {
        #region Generic Register Instance

        [TestMethod]
        public void RegisterInstance_Generic_Legacy()
        {
            // Act
            container.RegisterInstance<IUnityContainer>(container);

            // Validate
            Assert.AreEqual(typeof(IUnityContainer), container.Type);
            Assert.IsNull(container.MappedTo);
            Assert.IsNull(container.Name);
            Assert.IsInstanceOfType(container.LifetimeManager, typeof(ContainerControlledLifetimeManager));
            Assert.AreNotSame(manager, container.LifetimeManager);
            Assert.AreSame(container, container.Data);
            Assert.ThrowsException<ArgumentNullException>(() => unity.RegisterInstance<IUnityContainer>(container));
        }

        [TestMethod]
        public void RegisterInstance_Generic_Manager_Legacy()
        {
            // Act
            container.RegisterInstance<IUnityContainer>(container, (IInstanceLifetimeManager)manager);

            // Validate
            Assert.AreEqual(typeof(IUnityContainer), container.Type);
            Assert.IsNull(container.MappedTo);
            Assert.IsNull(container.Name);
            Assert.AreSame(manager, container.LifetimeManager);
            Assert.AreSame(container, container.Data);
            Assert.ThrowsException<ArgumentNullException>(() => unity.RegisterInstance<IUnityContainer>(container, (IInstanceLifetimeManager)manager));
            Assert.ThrowsException<ArgumentNullException>(() => unity.RegisterInstance<IUnityContainer>(container, FakeManager));
        }

        [TestMethod]
        public void RegisterInstance_Generic_Name_Legacy()
        {
            // Act
            container.RegisterInstance<IUnityContainer>(name, container);

            // Validate
            Assert.AreEqual(typeof(IUnityContainer), container.Type);
            Assert.IsNull(container.MappedTo);
            Assert.AreSame(name, container.Name);
            Assert.IsInstanceOfType(container.LifetimeManager, typeof(ContainerControlledLifetimeManager));
            Assert.AreNotSame(manager, container.LifetimeManager);
            Assert.AreSame(container, container.Data);
            Assert.ThrowsException<ArgumentNullException>(() => unity.RegisterInstance<IUnityContainer>(name, container));
        }

        [TestMethod]
        public void RegisterInstance_Generic_Name_Manger_Legacy()
        {
            // Act
            container.RegisterInstance<IUnityContainer>(name, container, (IInstanceLifetimeManager)manager);

            // Validate
            Assert.AreEqual(typeof(IUnityContainer), container.Type);
            Assert.IsNull(container.MappedTo);
            Assert.AreSame(name, container.Name);
            Assert.AreSame(manager, container.LifetimeManager);
            Assert.AreSame(container, container.Data);
            Assert.ThrowsException<ArgumentNullException>(() => unity.RegisterInstance<IUnityContainer>(name, container, (IInstanceLifetimeManager)manager, new InjectionConstructor()));
            Assert.ThrowsException<ArgumentNullException>(() => unity.RegisterInstance<IUnityContainer>(name, container, FakeManager));
        }

        #endregion


        #region Register Instance

        [TestMethod]
        public void RegisterInstance_Legacy()
        {
            // Act
            container.RegisterInstance(typeof(IUnityContainer), container);

            // Validate
            Assert.AreEqual(typeof(IUnityContainer), container.Type);
            Assert.IsNull(container.MappedTo);
            Assert.IsNull(container.Name);
            Assert.IsInstanceOfType(container.LifetimeManager, typeof(ContainerControlledLifetimeManager));
            Assert.AreNotSame(manager, container.LifetimeManager);
            Assert.AreSame(container, container.Data);
            Assert.ThrowsException<ArgumentNullException>(() => unity.RegisterInstance(typeof(IUnityContainer), container));
        }

        [TestMethod]
        public void RegisterInstance_Manager_Legacy()
        {
            // Act
            container.RegisterInstance(typeof(IUnityContainer), container, (IInstanceLifetimeManager)manager);

            // Validate
            Assert.AreEqual(typeof(IUnityContainer), container.Type);
            Assert.IsNull(container.MappedTo);
            Assert.IsNull(container.Name);
            Assert.AreSame(manager, container.LifetimeManager);
            Assert.AreSame(container, container.Data);
            Assert.ThrowsException<ArgumentNullException>(() => unity.RegisterInstance(typeof(IUnityContainer), container, (IInstanceLifetimeManager)manager));
            Assert.ThrowsException<ArgumentNullException>(() => container.RegisterInstance(typeof(IUnityContainer), container, FakeManager));
        }

        [TestMethod]
        public void RegisterInstance_Name_Legacy()
        {
            // Act
            container.RegisterInstance(typeof(IUnityContainer), name, container);

            // Validate
            Assert.AreEqual(typeof(IUnityContainer), container.Type);
            Assert.IsNull(container.MappedTo);
            Assert.AreSame(name, container.Name);
            Assert.IsInstanceOfType(container.LifetimeManager, typeof(ContainerControlledLifetimeManager));
            Assert.AreNotSame(manager, container.LifetimeManager);
            Assert.AreSame(container, container.Data);
            Assert.ThrowsException<ArgumentNullException>(() => unity.RegisterInstance(typeof(IUnityContainer), name, container));
        }

        [TestMethod]
        public void RegisterInstance_Name_Manger_Legacy()
        {
            // Act
            container.RegisterInstance(typeof(IUnityContainer), name, container, (IInstanceLifetimeManager)manager);

            // Validate
            Assert.AreEqual(typeof(IUnityContainer), container.Type);
            Assert.IsNull(container.MappedTo);
            Assert.AreSame(name, container.Name);
            Assert.AreSame(manager, container.LifetimeManager);
            Assert.AreSame(container, container.Data);
            Assert.ThrowsException<ArgumentNullException>(() => unity.RegisterInstance(typeof(IUnityContainer), name, container, (IInstanceLifetimeManager)manager, new InjectionConstructor()));
            Assert.ThrowsException<ArgumentNullException>(() => container.RegisterInstance(typeof(IUnityContainer), name, container, FakeManager));
        }

        #endregion
    }
}