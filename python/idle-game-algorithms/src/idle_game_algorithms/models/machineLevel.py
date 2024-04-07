from dataclasses import dataclass


@dataclass
class MachineLevel:
    upgrade_cost: int
    revenue: int
    time: int
