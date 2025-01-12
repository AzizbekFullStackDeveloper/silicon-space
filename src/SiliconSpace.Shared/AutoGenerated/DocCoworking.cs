using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SiliconSpace.Shared;

[Table("Doc_Coworking")]
public partial class DocCoworking
{
    [Key]
    public Guid Id { get; set; }

    public Guid OrganizationId { get; set; }

    [StringLength(255)]
    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    [StringLength(255)]
    public string Image { get; set; } = null!;

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public int StatusId { get; set; }

    [StringLength(255)]
    public string OpeningHours { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    [InverseProperty("Coworking")]
    public virtual ICollection<DocCoworkingZone> DocCoworkingZones { get; set; } = new List<DocCoworkingZone>();

    [ForeignKey("OrganizationId")]
    [InverseProperty("DocCoworkings")]
    public virtual DocOrganization Organization { get; set; } = null!;

    [ForeignKey("StatusId")]
    [InverseProperty("DocCoworkings")]
    public virtual EnumStatus Status { get; set; } = null!;
}
