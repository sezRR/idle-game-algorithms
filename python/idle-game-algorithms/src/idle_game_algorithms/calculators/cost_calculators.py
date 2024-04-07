import math

from src.idle_game_algorithms.helpers.number_helper import NumberHelper


class CostCalculators:
    @property
    def default_initial_cost(self) -> float:
        return self._default_initial_cost

    @default_initial_cost.setter
    def default_initial_cost(self, value: float):
        if value < 0:
            raise ValueError("Default initial cost cannot be negative.")
        self._default_initial_cost = value

    @property
    def initial_cost_multiplier(self) -> float:
        return self._initial_cost_multiplier

    @initial_cost_multiplier.setter
    def initial_cost_multiplier(self, value: float):
        if value <= 0:
            raise ValueError("Initial cost multiplier must be positive and non-zero.")
        self._initial_cost_multiplier = value

    def __init__(self, default_initial_cost: float = 6, initial_cost_multiplier: float = 45):
        self.default_initial_cost = default_initial_cost
        self.initial_cost_multiplier = initial_cost_multiplier

    @staticmethod
    def calculate_next_level_cost(initial_price: float, growth_rate: float, owned: int):
        return int(initial_price * math.pow(growth_rate, owned))

    def calculate_next_level_costs(self, max_level: int, initial_price: float, growth_rate: float):
        return [self.calculate_next_level_cost(initial_price, growth_rate, level) for level in range(1, max_level + 1)]

    def calculate_initial_cost(self, previous_initial_cost: float = 0):
        return self.default_initial_cost if previous_initial_cost == 0 else previous_initial_cost * self.initial_cost_multiplier

    def calculate_initial_costs(self, number_of_productions, no_letter=False):
        data = []
        previous_initial_cost = 0
        for _ in range(number_of_productions):
            previous_initial_cost = self.calculate_initial_cost(previous_initial_cost)
            custom = NumberHelper.format_large_number_custom(previous_initial_cost, no_letter)

            data.append(custom)

        return data

    def upgrade_cost_per_machine(self, machine_id: int, growth_rate: float, level: int):
        return self.calculate_next_level_cost(self.calculate_initial_cost_by_level(machine_id),
                                              growth_rate, level)

    def upgrade_costs_per_machine(self, machine_id: int, growth_rate: float, start_level: int, end_level: int):
        return self.calculate_next_level_costs(end_level,
                                               self.calculate_initial_cost_by_level(machine_id),
                                               growth_rate)[start_level - 1:]

    def calculate_initial_cost_by_level(self, level: int) -> float:  # TODO: ADD TEST
        return float(self.default_initial_cost * (self.initial_cost_multiplier ** (level - 1)))
