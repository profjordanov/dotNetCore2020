using System;

namespace DevOpsRenamer
{
    internal static class StringExtensions
    {
        internal static string GetStringBetween(this string input, string startString, string endString)
        {
            var startPosition = input.IndexOf(startString, StringComparison.Ordinal) + startString.Length;

            var endPosition = input.IndexOf(endString, startPosition, StringComparison.Ordinal);

            return input[startPosition..endPosition];
        }

        internal static string GetStringTo(this string input, string endString)
        {
            var endPosition = input.IndexOf(endString, 0, StringComparison.Ordinal);

            return input[..endPosition];
        }
    }
}