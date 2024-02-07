import collections
from typing import List


class TrieNode:
    def __init__(self):
        self.next = collections.defaultdict(TrieNode)
        self.index = -1
        self.list = []


class Solution:
    def palindromePairs(self, words: List[str]) -> List[List[int]]:
        res = []
        root = TrieNode()

        for i in range(len(words)):
            self.addWord(root, words[i], i)

        for i in range(len(words)):
            self.search(words, i, root, res)
        return res

    def addWord(self, root, word, index):
        for i in range(len(word))[::-1]:
            symbol = word[i]
            if self.isPalindrome(word, 0, i):
                root.list.append(index)

            root = root.next[symbol]
        root.list.append(index)
        root.index = index
    # [[0, 1], [1, 0], [2, 4], [3, 2]]
    def search(self, words, i, root, res):
        for j in range(len(words[i])):
            if root.index >= 0 and root.index != i and self.isPalindrome(words[i], j, len(words[i]) - 1):
                res.append([i, root.index])

            root = root.next[words[i][j]]
            if not root:
                return

        for j in root.list:
            if i != j:
                res.append([i, j])

    def isPalindrome(self, word, i, j):
        while i < j:
            if word[i] != word[j]:
                return False
            i += 1
            j -= 1
        return True


solution = Solution()
data = ["abcd", "dcba", "lls", "s", "sssll"]
print(solution.palindromePairs(data))

# data = ["bat","tab","cat"]
# print(solution.palindromePairs(data))
# data = ["a",""]
# print(solution.palindromePairs(data))
