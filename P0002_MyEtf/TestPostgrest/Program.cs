using Postgrest;
using TestPostgrest.Model;



// postgrest 的服务器地址.
var url = "http://192.168.1.153:3000";
var client = Postgrest.Client.Initialize(url);




// 尝试查询全部.(没有查询条件.)
Console.WriteLine("### 查询全部 ###");
var etfMasters = await client.Table<EtfMaster>().Get();
foreach (var etfMaster in etfMasters.Models)
{
    Console.WriteLine(etfMaster);
}


Console.WriteLine();
Console.WriteLine("### 查询 etf_code = SH510050 ###");
var sh510050 = await client.Table<EtfMaster>().Filter("etf_code", Postgrest.Constants.Operator.Equals, "SH510050").Single();
Console.WriteLine(sh510050);


Console.WriteLine();
Console.WriteLine("### 查询 etf_code LIKE SZ% ###");
var szEtfs = await client.Table<EtfMaster>().Filter("etf_code", Postgrest.Constants.Operator.Like, "SZ%").Get();
foreach (var etfMaster in szEtfs.Models)
{
    Console.WriteLine(etfMaster);
}


Console.WriteLine();
Console.WriteLine("### 查询 etf_code IN （SH510050，SH510180） ###");
List<object> codeList = new List<object>();
codeList.Add("SH510050");
codeList.Add("SH510180");
var testIn = await client.Table<EtfMaster>().Filter("etf_code", Postgrest.Constants.Operator.In, codeList).Get();
foreach (var etfMaster in testIn.Models)
{
    Console.WriteLine(etfMaster);
}





Console.WriteLine();
Console.WriteLine("### 查询 etf_code LIKE SH%  【AND】 etf_code LIKE %00   ###");
var testAnd = await client.Table<EtfMaster>()
    .Filter("etf_code", Postgrest.Constants.Operator.Like, "SH%")
    .Filter("etf_code", Postgrest.Constants.Operator.Like, "%00").Get();
foreach (var etfMaster in testAnd.Models)
{
    Console.WriteLine(etfMaster);
}




Console.WriteLine();
Console.WriteLine("### 查询翻页 ###");

int count = await client.Table<EtfMaster>().Count(Constants.CountType.Exact);
Console.WriteLine($"RowCount {count}");

int pageSize = 3;
int page = count / pageSize;
if(count % pageSize != 0)
{
    page++;
}


for (int i = 0; i < page; i++)
{
    var testPage = await client.Table<EtfMaster>().Offset(i * pageSize).Limit(pageSize).Get();
    Console.WriteLine($"Page {i + 1}");
    foreach (var etfMaster in testPage.Models)
    {
        Console.WriteLine(etfMaster);
    }
}







Console.WriteLine("----- Finish -----");
Console.ReadLine();