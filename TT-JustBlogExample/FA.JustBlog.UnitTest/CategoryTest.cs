using FA.JustBlog.Core.IRepositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using FA.JustBlog.Core.Models;

namespace FA.JustBlog.UnitTest
{
    /// <summary>
    /// Summary description for CategoryTest
    /// </summary>
    [TestClass]
    public class CategoryTest
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryTest(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void GetAllCategories()
        {
            var categories = _categoryRepository.GetAllCategories();
            Assert.AreEqual(3, categories.Count);
        }
        [TestMethod]
        public void AddCategory()
        {
            var category = new Category
            {
                Id = 4,
                Name = "Test Category",
                UrlSlug = "test-category",
                Description = "This is a test category"
            };
            _categoryRepository.AddCategory(category);
            var categories = _categoryRepository.GetAllCategories();
            Assert.AreEqual(4, categories.Count);
        }

        [TestMethod]
        public void UpdateCategory()
        {
            var category = _categoryRepository.Find(4);
            category.Name = "Test Category Updated";
            _categoryRepository.UpdateCategory(category);
            var updatedCategory = _categoryRepository.Find(4);
            Assert.AreEqual("Test Category Updated", updatedCategory.Name);
        }

        [TestMethod]
        public void DeleteCategory()
        {
            var category = _categoryRepository.Find(4);
            _categoryRepository.DeleteCategory(category);
            var categories = _categoryRepository.GetAllCategories();
            Assert.AreEqual(3, categories.Count);
        }
        //code to check abnormal case like: invalid required field, datetime, any special character
        [TestMethod]
        public void AddCategoryWithInvalidData()
        {
            var category = new Category
            {
                Id = 5,
                Name = "Test Category",
                UrlSlug = "test-category",
                Description = "This is a test category"
            };
            _categoryRepository.AddCategory(category);
            var categories = _categoryRepository.GetAllCategories();
            Assert.AreEqual(4, categories.Count);
        }
    }
}
