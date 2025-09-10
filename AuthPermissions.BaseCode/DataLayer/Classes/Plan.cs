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

    /// <summary>
    /// This contains the list of permissions as a series of unicode chars
    /// </summary>
    [Required(AllowEmptyStrings = false)] //A role must have at least one role in it
    public string Features { get; set; }


}
