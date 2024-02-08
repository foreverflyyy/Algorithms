class MyHashSet:
    def __init__(self):
        self.arr = [[] for _ in range(1 << 15)]

    def evel_hash(self, key):
        return ((key*100000) & (1 << 20) - 1) >> 5

    def add(self, key: int) -> None:
        hash_key = self.evel_hash(key)
        if key not in self.arr[hash_key]:
            self.arr[hash_key].append(key)

    def remove(self, key: int) -> None:
        hash_key = self.evel_hash(key)
        if key in self.arr[hash_key]:
            self.arr[hash_key].remove(key)

    def contains(self, key: int) -> bool:
        hash_key = self.evel_hash(key)
        return key in self.arr[hash_key]


class ListNode:
    def __init__(self, key, value, next):
        self.key = key
        self.value = value
        self.next = next


class MyHashMap:
    def __init__(self):
        self.size = 19997
        self.mult = 12582917
        self.data = [None for _ in range(self.size)]

    def hash(self, key):
        return key * self.mult % self.size

    def put(self, key: int, value: int) -> None:
        self.remove(key)
        hash_key = self.hash(key)
        node = ListNode(key, value, self.data[hash_key])
        self.data[hash_key] = node

    def get(self, key: int) -> int:
        hash_key = self.hash(key)
        node = self.data[hash_key]
        while node:
            if node.key == key:
                return node.value
            node = node.next
        return -1

    def remove(self, key: int) -> None:
        hash_key = self.hash(key)
        node = self.data[hash_key]
        if not node:
            return
        if node.key == key:
            self.data[hash_key] = node.next
            return
        while node.next:
            if node.next.key == key:
                node.next = node.next.next
                return
            node = node.next


solution = MyHashMap()
solution.put(5, 10)
print(solution.get(5))
solution.remove(5)
print(solution.get(5))

