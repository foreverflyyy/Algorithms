from typing import List


class Solution:
    field_with_arr = None
    numbers = {"1": 1, "2": 2, "3": 3, "4": 4, "5": 5, "6": 6, "7": 7, "8": 8, "9": 9}
    def check_matrix_on_unique(self, board, row_index, column_index) -> bool:
        need_matrix = []
        row_index = row_index - row_index % 3
        column_index = column_index - column_index % 3
        for i in range(row_index, row_index + 3):
            sub_matrix = board[i][column_index: column_index + 3]
            need_matrix.append(sub_matrix)

        nums = {}
        for i in range(len(need_matrix)):
            for j in range(len(need_matrix[0])):
                value = nums.get(need_matrix[i][j])
                if value != "." and value in nums:
                    return False
                else:
                    nums.setdefault(value, 1)
        return True
    def check_on_unique(self, field, index, is_row):
        nums = {}
        for i in range(len(field)):
            if is_row:
                value = nums.get(field[index][i])
            else:
                value = nums.get(field[i][index])

            if value != "." and value in nums:
                return False
            nums.setdefault(value, 1)
        return True
    def fill_row_nums(self, board, index_row):
        nums = self.numbers.copy()
        indexes_for_fill = []
        for i in range(len(board[0])):
            value = board[index_row][i]
            if value == '.':
                indexes_for_fill.append(i)
            else:
                nums.pop(value)
        nums = list(nums.keys())

        while len(indexes_for_fill) != 0:
            column_index_for_fill = indexes_for_fill.pop()

            index_free_num = -1
            correct_column = False
            correct_matrix = False
            while not correct_column or not correct_matrix or index_free_num == len(nums) - 1:
                index_free_num += 1
                free_num = nums[index_free_num]
                board[index_row][column_index_for_fill] = free_num

                correct_column = self.check_on_unique(board, column_index_for_fill, False)
                correct_matrix = self.check_matrix_on_unique(board, index_row, column_index_for_fill)

            if not correct_column or not correct_matrix:
                raise Exception(f"Не смог найти нужное значение в {index_row},{column_index_for_fill}")

            nums.pop(index_free_num)
    def fill_cells_arr(self, board):
        self.field_with_arr = []
        for i in range(len(board)):
            for j in range(len(board[0])):
                value = board[i][j]
                if value == '.':
                    self.field_with_arr[i][j] = []
                else:
                    self.field_with_arr[i][j] = value
    def unfinishedSolveSudoku(self, board: List[List[str]], index_row=0) -> None:
        if self.field_with_arr is None:
            self.fill_cells_arr(board)

        self.fill_row_nums(board, index_row)
        if index_row + 1 == len(board):
            return
        self.solveSudoku(board, index_row + 1)

    def sudoku_solver(selfself, board):
        def check(row, col, num):
            for i in range(9):
                if board[row][i] == num:
                    return False
                if board[i][col] == num:
                    return False
                if board[(3 * (row // 3) + i // 3)][(3 * (col // 3) + i % 3)] == num:
                    return False
            return True

        def solve(row, col):
            if row == 9:
                return True
            if col == 9:
                return solve(row + 1, 0)

            if board[row][col] == '.':
                for i in range(1, 10):
                    if check(row, col, str(i)):
                        board[row][col] = str(i)
                        if solve(row, col + 1):
                            return True

                        board[row][col] = '.'
                return False
            return solve(row, col + 1)

        solve(0, 0)


solution = Solution()
need_list = [["5","3",".",".","7",".",".",".","."],["6",".",".","1","9","5",".",".","."],[".","9","8",".",".",".",".","6","."],["8",".",".",".","6",".",".",".","3"],["4",".",".","8",".","3",".",".","1"],["7",".",".",".","2",".",".",".","6"],[".","6",".",".",".",".","2","8","."],[".",".",".","4","1","9",".",".","5"],[".",".",".",".","8",".",".","7","9"]]
solution.sudoku_solver(need_list)
print(need_list)
