﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Reflection;
using Unity.Injection;
using Unity.Policy;
using Unity.Policy.Tests;
using Unity.Resolution;

namespace Injection.Members
{
    public abstract class MethodBaseTests<TMemberInfo, TData>
        where TMemberInfo : MemberInfo
    {
        [Ignore]
        [TestMethod]
        public virtual void DerivedMemberInfo()
        {
            // Arrange
            var member = GetDefaultMember();

            // Act
            TMemberInfo info = member.MemberInfo(typeof(TestClass<int>));

            // Validate
            Assert.AreEqual(member.Name, info.Name);
        }

        [Ignore]
        [TestMethod]
        public virtual void MemberInfoSimpleTest()
        {
            // Arrange
            var member = GetDefaultMember();

            // Act
            var info = member.MemberInfo(typeof(SimpleClass));

            // Validate
            Assert.IsNotNull(info);
        }

        [Ignore]
        [TestMethod]
        public virtual void MemberInfoTest()
        {
            // Arrange
            var member = GetDefaultMember();

            // Act
            var info = member.MemberInfo(typeof(TestClass<object>));

            // Validate
            Assert.IsNotNull(info);
            Assert.AreEqual(typeof(TestClass<object>), info.DeclaringType);
        }

        [Ignore]
        [TestMethod]
        public virtual void DeclaredMembersTest()
        {
            //// Act
            //var member = GetDefaultMember();
            //var members = member.DeclaredMembers(typeof(TestClass<object>))
            //                    .ToArray();
            //// Validate
            //Assert.AreEqual(2, members.Length);
        }

        [Ignore]
        [TestMethod]
        public void ValidateParametersTest()
        {
            // Arrange
            var member = GetMember(typeof(TestClass<>), 2);
            
            // Act
            var info = member.MemberInfo(typeof(TestClass<object>));

            // Validate
            Assert.IsNotNull(info);
            Assert.AreEqual(typeof(TestClass<object>), info.DeclaringType);
        }


        #region Test Data

        protected abstract InjectionMember<TMemberInfo, TData> GetDefaultMember();

        protected abstract InjectionMember<TMemberInfo, TData> GetMember(Type type, int position, object value = null);

        #endregion
    }

    #region Test Data

    public class SimpleClass
    {
        public string TestField;
        public SimpleClass() => throw new NotImplementedException();
        public string TestProperty { get; set; }
        public void TestMethod(string a) => throw new NotImplementedException();
    }

    public class NoMatchClass
    {
        private NoMatchClass() { }
    }

    public class TestClass<T>
    {
        #region Constructors
        static TestClass() { }
        public TestClass() { }
        private TestClass(string _) { }
        protected TestClass(long _) { }
        internal TestClass(string a, IList<T> b) {}
        #endregion

        #region Fields

#pragma warning disable CS0169
#pragma warning disable CS0649

        public readonly string TestReadonlyField;
        internal string TestInternalField;
        static string TestStaticField;
        public string TestField;
        private string TestPrivateField;
        protected string TestProtectedField;

#pragma warning restore CS0169
#pragma warning restore CS0649

        #endregion

        #region Properties
        internal string TestInternalProperty { get; set; }
        public string TestReadonlyProperty { get; }
        static string TestStaticProperty { get; set; }
        public string TestProperty { get; set; }
        private string TestPrivateProperty { get; set; }
        protected string TestProtectedProperty { get; set; }
        #endregion

        #region Methods
        static void TestMethod() { }
        public void TestMethod(string _) { }
        private void TestMethod(int _) { }
        protected void TestMethod(long _) { }
        public void TestMethod(string a, IList<T> b, out object c) { c = null; }
        #endregion
    }

    #endregion
}
