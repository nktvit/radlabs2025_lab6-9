using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CsvHelper.Configuration.Attributes;

namespace ProductModel.GRN;

public class GRN
{
    [Key] public int GrnID { get; set; }

    [Required] public DateTime OrderDate { get; set; }

    [Optional] public DateTime? DeliveryDate { get; set; }

    [Required] public bool StockUpdated { get; set; }

    public virtual ICollection<GRNLine> GRNLines { get; set; }
}