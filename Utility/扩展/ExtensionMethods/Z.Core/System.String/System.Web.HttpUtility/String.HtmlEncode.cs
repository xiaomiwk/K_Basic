// Copyright (c) 2015 ZZZ Projects. All rights reserved
// Licensed under MIT License (MIT) (https://github.com/zzzprojects/Z.ExtensionMethods)
// Website: http://www.zzzprojects.com/
// Feedback / Feature Requests / Issues : http://zzzprojects.uservoice.com/forums/283927
// All ZZZ Projects products: Entity Framework Extensions / Bulk Operations / Extension Methods /Icon Library

using System;
using System.IO;
using System.Web;

public static partial class Extensions
{
    /// <summary>
    ///     Converts a string to an HTML-encoded string.
    /// </summary>
    /// <param name="s">The string to encode.</param>
    /// <returns>An encoded string.</returns>
    public static String HtmlEncode(this String s)
    {
        return HttpUtility.HtmlEncode(s);
    }

    /// <summary>
    ///     Converts a string into an HTML-encoded string, and returns the output as a  stream of output.
    /// </summary>
    /// <param name="s">The string to encode.</param>
    /// <param name="output">A  output stream.</param>
    public static void HtmlEncode(this String s, TextWriter output)
    {
        HttpUtility.HtmlEncode(s, output);
    }
}