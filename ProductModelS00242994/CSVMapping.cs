using System.Globalization;
using CsvHelper.Configuration;
using ProductModel.GRN;

namespace ProductModel;

public class GrnMap : ClassMap<GRN.GRN>
{
    public GrnMap()
    {
        string format = "dd/MM/yyyy";
        var msMY = CultureInfo.GetCultureInfo("en-ie");

        Map(m => m.GrnID).Name("GrnID");
        Map(m => m.OrderDate).Name("OrderDate")
            .TypeConverterOption.Format(format)
            .TypeConverterOption.CultureInfo(msMY);
        Map(m => m.DeliveryDate).Name("DeliveryDate")
            .TypeConverterOption.Format(format)
            .TypeConverterOption.CultureInfo(msMY)
            .TypeConverterOption.NullValues("");
        Map(m => m.StockUpdated).Name("StockUpdated");
    }
}

public class GrnLineMap : ClassMap<GRNLine>
{
    public GrnLineMap()
    {
        Map(m => m.LineID).Name("LineID");
        Map(m => m.QtyDelivered).Name("QtyDelivered");
        Map(m => m.StockID).Name("StockID");
        Map(m => m.GrnID).Name("GrnID");
    }
}