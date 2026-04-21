using Microsoft.VisualStudio.TestTools.UnitTesting;
using CloudSpritzers1.src.viewModel.general;
using System.ComponentModel;

namespace CloudSpritzers1Tests.src.viewModel
{
    [TestClass]
    public class YouSureViewModelTests
    {
        private YouSureViewModel _viewModel;

        [TestInitialize]
        public void Setup()
        {
            _viewModel = new YouSureViewModel();
        }

        [TestMethod]
        public void Constructor_SetsDefaultValuesCorrectly()
        {
           
            Assert.AreEqual("Confirm Action", _viewModel.Title);
            Assert.AreEqual("Are you sure you want to proceed?", _viewModel.Message);
            Assert.AreEqual("Yes", _viewModel.ConfirmButtonText);
            Assert.AreEqual("Cancel", _viewModel.CancelButtonText);
        }


    }
}