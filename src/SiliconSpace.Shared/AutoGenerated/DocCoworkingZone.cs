using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SiliconSpace.Shared;

[Table("Doc_Coworking_Zone")]
public partial class DocCoworkingZone
{
    [Key]
    public Guid Id { get; set; }

    public Guid CoworkingId { get; set; }

    [StringLength(255)]
    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    [StringLength(255)]
    public string Image { get; set; } = null!;

    [StringLength(255)]
    public string OpeningHours { get; set; } = null!;

    public decimal Cost { get; set; }

    public int StatusId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("CoworkingId")]
    [InverseProperty("DocCoworkingZones")]
    public virtual DocCoworking Coworking { get; set; } = null!;

    [InverseProperty("CoworkingZone")]
    public virtual ICollection<DocBooking> DocBookings { get; set; } = new List<DocBooking>();

    [ForeignKey("StatusId")]
    [InverseProperty("DocCoworkingZones")]
    public virtual EnumStatus Status { get; set; } = null!;
}
