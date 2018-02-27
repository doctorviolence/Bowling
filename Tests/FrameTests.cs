using BowlingApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
  [TestClass]
  public class FrameTests
  {

    private Frame _frame;
    private Frame _frame2;

    [TestInitialize]
    public void SetUp()
    {
      _frame = new Frame(10, 0);
      _frame2 = new Frame(10, 10, 10);
    }

    [TestMethod]
    public void TestIsStrike()
    {
      bool value = _frame.IsStrike();
      Assert.AreEqual(true, value);
    }

    [TestMethod]
    public void TestIsSpare()
    {
      bool value = _frame.IsSpare();
      Assert.AreEqual(false, value);
    }

    [TestMethod]
    public void TestGetFrameScore()
    {
      int value = _frame.GetFrameScore();
      Assert.AreEqual(10, value);
    }

    [TestMethod]
    public void TestCalculateScore()
    {
      int value = _frame.CalculateScore(true, false, _frame2);
      Assert.AreEqual(40, value);
    }

    [TestMethod]
    public void TestGetTotalScore()
    {
      int value = _frame2.CalculateTotalScore(true, false);
      Assert.AreEqual(30, value);
    }
  }
}