using App;

namespace Tests
{
    // структура модульних тестів відповідає структурі самого проєкту
    // кожному модулю (класу) програми є модуль (клас) тестів ...
    [TestClass]
    public class StringHelperTest
    {
        // ... кожному методу - тестовий метод (або декілька методів)
        [TestMethod]
        public void EllipsisTest()
        {
            StringHelper stringHelper = new();
            Assert.IsNotNull(stringHelper, "New StringHelper should be Non-Null");
            Assert.AreEqual(                          // Одне з найпоширеніших тверджень -
                "Hello...",                           // твердження рівності. Спочатку -
                stringHelper                          // очікуване значення, потім - 
                    .Ellipsis("Hello, World!", 5),    // фактичне.
                "'Hello, World!' -> 'Hello...'"       // Опціонально - повідомлення при
            );                                        // провалі тесту
            // Тести слід робити таким чином, щоб вони гарантовано
            // показали проблему, якщо вона є. Наявність малої кількості
            // константних тверджень - поганий підхід.
            // наприклад, при одному тесті, return "Hello..." буде проходити тест
            Assert.AreEqual(
                "Тести слід роби...",
                stringHelper
                    .Ellipsis("Тести слід робити таким чином", 15) );
            // return max==5 ? "Hello..." : "Тести слід роби..."

        }

        [TestMethod]
        public void SpacefyTest()
        {
            StringHelper stringHelper = new();
            Assert.IsNotNull(stringHelper, "New StringHelper should be Non-Null");
            // 1. Тест складається до дого, як створюється метод
            Assert.AreEqual("1 000", stringHelper.Spacefy(1000));
            // 3. При невеликій кількості тестових виразів тіло самого
            // методу може бути неалгоритмічним (див. п. 2.2 у проєкті)
            // - розширюємо базу тестів
            Dictionary<double, String> testCases = new()
            {
                { 2, "2" },
                { 20, "20" },
                { 200, "200" },
                { 2000, "2 000" },
                { 750387326, "750 387 326" },
                { 3750387326, "3 750 387 326" },  // це змусить розширювати тип <int> (до uint)
                { -3750387326, "-3 750 387 326" },  // це змусить розширювати до long
                { 1000.1, "1 000.1" },  // 5. це змусить розширювати до double
                { 1000.123, "1 000.123" },
                { 1000.1234, "1 000.123 4" },
                { 1000.12345, "1 000.123 45" },
                { 1000.123456, "1 000.123 456" },
                { 0.1, "0.1" },  
                { 0.01, "0.01" },
                { 0.001, "0.001" },
                { 0.0001, "0.000 1" },
                { 0.00001, "0.000 01" },
                { 0, "0" },
                { 1.0, "1" },
            };
            foreach( var testCase in testCases )
            {
                Assert.AreEqual(
                    testCase.Value,
                    stringHelper.Spacefy(testCase.Key),
                    $"Spacefy({testCase.Key})"
                );
            }
        }


        [TestMethod]
        public void EllipsisErrorsTest()
        {
            StringHelper stringHelper = new();
            Assert.IsNotNull(stringHelper, "New StringHelper should be Non-Null");
            
            var ex = Assert.ThrowsException<ArgumentNullException>(
                () => stringHelper.Ellipsis(null!),
                "Ellipsis(null!) --> Exception"
            );
            // вимагаємо, щоб повідомлення винятку містило назву параметра "input"
            Assert.IsTrue(
                ex.Message.Contains("input"),
                $"ArgumentNullException must contain 'input' msg='{ex.Message}' " 
            );
            // за відсутності Assert.NotThrows безпечний код можна
            // не оточувати Assert, поява виключення провалить тест
            stringHelper.Ellipsis("123");
            // або прямо використати try-catch
            try
            {
                stringHelper.Ellipsis("123", 20);
            }
            catch
            {
                Assert.Fail("Ellipsis('123', 20) must NOT throw exception ");
            }
            // Сумнівний випадок - String.Empty
            Assert.IsNotNull(
                stringHelper.Ellipsis(String.Empty)
            );  // ще один варіант для Assert.NotThrows
        }

    }
}
/* Д.З. UrlCombine
 * - реалізувати перевірку аргументів на null
 *  = якщо перший null - виняток у будь-якому випадку
 *  = якщо другий null, то це припустимо при ненульовому першому
 * - реалізувати перевірку на String.Empty 
 *  = якщо обидва порожні - повернути String.Empty
 *  = якщо перший не порожній, другий порожній - нормально
 *  = перший порожній, другий ні - виняток
 * * узагальнити метод UrlCombine на випадок довільної кількості
 *    складових частин (аргументів). Забезпечити перевірку за правилами, що 
 *    порожні (у т.ч. null) елементи допускаються тільки якщо ніде 
 *    за ними немає непорожніх, а також всі разом не можуть 
 *    порожніми
 */
