using CloudSpritzers1.src.view.faq;
using Microsoft.UI.Xaml;

namespace CloudSpritzers1.src.view.faq;

[TestClass]
public class BooleanToVisibilityConverterTests
{
    private BooleanToVisibilityConverter _converter;

    [TestInitialize]
    public void Setup()
    {
        _converter = new BooleanToVisibilityConverter();
    }

    [TestMethod]
    public void ConvertTrueToVisible()
    {
        var result = _converter.Convert(true, typeof(Visibility), null, null);
        Assert.AreEqual(Visibility.Visible, result);
    }

    [TestMethod]
    public void ConvertFalseToCollapsed()
    {
        var result = _converter.Convert(false, typeof(Visibility), null, null);
        Assert.AreEqual(Visibility.Collapsed, result);
    }

    [TestMethod]
    public void ConvertBackVisibleToTrue()
    {
        var result = _converter.ConvertBack(Visibility.Visible, typeof(bool), null, null);
        Assert.AreEqual(true, result);
    }

    [TestMethod]
    public void ConvertBackCollapsedToFalse()
    {
        var result = _converter.ConvertBack(Visibility.Collapsed, typeof(bool), null, null);
        Assert.AreEqual(false, result);
    }
}
