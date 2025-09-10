// Copyright (c) 2023 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

namespace AuthPermissions.BaseCode.DataLayer.Classes;

public class TenantPlan
{
    public int Id { get; set; }

    public Tenant Tenant { get; set; }
    public int TenentId { get; set; }

    public Plan Plan { get; set; }

    public int PlanId { get; set; }

    public bool IsActive { get; set; }

    public DateTime ValidFrom { get; set; }

    public DateTime ValidTo { get; set; }

    public string Remarks { get; set; }

    public string Permissions { get; set; }
}