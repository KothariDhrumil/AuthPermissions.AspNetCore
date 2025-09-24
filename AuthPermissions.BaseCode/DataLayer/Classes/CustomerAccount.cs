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
    public Guid Id { get; set; }

    /// <summary>
    /// Customer's phone number (login identifier)
    /// </summary>
    [Required, MaxLength(32)]
    public string PhoneNumber { get; set; }

    [MaxLength(256)]
    public string DisplayName { get; set; }

    public bool IsActive { get; set; } = true;

    public ICollection<CustomerTenantLink> TenantLinks { get; set; } = new List<CustomerTenantLink>();
}
