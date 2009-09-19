using System;
using System.IO;
using Core.Model;
using Machine.Specifications;

namespace Core.Specs.Model.Categories
{
    [Subject(typeof(CategoryLoader), "Loading category data")]
    public class when_loading_a_category_data_file_that_exists
    {
        static CategoryLoader _categoryLoader;
        static Category _category;
        static string _filePath;

        Establish context = () =>
        {
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "model/testdata/ValidCategoryData.xml");
            _categoryLoader = new CategoryLoader();
        };

        Because of = () =>
        {
            _category = _categoryLoader.Load(_filePath);
        };

        It should_load_the_correct_category_file = () =>
        {
            _category.Name.ShouldEqual("ORM");
            _category.Url.ShouldEqual("orm");
            _category.Description.ShouldEqual("Object-relational mapping (ORM, O/RM, and O/R mapping)....");
            _category.Image.ShouldEqual("images/orm-image.jpg");
        };
    }
}