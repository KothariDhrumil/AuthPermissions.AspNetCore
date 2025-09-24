// Copyright (c) 2025 DealersAndDistributors
// Licensed under MIT license. See License.txt in the project root for license information.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthPermissions.BaseCode.DataLayer.Classes;

/// <summary>
/// Link between a central customer account and a tenant.
/// </summary>
public class CustomerTenantLink
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid CustomerId { get; set; }

    [ForeignKey(nameof(CustomerId))]
    public CustomerAccount Customer { get; set; }

    [Required]
    public int TenantId { get; set; }
}
