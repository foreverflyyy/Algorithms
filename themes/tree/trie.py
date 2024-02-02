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


trie = Trie()
print(trie.startsWith("a"))

# print(trie.insert("apple"))
# print(trie.search("apple"))
# print(trie.startsWith("app"))
# print(trie.search("app"))
# print(trie.insert("app"))
# print(trie.search("app"))
