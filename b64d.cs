#!/usr/bin/env csharp -s

using System.Text;

public class Base64
{
    public static string Mixture(string instr, bool encode = false)
    {
        int len = instr.Length;
        int plus = (len % 8) + 1;
        var sbase64 = new byte[len];

        for(int i = 0; i < len; i++)
        {
            if(encode)
                sbase64[i] = (byte)((int)instr[i] - plus);
            else
                sbase64[i] = (byte)((int)instr[i] + plus);
        }

        return System.Text.Encoding.Default.GetString(sbase64);
        //return sbase64;
    }

    public static string Decode(string instr)
    {
        string sout = "";
        int len = instr.Length;
        var sbase64 = Mixture(instr);

        if(len > 0)
        {
            try
            {
                var tmp = System.Convert.FromBase64String(sbase64);
                sout = Encoding.Default.GetString(tmp);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }

        return sout;
    }

    public static string Encode(string instr)
    {
        string sout = "";
        int len = instr.Length;
        if(len > 0)
        {
            try
            {
                sout = System.Convert.ToBase64String(Encoding.Default.GetBytes(instr));
                sout = Mixture(sout, true);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
        return sout;
    }

}

//Console.WriteLine("[{0}] => [{1}]",Args[0], Base64.Encode(Args[0].ToString()))
//Console.WriteLine("[{0}] => [{1}]",Args[0], Base64.Decode(Args[0].ToString()))

Console.WriteLine("{0}", Base64.Decode(Args[0].ToString()))
