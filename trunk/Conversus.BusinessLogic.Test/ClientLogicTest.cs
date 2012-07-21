using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Conversus.Core.DomainModel;
using Conversus.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Conversus.BusinessLogic.Impl;

namespace Conversus.BusinessLogic.Test
{
    /// <summary>
    /// Сводное описание для UnitTest1
    /// </summary>
    [TestClass]
    public class ClientLogicTest
    {
        private const int pin = 12345;

        /// <summary>
        ///Получает или устанавливает контекст теста, в котором предоставляются
        ///сведения о текущем тестовом запуске и обеспечивается его функциональность.
        ///</summary>
        public TestContext TestContext { get; set; }

        #region Дополнительные атрибуты тестирования
        //
        // При написании тестов можно использовать следующие дополнительные атрибуты:
        //
        // ClassInitialize используется для выполнения кода до запуска первого теста в классе
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            //StorageLogicInitializer.Initialize();
            BusinessLogicInitializer.Initialize();
        }
        //
        // ClassCleanup используется для выполнения кода после завершения работы всех тестов в классе
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // TestInitialize используется для выполнения кода перед запуском каждого теста 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // TestCleanup используется для выполнения кода после завершения каждого теста
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        /// <summary>
        ///Тест для Конструктор ClientLogic
        ///</summary>
        [TestMethod()]
        public void ClientLogicConstructorTest()
        {
            IClientLogic target = BusinessLogicFactory.Instance.Get<IClientLogic>();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///Тест для ChangeStatus
        ///</summary>
        [TestMethod()]
        public void ChangeStatusTest()
        {
            IClientLogic target = BusinessLogicFactory.Instance.Get<IClientLogic>();
            Guid clientId = new Guid(); // TODO: инициализация подходящего значения
            ClientStatus status = new ClientStatus(); // TODO: инициализация подходящего значения
            target.ChangeStatus(clientId, status);
        }

        /// <summary>
        ///Тест для CreateForCommon
        ///</summary>
        [TestMethod()]
        public void CreateForCommonTest()
        {
            IClientLogic target = BusinessLogicFactory.Instance.Get<IClientLogic>();
            string name = "Clent Name";
            QueueType queueType = new QueueType(); // TODO: инициализация подходящего значения
            IClient actual = target.CreateForCommon(name, queueType);
            //Assert.AreEqual(queueType, actual.GetQueue().Type);
            Assert.AreEqual(name, actual.Name);
        }

    
        /// <summary>
        ///Тест для GetClients
        ///</summary>
        [TestMethod()]
        public void GetClientsTest()
        {
            IClientLogic target = BusinessLogicFactory.Instance.Get<IClientLogic>();
            QueueType queueType = new QueueType();
            IClient cl1 = target.CreateForCommon("1", queueType);
            IClient cl2 = target.CreateForCommon("2", queueType);
            IClient cl3 = target.CreateForCommon("3", queueType);
            ICollection<IClient> actual = target.GetClients(queueType);
            Assert.IsNotNull(actual);
            Assert.AreNotEqual(actual.Count, 0);
            //Assert.AreEqual(3, actual.Count);
        }
    }
}
