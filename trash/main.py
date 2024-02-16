from typing import List


class Solution:
    def containsNearbyAlmostDuplicate(self, nums: List[int], indexDiff: int, valueDiff: int) -> bool:
        for i in range(len(nums)):
            for j in range(len(nums) - 1):
                if i == j:
                    continue
                if abs(i - j) <= indexDiff and abs(nums[i] - nums[j]) <= valueDiff:
                    return True
        return False


solution = Solution()