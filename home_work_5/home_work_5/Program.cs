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
        static string Protocol = @"((?:(?:ftp|https?:\/\/){1}(?:www\.)?)|(?:www\.){1})"; // валидный url начинается либо с http/https/ftp + www, либо просто с www
        static string DomainAndSubdomain = @"(?:(?:[-A-Za-zА-Яа-я0-9])+\.)+"; // учитываем возможные домены и субдомены: каждая последовательность, завершающаяся точкой 
        static string TopLevelDomain = @"(?:[-A-Za-zА-Яа-я0-9])+"; // учитываем TLD : может состоять из букв, цифр и -
        static string Domain = "("+DomainAndSubdomain + TopLevelDomain+")"; // соберем группу для домена вида qwer.qwe.com
        //static string IP4 = @"(([0-9]{1,3}\:){4})"; // маска для IP4 адресов, что так же может быть валидным адресом. Выражение максимально простое, не рассматривает диапазоны допустимых значений для каждого блока (255)
        //static string DomainOrIP = "("+Domain+")|" + IP4;// возможно, адрес может быть задан, как IP: смотрим, что доменное имя может быть либо вида qwer.qwe.com либо ip4
        static string PathAndPageOrMethod = @"(\/[-\.A-Za-zА-Яа-я0-9]*)*"; // путь может быть, а может не быть, но в любом случае, пусть он начинается с / или ?
        static string Parameters = @"([a-zA-ZА-Яа-я0-9\-\.\?\,\/\+&%;\$#_=]+)*"; // популярные стоп-символы, которые используются с параметрами запросов
        public static string Pattern = Protocol + Domain + PathAndPageOrMethod + Parameters;// составим итоговый паттерн
        //Таким образом, в данном выражении мы имеем поиск валидных url адресов,
        // при этом, результаты разбиты на четыре группы: 1) Грубо говоря протокол (http(s),ftp,www) 2) Домен 3) Путь 4) Параметры запроса
        public static string GetHtml(string address) // получим текст страницы
        {
            string content;
            try
            {
                using (HttpClient client = new HttpClient()) // new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true })
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
        static string SanitizeInput(string inp)  // метод для очистки "грязи" от JS и HTML
        {
            return inp.Replace("&amp;", "&").Replace("&quot;", "\"");
        }
        public static List<string> ProcessResponse(string body) // обработка текста страницы
        {
            Regex regex = new Regex(Pattern, RegexOptions.IgnoreCase);
            List<string> output = new List<string>();
            foreach (Match match in regex.Matches(SanitizeInput(body)))
            {
                if(!string.IsNullOrEmpty(match.Value))
                    output.Add(match.Value);
            }
            return output;
        }
        static void Main(string[] args)
        {  
            if (args.Length != 0)
            {
                string result;
                foreach (string addr in args)
                {
                    Console.WriteLine("Analizing "+ addr + "...");
                    Regex regex = new Regex(Pattern,RegexOptions.IgnoreCase);
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
                //string address = "https://stackoverflow.com/questions/5525181/find-each-regex-match-in-string";
                string address = "http://tommarien.github.io/blog/2012/04/16/showdown-mstest-vs-nunit/";
                //Test Zone
                //string address = "http://yandex.ru";
                string result = GetHtml(address);
                if (result != null)
                {
                    //Console.WriteLine(result);
                    Console.WriteLine("Analizing " + address + "...");
                    int i = 1;
                    foreach(string item in ProcessResponse(result))
                        Console.WriteLine("\t" + i++.ToString()+ ": " +item);
                    Console.WriteLine("Total: " + (i-1).ToString());
                }
                
                //endtest
            }
            Console.ReadKey();
        }
    }
}