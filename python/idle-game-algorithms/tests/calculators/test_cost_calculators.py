import unittest
from unittest.mock import patch

from src.idle_game_algorithms.calculators.cost_calculators import CostCalculators


class TestCostCalculators(unittest.TestCase):

    def setUp(self):
        self.calculator = CostCalculators(default_initial_cost=4, initial_cost_multiplier=45)

    def test_default_initial_cost(self):
        self.assertEqual(self.calculator.default_initial_cost, 4)

    def test_initial_cost_multiplier(self):
        self.assertEqual(self.calculator.initial_cost_multiplier, 45)

    def test_set_default_initial_cost(self):
        with self.assertRaises(ValueError):
            self.calculator.default_initial_cost = -50

    def test_set_initial_cost_multiplier(self):
        with self.assertRaises(ValueError):
            self.calculator.initial_cost_multiplier = 0

    def test_calculate_next_level_cost(self):
        self.assertEqual(self.calculator.calculate_next_level_cost(10, 2, 3), 80)

    def test_calculate_next_level_costs(self):
        expected_output = [20, 40, 80]
        self.assertEqual(self.calculator.calculate_next_level_costs(3, 10, 2), expected_output)

    @patch('src.idle_game_algorithms.helpers.number_helper.NumberHelper.format_large_number_custom')
    def test_calculate_initial_costs(self, mock_format_large_number_custom):
        mock_format_large_number_custom.return_value = "100"
        expected_output = ['100', '100', '100']
        self.assertEqual(self.calculator.calculate_initial_costs(3), expected_output)

    def test_upgrade_cost_per_machine(self):
        self.assertEqual(self.calculator.upgrade_cost_per_machine(machine_id=1, growth_rate=1.07, level=30), 30)

    def test_upgrade_costs_per_machine(self):
        expected_output = [8, 16, 32]
        self.assertEqual(
            self.calculator.upgrade_costs_per_machine(machine_id=1, growth_rate=2, start_level=1, end_level=3),
            expected_output)

    def test_calculate_initial_cost_by_level(self):
        self.assertEqual(
            self.calculator.calculate_initial_cost_by_level(2),
            180
        )

if __name__ == '__main__':
    unittest.main()
