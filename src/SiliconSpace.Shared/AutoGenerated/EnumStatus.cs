using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SiliconSpace.Shared;

[Table("Enum_Status")]
[Index("Name", Name = "Enum_Status_Name_key", IsUnique = true)]
public partial class EnumStatus
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    public string Name { get; set; } = null!;

    [InverseProperty("Status")]
    public virtual ICollection<DocBooking> DocBookings { get; set; } = new List<DocBooking>();

    [InverseProperty("Status")]
    public virtual ICollection<DocCoworkingZone> DocCoworkingZones { get; set; } = new List<DocCoworkingZone>();

    [InverseProperty("Status")]
    public virtual ICollection<DocCoworking> DocCoworkings { get; set; } = new List<DocCoworking>();

    [InverseProperty("Status")]
    public virtual ICollection<DocOrganization> DocOrganizations { get; set; } = new List<DocOrganization>();

    [InverseProperty("Status")]
    public virtual ICollection<SysUser> SysUsers { get; set; } = new List<SysUser>();
}
