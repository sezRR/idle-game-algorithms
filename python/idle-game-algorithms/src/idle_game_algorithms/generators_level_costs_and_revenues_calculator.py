import math

from matplotlib import pyplot as plt

from src.idle_game_algorithms.generator_initial_cost_initial_revenue_calculator import format_large_number_custom


def calculate_next_level_cost(initial_price, growth_rate, owned):
    return int(initial_price * math.pow(growth_rate, owned))


def calculate_next_level_costs(max_level, initial_price, growth_rate):
    return [calculate_next_level_cost(initial_price, growth_rate, level) for level in range(1, max_level + 1)]


def calculate_production_revenue(production_base, owned, multipliers):
    return int((production_base * owned) * multipliers)


def calculate_production_revenues(max_level, growth_rate, multipliers):
    return [calculate_production_revenue(growth_rate, level, multipliers) for level in range(1, max_level + 1)]


MAX_LEVELS = 60
print(calculate_next_level_costs(MAX_LEVELS, 4, 1.07))
print(calculate_production_revenues(MAX_LEVELS, 1.67, 1))


levels = list(range(MAX_LEVELS))
next_level_costs = calculate_next_level_costs(MAX_LEVELS, 4, 1.07)
production_revenues = calculate_production_revenues(MAX_LEVELS, 1.67, 1)

plt.plot(levels, next_level_costs, label="Next Level Costs")
plt.plot(levels, production_revenues, label="Production Revenues")

plt.title("Production Metrics")
plt.xlabel("Levels")
plt.ylabel("Values")
plt.legend()

plt.ticklabel_format(axis='y', style='plain')
plt.gca().get_yaxis().set_major_formatter(plt.FuncFormatter(lambda x, _: format_large_number_custom(x)))

plt.yscale('log')  # Set y-axis to log scale

plt.grid(True)
plt.show()
