// <copyright file="IService.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace Microsoft.CGC.DataAccess.Service
{
    using Microsoft.CGC.DataAccess;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public interface IService
    {
        /// <summary>
        /// Gets the logger.
        /// </summary>
        ILogger Logger { get; }
    }
}
