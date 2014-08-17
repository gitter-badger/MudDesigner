//-----------------------------------------------------------------------
// <copyright file="ValidationNumberDataTypes.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Engine.Core.Engine.Validation
{
    /// <summary>
    /// The available number types that can be validated with the Max/Min, Greater/Lesser and Range attributes.
    /// </summary>
    public enum ValidationNumberDataTypes
    {
        /// <summary>
        /// The none
        /// </summary>
        None,

        /// <summary>
        /// The short
        /// </summary>
        Short,

        /// <summary>
        /// The integer
        /// </summary>
        Int,

        /// <summary>
        /// The long
        /// </summary>
        Long,

        /// <summary>
        /// The float
        /// </summary>
        Float,

        /// <summary>
        /// The double
        /// </summary>
        Double,

        /// <summary>
        /// The decimal
        /// </summary>
        Decimal,
    }
}
