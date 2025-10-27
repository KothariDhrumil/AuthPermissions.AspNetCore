// Copyright (c) 2025 DealersAndDistributors
// Licensed under MIT license. See License.txt in the project root for license information.

using Microsoft.AspNet.Identity.EntityFramework;

namespace AuthPermissions.BaseCode.DataLayer.Classes;

public class ApplicationUser : IdentityUser
{

    public string? UserImage { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public bool IsActive { get; set; } // use for whether its active or not
    public InactiveContactStatus? InactiveContactStatus { get; set; }
    public string? InActiveReason { get; set; }
    public bool IsMainContact { get; set; }
    public bool IsApproved { get; set; } // use for whether its pending or approved
    public bool ReRegistered { get; set; }
    public bool ShowPhonumber { get; set; }
    public bool NotificationEnabled { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public string? DeviceId { get; set; }
    public string? GeoInformation { get; set; }
    public string? MACAddress { get; set; }
    public bool IsAndroid { get; set; }
    public string FCMToken { get; set; }
    public string AppVersion { get; set; }
    public string BuildNumber { get; set; }

}


public enum InactiveContactStatus
{
    Block,
    AllowedForNewRegistration
}