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
            BusinessLogicInitializer.Initialize();
            StorageLogicInitializer.Initialize();
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
            Assert.Inconclusive("Невозможно проверить метод, не возвращающий значение.");
        }

        /// <summary>
        ///Тест для CreateForCommon
        ///</summary>
        [TestMethod()]
        public void CreateForCommonTest()
        {
            IClientLogic target = BusinessLogicFactory.Instance.Get<IClientLogic>();
            string name = string.Empty; // TODO: инициализация подходящего значения
            QueueType queueType = new QueueType(); // TODO: инициализация подходящего значения
            IClient expected = null; // TODO: инициализация подходящего значения
            IClient actual;
            actual = target.CreateForCommon(name, queueType);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Проверьте правильность этого метода теста.");
        }

        /// <summary>
        ///Тест для CreateFromLotus
        ///</summary>
        [TestMethod()]
        public void CreateFromLotusTest()
        {
            IClientLogic target = BusinessLogicFactory.Instance.Get<IClientLogic>();
            const string name = "Vasya";
            IClient actual = target.CreateFromLotus(name, pin);
            Assert.IsNotNull(actual);
            Assert.AreEqual(actual.Name, name);
            Assert.AreEqual(actual.PIN, pin);
        }

        /// <summary>
        ///Тест для GetClientByPin
        ///</summary>
        [TestMethod()]
        public void GetClientByPinTest()
        {
            IClientLogic target = BusinessLogicFactory.Instance.Get<IClientLogic>();
            IClient actual = target.GetClientByPin(pin);
            Assert.IsNotNull(actual);
            Assert.AreEqual(actual.PIN, pin);
        }

        /// <summary>
        ///Тест для GetClients
        ///</summary>
        [TestMethod()]
        public void GetClientsTest()
        {
            IClientLogic target = BusinessLogicFactory.Instance.Get<IClientLogic>();
            QueueType queue = new QueueType(); // TODO: инициализация подходящего значения
            ICollection<IClient> expected = null; // TODO: инициализация подходящего значения
            ICollection<IClient> actual;
            actual = target.GetClients(queue);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Проверьте правильность этого метода теста.");
        }

        /// <summary>
        ///Тест для GetTicket
        ///</summary>
        [TestMethod()]
        public void GetTicketTest()
        {
            IClientLogic target = BusinessLogicFactory.Instance.Get<IClientLogic>();
            Guid clientId = new Guid(); // TODO: инициализация подходящего значения
            string expected = string.Empty; // TODO: инициализация подходящего значения
            string actual;
            actual = target.GetTicket(clientId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Проверьте правильность этого метода теста.");
        }
    }
}
