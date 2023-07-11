using System;
using System.Collections.Generic;

namespace PraktikaAPI.Models;

public partial class Supply
{
    public int SupplyId { get; set; }

    public DateTime Date { get; set; }

    public virtual ICollection<SupplyProduct> SupplyProducts { get; set; } = new List<SupplyProduct>();
}