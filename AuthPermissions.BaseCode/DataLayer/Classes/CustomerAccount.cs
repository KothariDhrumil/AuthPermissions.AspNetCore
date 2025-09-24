// Copyright (c) 2025 DealersAndDistributors
// Licensed under MIT license. See License.txt in the project root for license information.

using System.ComponentModel.DataAnnotations;

namespace AuthPermissions.BaseCode.DataLayer.Classes;

/// <summary>
/// Central customer account that can access multiple tenants.
/// Lives in the AuthPermissions database so it is available across shards.
/// </summary>
public class CustomerAccount
{
    [Key]
    public Guid GlobalCustomerId { get; set; } // PK

    // FK to AspNetUsers (string/GUID)
    [Required]
    public string GlobalUserId { get; set; } = default!;

    [MaxLength(128)]
    public string? FirstName { get; set; }

    [MaxLength(128)]
    public string? LastName { get; set; }

    /// <summary>
    /// Customer's phone number (login identifier)
    /// </summary>
    [Required, MaxLength(32)]
    public string PhoneNumber { get; set; } = default!;


    public ICollection<CustomerTenantLink> TenantLinks { get; set; } = new List<CustomerTenantLink>();
}
