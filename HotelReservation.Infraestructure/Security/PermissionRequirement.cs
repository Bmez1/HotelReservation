﻿using Microsoft.AspNetCore.Authorization;

namespace HotelReservation.Infraestructure.Security;

internal sealed class PermissionRequirement : IAuthorizationRequirement
{
    public string Permission { get; }
    public PermissionRequirement(string permission)
    {
        Permission = permission;
    }
}
