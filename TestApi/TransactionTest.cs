using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Utilities;
using Utilities.Models;
using WebApiServer.Controllers;

namespace TestApi
{
    [TestClass]
    public class TransactionTest
    {
        const long  userid = 4;
        private  RepositoryContext SeedDb()
        {
            //Create In Memory Database
            var options = new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase(databaseName: "ERNI").Options;
            var dbContext = new RepositoryContext(options);
            dbContext.Database.EnsureCreated();
            return dbContext;
        }

        [TestMethod]
        public void Add_User_On_Success()
        {
            //Arrange
            var dbContext = SeedDb();
            var _controller = new UserController(dbContext);

            //Act
            //Create mocked context by seeding data as per schema
            if (dbContext.users.Count() <= 0)
            {
                //populate 5 username with empty transactions
                for (int i = 1; i <= 5; i++)
                {
                    User user = new User
                    {
                        Id = i,
                        CreateDateTime = DateTime.Now,
                        Username = "mock-username",
                        FirstName = "mock-firstname",
                        LastName = "mock-lastname",
                        Password = "ERNI",
                    };
                    ActionResult<User> result = _controller.Register(user);
                    var response = result.Result as OkObjectResult;
                    Assert.AreEqual(response.Value, "Inserted Successfully");
                }
            }
            //Assert
            Assert.AreEqual(5, dbContext.users.Count());

        }

        [TestMethod]
        public void Check_Balance_On_Success()
        {
            //Arrange
            var dbContext = SeedDb();

            var _controller = new TransactionController(dbContext);

            //Act
            IActionResult result = _controller.GetBalance(userid);
            var response = result as OkObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result is OkObjectResult);
            Assert.AreEqual("Your remaining balance is 0", response.Value);
        }

        [TestMethod]
        public void Deposit_Amount_On_Success()
        {
            //Arrange
            var dbContext = SeedDb();
            var _controller = new TransactionController(dbContext);

            //Act
            IActionResult result = _controller.Deposit(userid,1000);
            var text = result as OkObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result is OkObjectResult);
           Assert.AreEqual("Transaction successfull!\nYour new remaining balance is : 1000", text.Value);

        }

       

        [TestMethod]
        public void Transfer_On_Success()
        {
            //Arrange
            var dbContext = SeedDb();
            var _controller = new TransactionController(dbContext);

            //Act
            IActionResult result = _controller.Transfer(userid,1, 300);
            var text = result as OkObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result is OkObjectResult);
            Assert.AreEqual("Transaction successfull!\nYour new remaining balance is : 700", text.Value);
        }

        [TestMethod]
        public void Withdraw_On_Success()
        {
            //Arrange
            var dbContext = SeedDb();
            var _controller = new TransactionController(dbContext);

            //Act
            IActionResult result = _controller.Withdraw(userid, 100);
            var text = result as OkObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result is OkObjectResult);
            Assert.AreEqual("Transaction successfull!\nYour new remaining balance is : 600", text.Value);
        }
    }
}
