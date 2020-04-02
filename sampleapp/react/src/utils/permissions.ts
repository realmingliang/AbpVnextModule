const Permissions = {
  AbpIdentity: {
    Roles: {
      default: 'AbpIdentity.Roles',
      Create: 'AbpIdentity.Roles.Create',
      Update: 'AbpIdentity.Roles.Update',
      Delete: 'AbpIdentity.Roles.Delete',
      ManagePermissions: 'AbpIdentity.Roles.ManagePermissions',
    },
    Users: {
      default: 'AbpIdentity.Users',
      Create: 'AbpIdentity.Users.Create',
      Update: 'AbpIdentity.Users.Update',
      Delete: 'AbpIdentity.Users.Delete',
      ManagePermissions: 'AbpIdentity.Users.ManagePermissions',
    },
    UserLookup: {
      default: 'AbpIdentity.UserLookup',
    },
  },
  AbpTenantManagement:{
    default:'AbpTenantManagement.Tenants',
    Create: 'AbpTenantManagement.Tenants.Create',
    Update: 'AbpTenantManagement.Tenants.Update',
    Delete: 'AbpTenantManagement.Tenants.Delete',
    ManageConnectionStrings: 'AbpTenantManagement.Tenants.ManageConnectionStrings',
  }
}

export default Permissions;
