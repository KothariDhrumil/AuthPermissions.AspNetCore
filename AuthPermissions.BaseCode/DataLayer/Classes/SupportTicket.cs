// Copyright (c) 2025 DealersAndDistributors
// Licensed under MIT license. See License.txt in the project root for license information.

using System.ComponentModel.DataAnnotations;
using System.Net;

namespace AuthPermissions.BaseCode.DataLayer.Classes;

public class SupportTicket
{
    [Key]
    public int Id { get; set; }
    public string Message { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Method { get; set; } = string.Empty;
    public HttpStatusCode StatusCode { get; set; }
    public string StatusText { get; set; } = string.Empty;
    public string UserAgent { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public string RequestBody { get; set; } = string.Empty;
    public string ResponseBody { get; set; } = string.Empty;
    public string Headers { get; set; } = string.Empty;
    public string CorrelationId { get; set; } = string.Empty;
    public string? UserId { get; set; }
    public int? TenantId { get; set; }
    public string? Notes { get; set; }
    public TicketStatus TicketStatus { get; set; } = TicketStatus.Open;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public TicketPriority Priority { get; set; } = TicketPriority.Medium;
    public string Resolution { get; set; } = string.Empty;  

}

public enum TicketStatus
{
    Open,
    InProgress,
    Resolved,
    Closed
}

public enum TicketPriority
{
    Low,
    Medium,
    High,
    Urgent
}



