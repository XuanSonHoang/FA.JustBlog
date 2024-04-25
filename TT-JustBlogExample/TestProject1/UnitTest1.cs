using FA.JustBlog.Core.Context;
using FA.JustBlog.Core.IRepositories;

namespace TestProject1
{
    [TestFixture]
    public class Tests
    {
        private ICategoryRepository _categoryRepository;
        public Tests(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void GetAllCategory()
        {
            var list = _categoryRepository.GetAllCategories();

            Assert.IsNull(list);
        }

    }
}