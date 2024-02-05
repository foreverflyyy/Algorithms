from typing import Optional


class TreeNode:
    def __init__(self, val=0, left=None, right=None):
        self.val = val
        self.left = left
        self.right = right

class Solution:
    def isSameTree(self, p: Optional[TreeNode], q: Optional[TreeNode]) -> bool:
        def check(p, q):
            if not p and not q:
                return True
            if not p or not q:
                return False
            if p.val != q.val:
                return False
            return True

        queue = [(p, q)]
        while len(queue) != 0:
            (p, q) = queue.pop(0)
            if not check(p, q):
                return False
            if not p and not q:
                continue
            if p or q:
                queue.append((p.left, q.left))
                queue.append((p.right, q.right))
        return True

tr6 = TreeNode(3)
tr5 = TreeNode(2)
tr4 = TreeNode(1, tr5, tr6)

tr3 = TreeNode(3)
tr2 = TreeNode(2)
tr1 = TreeNode(1, tr2, tr3)
solution = Solution()
print(solution.isSameTree(tr1, tr4))
