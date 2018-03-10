// <copyright file="GroupMapping.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>
namespace Microsoft.CGC.DataAccess.Models
{
    /// <summary>
    /// State for job/experiment
    /// </summary>
    public enum State : int
    {
        /// <summary>
        /// Unknown state
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// New state
        /// </summary>
        New = 1,

        /// <summary>
        /// Running state
        /// </summary>
        Running = 2,

        /// <summary>
        /// Aborting state
        /// </summary>
        Aborting = 3,

        /// <summary>
        /// Aborted state
        /// </summary>
        Aborted = 4,

        /// <summary>
        /// Failed state
        /// </summary>
        Failed = 5,

        /// <summary>
        /// Partial failed state
        /// </summary>
        PartialFailed = 6,

        /// <summary>
        /// Completed state
        /// </summary>
        Completed = 7
    }
}
