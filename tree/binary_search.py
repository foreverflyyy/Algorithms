from typing import List, Optional

class TreeNode:
    def __init__(self, val=0, left=None, right=None):
        self.val = val
        self.left = left
        self.right = right


class Solution:
    # Default binary search
    def search(self, nums: List[int], target: int) -> int:
        lower, high = 0, len(nums) - 1
        while lower <= high:
            mid = (high + lower) // 2
            if nums[mid] > target:
                high = mid - 1
            elif nums[mid] < target:
                lower = mid + 1
            else:
                return mid

        return -1

    # Check symmetric tree
    def isSymmetric(self, root: Optional[TreeNode], first=None, second=None) -> bool:
        if root:
            return self.isSymmetric(None, root.left, root.right)

        if not first and not second:
            return True
        if not first or not second:
            return False
        if first.val != second.val:
            return False

        check_1 = self.isSymmetric(None, first.left, second.right)
        check_2 = self.isSymmetric(None, first.right, second.left)
        return check_1 and check_2

    # Construct Binary Tree from Inorder and Postorder Traversal
    def buildTree(self, inorder: List[int], postorder: List[int]) -> Optional[TreeNode]:
        in_indexes = {}
        for i, num in enumerate(inorder):
            in_indexes[num] = i

        def helper(low, high):
            if low > high:
                return None

            root = TreeNode(postorder.pop())
            mid = in_indexes[root.val]
            root.right = helper(mid + 1, high)
            root.left = helper(low, mid - 1)
            return root

        return helper(0, len(postorder) - 1)

    # Construct Binary Tree from Preorder and Inorder Traversal
    def build_tree_by_pre_and_in_order(self, pre_order: List[int], in_order: List[int]) -> Optional[TreeNode]:
        def helper(pre_start, in_start, in_end, pre_order, in_order):
            if pre_start > len(pre_order) - 1 or in_start > in_end or in_start == -1:
                return None

            root = TreeNode(pre_order[pre_start])
            in_index = -2
            for i in range(in_start, (in_end + 1)):
                if in_order[i] == root.val:
                    in_index = i
                    break

            root.left = helper(
                pre_start + 1,
                in_start,
                in_index - 1,
                pre_order,
                in_order
            )
            root.right = helper(
                pre_start + in_index - in_start + 1,
                in_index + 1,
                in_end,
                pre_order,
                in_order
            )
            return root
        return helper(0, 0, len(in_order) - 1, pre_order, in_order)


solution = Solution()
# print(solution.search([-1,0,3,5,9,12], 9))
# print(solution.search([-1,0,3,5,9,12], 2))

# tr3 = TreeNode(2)
# tr2 = TreeNode(2)
# tr1 = TreeNode(1, tr2, tr3)
# print(solution.isSymmetric(tr1))

# print(solution.buildTree([3,9,20,15,7], [9,3,15,20,7]))
res = solution.buildTree([9,3,15,20,7], [9,15,7,20,3])
print(res)
