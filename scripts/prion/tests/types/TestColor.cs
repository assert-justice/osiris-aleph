
using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Prion.Tests
{
    [TestClass]
    public class TestColor
    {
        readonly Random Rng = new();
        [TestMethod]
        public void Create()
        {
            string[] hexValues = [
                "0x00000000",
                "0xffff00ff",
            ];
            string[] htmlValues = [
                "#00000000",
                "#ffff00ff",
            ];
            foreach (var item in htmlValues)
            {
                DoesMatchHtml(item, item);
            }
            foreach (var item in hexValues)
            {
                DoesMatchHex(item, item);
            }
            int trials = 100;
            for (int idx = 0; idx < trials; idx++)
            {
                string str = "0x" + GetRandomColor();
                DoesMatchHex(str, Expand(str));
            }
            for (int idx = 0; idx < trials; idx++)
            {
                string str = "#" + GetRandomColor();
                DoesMatchHtml(str, Expand(str));
            }
        }
        static void DoesMatchHex(string input, string expected)
        {
            if(!PrionColor.TryFromHexString(input, out PrionNode node))
            {
                Assert.Fail($"Failed to parse, input: {input}, expected: {expected}, error: {node}");
            }
            if(node is not PrionColor)
            {
                Assert.Fail($"input: {input}, expected: {expected}, error: {node}");
            }
            if((node as PrionColor).ToHexString() != expected)
            {
                Assert.Fail($"input: {input}, expected: {expected}, error: {node}");
            }
        }
        static void DoesMatchHtml(string input, string expected)
        {
            Assert.IsTrue(PrionColor.TryFromHtmlString(input, out PrionNode node));
            Assert.IsTrue(node is PrionColor);
            Assert.IsTrue((node as PrionColor).ToHtmlString() == expected);
            Assert.IsTrue((node as PrionColor).ToString() == $"color:{expected}");
        }
        string GetRandomColor()
        {
            const string hexChars = "0123456789abcdefABCDEF";
            int[] lens = [3, 4, 6, 8];
            int length = lens[Rng.Next(4)];
            var sb = new StringBuilder(length + 2);
            for (int idx = 0; idx < length; idx++)
            {
                sb.Append(hexChars[Rng.Next(hexChars.Length)]);
            }
            return sb.ToString();
        }
        static string Expand(string s)
        {
            s = s.ToLower();
            string prefix;
            string value;
            if (s.StartsWith("0x"))
            {
                prefix = "0x";
                value = s[2..];
            }
            else
            {
                prefix = "#";
                value = s[1..];
            }
            string expanded = "";
            switch (value.Length)
            {
                case 3:
                for (int idx = 0; idx < value.Length; idx++)
                {
                    expanded += value[idx];
                    expanded += value[idx];
                }
                expanded += "ff";
                break;
                case 4:
                for (int idx = 0; idx < value.Length; idx++)
                {
                    expanded += value[idx];
                    expanded += value[idx];
                }
                break;
                case 6:
                expanded = value + "ff";
                break;
                case 8:
                expanded = value;
                break;
                default:
                break;
            }
            return prefix + expanded;
        }
    }
}
