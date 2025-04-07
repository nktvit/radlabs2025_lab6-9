using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductModel.GRN;

public class GRNLine
{
    [Key] public int LineID { get; set; }
    [ForeignKey("parentGRN")] public int GrnID { get; set; }
    [ForeignKey("associatedProduct")] public int StockID { get; set; }
    public int QtyDelivered { get; set; }

    public virtual GRN parentGRN { get; set; }
    public virtual Product associatedProduct { get; set; }
}