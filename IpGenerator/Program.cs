using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace IpGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var chars = new byte[] { 0, 0, 0, 0 };
            var path = @"D:\test.dat";

            IPAddress ip;
            using (var file = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
            {
                for (int i = 0; ; i++)
                {
                    ip = new IPAddress(chars);
                    var bytes = ip.GetAddressBytes();
                    file.Write(bytes);
                    if (chars[3] == 255)
                    {
                        chars[3] = 0;
                        if (chars[2] == 255)
                        {
                            chars[2] = 0;
                            if (chars[1] == 255)
                            {
                                chars[1] = 0;
                                if (chars[0] == 255)
                                    break;
                                chars[0]++;
                                Console.WriteLine(chars[0]);
                            }
                            chars[1]++;
                        }
                        chars[2]++;
                    }
                    chars[3]++;
                }
            }
        }
    }
}