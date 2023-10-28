using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;
namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
        //    ProductTest();
        //    //CategoryTest();
        //}

        //private static void CategoryTest()
        //{
        //    CategoryManager categoryManager = new CategoryManager(new EfCategoryDAL());
        //    foreach (var cat in categoryManager.GetAll().Data)
        //    {
        //        Console.WriteLine(cat.CategoryName);
        //    }
        //}

        //private static void ProductTest()
        //{
        //    ProductManager productManager = new ProductManager(new EfProductDAL(),new CategoryManager(new EfCategoryDAL ));

        //    var result = productManager.GetProductDetails();
        //    if(result.Success)
        //    {

        //        foreach (var product in productManager.GetProductDetails().Data)
        //        {
        //            Console.WriteLine(product.ProductName + " / " + product.CategoryName);
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine(result.Message);
        //    }

        }
    }
}