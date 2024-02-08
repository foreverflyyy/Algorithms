import ast
from collections import deque
from typing import Optional, List


class Node:
    def __init__(self, val: int = 0, left: 'Node' = None, right: 'Node' = None, next: 'Node' = None):
        self.val = val
        self.left = left
        self.right = right
        self.next = next

class TreeNode:
    def __init__(self, val=0, left=None, right=None):
        self.val = val
        self.left = left
        self.right = right

class Solution:
    def levelOrder(self, root: Optional[TreeNode]) -> List[List[int]]:
        queue = deque()
        result_list = []

        if not root:
            return result_list

        queue.append(root)
        while len(queue) != 0:
            level_num = len(queue)
            sub_list = []
            for i in range(level_num):
                if queue[0].left:
                    queue.append(queue[0].left)
                if queue[0].right:
                    queue.append(queue[0].right)
                sub_list.append(queue.popleft().mult)
            result_list.append(sub_list)

        return result_list

    # Populating Next Right Pointers in Each Node
    def connect(self, root: 'Optional[Node]') -> 'Optional[Node]':
        queue = deque()
        result_list = []
        if not root:
            return None

        queue.append(root)
        while len(queue) != 0:
            level_num = len(queue)
            sub_list = []
            for i in range(level_num):
                if queue[0].left:
                    queue.append(queue[0].left)
                if queue[0].right:
                    queue.append(queue[0].right)
                sub_list.append(queue.popleft())
            result_list.append(sub_list)

        for i in range(len(result_list)):
            length = len(result_list[i])
            for j in range(length):
                if j == length - 1:
                    result_list[i][j].next = None
                    continue
                result_list[i][j].next = result_list[i][j + 1]

        return root

    # Serialize and Deserialize Binary Tree
    def serialize(self, root: TreeNode) -> str:
        queue = deque()
        result_list = []
        if not root:
            return '[]'

        queue.append(root)
        while len(queue) != 0:
            count_none_in_lvl = 0
            level_num = len(queue)
            sub_list = []
            for _ in range(level_num):
                if queue[0] is not None:
                    queue.append(queue[0].left)
                    queue.append(queue[0].right)
                    sub_list.append(queue.popleft().mult)
                else:
                    count_none_in_lvl += 1
                    sub_list.append(queue.popleft())
            result_list.append(sub_list)

            if count_none_in_lvl == level_num:
                break

        return str(result_list)

    def get_tree_path(self, data, level, index) -> 'Optional[TreeNode]':
        value = data[level][index]
        if value is None or value == "None":
            return None

        indentation_before_index = 0
        for i in range((index + 1)):
            if data[level][i] is None:
                indentation_before_index += 2

        root = TreeNode(value)
        need_index = (index * 2) - indentation_before_index
        root.left = self.get_tree_path(data, level + 1, need_index)
        root.right = self.get_tree_path(data, level + 1, need_index + 1)
        return root

    def deserialize(self, data: str) -> 'Optional[TreeNode]':
        data = ast.literal_eval(data)
        if len(data) == 0:
            return None

        return self.get_tree_path(data, 0, 0)

    # Lowest Common Ancestor of a Binary Tree
    def lowestCommonAncestor(self, root, p, q):
        if root in (None, p, q):
            return root
        left, right = (self.lowestCommonAncestor(kid, p, q) for kid in (root.left, root.right))
        return root if left and right else left or right


tr_9 = TreeNode(4)
tr_8 = TreeNode(7)

tr_7 = TreeNode(8)
tr_6 = TreeNode(0)

tr_5 = TreeNode(2, tr_8, tr_9)
tr_4 = TreeNode(6)

tr_3 = TreeNode(1, tr_6, tr_7)
tr_2 = TreeNode(5, tr_4, tr_5)
tr_1 = TreeNode(3, tr_2, tr_3)

tr5 = TreeNode(5)
tr4 = TreeNode(4)
tr3 = TreeNode(3, tr4, tr5)
tr2 = TreeNode(2)
tr1 = TreeNode(1, tr2, tr3)

solution = Solution()
res = solution.lowestCommonAncestor(tr_1, tr_2, tr_9)
print("res", res)
