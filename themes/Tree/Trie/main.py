from collections import defaultdict
from typing import List


class TrieNode:
    value: str
    children: {}

    def __init__(self, value=None, children=None):
        self.value = value
        self.children = children


class Trie:
    root = TrieNode(None, {})

    def insert(self, word: str) -> None:
        if not word:
            return
        current_node = self.root
        for char in word:
            if char in current_node.children:
                current_node = current_node.children.get(char)
            else:
                new_node = TrieNode(char, {})
                current_node.children.setdefault(char, new_node)
                current_node = new_node
        current_node.children.setdefault("", TrieNode())

    def search(self, word: str) -> bool:
        if not word:
            return True
        current_node = self.root
        for char in word:
            if char in current_node.children:
                current_node = current_node.children.get(char)
            else:
                return False
        if current_node.children.get(""):
            return True
        return False

    def startsWith(self, prefix: str) -> bool:
        if not prefix:
            return True
        current_node = self.root
        for char in prefix:
            if char in current_node.children:
                current_node = current_node.children.get(char)
            else:
                return False
        return True

    def search_root_of_word(self, word):
        result_word = ""
        current_node = self.root
        for char in word:
            next_node_of_char = current_node.children.get(char)
            have_end_word = current_node.children.get("")
            if result_word and have_end_word:
                return result_word
            if not next_node_of_char:
                return ""
            current_node = next_node_of_char
            result_word += char
        return result_word

    def replaceWords(self, dictionary: List[str], sentence: str) -> str:
        for word in dictionary:
            self.insert(word)

        words = sentence.split(' ')
        result_sentence = ""
        for word in words:
            root_word = self.search_root_of_word(word)
            if root_word:
                result_sentence += root_word
            else:
                result_sentence += word
            result_sentence += " "
        return result_sentence[0:len(result_sentence)-1]


    def replaceWords_2(self, dic, sentence):
        trie = {}

        for word in dic:
            t = trie
            for ch in word:
                if ch not in t:
                    t[ch] = {}
                t = t[ch]
            t['#'] = '#'

        def process(string):
            t = trie
            for i, ch in enumerate(string):
                if ch not in t:
                    break
                t = t[ch]
                if '#' in t:
                    return string[:i + 1]
            return string

        return ' '.join(map(process, sentence.split()))


trie = Trie()
# print(trie.replaceWords(["cat","bat","rat"], "the cattle was rattled by the battery"))
print(trie.replaceWords(
    ["a", "aa", "aaa", "aaaa"],
    "a aa a aaaa aaa aaa aaa aaaaaa bbb baba ababa"
))

# print(trie.insert("apple"))
# print(trie.search("apple"))
# print(trie.startsWith("app"))
# print(trie.search("app"))
# print(trie.insert("app"))
# print(trie.search("app"))
