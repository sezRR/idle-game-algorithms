from matplotlib import pyplot as plt

NUMBER_OF_PRODUCTIONS = 12


def format_large_number_custom(number, no_letter=False):
    prefixes = ['', 'K', 'M', 'B', 'T', 'aa', 'ab', 'ac', 'ad', 'ae', 'af', 'ag', 'ah', 'ai', 'aj', 'ak', 'al', 'am',
                'an', 'ao', 'ap', 'aq', 'ar', 'as', 'at', 'au', 'av', 'aw', 'ax', 'ay', 'az', 'ba', 'bb', 'bc', 'bd',
                'be', 'bf', 'bg', 'bh', 'bi', 'bj', 'bk', 'bl', 'bm', 'bn', 'bo', 'bp', 'bq', 'br', 'bs', 'bt', 'bu',
                'bv', 'bw', 'bx', 'by', 'bz', 'ca', 'cb', 'cc', 'cd', 'ce', 'cf', 'cg', 'ch', 'ci', 'cj', 'ck', 'cl',
                'cm', 'cn', 'co', 'cp', 'cq', 'cr', 'cs', 'ct']
    suffix_index = 0

    while number >= 1000 and suffix_index < len(prefixes) - 1 and not no_letter:
        suffix_index += 1
        number /= 1000.0

    if no_letter:
        return float(rounder(number))

    return f"{rounder(number)} {prefixes[suffix_index].upper()}"


def rounder(number, ndigits=2):
    return f"{round(number, ndigits):.{ndigits}f}"


# INITIAL COST CALCULATOR
initial_cost_multiplier = 24
default_initial_cost = 5


def calculate_initial_cost(previous_initial_cost=0):
    return default_initial_cost if previous_initial_cost == 0 else previous_initial_cost * initial_cost_multiplier


def calculate_initial_costs(number_of_productions, no_letter=False):
    data = []
    previous_initial_cost = 0
    for _ in range(number_of_productions):
        previous_initial_cost = calculate_initial_cost(previous_initial_cost)
        custom = format_large_number_custom(previous_initial_cost, no_letter)
        print(custom)

        data.append(custom)

    return data


print("CALCULATION OF INITIAL COSTS")
calculate_initial_costs(NUMBER_OF_PRODUCTIONS)
print()

# Initial Time Calculator
initial_time_multiplier = 1.75
default_initial_time = 5
maximum_initial_time = 100


def calculate_initial_time(previous_initial_time=0):
    if previous_initial_time >= maximum_initial_time:
        return maximum_initial_time

    initial_time = (
        default_initial_time
        if previous_initial_time == 0
        else previous_initial_time * initial_time_multiplier
    )

    if initial_time >= maximum_initial_time:
        return maximum_initial_time

    return initial_time


def calculate_initial_times(number_of_productions):
    data = []
    previous_initial_time = 0
    for _ in range(number_of_productions):
        previous_initial_time = calculate_initial_time(previous_initial_time)
        s = rounder(previous_initial_time, 1)
        print(s)
        data.append(s)

    return data


print("CALCULATION OF INITIAL TIMES")
calculate_initial_times(NUMBER_OF_PRODUCTIONS)
print()

# CALCULATE INITIAL REVENUES
default_initial_revenue = 1
initial_revenue_multiplier = 24


def calculate_initial_revenue(previous_initial_revenue=0):
    return (default_initial_revenue
            if previous_initial_revenue == 0 else previous_initial_revenue * initial_revenue_multiplier)


def calculate_initial_revenues(number_of_productions, no_letter=False):
    previous_initial_revenue = 0
    data = []
    for _ in range(number_of_productions):
        previous_initial_revenue = calculate_initial_revenue(previous_initial_revenue)
        custom = format_large_number_custom(previous_initial_revenue, no_letter)
        print(custom)
        data.append(custom)

    return data


print("CALCULATION OF INITIAL REVENUES")
calculate_initial_revenues(NUMBER_OF_PRODUCTIONS)
print()

# Calculate values for the graph
x_values = list(range(1, NUMBER_OF_PRODUCTIONS + 1))
initial_cost_values = calculate_initial_costs(NUMBER_OF_PRODUCTIONS, True)
# initial_time_values = calculate_initial_times(NUMBER_OF_PRODUCTIONS)
initial_revenue_values = calculate_initial_revenues(NUMBER_OF_PRODUCTIONS, True)

plt.plot(x_values, initial_cost_values, marker='o', label="Initial Cost")
# plt.plot(x_values, initial_time_values, marker='o', label="Initial Time")
plt.plot(x_values, initial_revenue_values, marker='o', label="Initial Revenue")

plt.title("Production Metrics")
plt.xlabel("Number of Productions")
plt.ylabel("Values")
plt.legend()

plt.ticklabel_format(axis='y', style='plain')
plt.gca().get_yaxis().set_major_formatter(plt.FuncFormatter(lambda x, _: format_large_number_custom(x)))

plt.yscale('log')  # Set y-axis to log scale

plt.grid(True)

plt.show()
