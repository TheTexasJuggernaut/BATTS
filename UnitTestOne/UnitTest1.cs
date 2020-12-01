using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BATTS.ViewModels;
using BATTS.Views;

namespace UnitTestOne
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async System.Threading.Tasks.Task TestLoginCheckAsyncFalse()
        {
       
            //Arrange  
            LoginViewModel m1 = new LoginViewModel();
            bool expectedResult = false;


            //Act  
           bool actualResult = await m1.LoginCheckAsync("shawn", "1234");


            //Assert  
            Assert.AreEqual(expectedResult, actualResult);

        }

        [TestMethod]
        public async System.Threading.Tasks.Task TestLoginCheckAsyncTrue()
        {

            //Arrange  
            LoginViewModel m1 = new LoginViewModel();
            bool expectedResult = true;


            //Act  
            bool actualResult = await m1.LoginCheckAsync("dat@gmail.com", "1234");


            //Assert  
            Assert.AreEqual(expectedResult, actualResult);

        }

       


    }
}
