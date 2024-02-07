import sys
from typing import List, Optional


class ListNode:
        def __init__(self, val=0, next=None):
            self.val = val
            self.next = next


class Solution:
    def selection_sort(self, nums: List[int]) -> None:
        for i in range(len(nums)):
            min_index = i
            for j in range(i + 1, len(nums)):
                if nums[min_index] > nums[j]:
                    min_index = j
            nums[min_index], nums[i] = nums[i], nums[min_index]

    def insertionSortList(self, head):
        extra_head = ListNode(0)
        extra_head.next = head

        while head and head.next:
            if head.val > head.next.val:
                node_to_insert = head.next
                node_to_insert_pre = extra_head
                while node_to_insert_pre.next.val < node_to_insert.val:
                    node_to_insert_pre = node_to_insert_pre.next

                head.next = node_to_insert.next
                node_to_insert.next = node_to_insert_pre.next
                node_to_insert_pre.next = node_to_insert
            else:
                head = head.next

        return extra_head.next

    def findKthLargest(self, nums: List[int], k: int) -> int:
        def check_to_heap(heap_size, index):
            left, right = index * 2 + 1, index * 2 + 2
            largest = index
            if left < heap_size and nums[largest] < nums[left]:
                largest = left
            if right < heap_size and nums[largest] < nums[right]:
                largest = right
            if largest != index:
                nums[largest], nums[index] = nums[index], nums[largest]
                check_to_heap(heap_size, largest)

        for i in range(len(nums) // 2 - 1, -1, -1):
            check_to_heap(len(nums), i)

        count = 0
        for i in range(len(nums) - 1, -1, -1):
            count += 1
            nums[0], nums[i] = nums[i], nums[0]
            if count == k:
                return nums[i]
            check_to_heap(i, 0)
        # return nums[0]

    def counting_sort(self, nums: List[int]) -> None:
        min_num = min(nums)
        K = max(nums) - min_num
        arr = [0] * (K + 1)

        for num in nums:
            arr[num - min_num] += 1

        starting_index = 0
        for i, count in enumerate(arr):
            arr[i] = starting_index
            starting_index += count

        sorted_arr = [0] * len(nums)
        for num in nums:
            sorted_arr[arr[num - min_num]] = num
            arr[num - min_num] += 1

        for i in range(len(nums)):
            nums[i] = sorted_arr[i]

    def minimumAbsDifference(self, arr: List[int]) -> List[List[int]]:
        min_num = min(arr)
        K = max(arr) - min_num
        indexes_arr = [0] * (K + 1)

        for num in arr:
            indexes_arr[num - min_num] += 1

        starting_index = 0
        for i, count in enumerate(indexes_arr):
            indexes_arr[i] = starting_index
            starting_index += count

        sorted_arr = [0] * len(arr)
        for num in arr:
            sorted_arr[indexes_arr[num - min_num]] = num
            indexes_arr[num - min_num] += 1

        result = {}
        old_num = None
        min_difference = sys.maxsize
        for num in sorted_arr:
            if old_num is not None:
                different = num - old_num
                if min_difference > different:
                    min_difference = different
                if result.get(different):
                    result[different].append([old_num, num])
                else:
                    result.setdefault(different, [[old_num, num]])
            old_num = num

        return result[min_difference]

    def smallestTrimmedNumbers_my(self, nums: List[str], queries: List[List[int]]) -> List[int]:
        def counting_sort(lst, place_val, k=10):
            counts = [0] * k
            for elem in lst:
                digit = (int(elem) // place_val) % 10
                counts[digit] += 1

            starting_index = 0
            for i, count in enumerate(counts):
                counts[i] = starting_index
                starting_index += count

            sorted_lst = [0] * len(lst)
            for elem in lst:
                digit = (int(elem) // place_val) % 10
                sorted_lst[counts[digit]] = int(elem)
                counts[digit] += 1
            for i in range(len(lst)):
                lst[i] = sorted_lst[i]

        data = {}
        default_nums = nums.copy()
        min_value = sys.maxsize
        for num in nums:
            need_num = int(num)
            if need_num < min_value:
                min_value = need_num

        max_value = -sys.maxsize
        for num in nums:
            need_num = int(num)
            if need_num > max_value:
                max_value = need_num

        place_val = 1
        digit = 1
        while place_val <= max_value:
            counting_sort(nums, place_val)
            data.setdefault(digit, nums.copy())
            place_val *= 10
            digit += 1

        nums = default_nums.copy()
        result = []
        for [k, trim] in queries:
            need_value = data.get(trim)[(k - 1)]
            for i in range(len(nums)):
                if int(nums[i]) == need_value:
                    result.append(i)
                    break
        return result

    def smallestTrimmedNumbers(self, nums: List[str], queries: List[List[int]]) -> List[int]:
        ans, trimmed = [], {}
        for k, trim in queries:
            trimmed.setdefault(trim, sorted([(num[-trim:], i) for i, num in enumerate(nums)]))
            ans.append(trimmed[trim][k - 1][1])
        return ans

    def maximumGap(self, nums: List[int]) -> int:
        if len(nums) < 2:
            return 0

        def counting_sort(place_val, k=10):
            counts = [0] * k
            for elem in nums:
                digit = (elem // place_val) % 10
                counts[digit] += 1

            starting_index = 0
            for i, count in enumerate(counts):
                counts[i] = starting_index
                starting_index += count

            sorted_lst = [0] * len(nums)
            for elem in nums:
                digit = (int(elem) // place_val) % 10
                sorted_lst[counts[digit]] = int(elem)
                counts[digit] += 1
            for i in range(len(nums)):
                nums[i] = sorted_lst[i]

        place_val = 1
        max_value = max(nums)
        while place_val <= max_value:
            counting_sort(place_val)
            place_val *= 10

        max_difference = -sys.maxsize
        old_num = None
        for num in nums:
            if old_num is not None:
                different = num - old_num
                if max_difference < different:
                    max_difference = different
            old_num = num

        return max_difference

    def topKFrequent(self, nums: List[int], k: int) -> List[int]:
        data = {}
        for num in nums:
            if data.get(num):
                data[num] += 1
            else:
                data.setdefault(num, 1)
        data = list({k: v for k, v in sorted(data.items(), key=lambda item: item[1], reverse=True)}.keys())
        return data[:k]


solution = Solution()
print(solution.topKFrequent([1,1,1,2,2,3], 2))
print(solution.topKFrequent([1], 1))
