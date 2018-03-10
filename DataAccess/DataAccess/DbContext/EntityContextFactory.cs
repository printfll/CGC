// <copyright file="GroupMapping.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>
namespace Microsoft.CGC.Data.DataAccess.DbContext
{
    using Microsoft.CGC.DataAccess;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;

    /// <summary>
    /// User context factory as design time DbContext factory for
    /// database migration
    /// </summary>
    public class EntityContextFactory : IDesignTimeDbContextFactory<EntityContext>
    {
        public EntityContext CreateDbContext(string[] args)
        {
            var optionBuilder = new DbContextOptionsBuilder<EntityContext>();

            var connectionString = ConfigurationHepler.GetDataConnectionString(
                Constant.ConfigurationFilePath,
                Constant.ConnectionStringPath);
            optionBuilder.UseSqlServer(connectionString);

            return new EntityContext(optionBuilder.Options);
        }
    }
}