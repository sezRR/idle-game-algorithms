class NumberHelper:
    @classmethod
    def format_large_number_custom(cls, number, no_letter=False):
        prefixes = ['', 'K', 'M', 'B', 'T', 'aa', 'ab', 'ac', 'ad', 'ae', 'af', 'ag', 'ah', 'ai', 'aj', 'ak', 'al',
                    'am', 'an', 'ao', 'ap', 'aq', 'ar', 'as', 'at', 'au', 'av', 'aw', 'ax', 'ay', 'az', 'ba', 'bb',
                    'bc', 'bd', 'be', 'bf', 'bg', 'bh', 'bi', 'bj', 'bk', 'bl', 'bm', 'bn', 'bo', 'bp', 'bq', 'br',
                    'bs', 'bt', 'bu', 'bv', 'bw', 'bx', 'by', 'bz', 'ca', 'cb', 'cc', 'cd', 'ce', 'cf', 'cg', 'ch',
                    'ci', 'cj', 'ck', 'cl', 'cm', 'cn', 'co', 'cp', 'cq', 'cr', 'cs', 'ct']
        suffix_index = 0

        while number >= 1000 and suffix_index < len(prefixes) - 1 and not no_letter:
            suffix_index += 1
            number /= 1000.0

        if no_letter:
            return float(cls.rounder(number))

        return f"{cls.rounder(number)} {prefixes[suffix_index].upper()}".strip()

    @staticmethod
    def rounder(number, ndigits=2):
        return f"{round(number, ndigits):.{ndigits}f}"
