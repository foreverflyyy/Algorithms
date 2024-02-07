from typing import List


class Solution:
    def calculate(self, token, num1, num2):
        for num in current_nums:

    def evalRPN(self, tokens: List[str]) -> int:
        res = None
        stack = []
        for token in tokens:
            need_token = int(token)
            if need_token is not None:
                stack.append(need_token)
                continue
            while len(stack) != 0:
                self.calculate(token, )




solution = Solution()
print(solution.evalRPN(["2","1","+","3","*"]))
print(solution.evalRPN(["4","13","5","/","+"]))
print(solution.evalRPN(["10","6","9","3","+","-11","*","/","*","17","+","5","+"]))
