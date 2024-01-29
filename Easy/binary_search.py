import math
from typing import List


class Solution:
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


solution = Solution()
print(solution.search([-1,0,3,5,9,12], 9))
print(solution.search([-1,0,3,5,9,12], 2))
print(solution.search([-1,0,3,5,9,12], -2))
print(solution.search([-1,0,3,5,9,12], 12))
print(solution.search([5], 5))
