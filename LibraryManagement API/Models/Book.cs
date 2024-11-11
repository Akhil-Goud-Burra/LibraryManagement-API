using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement_API.Models;

[Table("BOOK")]
public partial class Book
{
    [Key]
    public int Id { get; set; }

    [Unicode(false)]
    public string Title { get; set; } = null!;

    [Unicode(false)]
    public string Name { get; set; } = null!;

    public int StreamId { get; set; }

    [InverseProperty("Book")]
    public virtual ICollection<Availability> Availabilities { get; set; } = new List<Availability>();

    [ForeignKey("StreamId")]
    [InverseProperty("Books")]
    public virtual Stream Stream { get; set; } = null!;
}
