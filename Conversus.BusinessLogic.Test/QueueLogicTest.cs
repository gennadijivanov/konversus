using Conversus.BusinessLogic.Impl;
using Conversus.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Conversus.Core.DomainModel;
using System.Collections.Generic;

namespace Conversus.BusinessLogic.Test
{
    /// <summary>
    ///Это класс теста для QueueLogicTest, в котором должны
    ///находиться все модульные тесты QueueLogicTest
    ///</summary>
    [TestClass]
    public class QueueLogicTest
    {
        /// <summary>
        ///Получает или устанавливает контекст теста, в котором предоставляются
        ///сведения о текущем тестовом запуске и обеспечивается его функциональность.
        ///</summary>
        public TestContext TestContext { get; set; }

        #region Дополнительные атрибуты теста
        // 
        //При написании тестов можно использовать следующие дополнительные атрибуты:
        //
        //ClassInitialize используется для выполнения кода до запуска первого теста в классе
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            BusinessLogicInitializer.Initialize();
            StorageLogicInitializer.Initialize();
        }
        //
        //ClassCleanup используется для выполнения кода после завершения работы всех тестов в классе
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //TestInitialize используется для выполнения кода перед запуском каждого теста
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //TestCleanup используется для выполнения кода после завершения каждого теста
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///Тест для Конструктор QueueLogic
        ///</summary>
        [TestMethod]
        public void QueueLogicConstructorTest()
        {
            // не упал и хорошо
            IQueueLogic target = BusinessLogicFactory.Instance.Get<IQueueLogic>();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///Тест для GetOrCreateQueue
        ///</summary>
        [TestMethod]
        public void GetOrCreateQueueTest()
        {
            IQueueLogic target = BusinessLogicFactory.Instance.Get<IQueueLogic>();
            const QueueType queueType = QueueType.Approvement;
            IQueue actual = target.GetOrCreateQueue(queueType);
            Assert.IsNotNull(actual);
            Assert.AreEqual(queueType, actual.Type);
        }

        /// <summary>
        ///Тест для GetQueues
        ///</summary>
        [TestMethod]
        public void GetQueuesTest()
        {
            IQueueLogic target = BusinessLogicFactory.Instance.Get<IQueueLogic>();
            ICollection<IQueue> actual = target.GetQueues();
            Assert.IsNotNull(actual);
        }
    }
}
