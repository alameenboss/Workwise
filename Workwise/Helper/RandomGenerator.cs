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
            _words = new string[] { "anemone", "wagstaff", "man", "the", "for", "and", "a", "with", "bird", "fox", "alphabetized", "an", "and", "any", "appear", "box", "button", "click", "copy", "count", "first", "formatting", "generates", "in", "into", "list", "of", "options", "paste", "second", "select", "several", "text", "the", "this", "unique", "utility", "will", "with", "words", "your" };
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