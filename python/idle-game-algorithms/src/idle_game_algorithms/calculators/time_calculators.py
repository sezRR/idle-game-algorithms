from src.idle_game_algorithms.helpers.number_helper import NumberHelper


class TimeCalculators:
    def __init__(self, initial_time_multiplier: float = 1.75, default_initial_time: float = 5,
                 maximum_initial_time: float = 100):
        self.initial_time_multiplier = initial_time_multiplier
        self.default_initial_time = default_initial_time
        self.maximum_initial_time = maximum_initial_time

    def calculate_initial_time(self, previous_initial_time: float = 0):
        if previous_initial_time >= self.maximum_initial_time:
            return self.maximum_initial_time

        initial_time = (
            self.default_initial_time
            if previous_initial_time == 0
            else previous_initial_time * self.initial_time_multiplier
        )

        if initial_time >= self.maximum_initial_time:
            return self.maximum_initial_time

        return initial_time

    def calculate_initial_times(self, number_of_productions):
        data = []
        previous_initial_time = 0
        for _ in range(number_of_productions):
            previous_initial_time = self.calculate_initial_time(previous_initial_time)
            calculated_initial_time = NumberHelper.rounder(previous_initial_time, 1)
            data.append(calculated_initial_time)

        return data
