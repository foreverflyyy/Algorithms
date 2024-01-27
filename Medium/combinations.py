from typing import List


class Solution:
    def combine(self, n: int, k: int) -> List[List[int]]:
        combs = []

        def solve_combine(comb, start, n, k):
            if k == 0:
                combs.append(comb.copy())
                return

            for i in range(start, n + 1):
                comb.append(i)
                solve_combine(comb, i + 1, n, k - 1)
                comb.pop()

        solve_combine([], 1, n, k)
        return combs


solution = Solution()
print(solution.combine(4, 2))
