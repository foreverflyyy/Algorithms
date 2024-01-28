from typing import List


class Solution:
    def largestRectangleArea(self, heights: List[int]):
        h_length = len(heights)
        if not heights or h_length == 0:
            return 0

        less_from_left = [None] * h_length
        less_from_right = [None] * h_length

        less_from_right[h_length - 1] = h_length
        less_from_left[0] = -1

        for i in range(1, h_length):
            p = i - 1
            while p >= 0 and heights[p] >= heights[i]:
                p = less_from_left[p]
            less_from_left[i] = p

        for i in range((h_length - 2), -1, -1):
            p = i + 1
            while p < h_length and heights[p] >= heights[i]:
                p = less_from_right[p]
            less_from_right[i] = p

        max_area = 0
        for i in range(h_length):
            max_area = max(max_area, heights[i] * (less_from_right[i] - less_from_left[i] - 1))
        return max_area


solution = Solution()
# print(solution.largestRectangleArea([2, 4]))
print(solution.largestRectangleArea([2,1,5,6,2,3]))
