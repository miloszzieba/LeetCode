using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    internal class RansomNote
    {
        public bool CanConstruct(string ransomNote, string magazine)
        {
            if(ransomNote.Length > magazine.Length)
                return false;

            var array = new int[26];

            foreach (var c in magazine)
                array[c - 'a']++;

            foreach (var c in ransomNote)
            {
                if (array[c - 'a'] == 0) return false;
                array[c - 'a']--;
            }

            return true;
        }
    }
}
