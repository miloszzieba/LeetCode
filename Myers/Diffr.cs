//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Myers
//{
//    internal class Diffr
//    {
//        public List<string> result = new List<string>();

//        public string Run(string s)
//        {
//            var lines = s.Split('\n');
//            foreach (var line in lines)
//            {
//                var first = line.FirstOrDefault();
//                switch(first)
//                {
//                    case default(char):
//                    case ' ': result.Add(line); break;
//                    case '+': break;
//                    case '-': break;
//                    default: throw new ArgumentException($"Character {first} shouldn't be at the beginning of line.");
//                }
//            }
//        }
//    }
//}
