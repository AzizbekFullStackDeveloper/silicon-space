using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SiliconSpace.Shared;

[Table("Sys_User")]
public partial class SysUser
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(255)]
    public string Firstname { get; set; } = null!;

    [StringLength(255)]
    public string Lastname { get; set; } = null!;

    [StringLength(20)]
    public string PhoneNumber { get; set; } = null!;

    [StringLength(255)]
    public string Password { get; set; } = null!;

    [StringLength(255)]
    public string Salt { get; set; } = null!;

    public int RoleId { get; set; }

    public int StatusId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    [StringLength(255)]
    public string? Image { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<DocBooking> DocBookings { get; set; } = new List<DocBooking>();

    [ForeignKey("RoleId")]
    [InverseProperty("SysUsers")]
    public virtual EnumRole Role { get; set; } = null!;

    [ForeignKey("StatusId")]
    [InverseProperty("SysUsers")]
    public virtual EnumStatus Status { get; set; } = null!;
}
