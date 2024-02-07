import math
import sys
from typing import List


class Solution:
    def mySqrt(self, x: int) -> int:
        if x == 0:
            return x
        left, right = 1, x
        while True:
            mid = left + (right - left) // 2
            if mid > x/mid:
                right = mid - 1
            else:
                if mid + 1 > x/(mid + 1):
                    return math.floor(mid)
                left = mid + 1

    def firstBadVersion(self, n: int) -> int:
        left, right = 1, n
        while left < right:
            mid = (left + right) // 2
            check = self.isBadVersion(mid)
            if check:
                right = mid
            else:
                left = mid + 1

        return left

    def isBadVersion(self, version):
        return True if version >= 3 else False

    def findPeakElement(self, nums: List[int]) -> int:
        length = len(nums)
        if length == 0:
            return -1
        old_num = sys.maxsize
        left_num = -sys.maxsize
        index = 0
        while index < length:
            current_val = nums[index]
            right_value = nums[index + 1] if (index + 1) < length else -sys.maxsize
            if left_num < current_val and current_val > right_value:
                return index
            if old_num < left_num and left_num > nums[index]:
                return index - 1
            old_num = current_val
            left_num = nums[index + 1] if (index + 1) < length else -sys.maxsize
            if (index + 2) < length:
                index += 2
            else:
                index += length - index - 1

    def findMin(self, nums: List[int]) -> int:
        length = len(nums)
        old_num = -sys.maxsize
        left_num = -sys.maxsize
        index = 0
        while index < length:
            current_num = nums[index]
            if left_num > current_num:
                return current_num
            if old_num > left_num:
                return left_num

            old_num = current_num
            left_num = nums[index + 1] if (index + 1) < length else sys.maxsize
            if index == length - 1:
                return nums[0]
            if (index + 2) < length:
                index += 2
            else:
                index += length - index - 1

    def search(self, nums: List[int], target: int) -> int:
        length = len(nums)
        if length == 0:
            return -1
        old_num = None
        left_num = None
        index = 0
        while True:
            current_val = nums[index]
            if current_val == target:
                return index
            if old_num == target:
                return index - 2
            if left_num == target:
                return index - 1

            right_value = nums[index + 1] if (index + 1) < length else None
            if right_value == target:
                return index + 1

            old_num = current_val
            left_num = right_value

            if index >= length - 1:
                return -1
            index += 2 if (index + 2) < length else (length - index - 1)


solution = Solution()
print(solution.search([4,5,6,7,0,1,2], 0))
print(solution.search([4,5,6,7,0,1,2], 3))
print(solution.search([1], 0))
print(solution.search([1], 1))
