import unittest
from unittest.mock import patch

from src.idle_game_algorithms.calculators.revenue_calculators import RevenueCalculators


class TestRevenueCalculators(unittest.TestCase):

    def setUp(self):
        self.calculator = RevenueCalculators(default_initial_revenue=100, initial_revenue_multiplier=1.5)

    def test_default_initial_revenue(self):
        self.assertEqual(self.calculator.default_initial_revenue, 100)

    def test_initial_revenue_multiplier(self):
        self.assertEqual(self.calculator.initial_revenue_multiplier, 1.5)

    def test_set_default_initial_revenue(self):
        with self.assertRaises(ValueError):
            self.calculator.default_initial_revenue = -50

    def test_set_initial_revenue_multiplier(self):
        with self.assertRaises(ValueError):
            self.calculator.initial_revenue_multiplier = 0

    def test_calculate_initial_revenues(self):
        expected_output = ['100.00', '150.00', '225.00']
        self.assertEqual(self.calculator.calculate_initial_revenues(3), expected_output)

    def test_calculate_production_revenue(self):
        self.assertEqual(self.calculator.calculate_production_revenue(10, 5, 2), 257)

    def test_calculate_production_revenues(self):
        expected_output = [5, 7, 8]
        self.assertEqual(self.calculator.calculate_production_revenues(3, 2.8, 1.2), expected_output)

    def test_calculate_growth_rate_by_machine_id(self):
        self.assertEqual(self.calculator.calculate_growth_rate_by_machine_id(5), 7.5)

    def test_production_revenue_per_machine_with_growth_rate(self):
        self.assertEqual(
            self.calculator.production_revenue_per_machine(machine_id=1, growth_rate=2, owned_machine=3,
                                                           multipliers=1.5), 6)

    def test_production_revenue_per_machine_without_growth_rate(self):
        with patch.object(self.calculator, 'calculate_growth_rate_by_machine_id', return_value=7.5):
            self.assertEqual(
                self.calculator.production_revenue_per_machine(machine_id=1, growth_rate=None, owned_machine=3,
                                                               multipliers=1.5), 73)

    def test_production_revenues_per_machine_with_growth_rate(self):
        expected_output = [4, 5, 6]
        self.assertEqual(
            self.calculator.production_revenues_per_machine(machine_id=1, growth_rate=2, multipliers=1.5,
                                                            start_level=1, end_level=3), expected_output)

    def test_production_revenues_per_machine_without_growth_rate(self):
        expected_output = [14, 17, 21]
        self.assertEqual(
            self.calculator.production_revenues_per_machine(machine_id=1, growth_rate=None, multipliers=4,
                                                            start_level=1, end_level=3), expected_output)


if __name__ == '__main__':
    unittest.main()
