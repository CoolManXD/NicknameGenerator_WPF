using System;
using System.Linq;
using System.Xml.Linq;

namespace NicknameGeneratorForWow
{
    class Generator
    {
        public Random Rand { get; set; }
        public Generator()
        {
            Rand = new Random();
        }
        public string generateNickname(GenerateData currentData)
        {
            string nickname = string.Empty;

            char firstLetter = currentData.FirstLetter;
            int min = currentData.MinLength;
            int max = currentData.MaxLength;
            
            // вибор префикса
            var prefixes = from r in XElement.Load("resources/preElf.xml").Elements("prefix")
                     where r.Element("string").Value.ToString()[0] == firstLetter
                     select r.Element("string").Value;

            int elementsQuantity = prefixes.Count();
            string prefix = prefixes.ToList().ElementAtOrDefault(Rand.Next(0, elementsQuantity));

            // выбор суффикса
            elementsQuantity = XElement.Load("resources/sufElf.xml").Elements("suffix").Count();

            int temp = Rand.Next(1, elementsQuantity + 1);
            string suffix = XElement.Load("resources/sufElf.xml").Elements("suffix").Where(x => x.Attribute("id").Value 
                                    == temp.ToString()).Select(x => x.Element("string").Value).FirstOrDefault();

            //var prefixes = from r in XElement.Load("resources/sufElf.xml").Elements("suffix")
            //               where r.Attribute("id").Value == firstLetter
            //               select r.Element("string").Value;

            // недостающие буквы
            if (prefix != null)
            {
                int size = prefix.Length + suffix.Length;
                int check = min - size;
                if (check < 0)
                    min = 0;
                else
                    min = check;
                max = max - size;

                string lack = string.Empty;
                int temp1 = Rand.Next(min, max + 1);
                for (int i = 0; i < temp1; i++)
                    lack += (char)Rand.Next(0x0061, 0x007B);

                nickname = prefix + lack + suffix;        
            }
            else
            {
                int size = suffix.Length + 1;
                max = max - size;

                string lack = string.Empty;
                int temp2 = Rand.Next(min, max + 1);
                for (int i = 0; i < temp2; i++)
                    lack += (char)Rand.Next(0x0061, 0x007B);

                nickname = firstLetter + lack + suffix;
            }
            return nickname;
        }

    }
}
