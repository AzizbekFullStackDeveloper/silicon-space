using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SiliconSpace.Shared;

[Table("Doc_Organization")]
public partial class DocOrganization
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(255)]
    public string Name { get; set; } = null!;

    [StringLength(20)]
    public string PhoneNumber { get; set; } = null!;

    [StringLength(255)]
    public string Password { get; set; } = null!;

    [StringLength(255)]
    public string Salt { get; set; } = null!;

    public string Description { get; set; } = null!;

    [StringLength(255)]
    public string Image { get; set; } = null!;

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public string Address { get; set; } = null!;

    public int StatusId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? RoleId { get; set; }

    [InverseProperty("Organization")]
    public virtual ICollection<DocCoworking> DocCoworkings { get; set; } = new List<DocCoworking>();

    [ForeignKey("RoleId")]
    [InverseProperty("DocOrganizations")]
    public virtual EnumRole? Role { get; set; }

    [ForeignKey("StatusId")]
    [InverseProperty("DocOrganizations")]
    public virtual EnumStatus Status { get; set; } = null!;
}
