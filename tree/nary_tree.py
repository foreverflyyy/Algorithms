from typing import List


class Node:
    def __init__(self, val=None, children=None):
        self.val = val
        self.children = children


class Solution:
    def levelOrder(self, root: 'Node') -> List[List[int]]:
        result = []
        if root is None:
            return result

        sequence = [root]
        while len(sequence) != 0:
            current_level = []
            for _ in (range(len(sequence))):
                node = sequence.pop(0)
                current_level.append(node.val)
                if node.children is not None:
                    for child_node in node.children:
                        sequence.append(child_node)
            result.append(current_level)
        return result

    def preorder(self, root: 'Node') -> List[int]:
        result = []
        if root is None:
            return result

        result.append(root.val)
        if root.children is None:
            return result

        for child in root.children:
            new_nums = self.preorder(child)
            result.extend(new_nums)

        return result

    def postorder(self, root: 'Node') -> List[int]:
        result = []
        if root is None:
            return result

        if root.children is None:
            result.append(root.val)
            return result

        result = []
        stack = [root]
        while len(stack) != 0:
            node = stack.pop()
            if node is None:
                continue
            if node.children is None:
                result.append(node.val)
                continue

            children = node.children.copy()
            children.reverse()
            stack.append(Node(node.val))
            for child in children:
                stack.append(child)

        return result

    def maxDepth(self, root: 'Node', depth=1) -> int:
        if root is None:
            return depth - 1
        if root.children is None:
            return depth

        depths = {depth}
        for child in root.children:
            new_depth = self.maxDepth(child, depth + 1)
            if new_depth > depth:
                depths.add(new_depth)
        return max(depths)



n6 = Node(6)
n5 = Node(5)
n4 = Node(4)
n3 = Node(2)
n2 = Node(3, [n5, n6])
n1 = Node(1, [n2, n3, n4])

solution = Solution()
# print(solution.levelOrder(n1))
print(solution.maxDepth(n1))
# print(solution.levelOrder(n1))
