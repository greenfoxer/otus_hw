using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Net;

namespace home_work_5
{
    public class Program
    {

        //static string Pattern = @"((ftp|http(s)?:\/\/)?(www\.)?)(([A-Za-z0-9])+\.){1,256}([A-Za-z0-9]){1,6}(\/[A-Za-z0-9]+)*([a-zA-Z0-9\-\.\?\,\'\/\\\+&%\$#_]*)?";
        //static string Pattern = @"((ftp|http(s)?:\/\/)?(www\.)?)(([A-Za-z0-9])+\.)+([A-Za-z0-9])+(\/[A-Za-z0-9]+)+([a-zA-Z0-9\-\.\?\,\/\+&%;\$#_=]+)*";
        static string Protocol = @"(((ftp|http(s)?:\/\/){1}(www\.)?)|(www\.){1})"; // валидный url начинается либо с http/https/ftp/www
        static string DomainAndSubdomain = @"(([A-Za-z0-9])+\.)+"; // учитываем возможные домены и субдомены 
        static string TopLevelDomain = @"([A-Za-z0-9])+"; // учитываем TLD
        static string Domain = DomainAndSubdomain + TopLevelDomain;
        static string IP4 = @"(([0-9]{1,3}\:){4})"; // маска для IP4 адресов
        static string DomainOrIP = "("+Domain+")|" + IP4;// возможно, адрес может быть задан, как IP
        static string Path = @"((\/|\?)[A-Za-z0-9]*)*"; // путь может быть, а может не быть, но в любом случае, пусть он начинается с / или ?
        static string PageOrMethodAndParameters = @"([a-zA-Z0-9\-\.\?\,\/\+&%;\$#_=]+)*"; // популярные стоп-символы, которые используются с параметрами запросов
        static string Pattern = Protocol + Domain + Path + PageOrMethodAndParameters;// составим итоговый паттерн
        public static string GetHtml(string address)
        {
            string content;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                    content = client.GetStringAsync(address).Result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
            return content;
        }
        static string SanitizeInput(string inp)  // метод для очистки "грязи" от JS
        {
            return inp.Replace("&amp", " ").Replace("&quot", " ");
        }
        public static List<string> ProcessResponse(string body) // обработка текста страницы
        {
            Regex regex = new Regex(Pattern);
            List<string> output = new List<string>();
            foreach (Match match in regex.Matches(SanitizeInput(body)))
            {
                if(!string.IsNullOrEmpty(match.Value))
                    output.Add(match.Value);
            }
            return output;
        }
        public static void Main(string[] args)
        {  
            if (args.Length != 0)
            {
                string result;
                foreach (string addr in args)
                {
                    Console.WriteLine("Analizing "+ addr + "...");
                    Regex regex = new Regex(Pattern);
                    if (regex.IsMatch(addr)) // с помощью нажей же регулярки проверяем, что нам дали валидный url
                    {
                        result = GetHtml(addr); // получаем данные со страницы
                        if (result != null)
                        {
                            foreach (string item in ProcessResponse(result)) // выводим результаты
                                Console.WriteLine("\t" + item);
                        }
                        else
                            Console.WriteLine("URL " + addr + " didn't response.");
                    }
                    else
                        Console.WriteLine("Address is not valid!");
                }
            }
            else
            {
                Console.WriteLine("Usage: home_work_5.exe url_to_analize");
                //test
                //string address = "http://webcode.me";
                //string address = "https://stackoverflow.com/questions/15128766/write-boldface-text-using-console-writeline-c-or-printfn-f";
                string address = "https://stackoverflow.com/questions/5525181/find-each-regex-match-in-string";
                string result = GetHtml(address);
                if (result != null)
                {
                    //Console.WriteLine(result);
                    Console.WriteLine("Analizing " + address + "...");
                    foreach(string item in ProcessResponse(result))
                        Console.WriteLine("\t" + item);
                }
                //endtest
            }
            Console.ReadKey();
        }
    }
}