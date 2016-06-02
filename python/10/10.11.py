import unittest


class TestFixture(unittest.TestCase):

    def testEqual(self):
        self.assertEqual(1, 1)

    def testRaise(self):
        with self.assertRaises(ZeroDivisionError):
            0 / 0

unittest.main()
