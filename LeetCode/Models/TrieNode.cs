using System;
using System.Collections.Generic;
using System.Text;

namespace LeetCode.Models
{
    public class TrieNode
    {
        public bool IsEnd { get; set; }
        public Dictionary<char, TrieNode> Children { get; } = new Dictionary<char, TrieNode>();
    }
}
