from typing import List


class Solution:
    def containsDuplicate(self, nums: List[int]) -> bool:
        data = {}
        for num in nums:
            if num in data:
                return True
            data.setdefault(num, 1)
        return False

    def singleNumber(self, nums: List[int]) -> int:
        data = set()
        for i in range(len(nums)):
            num = nums[i]
            if num in data:
                data.remove(num)
            else:
                data.add(num)
        return data.pop()

    def intersection(self, nums1: List[int], nums2: List[int]) -> List[int]:
        index = 0
        set_1 = set()
        set_2 = set()
        result = set()
        num1 = nums1[0]
        num2 = nums2[0]
        while index < len(nums1) or index < len(nums2):
            if index < len(nums1):
                num1 = nums1[index]
                set_1.add(num1)
            if index < len(nums2):
                num2 = nums2[index]
                set_2.add(num2)
            if num1 in set_2:
                result.add(num1)
            if num2 in set_1:
                result.add(num2)
            index += 1
        return list(result)

    def twoSum(self, nums: List[int], target: int) -> List[int]:
        prev_nums = {}
        for i in range(len(nums)):
            num = nums[i]
            diff = target - num
            if diff in prev_nums:
                return [prev_nums[diff], i]
            prev_nums[num] = i
        return []


solution = Solution()
print(solution.intersection([3, 1, 2], [1]))
print(solution.intersection([1], [1]))
print(solution.intersection([1,2,2,1], [2,2]))
print(solution.intersection([4,9,5], [9,4,9,8,4]))
