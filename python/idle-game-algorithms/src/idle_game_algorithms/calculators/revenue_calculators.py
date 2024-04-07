import math

from src.idle_game_algorithms.helpers.number_helper import NumberHelper


class RevenueCalculators:
    @property
    def default_initial_revenue(self):
        return self._default_initial_revenue

    @default_initial_revenue.setter
    def default_initial_revenue(self, value):
        if value < 0:
            raise ValueError("Default initial revenue cannot be negative.")
        self._default_initial_revenue = value

    @property
    def initial_revenue_multiplier(self):
        return self._initial_revenue_multiplier

    @initial_revenue_multiplier.setter
    def initial_revenue_multiplier(self, value):
        if value <= 0:
            raise ValueError("Initial revenue multiplier must be positive and non-zero.")
        self._initial_revenue_multiplier = value

    def __init__(self, default_initial_revenue, initial_revenue_multiplier):
        self.default_initial_revenue = default_initial_revenue
        self.initial_revenue_multiplier = initial_revenue_multiplier

    @staticmethod
    def calculate_production_revenue(growth_rate: float, owned: int, multipliers: float):
        return int((growth_rate * owned) * multipliers * math.log(growth_rate, owned + 1) * multipliers)
        # return int((growth_rate * owned) * multipliers) # old calculation

    @classmethod
    def calculate_production_revenues(cls, max_level, growth_rate, multipliers):
        return [cls.calculate_production_revenue(growth_rate, level, multipliers) for level in range(1, max_level + 1)]

    def calculate_initial_revenue(self, previous_initial_revenue: float = 0):
        return (
            self.default_initial_revenue if previous_initial_revenue == 0 else previous_initial_revenue * self.initial_revenue_multiplier)

    def calculate_initial_revenues(self, number_of_productions: int, no_letter: bool = False):
        previous_initial_revenue = 0
        data = []
        for _ in range(number_of_productions):
            previous_initial_revenue = self.calculate_initial_revenue(previous_initial_revenue)
            custom = NumberHelper.format_large_number_custom(previous_initial_revenue, no_letter)
            data.append(custom)

        return data

    def calculate_growth_rate_by_machine_id(self, machine_id: int):
        return self.initial_revenue_multiplier * machine_id

    def production_revenue_per_machine(self, machine_id: int, growth_rate: float, owned_machine: int,
                                       multipliers: float):
        """
        :param machine_id:
        :param growth_rate:
        :param owned_machine: The level of the machine.
        :param multipliers:
        :return:
        """
        if growth_rate:
            return self.calculate_production_revenue(growth_rate, owned_machine, multipliers)
        else:
            return self.calculate_production_revenue(self.calculate_growth_rate_by_machine_id(machine_id),
                                                     owned_machine, multipliers)

    def production_revenues_per_machine(self, machine_id: int, growth_rate: float, multipliers: float, start_level: int,
                                        end_level: int):
        if growth_rate:
            return self.calculate_production_revenues(end_level, growth_rate, multipliers)[start_level - 1:]
        else:
            return self.calculate_production_revenues(end_level, self.calculate_growth_rate_by_machine_id(machine_id),
                                                      multipliers)[start_level - 1:]
