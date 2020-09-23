using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Xml.Serialization;
using System.IO.Compression;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            /*БСБО-05-19
             * Копейкин Д.С.
             * 1.	Вывести информацию в консоль о логических дисках, именах, метке тома, размере типе файловой системы.
                2.	Работа  с файлами ( класс File, FileInfo, FileStream и другие)
                    a)	Создать файл
                    b)	Записать в файл строку
                    c)	Прочитать файл в консоль
                    d)	Удалить файл
                3.	Работа с форматом JSON
                4.	Работа с форматом XML
                5.	Создание zip архива, добавление туда файла, определение размера архива

             */
            Console.Write("Введите номер задания ");
            while (true)
            {
                switch (Console.ReadLine())
                {

                    case "1":
                        #region задание 1
                        #region текст задания
                        //.Вывести информацию в консоль о логических дисках, именах, метке тома, размере типе файловой системы.
                        #endregion
                        DriveInfo[] drives = DriveInfo.GetDrives();

                        foreach (DriveInfo drive in drives)
                        {
                            Console.WriteLine($"Название: {drive.Name}");
                            Console.WriteLine($"Тип: {drive.DriveType}");
                            if (drive.IsReady)
                            {
                                Console.WriteLine($"Объемдиска: {drive.TotalSize}");
                                Console.WriteLine($"Свободноепространство: {drive.TotalFreeSpace}");
                                Console.WriteLine($"Метка: {drive.VolumeLabel}");
                            }
                            Console.ReadKey();
                        }
                        #endregion
                        break;
                    case "2":
                        #region задание 2
                        #region текст задания
                        //Работа  с файлами(класс File, FileInfo, FileStream и другие)
                        //        a)	Создать файл
                        //        b)	Записать в файл строку
                        //        c)	Прочитать файл в консоль
                        //        d)	Удалить файл
                        #endregion
                        Console.InputEncoding = Encoding.Unicode;
                        using (var sw = new StreamWriter(@"text.txt", false, Encoding.Unicode))//не стал создавать аудиторий и помещать туда файл, т.к. не знаю, где их стоит создавать не на своем пк, поэто файл будеи помещен "имя папки проэкта"/bin/debug/text.txt
                        {
                            Console.Write("Введите текст для записи в файл:");
                            sw.WriteLine(Console.ReadLine());
                        }
                        using (var sr = new StreamReader(@"text.txt", Encoding.Unicode))
                        {
                            Console.WriteLine("нажмите клавишу для считывания текста из файла");
                            Console.ReadKey();
                            while (!sr.EndOfStream)
                            {
                                Console.WriteLine(sr.ReadLine());
                            }
                        }
                        Console.WriteLine("Нажмите кнопку для удаления файла");
                        Console.ReadKey();
                        File.Delete(@"text.txt");
                        Console.WriteLine("задание завершено");
                        #endregion
                        break;
                    case "3":
                        #region задание 3
                        #region текст задания
                        //Работа с форматом JSON
                        #endregion
                        User user1 = new User { Name = "Bill Gates", Age = 48, Company = "Microsoft" };//первый пользователь создан автоматически

                        User user2 = new User();
                        Console.WriteLine("Введите имя второго человека");
                        user2.Name = Console.ReadLine();
                        Console.WriteLine("возраст");
                        user2.Age = int.Parse(Console.ReadLine());
                        Console.WriteLine("наименование компании");
                        user2.Company = Console.ReadLine();
                        List<User> users = new List<User> { user1, user2 };
                        var json = new DataContractJsonSerializer(typeof(List<User>));
                        using (var file = new FileStream("users.json", FileMode.Create))//опять файл будет создан в "имя папки проэкта"/bin/debug/users.json
                        {
                            json.WriteObject(file, users);
                        }
                        using (var file = new FileStream("users.json", FileMode.OpenOrCreate))
                        {

                            var newUsers = json.ReadObject(file) as List<User>;
                            if (newUsers != null)
                            {
                                foreach (var user in newUsers)
                                {
                                    {
                                        Console.WriteLine(user);
                                    }
                                }
                            }
                        }
                        Console.WriteLine("Нажмите кнопку для удаления файла");
                        Console.ReadKey();
                        File.Delete(@"users.json");
                        Console.WriteLine("задание завершено");
                        Console.ReadLine();
                        #endregion
                        break;
                    case "4":
                        #region задание 4
                        #region текст задания
                        //Работа с форматом XML
                        #endregion
                        User user3 = new User { Name = "Bill Gates", Age = 48, Company = "Microsoft" };//первый пользователь создан автоматически

                        User user4 = new User();
                        Console.WriteLine("Введите имя второго человека");
                        user4.Name = Console.ReadLine();
                        Console.WriteLine("возраст");
                        user4.Age = int.Parse(Console.ReadLine());
                        Console.WriteLine("наименование компании");
                        user4.Company = Console.ReadLine();
                        List<User> users2 = new List<User> { user3, user4 };

                        var xml = new XmlSerializer(typeof(List<User>));
                        using (var file = new FileStream("users.xml", FileMode.Create))//опять файл будет создан в "имя папки проэкта"/bin/debug/users.xml
                        {
                            xml.Serialize(file, users2);
                        }
                        Console.WriteLine("для чтения файла нажмите клавишу");
                        Console.ReadKey();
                        using (var file = new FileStream("users.xml", FileMode.OpenOrCreate))
                        {

                            var newUsers = xml.Deserialize(file) as List<User>;
                            if (newUsers != null)
                            {
                                foreach (User user in users2)
                                {
                                    {
                                        Console.WriteLine(user);
                                    }
                                }
                            }
                        }
                        Console.WriteLine("Нажмите кнопку, для удаления файла");
                        Console.ReadKey();
                        File.Delete(@"users.xml");
                        Console.WriteLine("задание завершено");
                        #endregion
                        break;
                    case "5":
                        #region задание 5
                        #region текст задания
                        //Создание zip архива, добавление туда файла, определение размера архива
                        #endregion
                        using (var sw = new StreamWriter(@"text2.txt", false, Encoding.Unicode))//не стал создавать аудиторий и помещать туда файл, т.к. не знаю, где их стоит создавать не на своем пк, поэто файл будеи помещен "имя папки проэкта"/bin/debug/text.txt
                        {

                            sw.WriteLine("asdjdsjgnjdncvubbhxcvnbmcjbnjfdnkfmgksndkfmlsdmf");
                        }
                        string sourceFile = @"text2.txt"; // исходныйфайл
                        string compressedFile = @"text2.gz"; // сжатыйфайл

                        using (FileStream sourceStream = new FileStream(sourceFile, FileMode.OpenOrCreate))
                        {

                            using (FileStream targetStream = File.Create(compressedFile))
                            {

                                using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                                {
                                    sourceStream.CopyTo(compressionStream);
                                    Console.WriteLine("Сжатиефайла {0} завершено. Исходныйразмер: {1}  сжатыйразмер: {2}.",
                                        sourceFile, sourceStream.Length.ToString(), targetStream.Length.ToString());
                                }
                            }
                        }
                        #endregion
                        break;
                    default:
                        Console.WriteLine("неверно указанно задание");
                        break;
                }
            }
        }
    }
}
