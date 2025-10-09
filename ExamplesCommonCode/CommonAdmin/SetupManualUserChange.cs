// Copyright (c) 2021 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using AuthPermissions.AdminCode;
using AuthPermissions.BaseCode.DataLayer.Classes.SupportTypes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExamplesCommonCode.CommonAdmin
{
    public class SetupManualUserChange
    {
        /// <summary>
        /// This is used by SyncUsers to define what to do
        /// </summary>
        public SyncAuthUserChangeTypes FoundChangeType { get; set; }

        /// <summary>
        /// The userId of the user (NOTE: this is not show)
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MaxLength(AuthDbConstants.UserIdSize)]
        public string UserId { get; set; }

        [MaxLength(AuthDbConstants.EmailSize)]
        public string Email { get; set; }
        /// <summary>
        /// The user's name
        /// </summary>
        [MaxLength(AuthDbConstants.UserNameSize)]
        public string UserName { get; set; }

        /// <summary>
        /// The AuthRoles for this AuthUser
        /// </summary>
        public List<int> RoleIds { set; get; }

        /// <summary>
        /// The name of the AuthP Tenant for this AuthUser (can be null)
        /// </summary>
        public string TenantName { set; get; }


    }
}