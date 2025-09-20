// Copyright (c) 2023 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System.ComponentModel.DataAnnotations;

namespace AuthPermissions.BaseCode.DataLayer.Classes;

public class Plan
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public string Description { get; set; } = string.Empty;

    public int PlanValidityInDays { get; set; }

    public int PlanRate { get; set; }

    public bool IsActive { get; set; }

    ///// <summary>
    ///// Packed permissions string (kept for compatibility).
    ///// </summary>
    //[Required(AllowEmptyStrings = false)]
    //public string Features { get; set; }

    /// <summary>
    /// Roles included in this plan (one plan can contain many roles).
    /// </summary>
    public ICollection<RoleToPermissions> Roles { get; set; } = new List<RoleToPermissions>();

    /// <summary>
    /// Assignments of this plan to tenants.
    /// </summary>
    public ICollection<TenantPlan> TenantPlans { get; set; } = new List<TenantPlan>();


}
