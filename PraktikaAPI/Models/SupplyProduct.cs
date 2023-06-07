using System;
using System.Collections.Generic;

namespace PraktikaAPI.Models;

public partial class SupplyProduct
{
    public int SupplyProductsId { get; set; }

    public int SupplyId { get; set; }

    public int ProductId { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Supply Supply { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
