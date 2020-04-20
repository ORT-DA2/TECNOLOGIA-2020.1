using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using ej_filters.api.Models;
using System.Collections.Generic;

namespace ej_filters.api.Logic
{
    public static class ServiceLogic
    {
        private static string _path = "jokes.txt";
        private static string _pathLog = "log.txt";
        private static string _separator = "###########################################################################";
        private static string _separatorType = "-" + Environment.NewLine;
        public static void Startup()
        {
            if (!System.IO.File.Exists(_path))
            {
                Joke joke = new Joke() { Type = "Mama mama", Content = "Chiste de mama mama" };
                string row = joke.Type + _separatorType + joke.Content + Environment.NewLine + _separator;
                System.IO.File.WriteAllText(_path, row, Encoding.UTF8);
            }
        }
        public static void AddJoke(Joke joke)
        {
            string row = Environment.NewLine + joke.Type + _separatorType + joke.Content + Environment.NewLine + _separator;
            System.IO.File.AppendAllText(_path, row);
        }
        public static IEnumerable<Joke> GetJokes()
        {

            string result = System.IO.File.ReadAllText(_path);
            string[] jokesStr = result.Split(_separator);
            for (int i = 0; i < jokesStr.Length; i++)
            {
                if (jokesStr[i] != "")
                {
                    string[] j = jokesStr[i].Split(_separatorType);
                    yield return new Joke() { Type = Regex.Replace(j[0], @"\r\n", ""), Content = Regex.Replace(j[1], @"\r\n", "") };
                }
            }
        }
        public static void EntryLog()
        {
            if (!System.IO.File.Exists(_pathLog))
            {
                System.IO.File.WriteAllText(_pathLog, "", Encoding.UTF8);
            }
            System.IO.File.AppendAllText(_pathLog, "Say Hello at:   " + DateTime.Now.ToString() + Environment.NewLine);
        }
    }
}