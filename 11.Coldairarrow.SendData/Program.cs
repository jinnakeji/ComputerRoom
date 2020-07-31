using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

namespace _11.Coldairarrow.SendData
{
    class Program
    {
        static void Main(string[] args)
        {
            Start();
        }

        static void Start()
        {
            while (true)
            {
                Console.WriteLine("输入r发送请求,输入l每秒发送,输入n结束");
                var key = Console.ReadKey();
                if (key.KeyChar == 'n')
                {
                    break;
                }
                else if (key.KeyChar == 'r')
                {
                    try
                    {
                        Console.WriteLine();
                        sendData().Wait();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else if (key.KeyChar == 'l')
                {
                    try
                    {
                        Thread thread = new Thread(() =>
                        {
                            int i = 0;
                            while (true)
                            {
                                try
                                {
                                    sendData();
                                    Thread.Sleep(1000);
                                    Console.WriteLine(i++);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }
                        });

                        thread.Start();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("输入错误，请重新输入");
                }
            }
            Console.WriteLine("程序已停止，按任意键结束");
            Console.ReadKey();
        }

        async static Task sendData()
        {
            string baseUrl = "http://localhost:5000";
            HttpClient client = new HttpClient();

            var currentDir = Environment.CurrentDirectory;
            var jsonPath = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(currentDir))) + "\\data.json";
            var json = File.ReadAllText(jsonPath);
            var httpcontent = new StringContent(json);

            var response = await client.PostAsync(baseUrl + "/api/Remote/DeviceData", httpcontent);

            var aa = response.Content;
            var stream = await aa.ReadAsStreamAsync();
            var bs = new byte[stream.Length];
            var str = System.Text.Encoding.UTF8.GetString(bs);
            Console.WriteLine(str);
            Console.WriteLine("请求结束.....");
        }
    }
}
