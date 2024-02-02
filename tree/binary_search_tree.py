from typing import Optional, List


class TreeNode:
    def __init__(self, val=0, left=None, right=None):
        self.val = val
        self.left = left
        self.right = right

class BSTIterator:
    _root = None

    def __init__(self, root: Optional[TreeNode]):
        _root = root

    def inorder_tree(self, root, sequence=None):
        if not root:
            return None
        if sequence is None:
            sequence = []
        self.inorder_tree(root.left, sequence)
        sequence.append(root.val)
        self.inorder_tree(root.right, sequence)
        return sequence

    def insertIntoBST(self, root: Optional[TreeNode], val: int) -> Optional[TreeNode]:
        if not root:
            return None

        new_node = TreeNode(val)
        current_node = root
        while True:
            if new_node.val > current_node.val:
                if current_node.right:
                    current_node = current_node.right
                    continue
                else:
                    current_node.right = new_node
                    return root
            if new_node.val < current_node.val:
                if current_node.left:
                    current_node = current_node.left
                    continue
                else:
                    current_node.left = new_node
                    return root

    def deleteNode(self, root: Optional[TreeNode], key: int) -> Optional[TreeNode]:
        if not root:
            return None

        if key > root.val:
            root.right = self.deleteNode(root.right, key)
        elif key < root.val:
            root.left = self.deleteNode(root.left, key)
        else:
            if not root.right:
                return root.left
            if not root.left:
                return root.right

            min_node = root.right
            while min_node.left:
                min_node = min_node.left
            root.val = min_node.val
            root.right = self.deleteNode(root.right, root.val)
        return root


    def lowestCommonAncestor(self, root: 'TreeNode', p: 'TreeNode', q: 'TreeNode') -> 'TreeNode':
        if root in (None, p, q):
            return root

        left, right = (self.lowestCommonAncestor(kid, p, q) for kid in(root.left, root.right))
        return root if left and right else left or right


tr9 = TreeNode(5)
tr8 = TreeNode(3)
tr7 = TreeNode(9)
tr6 = TreeNode(7)
tr5 = TreeNode(4, tr8, tr9)
tr4 = TreeNode(0)
tr3 = TreeNode(8, tr6, tr7)
tr2 = TreeNode(2, tr4, tr5)
tr1 = TreeNode(6, tr2, tr3)

solution = BSTIterator(tr1)
res = solution.lowestCommonAncestor(tr1, tr2, tr3)
print(res)
# print(obj.inorder_tree(tr1))
# obj.deleteNode(tr1, 3)
# obj.deleteNode(tr1, 7)
# print(obj.inorder_tree(tr1))
print()