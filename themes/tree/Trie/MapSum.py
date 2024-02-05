from collections import defaultdict


class TrieMapNode:
    def __init__(self):
        self.children = defaultdict(TrieMapNode)
        self.sum = 0

class MapSum:
    def __init__(self):
        self.root = TrieMapNode()
        self.map = defaultdict(int)

    def insert(self, key: str, val: int) -> None:
        diff = val - int(self.map[key])
        current_node = self.root
        for char in key:
            current_node = current_node.children[char]
            current_node.sum += diff
        self.map[key] = val

    def sum(self, prefix: str) -> int:
        current_node = self.root
        for char in prefix:
            if current_node is None:
                return 0
            current_node = current_node.children[char]
        return int(current_node.sum)


# trie = MapSum()
# print(trie.insert("apple", 3))
# print(trie.sum("apple"))
# print(trie.insert("app", 2))
# print(trie.insert("apple", 2))
# print(trie.sum("ap"))
