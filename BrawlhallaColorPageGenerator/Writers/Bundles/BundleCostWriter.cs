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
* <code><nowiki>{{BundleCost|Valhallentine's 2026 Mega Bundle}}</nowiki></code> = {{BundleCost|Valhallentine's 2026 Mega Bundle}}
* <code><nowiki>{{BundleCost|Valhallentine's 2026 Mega Bundle|cost}}</nowiki></code> = {{BundleCost|Valhallentine's 2026 Mega Bundle|cost}}
* <code><nowiki>{{BundleCost|Valhallentine's 2026 Mega Bundle|discount}}</nowiki></code> = {{BundleCost|Valhallentine's 2026 Mega Bundle|discount}}
* <code><nowiki>{{BundleCost|Valhallentine's 2026 Mega Bundle|mammoth}}</nowiki></code> = {{BundleCost|Valhallentine's 2026 Mega Bundle|mammoth}}
* <code><nowiki>{{BundleCost|Valhallentine's 2026 Mega Bundle|mammoth+discount}}</nowiki></code> = {{BundleCost|Valhallentine's 2026 Mega Bundle|mammoth+discount}}
* <code><nowiki>{{BundleCost|unknown}}</nowiki></code> = {{BundleCost|unknown}}
* <code><nowiki>{{BundleCost|unknown|cost}}</nowiki></code> = {{BundleCost|unknown|cost}}
* <code><nowiki>{{BundleCost|unknown|discount}}</nowiki></code> = {{BundleCost|unknown|discount}}
* <code><nowiki>{{BundleCost|unknown|mammoth}}</nowiki></code> = {{BundleCost|unknown|mammoth}}
* <code><nowiki>{{BundleCost|unknown|mammoth+discount}}</nowiki></code> = {{BundleCost|unknown|mammoth+discount}}

[[Category:Templates]]</noinclude>
""");
    }

    private int GetBundleBaseCost(StoreType store)
    {
        return store.ItemList.Sum((item) => data.StoreTypes.StoresMap[item].IdolCost);
    }
}