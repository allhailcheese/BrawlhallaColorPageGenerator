using System.IO;
using System.Linq;
using BrawlhallaColorPageGenerator.Objects;

namespace BrawlhallaColorPageGenerator.Writers.Bundles;

public sealed class BundleCostWriter(WriterData data)
{
    public void WriteTo(string path)
    {
        using StreamWriter writer = new(path) { NewLine = "\n" };

        writer.WriteLine("<includeonly>{{#vardefine:base_cost|{{#switch:{{lc:{{{1}}}}}");
        writer.WriteLine("<!-- cost before applying discount. aka sum of costs of items. names should be lowercase -->");
        foreach (StoreType item in BundleUtils.Bundles(data))
        {
            writer.Write("| ");
            writer.Write(data.LangFile.Entries[item.DisplayNameKey!].ToLowerInvariant());
            writer.Write(" = ");
            writer.WriteLine(GetBundleBaseCost(item));
        }
        writer.WriteLine("|0");
        writer.WriteLine("}}}}{{#vardefine:discount|{{#switch:{{lc:{{{1}}}}}");
        writer.WriteLine("<!-- fraction of base cost. names should be lowercase -->");
        foreach (StoreType item in BundleUtils.Bundles(data))
        {
            writer.Write("| ");
            writer.Write(data.LangFile.Entries[item.DisplayNameKey!].ToLowerInvariant());
            writer.Write(" = ");
            writer.WriteLine(item.IdolBundleDiscount);
        }
        writer.WriteLine("""
|1
}}}}{{#switch:{{lc:{{{2|}}}}}
|discount = {{CalcBundleDiscount|{{#var:discount}}|discount}}
|cost = {{CalcBundleDiscount|{{#var:base_cost}}|{{#var:discount}}}}
|mammoth = {{Coin|mammoth|{{CalcBundleDiscount|{{#var:base_cost}}|{{#var:discount}}}}}}
|mammoth+discount|{{Coin|mammoth|{{CalcBundleDiscount|{{#var:base_cost}}|{{#var:discount}}}}}} / {{CalcBundleDiscount|{{#var:discount}}|discount}} off
}}</includeonly><noinclude>
{{doc}}
[[Category:Templates]]</noinclude>
""");
    }

    private int GetBundleBaseCost(StoreType store)
    {
        return store.ItemList.Sum((item) => data.StoreTypes.StoresMap[item].IdolCost);
    }
}