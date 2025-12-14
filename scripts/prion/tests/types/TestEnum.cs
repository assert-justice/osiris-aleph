using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Prion.Tests
{
    [TestClass]
    public class TestEnum
    {
        readonly Random Rng = new();
        readonly StringBuilder sb = new();
        const string validEnumChars = PrionParseUtils.AlphaLower + PrionParseUtils.AlphaUpper + PrionParseUtils.Numeric + "_";
        [TestMethod]
        public void Create()
        {
            int trials = 100;
            for (int idx = 0; idx < trials; idx++)
            {
                string[] options = GetRandomOptions();
                int index = Rng.Next(options.Length);
                string res = string.Join(", ", options);
                res = "enum: " + res + ": " + options[index];
                if(!PrionEnum.TryFromString(res, out PrionNode node))
                {
                    Assert.Fail($"Enum parse failed with error: {node}.");
                }
            }
        }
        string GetRandomIdent()
        {
            int length = Rng.Next(1,256);
            sb.Clear();
            sb.EnsureCapacity(length);
            for (int f = 0; f < length; f++)
            {
                char c = validEnumChars[Rng.Next(validEnumChars.Length)];
                sb.Append(c);
            }
            return sb.ToString();
        }
        string[] GetRandomOptions()
        {
            int length = Rng.Next(1,256);
            List<string> options = new(length);
            for (int idx = 0; idx < length; idx++)
            {
                options.Add(GetRandomIdent());
            }
            options.Sort();
            return [.. options];
        }
    }
}
