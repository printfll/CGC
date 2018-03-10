namespace Microsoft.CGC.Data.DataAccess.DotNetStd
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.CGC.DataAccess.DotNetStd.DataContext;
    using Microsoft.CGC.DataAccess.Models;

    /// <summary>
    /// Entry point
    /// </summary>
    public class Program
    {
        private static string connectionString;

        public static void Main(string[] args)
        {
            var connectionStringSetting = ConfigurationManager.ConnectionStrings["CGCProd"];
            connectionString = connectionStringSetting.ConnectionString;

            RefreshContext();

            Console.Read();
        }

        private static void RefreshContext()
        {
            using (var context = new EntityContext(connectionString))
            {
                // only need to load one property in context
                context.Users.FirstOrDefault();
            }

            Console.WriteLine($"{typeof(EntityContext).Name} is initialized");
        }
    }
}
