using System;
using System.Linq;
using System.Xml.Linq;

namespace NicknameGeneratorForWow
{
    abstract class Generator
    {
        public Random Rand { get; set; }
        private char[] vowelLetters = new char[] { 'a', 'e', 'i', 'o', 'u' };
        private char[] consonantLetters = new char[] { 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'y', 'z' };
        public Generator()
        {
            Rand = new Random();
        }
        public string generateNickname(GenerateData currentData)
        {
            char firstLetter = currentData.FirstLetter;
            int min = currentData.MinLength;
            int max = currentData.MaxLength;
            
            // вибор префикса
            string prefix = generatePrefix(firstLetter);
            // выбор суффикса
            string suffix = generateSuffix();
            // недостающие буквы            
            if (prefix == null)
                prefix = firstLetter.ToString();
            int size = prefix.Length + suffix.Length;
            if (size > max)
                prefix = firstLetter.ToString();
            size = prefix.Length + suffix.Length;
            int check = min - size;
            if (check < 0)
                min = 0;
            else
                min = check;
            max = max - size;

            string lack = string.Empty;
            int temp = Rand.Next(min, max + 1);
            for (int i = 0; i < temp; i++)
            {
                if (i % 2 == 0)
                    lack += vowelLetters[Rand.Next(0, vowelLetters.Length)];
                else
                    lack += consonantLetters[Rand.Next(0, consonantLetters.Length)];
                // lack += (char)Rand.Next(0x0061, 0x007B);
            }
            return prefix + lack + suffix;
        }
        public abstract string generateSuffix();
        public abstract string generatePrefix(char letter);
    }

    class GenaratorElfNames: Generator
    {
        public override string generatePrefix(char letter)
        {
            var prefixes = from r in XElement.Load("resources/preElf.xml").Elements("prefix")
                           where r.Element("string").Value.ToString()[0] == letter
                           select r.Element("string").Value;
            
            int elementsQuantity = prefixes.Count();
            return prefixes.ToList().ElementAtOrDefault(Rand.Next(0, elementsQuantity));
        }
        public override string generateSuffix()
        {
            int elementsQuantity = XElement.Load("resources/sufElf.xml").Elements("suffix").Count();
            int temp = Rand.Next(1, elementsQuantity + 1);

            //string suffix = XElement.Load("resources/sufElf.xml").Elements("suffix").Where(x => x.Attribute("id").Value
            //                        == temp.ToString()).Select(x => x.Element("string").Value).FirstOrDefault();

            string suffix = (from r in XElement.Load("resources/sufElf.xml").Elements("suffix")
                           where r.Attribute("id").Value == temp.ToString()
                           select r.Element("string").Value).FirstOrDefault();
            return suffix;
        }
    }
    class GenaratorOrcNames : Generator
    {
        public override string generatePrefix(char letter)
        {
            var prefixes = from r in XElement.Load("resources/preOrc.xml").Elements("prefix")
                           where r.Element("string").Value.ToString()[0] == letter
                           select r.Element("string").Value;

            int elementsQuantity = prefixes.Count();
            return prefixes.ToList().ElementAtOrDefault(Rand.Next(0, elementsQuantity));
        }
        public override string generateSuffix()
        {
            int elementsQuantity = XElement.Load("resources/sufOrc.xml").Elements("suffix").Count();
            int temp = Rand.Next(1, elementsQuantity + 1);
            string suffix = (from r in XElement.Load("resources/sufElf.xml").Elements("suffix")
                             where r.Attribute("id").Value == temp.ToString()
                             select r.Element("string").Value).FirstOrDefault();
            return suffix;
        }
    }
}
