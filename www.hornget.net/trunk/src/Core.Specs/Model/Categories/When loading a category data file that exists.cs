using System;
using System.IO;
using Core.Model;
using Machine.Specifications;

namespace Core.Specs.Model.Categories
{
    [Subject(typeof(CategoryLoader), "Loading category data")]
    public class When_loading_a_category_data_file_that_exists
    {
        static CategoryLoader _categoryLoader;
        static Category _category;
        static string _dataPath;

        Establish context = () =>
        {
            _dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "model/categories/testdata/");
            _categoryLoader = new CategoryLoader(_dataPath);
        };

        Because of = () =>
        {
            _category = _categoryLoader.Load("ValidCategoryData.xml");
        };

        It should_look_in_the_correct_location_for_the_category_data_files = () => _categoryLoader.DataDirectory.ShouldEqual(_dataPath);

        It should_load_the_correct_package_file = () =>
        {
            _category.Name.ShouldEqual("ORM");
            _category.Url.ShouldEqual("orm");
            _category.Description.ShouldEqual("Object-relational mapping (ORM, O/RM, and O/R mapping)....");
            _category.Image.ShouldEqual("images/orm-image.jpg");
        };
    }
}