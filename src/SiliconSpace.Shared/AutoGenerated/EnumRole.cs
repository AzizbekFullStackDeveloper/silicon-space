using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SiliconSpace.Shared;

[Table("Enum_Roles")]
[Index("Name", Name = "Enum_Roles_Name_key", IsUnique = true)]
public partial class EnumRole
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    public string Name { get; set; } = null!;

    [InverseProperty("Role")]
    public virtual ICollection<DocOrganization> DocOrganizations { get; set; } = new List<DocOrganization>();

    [InverseProperty("Role")]
    public virtual ICollection<SysUser> SysUsers { get; set; } = new List<SysUser>();
}
