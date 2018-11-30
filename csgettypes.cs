#!/usr/bin/env csharp -s

// using System;
using System.IO;
// using System.Linq;
// using System.Collections;
// using System.Collections.Generic;

using System.Security.Cryptography;

using System.Text;

using System.Reflection;

public class T
{
    public static void Do(string path)
    {
        var dll = Assembly.LoadFrom(path);
        string s = "";
        foreach (var i in dll.GetTypes())
        {
            if(i.IsPublic)
                s += i.FullName + "\n";
        }
        // File.WriteAllText("GetTypes.txt", s);
        Console.WriteLine(s);
    }
}

// T.genKey();
T.Do(Args[0].ToString());
