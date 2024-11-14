using System;
using System.Collections.Generic;
using System.IO;

namespace BusinessObjects.Model;

public partial class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public string? Category { get; set; }

    public decimal Price { get; set; }

    public int StockQuantity { get; set; }

    public DateOnly? ExpiryDate { get; set; }

    public byte[]? ProductImage { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
