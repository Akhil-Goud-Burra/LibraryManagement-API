using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement_API.Models;

[Table("STREAM")]
public partial class Stream
{
    [Key]
    public int Id { get; set; }

    [Unicode(false)]
    public string Name { get; set; } = null!;

    [InverseProperty("Stream")]
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
