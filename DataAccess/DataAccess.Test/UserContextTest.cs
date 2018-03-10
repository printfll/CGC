// <copyright file="JobContextTest.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace Microsoft.CGC.Data.DataAccess.Test
{
    using Microsoft.CGC.DataAccess;
    using Microsoft.CGC.DataAccess.Utility;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UserContextTest
    {

        [TestMethod]
        public void AddUserTest()
        {
            UserManager.Instance.AddUser("admin", "luye", "luye@microsoft.com");
        }
    }
}
