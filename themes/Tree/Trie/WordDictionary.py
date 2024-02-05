from typing import List


class TrieNode:
    value: str
    children: {}

    def __init__(self, value=None, children=None):
        self.value = value
        self.children = children


class WordDictionary:
    root = TrieNode(None, {})

    def addWord(self, word: str) -> None:
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

    def search(self, word: str, root=None) -> bool:
        if not root:
            root = self.root

        current_node = root
        for index, char in enumerate(word):
            if char != ".":
                next_node_of_char = current_node.children.get(char)
                if not next_node_of_char:
                    return False
                current_node = next_node_of_char
                continue

            if len(word) > (index + 1):
                if len(current_node.children.keys()) == 0:
                    return False
                return True

            for key in current_node.children.keys():
                considered_node = current_node.children[key]
                if self.search(word[(index + 1)], considered_node):
                    return True
            return False
        return True


# word_dict = WordDictionary()
# print(word_dict.search("a"))
# print(word_dict.addWord("bad"))
# print(word_dict.addWord("dad"))
# print(word_dict.addWord("mad"))
