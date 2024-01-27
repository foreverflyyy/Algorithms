from typing import List


class Solution:
    def generateParenthesis(self, n):
        result = []

        def generate(s, _open, close):
            if _open == n and close == n:
                result.append(s)
                return
            if _open < n:
                generate(s + "(", _open + 1, close)
            if close < _open:
                generate(s + ")", _open, close + 1)

        generate("", 0, 0)
        return result


solution = Solution()
print(solution.generateParenthesis(3))
