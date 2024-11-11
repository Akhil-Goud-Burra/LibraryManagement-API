using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement_API.Models;

[Table("Availability")]
public partial class Availability
{
    [Key]
    public int Id { get; set; }

    [Unicode(false)]
    public string Quantity { get; set; } = null!;

    public bool Status { get; set; }

    public int BookId { get; set; }

    [ForeignKey("BookId")]
    [InverseProperty("Availabilities")]
    public virtual Book Book { get; set; } = null!;
}
