#!/usr/bin/env csharp -s

// using System;
using System.IO;
// using System.Linq;
using System.Reflection;

public static class Tool
{
    public static void go(string path)
    {
        var dll = Assembly.LoadFrom(path);
        // var dll = Assembly.ReflectionOnlyLoadFrom(path);
        var s = new List<string>();
        foreach (var i in dll.GetTypes())
        {
            if(    i.IsVisible 
                && !i.IsGenericType
                && !i.IsAbstract // abstract static interface
                && !i.IsDefined(typeof(System.ObsoleteAttribute), true)
                && !i.IsDefined(typeof(System.Runtime.CompilerServices.UnsafeValueTypeAttribute))
                && !i.IsDefined(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute))
                
                && !i.FullName.EndsWith("Attribute")
                && !i.FullName.StartsWith("UnityEngine.EventProvider")
                && !i.FullName.StartsWith("UnityEngine.DrivenRectTransformTracker")
                && !i.FullName.StartsWith("UnityEngine.Caching")
                && !i.FullName.StartsWith("UnityEngine.AudioSettings")
                && !i.FullName.StartsWith("UnityEngine.TextureCompressionQuality")
                && !i.FullName.StartsWith("UnityEngine.TrailRenderer")
                && !i.FullName.StartsWith("UnityEngine.LineRenderer")
                && !i.FullName.StartsWith("Unity.Jobs.LowLevel.Unsafe")
                && !i.FullName.StartsWith("Unity.Collections.LowLevel.Unsafe")

                // && i.IsAutoClass
                // && i.IsNestedPublic // inner public class
            )
            {
                s.Add("typeof(" + i.FullName.Replace("+", ".") + "),");
                // File.WriteAllText("/dev/stdout", i.FullName);
                // File.WriteAllText("/dev/stdout", "\n");
            }
        }
        // s += "// GetModules\n";
        // foreach (var i in dll.GetModules())
        // {
        //     // if (i.IsPublic)
        //     {
        //         s += i.ScopeName + "\n";
        //         Console.WriteLine("{1}: {0}", i.ScopeName, i.Name);
        //     }
        // }
        s.Sort((i,j)=>i.CompareTo(j));
        // File.WriteAllLines("/dev/stdout", s);

        File.WriteAllLines("/dev/stdout", s);
        File.WriteAllLines(path + ".txt", s);
        Console.WriteLine(path + ": {0}", s.Count());

    }
}
Tool.go(Args[0].ToString());
