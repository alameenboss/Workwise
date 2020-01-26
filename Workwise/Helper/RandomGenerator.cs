using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Workwise.Helper
{
    public static class RandomGenerator
    {
        private static Random r = new Random();
        private static object l = new object();
        private static Random globalRandom = new Random();
        [ThreadStatic]
        private static Random localRandom;
        public static int GenerateNewRandom(int min, int max)
        {
            return new Random().Next(min, max);
        }
        public static int GenerateLockedRandom(int min, int max)
        {
            int result;
            lock (RandomGenerator.l)
            {
                result = RandomGenerator.r.Next(min, max);
            }
            return result;
        }
        public static int GenerateRandom(int min, int max)
        {
            Random random = RandomGenerator.localRandom;
            if (random == null)
            {
                int seed;
                lock (RandomGenerator.globalRandom)
                {
                    seed = RandomGenerator.globalRandom.Next();
                }
                random = (RandomGenerator.localRandom = new Random(seed));
            }
            return random.Next(min, max);
        }



    }
    public class RandomText
    {
        static Random _random = new Random();
        StringBuilder _builder;
        string[] _words;

        public RandomText()
        {
            _builder = new StringBuilder();
            _words = new string[] { "anemone", "wagstaff", "man", "the", "for", "and", "a", "with", "bird", "fox", "a", "about", "advised", "after", "afterwards",
                "age", "also", "ambassador", "amongst", "ample", "an", "ancient", "and", "annotationes", "ans", "application", "appointed", "as", "asse", "at",
                "author", "bartholomew", "be", "been", "bellay", "biblioth", "bishop", "born", "bud", "budde", "buddeus", "buried", "but", "by", "called",
                "calvinism", "caused", "coins", "coll", "collection", "collegium", "commentarii", "contains", "contributed", "corresponded", "correspondence",
                "day", "death", "des", "died", "dissipated", "du", "earlier", "eius", "epistolae", "equal", "erasmus", "esteem", "et", "extensive", "facility",
                "family", "flee", "fontainebleau", "for", "found", "france", "francis", "franz", "from", "gained", "ge", "geneva", "graecae", "great", "greatest",
                "greatly", "greek", "guillaume", "had", "having","he","held","high","him","his","history","house","in","idle","inclination","induced","i",
                "influence","is","jean","johann","latin","law","leanings","learned","le","leo","lexicographical","library","libros","life","linguae",
                "literature","louis","ma","made","marchands","marvel","massacre","means","measures","members","men","more","most","name","narbonne","nationale",
                "night","notes","obliged","of","on","only","open","or","origin","orl","others","pandectarum","paris","part","partibus","particularl","passion",
                "persuaded","philology","pomerania","printing","profession","progress","prohibiting","protestantism","purity","que","rapid","refrain","refuge",
                "religious","emarkable","removed","reputation","requ","request","retired","roman","rome","sections","see","seized","sent","settled","several",
                "she","should","small","some","sorbonne", "study", "sudden", "suggest", "suspected", "swedish", "switzerland", "that", "the", "their", "them",
                "they", "this", "thomas", "time", "times", "to", "took", "towards", "traditions", "tratise", "trilingue", "twentyfour", "under",
                "university", "upheld", "voluminous", "was", "went", "were", "when", "where", "which", "while", "who", "widow", "with", "work", "worthily",
                "written", "wroteM", "years" };
        }

        public void AddContentParagraphs(int numberParagraphs, int minSentences,
            int maxSentences, int minWords, int maxWords)
        {
            for (int i = 0; i < numberParagraphs; i++)
            {
                AddParagraph(_random.Next(minSentences, maxSentences + 1),
                             minWords, maxWords);
                _builder.Append("\n\n");
            }
        }

        void AddParagraph(int numberSentences, int minWords, int maxWords)
        {
            for (int i = 0; i < numberSentences; i++)
            {
                int count = _random.Next(minWords, maxWords + 1);
                AddSentence(count);
            }
        }

        void AddSentence(int numberWords)
        {
            StringBuilder b = new StringBuilder();
            // Add n words together.
            for (int i = 0; i < numberWords; i++) // Number of words
            {
                b.Append(_words[_random.Next(_words.Length)]).Append(" ");
            }
            string sentence = b.ToString().Trim() + ". ";
            // Uppercase sentence
            sentence = char.ToUpper(sentence[0]) + sentence.Substring(1);
            // Add this sentence to the class
            _builder.Append(sentence);
        }

        public string Content
        {
            get
            {
                return _builder.ToString();
            }
        }

        public string GetNewSentence(int numberWords)
        {
            AddSentence(numberWords);
            return _builder.ToString();
        }
    }
}