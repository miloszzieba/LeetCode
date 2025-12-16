using System;
using System.Collections.Generic;
using System.Text;

namespace LeetCode.Models
{
    public class Trie
    {
        private readonly TrieNode _root = new TrieNode();

        public TrieNode Root { get { return _root; } }

        public void AddWord(ReadOnlySpan<char> word)
        {
            var node = _root;
            for (int i = 0; i < word.Length; i++)
            {
                if (!node.Children.ContainsKey(word[i]))
                    node.Children[word[i]] = new TrieNode();
                node = node.Children[word[i]];
            }
            node.IsEnd = true;
        }

        public bool Search(ReadOnlySpan<char> word)
        {
            var node = _root;
            for (int i = 0; i < word.Length; i++)
            {
                if (!node.Children.ContainsKey(word[i]))
                    return false;
                node = node.Children[word[i]];
            }
            return node.IsEnd;
        }
    }
}
