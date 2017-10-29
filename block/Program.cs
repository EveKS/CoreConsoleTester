using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace block
{
    class Program
    {
        static double Avg(double[] mass)
        {
            
            return mass.Average();
        }

        static void Main(string[] args)
        {
            DateTimeOffset date = DateTimeOffset.FromUnixTimeSeconds(1415115303);
            Console.WriteLine($"{date:F}");

            //string key = "vk manager, vk парсер, вк парсер, вк менеджер, парсер,Грабер, Грабинг, VkManager, Manager, Post, соцсеть, бесплатно, парсер vk, парсер vk бесплатно, группы, парсер групп вк, бесплатный парсер вк, парсер вк онлайн, онлайн, парсер групп вк бесплатно, парсер групп вк онлайн, сайты парсеры вк, парсер постов вк, сообщества, парсер сообществ вк, парсер вк бесплатно онлайн";
            //Console.WriteLine(string.Join(", ", key.Split(new[] { ',', ' ' }).Distinct()));
            // a2ee4a9195f4a90e893cff4f62eeba0b662321f9
            // 01234567890ABCDEF01234567890

            Console.ReadKey();
        }



        public static string GetHash2(string val)
        {
            var enc = Encoding.GetEncoding(0);

            byte[] buffer = enc.GetBytes(val);
            var sha1 = SHA1.Create();
            var hash = BitConverter.ToString(sha1.ComputeHash(buffer)).Replace("-", "");
            return hash;
        }

        public static string CalculateMD5Hash(string input)
        {

            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public static string GetHash(string val)
        {
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] data = sha.ComputeHash(Encoding.Default.GetBytes(val));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }


        void HttpGetStringContent(string url, string path, int take, Action<string, string, int> writeContent)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "text/html,application/xhtml+xml,application/xml";
                client.Headers[HttpRequestHeader.AcceptEncoding] = "gzip, deflate";
                client.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";
                client.Headers[HttpRequestHeader.AcceptCharset] = "ISO-8859-1";

                var responseStream = new GZipStream(client.OpenRead(url), CompressionMode.Decompress);
                var reader = new StreamReader(responseStream);

                var content = reader.ReadToEnd();
                writeContent(content, path, take);

                responseStream.Dispose();
                reader.Dispose();
                client.Dispose();
            }
            catch (Exception ex)
            {

            }
        }

        private string PdfRead(string path)
        {
            using (PdfReader pdfReader = new PdfReader(path))
            {
                StringBuilder text = new StringBuilder();

                for (int page = 1; page <= pdfReader.NumberOfPages; page++)
                {
                    ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                    string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);

                    currentText = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));
                    text.Append(currentText);
                }

                return text.ToString();
            }
        }

        public static void BC_AddTakeCompleteAdding()
        {
            using (BlockingCollection<int> bc = new BlockingCollection<int>())
            {

                // Spin up a Task to populate the BlockingCollection 
                using (Task t1 = Task.Factory.StartNew(() =>
                {
                    bc.Add(1);
                    bc.Add(2);
                    bc.Add(3);
                    bc.CompleteAdding();
                }))
                {

                    // Spin up a Task to consume the BlockingCollection
                    using (Task t2 = Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            // Consume consume the BlockingCollection
                            while (true) Console.WriteLine(bc.Take());
                        }
                        catch (InvalidOperationException)
                        {
                            // An InvalidOperationException means that Take() was called on a completed collection
                            Console.WriteLine("That's All!");
                        }
                    }))

                        Task.WaitAll(t1, t2);
                }
            }
        }

        public static void BC_TryTake()
        {
            // Construct and fill our BlockingCollection
            using (BlockingCollection<int> bc = new BlockingCollection<int>())
            {
                int NUMITEMS = 10000;
                for (int i = 0; i < NUMITEMS; i++) bc.Add(i);
                bc.CompleteAdding();
                int outerSum = 0;

                // Delegate for consuming the BlockingCollection and adding up all items
                Action action = () =>
                {
                    int localSum = 0;

                    while (bc.TryTake(out int localItem)) localSum += localItem;
                    Interlocked.Add(ref outerSum, localSum);
                };

                // Launch three parallel actions to consume the BlockingCollection
                Parallel.Invoke(action, action, action);

                Console.WriteLine("Sum[0..{0}) = {1}, should be {2}", NUMITEMS, outerSum, ((NUMITEMS * (NUMITEMS - 1)) / 2));
                Console.WriteLine("bc.IsCompleted = {0} (should be true)", bc.IsCompleted);
            }
        }

        public static void BC_FromToAny()
        {
            BlockingCollection<int>[] bcs = new BlockingCollection<int>[2];
            bcs[0] = new BlockingCollection<int>(5); // collection bounded to 5 items
            bcs[1] = new BlockingCollection<int>(5); // collection bounded to 5 items

            // Should be able to add 10 items w/o blocking
            int numFailures = 0;
            for (int i = 0; i < 10; i++)
            {
                if (BlockingCollection<int>.TryAddToAny(bcs, i) == -1) numFailures++;
            }
            Console.WriteLine("TryAddToAny: {0} failures (should be 0)", numFailures);

            // Should be able to retrieve 10 items
            int numItems = 0;
            int item;
            while (BlockingCollection<int>.TryTakeFromAny(bcs, out item) != -1) numItems++;
            Console.WriteLine("TryTakeFromAny: retrieved {0} items (should be 10)", numItems);
        }

        public static void BC_GetConsumingEnumerable()
        {
            using (BlockingCollection<int> bc = new BlockingCollection<int>())
            {

                // Kick off a producer task
                Task.Factory.StartNew(() =>
                {
                    for (int i = 0; i < 10; i++)
                    {
                        bc.Add(i);
                        Thread.Sleep(100); // sleep 100 ms between adds
                    }

                    // Need to do this to keep foreach below from hanging
                    bc.CompleteAdding();
                });

                // Now consume the blocking collection with foreach.
                // Use bc.GetConsumingEnumerable() instead of just bc because the
                // former will block waiting for completion and the latter will
                // simply take a snapshot of the current state of the underlying collection.
                foreach (var item in bc.GetConsumingEnumerable())
                {
                    Console.WriteLine(item);
                }
            }
        }
    }
}
