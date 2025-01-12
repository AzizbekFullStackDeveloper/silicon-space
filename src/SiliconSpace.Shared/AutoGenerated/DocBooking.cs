using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SiliconSpace.Shared;

[Table("Doc_Booking")]
public partial class DocBooking
{
    [Key]
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid CoworkingZoneId { get; set; }

    public int StatusId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("CoworkingZoneId")]
    [InverseProperty("DocBookings")]
    public virtual DocCoworkingZone CoworkingZone { get; set; } = null!;

    [ForeignKey("StatusId")]
    [InverseProperty("DocBookings")]
    public virtual EnumStatus Status { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("DocBookings")]
    public virtual SysUser User { get; set; } = null!;
}
