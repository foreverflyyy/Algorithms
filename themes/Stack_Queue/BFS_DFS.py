class Node:
    def __init__(self, val: int = 0, children = None):
        self.val = val
        self.children = children

class Solution:
    def BFS_1(self, root: Node, target: Node):
        queue = []
        step = 0

        queue.append(root)
        while len(queue) != 0:
            size = len(queue)
            for i in range(size):
                node = queue.pop(0)
                if node == target:
                    return step

                for child in node.children:
                    queue.append(child)
            step += 1
        return -1

    def BFS_2(self, root: Node, target: Node):
        queue = []
        visited = []
        step = 0

        queue.append(root)
        visited.append(root)
        while len(queue) != 0:
            size = len(queue)
            for i in range(size):
                node = queue.pop(0)
                if node == target:
                    return step

                for child in node.children:
                    queue.append(child)
                    visited.append(child)
            step += 1
        return -1

    def DFS_1(self, node: Node, target: Node, visited=None):
        if node is target:
            return True
        if not visited:
            visited = []

        for child in node.children:
            if child not in visited:
                visited.append(child)
                if self.DFS_1(child, target, visited):
                    return True
        return False

    def DFS_2(self, root: Node, target: Node):
        stack = []
        visited = []
        stack.append(root)
        while len(stack) != 0:
            current_node = stack.pop()
            if current_node is target:
                return True
            for child in current_node.children:
                if child not in visited:
                    stack.append(child)
                    visited.append(child)
        return False


solution = Solution()
node7 = Node(4, [])
node6 = Node(2, [])
node5 = Node(1, [])
node4 = Node(5, [])
node3 = Node(3, [node6, node7])
node2 = Node(7, [node4, node5])
node1 = Node(9, [node2, node3])
print(solution.DFS_2(node1, node4))
