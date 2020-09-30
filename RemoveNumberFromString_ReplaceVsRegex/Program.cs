using System;
using System.Linq;
using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace RemoveNumberFromString_ReplaceVsRegex
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<TestRunner>();
        }
    }

    [ClrJob, MonoJob, CoreJob] // 可以針對不同的 CLR 進行測試
    [MinColumn, MaxColumn]
    [MemoryDiagnoser]
    public class TestRunner
    {
        private readonly TestClass _test = new TestClass();

        public TestRunner()
        {
        }

        [Benchmark]
        public void TestMethodRegex() => _test.TestMethodRegex("a1b2bc3d4e5f6g7h8i9j0k");

        [Benchmark]
        public void TestMethodReplaceType1() => _test.TestMethodReplaceType1("a1b2bc3d4e5f6g7h8i9j0k");

        [Benchmark]
        public void TestMethodReplaceType2() => _test.TestMethodReplaceType2("a1b2bc3d4e5f6g7h8i9j0k");
    }

    public class TestClass
    {
        public string TestMethodRegex(string input)
        {
            var    regexPattern = @"[0-9]";
            var    rx           = new Regex(regexPattern, RegexOptions.IgnoreCase);
            string result       = rx.Replace(input, string.Empty);

            return result;
        }

        public string TestMethodReplaceType1(string input)
        {
            foreach (var i in Enumerable.Range(0, 10))
            {
                input = input.Replace(i.ToString(), string.Empty);
            }

            return input;
        }

        public string TestMethodReplaceType2(string input)
        {
            return input.Replace("0", string.Empty)
                        .Replace("1", string.Empty)
                        .Replace("2", string.Empty)
                        .Replace("3", string.Empty)
                        .Replace("4", string.Empty)
                        .Replace("5", string.Empty)
                        .Replace("6", string.Empty)
                        .Replace("7", string.Empty)
                        .Replace("8", string.Empty)
                        .Replace("9", string.Empty);
        }
    }
}
