using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace A3001_Json.Sample
{
    public class TestJsonDocument
    {




        public static void Test1()
        {
            Console.WriteLine();
            Console.WriteLine("----- TestJsonDocument.Test1 -----");

            string json = "{\"TestCode\":\"Z3\",\"TestName\":\"\u5F20\u4E09\",\"TestAge\":33,\"IsAdult\":true,\"TestRemark\":null}";

            JsonDocument doc = JsonDocument.Parse(json);

            JsonElement root = doc.RootElement;

            JsonElement testCode = root.GetProperty("TestCode");
            JsonElement testName = root.GetProperty("TestName");
            JsonElement testAge = root.GetProperty("TestAge");
            JsonElement isAdult = root.GetProperty("IsAdult");


            Console.WriteLine(json);

            Console.WriteLine($"TestCode = {testCode.GetString()}");
            Console.WriteLine($"TestName = {testName.GetString()}");
            Console.WriteLine($"TestAge = {testAge.GetInt32()}");
            Console.WriteLine($"IsAdult = {isAdult.GetBoolean()}");
        }


    }
}
