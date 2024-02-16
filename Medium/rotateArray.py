from typing import List


class Solution:
    def rotate(self, nums: List[int], k: int) -> None:
        values = {}
        length = len(nums)
        for i in range(length):
            current_value = nums[i]
            if i in values:
                nums[i] = values[i]
            need_index = (i + k) % length
            if need_index < i:
                nums[need_index] = current_value
            else:
                values[need_index] = current_value

    def searchInsert(self, nums: List[int], target: int) -> int:
        if len(nums) == 0:
            nums.append(target)
            return 0

        left, right = 0, len(nums) - 1
        mid = None
        while left <= right:
            mid = (right + left) // 2
            if nums[mid] < target:
                left = mid + 1
            elif nums[mid] > target:
                right = mid - 1
            else:
                return mid

        if nums[mid] < target:
            nums.insert(mid + 1, target)
            return mid + 1

        nums.insert(mid, target)
        return mid


solution = Solution()
lst1 = [1,3,5,6]
lst2 = [1,3,5,6]
print(solution.searchInsert(lst1, 0))
# print(solution.searchInsert(lst2, 2))
# print(solution.searchInsert(lst2, 7))
print(ord("c"))
print("d" > "c")
# print(lst2)
