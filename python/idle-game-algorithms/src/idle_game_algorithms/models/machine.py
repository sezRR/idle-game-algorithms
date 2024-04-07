from dataclasses import dataclass
from typing import Dict, List

from src.idle_game_algorithms.generator_initial_cost_initial_revenue_calculator import calculate_initial_costs, \
    calculate_initial_times, calculate_initial_revenues
from src.idle_game_algorithms.generators_level_costs_and_revenues_calculator import calculate_production_revenues, \
    calculate_next_level_costs
from src.idle_game_algorithms.models.machineLevel import MachineLevel


@dataclass
class Machine:
    buy_price: int
    levels: Dict[int, MachineLevel]


def create_machine(max_levels: int) -> List[Machine]:
    times = calculate_initial_times(max_levels)
    initial_cost = calculate_initial_costs(1, True)[0]
    revenues = calculate_production_revenues(max_levels, 1.67, calculate_initial_revenues(1, True)[0])
    level_costs = calculate_next_level_costs(max_levels, initial_cost, 1.07)

    machines = []
    levels = {i + 1: MachineLevel(level_costs[i], revenues[i], times[i]) for i in range(max_levels)}

    machines.append(Machine(initial_cost, levels))

    return machines


print(create_machine(100))
