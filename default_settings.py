class TreeNode:
    def __init__(self, val=0, left=None, right=None):
        self.val = val
        self.left = left
        self.right = right


class ListNode:
    def __init__(self, val=0, next=None):
        self.val = val
        self.next = next


# tr6 = TreeNode(25)
# tr5 = TreeNode(19, None, tr6)
# tr4 = TreeNode(56)
# tr3 = TreeNode(47, None, tr4)
# tr2 = TreeNode(26, tr5)
# tr1 = TreeNode(32, tr2, tr3)